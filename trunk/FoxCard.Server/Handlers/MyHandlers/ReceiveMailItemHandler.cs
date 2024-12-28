using System.Collections.Concurrent;
using System.Net.WebSockets;
using FoxCard.Server.Datas.Config.Scripts;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Handlers;

public class ReceiveMailItemHandler : HandleBase, ICommandHandler
{
    public ReceiveMailItemHandler(IConnectionMultiplexer redis,
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

        var outputContentStr = JsonConvert.SerializeObject(mailId);

        var context = new Context
        {
            message = message,
            inputContentStr = "",
            outputContentStr = outputContentStr
        };
        return context;
    }
}