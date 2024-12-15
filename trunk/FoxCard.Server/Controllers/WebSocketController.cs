using System.Collections.Concurrent;
using System.Net.WebSockets;
using FoxCard.Server.Datas;
using FoxCard.Server.Datas.Config.Scripts;
using FoxCard.Server.Services;
using HotFix_UI;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Diagnostics;
using FoxCard.Server.Log;
using UnityEngine;


namespace FoxCard.Server.Controllers;

public class WebSocketController : ControllerBase
{
    private readonly IConnectionMultiplexer _redis;
    private readonly HttpClient _httpClient;
    private readonly IRedisCacheService _redisCache;

    private static readonly MessagePackSerializerOptions options =
        MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block);
    //private readonly MyConfig _myConfig;

    public WebSocketController(IConnectionMultiplexer redis, HttpClient httpClient,
        IRedisCacheService redisCache)
    {
        _redis = redis;
        _httpClient = httpClient;
        _redisCache = redisCache;
    }

    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            Console.WriteLine(
                $"ConnectionId:{HttpContext.Connection.Id} Ip:{HttpContext.Connection.RemoteIpAddress}");
            await Echo(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    //链接 openid
    private static readonly ConcurrentDictionary<WebSocket, string> _connections = new();

    private async Task Echo(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult receiveResult;
        var timeoutCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(3)); // 超时设置为5分钟

        try
        {
            while (!timeoutCancellationTokenSource.Token.IsCancellationRequested)
            {
                receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),
                    timeoutCancellationTokenSource.Token);

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    _connections.TryRemove(webSocket, out _);
                    // 正常关闭
                    await webSocket.CloseAsync(receiveResult.CloseStatus.Value, receiveResult.CloseStatusDescription,
                        CancellationToken.None);
                    return;
                }

                //webSocket.
                var receivedMessage = MessagePackSerializer.Deserialize<MyMessage>(buffer, options);

                // 处理消息并生成回复
                var responseMessage = await ProcessMessage(receivedMessage, webSocket);

                // 使用 MessagePack 序列化回复消息
                var responseBuffer = MessagePackSerializer.Serialize(responseMessage, options);

                // 将回复发送回客户端
                await webSocket.SendAsync(
                    new ArraySegment<byte>(responseBuffer, 0, responseBuffer.Length),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);
            }
        }
        catch (OperationCanceledException)
        {
            // 超时
            Console.WriteLine($"ConnectionId:{HttpContext.Connection.Id} Connection timed out");
            try
            {
                _connections.TryRemove(webSocket, out _);
                await webSocket.CloseAsync(WebSocketCloseStatus.PolicyViolation, "Connection timed out",
                    CancellationToken.None);
            }
            catch
            {
                // Ignore exceptions during close
            }
        }
        catch (Exception ex)
        {
            // 处理其他异常
            Console.WriteLine($"ConnectionId:{HttpContext.Connection.Id} Error: {ex.Message}");
            try
            {
                _connections.TryRemove(webSocket, out _);
                await webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Error occurred",
                    CancellationToken.None);
            }
            catch
            {
                // Ignore exceptions during close
            }
        }
        finally
        {
            timeoutCancellationTokenSource.Dispose();
        }
    }

    private async Task<PlayerResource?> TryGetPlayerResourceAsync(WebSocket webSocket)
    {
        if (!_connections.TryGetValue(webSocket, out var openId))
        {
            //Console.WriteLine($"webSocket:{webSocket.} not found");
            //用户未登记
            //message.ErrorCode = 1001;
        }

        var db = _redis.GetDatabase();
        var redisKey = GetRedisDBStr(1, openId);
        var rv = await db.StringGetAsync(redisKey);
        return JsonConvert.DeserializeObject<PlayerResource>(rv);
    }

    private async Task<MyMessage> ProcessMessage(MyMessage message, WebSocket webSocket)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        string inputContentStr = default;
        string outputContentStr = default;
        if (message.Cmd == CMD.LOGIN)
        {
            //MyConfig.InitConfig();
            //Console.WriteLine($"{MyConfig.Tables?.Tbitem.Get(10000).id}");
            var db = _redis.GetDatabase();
            var playerData = MessagePackSerializer.Deserialize<PlayerData>(message.Content, options);
            inputContentStr = JsonConvert.SerializeObject(playerData);
            //Console.WriteLine($"PlayerData1111:{JsonConvert.SerializeObject(playerData)}");
            Console.WriteLine($"PlayerData:{JsonConvert.SerializeObject(playerData)}");
            //long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            //var playerdata = await db.StringGetAsync(player.Id.ToString());
            var wxCode2Session = await GetSessionJson(playerData.OtherData.Code);
            var openId = wxCode2Session.openid;
            _connections.TryAdd(webSocket, openId);


            var player = await db.StringGetAsync(openId);
            PlayerResource playerRes;
            if (player.IsNullOrEmpty)
            {
                await db.StringSetAsync(openId, JsonConvert.SerializeObject(playerData));
                playerRes = InitPlayerResource();
            }
            else
            {
                var rvRes = await db.StringGetAsync(GetRedisDBStr(1, openId));
                playerRes = JsonConvert.DeserializeObject<PlayerResource>(rvRes);
            }

            if (CanSignOrLoginToday(playerRes.LastLoginTimeStamp, out var date, out var utclong))
            {
                var lastDate = DateTimeOffset.FromUnixTimeMilliseconds(playerRes.LastLoginTimeStamp).DateTime;
                playerRes.LastLoginTimeStamp = utclong;
                playerRes.LoginCount++;
                var timeSpan = date - lastDate;
                playerRes.ContinuousLoginCount = timeSpan.TotalHours < 48 ? playerRes.ContinuousLoginCount + 1 : 0;
                if (player.IsNullOrEmpty)
                {
                    playerRes.ContinuousLoginCount = 1;
                }

                playerRes.GameAchieve?.SetAchievePara(1012);
            }

            await SetPlayerResDB(openId, playerRes);

            playerData.ThirdId = MyEncryptor.Decrypt(openId);
            var temp = playerData.OtherData;
            temp.UnionidId = wxCode2Session.unionid;
            playerData.OtherData = temp;

            message.Content = MessagePackSerializer.Serialize(playerData, options);
            outputContentStr = JsonConvert.SerializeObject(playerData);
        }
        else if (message.Cmd == CMD.QUERYRESOURCE)
        {
            var playerRes = new PlayerResource();
            if (!_connections.TryGetValue(webSocket, out var openId))
            {
                //Console.WriteLine($"webSocket:{webSocket.} not found");
                //用户未登记
                message.ErrorCode = 1001;
            }

            var db = _redis.GetDatabase();
            var rv = await db.StringGetAsync(GetRedisDBStr(1, openId));
            playerRes = JsonConvert.DeserializeObject<PlayerResource>(rv);
            message.Content =
                MessagePackSerializer.Serialize(playerRes, options);

            outputContentStr = rv.ToString();
        }
        else if (message.Cmd == CMD.DAILYSIGN)
        {
            var rewards = new Rewards { };
            if (!_connections.TryGetValue(webSocket, out var openId))
            {
                //Console.WriteLine($"webSocket:{webSocket.} not found");
                //用户未登记
                message.ErrorCode = 1001;
            }

            var db = _redis.GetDatabase();
            var redisKey = GetRedisDBStr(1, openId);
            var rv = await db.StringGetAsync(redisKey);
            var playerRes = JsonConvert.DeserializeObject<PlayerResource>(rv);

            if (CanSignOrLoginToday(playerRes.LastSignTimeStamp, out var date, out var utclong))
            {
                playerRes.LastSignTimeStamp = utclong;
                playerRes.SignCount++;
                Console.WriteLine($"签到时间:{date.ToShortDateString()}");
                var tbsignDaily = MyConfig.Tables?.Tbsign_daily.Get(date.Day);
                if (tbsignDaily != null)
                {
                    rewards.rewards = tbsignDaily.reward;
                }

                await SetPlayerResDB(openId, playerRes);
            }
            else
            {
                Console.WriteLine($"不可签 上次签到时间戳:{playerRes.LastSignTimeStamp}");
            }

            message.Content =
                MessagePackSerializer.Serialize(rewards, options);
        }
        else if (message.Cmd == CMD.RECEIVEACHIEVEITEM)
        {
            var rewards = new Rewards { };
            if (!_connections.TryGetValue(webSocket, out var openId))
            {
                //Console.WriteLine($"webSocket:{webSocket.} not found");
                //用户未登记
                message.ErrorCode = 1001;
            }

            var db = _redis.GetDatabase();

            var redisKey = GetRedisDBStr(1, openId);
            var rv = await db.StringGetAsync(redisKey);
            var playerRes = JsonConvert.DeserializeObject<PlayerResource>(rv);


            var achieveId = MessagePackSerializer.Deserialize<int>(message.Content, options);
            var tbtask = MyConfig.Tables?.Tbtask.GetOrDefault(achieveId);

            if (tbtask != null && playerRes != null)
            {
                foreach (var achieveItem in playerRes.GameAchieve.AchieveItemList)
                {
                    if (achieveItem.GroupId == tbtask.group)
                    {
                        if (achieveItem.ReceivedAchieveId < achieveId && achieveItem.CurPara >= tbtask.para)
                        {
                            achieveItem.ReceivedAchieveId = achieveId;
                            playerRes.GameAchieve.Score += tbtask.score;
                            rewards.rewards = tbtask.reward;
                            await SetPlayerResDB(openId, playerRes);
                        }

                        break;
                    }
                }
            }

            message.Content =
                MessagePackSerializer.Serialize(rewards, options);

            outputContentStr = JsonConvert.SerializeObject(achieveId);
        }
        else if (message.Cmd == CMD.RECEIVEACHIEVEBOX)
        {
            var rewards = new Rewards { };
            if (!_connections.TryGetValue(webSocket, out var openId))
            {
                //Console.WriteLine($"webSocket:{webSocket.} not found");
                //用户未登记
                message.ErrorCode = 1001;
            }

            var db = _redis.GetDatabase();

            var redisKey = GetRedisDBStr(1, openId);
            var rv = await db.StringGetAsync(redisKey);
            var playerRes = JsonConvert.DeserializeObject<PlayerResource>(rv);


            var achieveBoxId = MessagePackSerializer.Deserialize<int>(message.Content, options);
            var tbtask_score = MyConfig.Tables?.Tbtask_score.GetOrDefault(achieveBoxId);

            if (tbtask_score != null && playerRes != null)
            {
                switch (tbtask_score.tagFunc)
                {
                    case 3604:
                        if (playerRes.GameAchieve.Score >= tbtask_score.score &&
                            !playerRes.GameAchieve.AchieveRewardBoxList.Contains(achieveBoxId))
                        {
                            rewards.rewards = tbtask_score.reward;
                            await SetPlayerResDB(openId, playerRes);
                        }

                        break;
                }
            }

            message.Content =
                MessagePackSerializer.Serialize(rewards, options);

            outputContentStr = JsonConvert.SerializeObject(achieveBoxId);
        }
        else if (message.Cmd == CMD.RECEIVEMAILITEM)
        {
            var rewards = new Rewards { };
            if (!_connections.TryGetValue(webSocket, out var openId))
            {
                //Console.WriteLine($"webSocket:{webSocket.} not found");
                //用户未登记
                message.ErrorCode = 1001;
            }

            var db = _redis.GetDatabase();

            var redisKey = GetRedisDBStr(1, openId);
            var rv = await db.StringGetAsync(redisKey);
            var playerRes = JsonConvert.DeserializeObject<PlayerResource>(rv);


            var mailId = MessagePackSerializer.Deserialize<int>(message.Content, options);
            foreach (var item in playerRes.GameMail.MailItems)
            {
                if (item.Id == mailId && item.State == 0)
                {
                    var tbmail = MyConfig.Tables?.Tbmail.GetOrDefault(item.TemplateId);
                    if (tbmail != null)
                    {
                        rewards.rewards = tbmail.reward;
                    }

                    item.State = 1;
                    await SetPlayerResDB(openId, playerRes);
                    break;
                }
            }

            message.Content =
                MessagePackSerializer.Serialize(rewards, options);

            outputContentStr = JsonConvert.SerializeObject(mailId);
        }

        stopwatch.Stop();
        string errorStr = message.ErrorCode != 0 ? $"ErrorCode:{message.ErrorCode}" : "";

        MyLogger.Log(message.Cmd.ToString(), _connections[webSocket], inputContentStr,
            $"ErrorCode:{errorStr},Content:{outputContentStr}",
            stopwatch);
        return message;
    }

    private async Task SetPlayerResDB(string openId, PlayerResource playerRes)
    {
        var db = _redis.GetDatabase();
        var redisKey = GetRedisDBStr(1, openId);
        await db.StringSetAsync(redisKey, JsonConvert.SerializeObject(playerRes));
    }

    private long GetCurrentUtcLong()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    /// <summary>
    /// 判断是否今天可以签到
    /// </summary>
    /// <param name="lastSignedTime">上次签到时间戳 /ms</param>
    /// <returns>bool</returns>
    bool CanSignOrLoginToday(long lastSignedTime, out DateTime utcNowDate, out long utclong)
    {
        const int Hour = 6;
        // 将 Unix 时间戳转换为 DateTime
        DateTime lastSignTime = DateTimeOffset.FromUnixTimeMilliseconds(lastSignedTime).DateTime;
        var utcNow = DateTimeOffset.UtcNow;
        DateTime currentTime = utcNow.DateTime;
        DateTime today6 =
            new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, Hour, 0, 0, DateTimeKind.Utc);

        utcNowDate = currentTime;
        utclong = utcNow.ToUnixTimeMilliseconds();
        return lastSignTime < today6;
    }

    string GetRedisDBStr(int type, string openId)
    {
        string redisStr = openId;
        switch (type)
        {
            case 1:
                redisStr += "_PlayerResource";
                break;
        }

        return redisStr;
    }

    /// <summary>
    /// 初始化玩家数据
    /// </summary>
    /// <param name="db"></param>
    /// <param name="openId"></param>
    private PlayerResource InitPlayerResource()
    {
        var itemInfos = MyConfig.Tables?.Tbitem.DataList.Where(a => a.initEnable == 1)
            .Select(item => new Vector3(item.id, 1, 0)).ToList();

        var initAchieveItemList = MyConfig.Tables?.Tbtask.DataList
            .GroupBy(item => item.group) // 根据 group 分组
            .Select(group => new AchieveItem // 直接创建 AchieveItem 对象
            {
                GroupId = group.Key, // 使用分组键作为 GroupId
                CurPara = 0,
                Type = group.First().type // 从组中获取第一个 item's type
            })
            .ToList(); // 转换为 List

        List<MailItem> mailItems = new List<MailItem>();
        mailItems.Add(new MailItem
        {
            Id = 1001,
            TemplateId = 10001,
            State = 0,
            SendTimeStamp = GetCurrentUtcLong()
        });
        var playerRes = new PlayerResource
        {
            ItemList = itemInfos,
            LastSignTimeStamp = 0,
            SignCount = 0,
            LastLoginTimeStamp = 0,
            LoginCount = 0,
            ContinuousLoginCount = 0,
            GameAchieve = new GameAchievement
            {
                AchieveItemList = initAchieveItemList,
                Score = 0,
                AchieveRewardBoxList = new List<int>()
            },
            GameMail = new GameMail
            {
                MailItems = mailItems
            }
        };
        return playerRes;
    }

    public async Task<WXCode2Session>? GetSessionJson(string jsCode)
    {
        string appId = AppKeys.AppID;
        string appSecret = AppKeys.AppSecret;
        // 构建请求的URL
        string url =
            $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={appSecret}&js_code={jsCode}&grant_type=authorization_code";
        string responseBody = default;
        // 发起GET请求
        //HttpResponseMessage response = await _httpClient.GetAsync(url);

        //if (response.IsSuccessStatusCode)
        if (false)
        {
            //TODO: 读取响应内容

            //responseBody = await response.Content.ReadAsStringAsync();
            //TODO:删

            Console.WriteLine($"responseBody:{responseBody}");
        }
        else
        {
            responseBody = JsonConvert.SerializeObject(new WXCode2Session
            {
                session_key = "safasfs234",
                unionid = "sfs1",
                errmsg = null,
                openid = jsCode,
                errcode = 0
            });
            //Console.WriteLine($"response from server: {response.ReasonPhrase}");
        }

        var wxCode2Session = JsonConvert.DeserializeObject<WXCode2Session>(responseBody);
        //Console.WriteLine($"responseBody:{wxCode2Session.ToString()}");
        return wxCode2Session;
    }

    #region BoardCast

    private async Task NotifyUserAsync(string openId, MyMessage message)
    {
        var ws = _connections.Where(a => a.Value == openId).FirstOrDefault();
        if (ws.Key != null)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var webSocket = ws.Key;
            // 使用 MessagePack 序列化消息
            var responseBuffer = MessagePackSerializer.Serialize(message, options);

            // 向客户端推送消息
            await webSocket.SendAsync(
                new ArraySegment<byte>(responseBuffer, 0, responseBuffer.Length),
                WebSocketMessageType.Binary,
                true, // 表示结束消息
                CancellationToken.None);

            stopwatch.Stop();
            string errorStr = message.ErrorCode != 0 ? $"ErrorCode:{message.ErrorCode}" : "";

            MyLogger.Log(message.Cmd.ToString(), _connections[webSocket], $"ToUser:{openId}",
                $"ErrorCode:{errorStr},Content:",
                stopwatch);
        }
        else
        {
            // WebSocket 连接不可用或未找到该用户
            Console.WriteLine($"User {openId} is not connected or WebSocket is not open.");
        }
    }

    #endregion
}