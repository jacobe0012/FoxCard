using System.Collections.Concurrent;
using System.Net.WebSockets;
using FoxCard.Server.Datas.Config.Scripts;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Handlers;

public class ReceiveAchieveBoxHandler : HandleBase, ICommandHandler
{
    public ReceiveAchieveBoxHandler(IConnectionMultiplexer redis,
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

        var outputContentStr = JsonConvert.SerializeObject(achieveBoxId);

        var context = new Context
        {
            message = message,
            inputContentStr = "",
            outputContentStr = outputContentStr
        };
        return context;
    }
}