using System.Collections.Concurrent;
using System.Net.WebSockets;
using FoxCard.Server.Datas;
using FoxCard.Server.Datas.Config.Scripts;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;
using UnityEngine;

namespace FoxCard.Server.Handlers;

public class LoginCommandHandler : HandleBase, ICommandHandler
{
    public LoginCommandHandler(IConnectionMultiplexer redis, ConcurrentDictionary<WebSocket, string> connections) :
        base(redis, connections)
    {
    }

    public async Task<Context> HandleAsync(MyMessage message, WebSocket webSocket)
    {
        var db = _redis.GetDatabase();
        var playerData = MessagePackSerializer.Deserialize<PlayerData>(message.Content, options);
        var inputContentStr = JsonConvert.SerializeObject(playerData);

        //Console.WriteLine($"PlayerData:{JsonConvert.SerializeObject(playerData)}");
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

        if (CanSignOrLoginToday(playerRes.PlayerServerData.LastLoginTimeStamp, out var date, out var utclong))
        {
            var lastDate = DateTimeOffset.FromUnixTimeMilliseconds(playerRes.PlayerServerData.LastLoginTimeStamp)
                .DateTime;
            playerRes.PlayerServerData.LastLoginTimeStamp = utclong;
            playerRes.PlayerServerData.LoginCount++;
            var timeSpan = date - lastDate;
            playerRes.PlayerServerData.ContinuousLoginCount = timeSpan.TotalHours < 48
                ? playerRes.PlayerServerData.ContinuousLoginCount + 1
                : 0;
            if (player.IsNullOrEmpty)
            {
                playerRes.PlayerServerData.ContinuousLoginCount = 1;
            }

            playerRes.GameAchieve?.SetAchievePara(1012);
        }

        await SetPlayerResDB(openId, playerRes);

        playerData.ThirdId = MyEncryptor.Decrypt(openId);
        var temp = playerData.OtherData;
        temp.UnionidId = wxCode2Session.unionid;
        playerData.OtherData = temp;

        message.Content = MessagePackSerializer.Serialize(playerData, options);
        var outputContentStr = JsonConvert.SerializeObject(playerData);

        var context = new Context
        {
            message = message,
            inputContentStr = inputContentStr,
            outputContentStr = outputContentStr
        };
        return context;
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
            GameAchieve = new GameAchievement
            {
                AchieveItemList = initAchieveItemList,
                Score = 0,
                AchieveRewardBoxList = new List<int>()
            },
            GameMail = new GameMail
            {
                MailItems = mailItems
            },
            GameSign = new GameSign
            {
                isSignedToday = false
            },
            GameSignAcc7 = new GameSignAcc7(),
            PlayerServerData = new PlayerServerData()
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
}