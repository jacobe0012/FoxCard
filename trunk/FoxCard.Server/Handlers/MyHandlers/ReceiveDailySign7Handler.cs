using System.Collections.Concurrent;
using System.Net.WebSockets;
using FoxCard.Server.Datas.Config.Scripts;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Handlers;

public class ReceiveDailySign7Handler : HandleBase, ICommandHandler
{
    public ReceiveDailySign7Handler(IConnectionMultiplexer redis,
        ConcurrentDictionary<WebSocket, string> connections) :
        base(redis, connections)
    {
    }

    public async Task<Context> HandleAsync(MyMessage message, WebSocket webSocket)
    {
        var rewards = new Rewards { };
        if (!_connections.TryGetValue(webSocket, out var openId))
        {
            //Console.WriteLine($"webSocket:{webSocket.} not found");
            //用户未登记
            message.ErrorCode = 1001;
        }

        int signType = MessagePackSerializer.Deserialize<int>(message.Content, options);
        var db = _redis.GetDatabase();
        var redisKey = GetRedisDBStr(1, openId);
        var rv = await db.StringGetAsync(redisKey);
        var playerRes = JsonConvert.DeserializeObject<PlayerResource>(rv);

        if (CanSignOrLoginToday(playerRes.PlayerServerData.Last7SignTimeStamp, out var date, out var utclong))
        {
            playerRes.PlayerServerData.Last7SignTimeStamp = utclong;
            playerRes.GameSignAcc7.SignedDay++;
            playerRes.GameSignAcc7.isSignedToday = true;
            var serverData = await GetServerRootData();

            Console.WriteLine($"7日签到时间:{date.ToShortDateString()}");
            var tbsignAcc7 =
                MyConfig.Tables?.Tbsign_acc7.DataList.Where(a =>
                        a.groupId == serverData.Signed7GroupId && a.id == playerRes.GameSignAcc7.SignedDay)
                    .FirstOrDefault();


            if (tbsignAcc7 != null)
            {
                rewards.rewards = tbsignAcc7.reward;
                if (signType == 2)
                {
                    for (int i = 0; i < rewards.rewards.Count; i++)
                    {
                        var temp = rewards.rewards[i];
                        temp.z *= 2;
                        rewards.rewards[i] = temp;
                    }
                }
            }

            await SetPlayerResDB(openId, playerRes);
        }
        else
        {
            rewards = null;
            Console.WriteLine($"7日签到不可签 上次签到时间戳:{playerRes.PlayerServerData.Last7SignTimeStamp}");
        }

        message.Content =
            MessagePackSerializer.Serialize(rewards, options);

        var context = new Context
        {
            message = message,
            inputContentStr = signType.ToString(),
            outputContentStr = rewards?.ToString()
        };
        return context;
    }
}