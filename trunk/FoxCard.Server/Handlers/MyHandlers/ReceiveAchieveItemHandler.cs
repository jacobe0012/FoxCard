using System.Collections.Concurrent;
using System.Net.WebSockets;
using FoxCard.Server.Datas.Config.Scripts;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Handlers;

public class ReceiveAchieveItemHandler : HandleBase, ICommandHandler
{
    public ReceiveAchieveItemHandler(IConnectionMultiplexer redis,
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

        var outputContentStr = JsonConvert.SerializeObject(achieveId);

        var context = new Context
        {
            message = message,
            inputContentStr = "",
            outputContentStr = outputContentStr
        };
        return context;
    }
}