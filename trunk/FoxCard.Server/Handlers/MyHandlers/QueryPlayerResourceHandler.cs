using System.Collections.Concurrent;
using System.Net.WebSockets;
using FoxCard.Server.Datas;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Handlers;

public class QueryPlayerResourceHandler : HandleBase, ICommandHandler
{
    public QueryPlayerResourceHandler(IConnectionMultiplexer redis,
        ConcurrentDictionary<WebSocket, string> connections) :
        base(redis, connections)
    {
    }

    public async Task<Context> HandleAsync(MyMessage message, WebSocket webSocket)
    {
        PlayerResource playerRes;
        if (!_connections.TryGetValue(webSocket, out var openId))
        {
            //Console.WriteLine($"webSocket:{webSocket.} not found");
            //用户未登记
            message.ErrorCode = 1001;
        }

        var db = _redis.GetDatabase();
        var rv = await db.StringGetAsync(GetRedisDBStr(1, openId));
        playerRes = JsonConvert.DeserializeObject<PlayerResource>(rv);
        var serverRootData = await GetServerRootData();
        playerRes.GameSignAcc7.Signed7GroupId = serverRootData.Signed7GroupId;
        playerRes.GameSignAcc7.MaxSignedDay = serverRootData.MaxSignedDay;
        await SetPlayerResDB(openId, playerRes);

        playerRes.PlayerServerData = null;
        message.Content =
            MessagePackSerializer.Serialize(playerRes, options);

        var outputContentStr = playerRes.ToString();

        var context = new Context
        {
            message = message,
            inputContentStr = "",
            outputContentStr = outputContentStr
        };
        return context;
    }

    public async Task<ServerRootData?> GetServerRootData()
    {
        var db = _redis.GetDatabase();
        var rv = await db.StringGetAsync(ServerConst.ServerRootName);
        if (rv.IsNullOrEmpty)
        {
            //TODO:初始化服务器Root数据
            var serverdate = new ServerRootData
            {
                Signed7GroupId = 2,
                MaxSignedDay = 3
            };
            db.StringSetAsync(ServerConst.ServerRootName, JsonConvert.SerializeObject(serverdate));
            return serverdate;
        }

        var serverRootData = JsonConvert.DeserializeObject<ServerRootData>(rv);
        return serverRootData;
    }
}