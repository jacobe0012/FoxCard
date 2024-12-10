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
            if (player.IsNullOrEmpty)
            {
                await db.StringSetAsync(openId, JsonConvert.SerializeObject(playerData));
                await InitPlayerResource(db, openId);
            }

            playerData.ThirdId = MyEncryptor.Decrypt(openId);
            var temp = playerData.OtherData;
            temp.UnionidId = wxCode2Session.unionid;
            playerData.OtherData = temp;

            message.Content = MessagePackSerializer.Serialize(playerData, options);
            outputContentStr = JsonConvert.SerializeObject(playerData);
        }
        else if (message.Cmd == CMD.QUERYRESOURCE)
        {
            if (!_connections.TryGetValue(webSocket, out var openId))
            {
                //Console.WriteLine($"webSocket:{webSocket.} not found");
                //用户未登记
                message.ErrorCode = 1001;
            }

            var db = _redis.GetDatabase();
            var playerRes = await db.StringGetAsync(GetRedisDBStr(1, openId));

            message.Content =
                MessagePackSerializer.Serialize(JsonConvert.DeserializeObject<PlayerResource>(playerRes), options);
            outputContentStr = playerRes.ToString();
        }
        else if (message.Cmd == CMD.DAILYSIGN)
        {
        }

        stopwatch.Stop();
        string errorStr = message.ErrorCode != 0 ? $"ErrorCode:{message.ErrorCode}" : "";

        MyLogger.Log(_connections[webSocket], inputContentStr,
            $"CMD:{message.Cmd},ErrorCode:{errorStr},{outputContentStr}",
            stopwatch);
        return message;
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

    async Task InitPlayerResource(IDatabase db, string openId)
    {
        var itemInfos = MyConfig.Tables.Tbitem.DataList.Where(a => a.initEnable == 1).Select(item => new ItemInfo
        {
            ItemId = item.id,
            Count = 1
        }).ToList();

        var playerRes = new PlayerResource
        {
            ItemList = itemInfos
        };
        await db.StringSetAsync(GetRedisDBStr(1, openId), JsonConvert.SerializeObject(playerRes));
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
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            // 读取响应内容
            responseBody = await response.Content.ReadAsStringAsync();
            responseBody = JsonConvert.SerializeObject(new WXCode2Session
            {
                session_key = "safasfs234",
                unionid = "sfs1",
                errmsg = null,
                openid = jsCode,
                errcode = 0
            });
            Console.WriteLine($"responseBody:{responseBody}");
        }
        else
        {
            Console.WriteLine($"response from server: {response.ReasonPhrase}");
        }

        var wxCode2Session = JsonConvert.DeserializeObject<WXCode2Session>(responseBody);
        //Console.WriteLine($"responseBody:{wxCode2Session.ToString()}");
        return wxCode2Session;
    }
}