using System.Collections.Concurrent;
using System.Net.WebSockets;
using FoxCard.Server.Datas.Config.Scripts;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Handlers;

public class ReceiveDailySignHandler : HandleBase, ICommandHandler
{
    public ReceiveDailySignHandler(IConnectionMultiplexer redis,
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

        if (CanSignOrLoginToday(playerRes.PlayerServerData.LastSignTimeStamp, out var date, out var utclong))
        {
            playerRes.PlayerServerData.LastSignTimeStamp = utclong;
            playerRes.PlayerServerData.SignCount++;
            Console.WriteLine($"签到时间:{date.ToShortDateString()}");
            var tbsignDaily = MyConfig.Tables?.Tbsign_daily.Get(date.Day);
            if (tbsignDaily != null)
            {
                rewards.rewards = tbsignDaily.reward;
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
            Console.WriteLine($"不可签 上次签到时间戳:{playerRes.PlayerServerData.LastSignTimeStamp}");
        }

        message.Content =
            MessagePackSerializer.Serialize(rewards, options);

        var context = new Context
        {
            message = message,
            inputContentStr = "",
            outputContentStr = ""
        };
        return context;
    }
}