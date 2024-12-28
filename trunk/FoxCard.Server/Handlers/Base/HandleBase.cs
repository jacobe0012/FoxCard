using System.Collections.Concurrent;
using System.Net.WebSockets;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Handlers;

public abstract class HandleBase
{
    protected readonly IConnectionMultiplexer _redis;

    protected readonly ConcurrentDictionary<WebSocket, string> _connections;

    protected static MessagePackSerializerOptions options =
        MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block);

    public HandleBase(IConnectionMultiplexer redis, ConcurrentDictionary<WebSocket, string> connections)
    {
        _redis = redis;
        _connections = connections;
    }


    /// <summary>
    /// 判断是否今天可以签到
    /// </summary>
    /// <param name="lastSignedTime">上次签到时间戳 /ms</param>
    /// <returns>bool</returns>
    public bool CanSignOrLoginToday(long lastSignedTime, out DateTime utcNowDate, out long utclong)
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


    public long GetCurrentUtcLong()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    public async Task SetPlayerResDB(string openId, PlayerResource playerRes)
    {
        var db = _redis.GetDatabase();
        var redisKey = GetRedisDBStr(1, openId);
        await db.StringSetAsync(redisKey, JsonConvert.SerializeObject(playerRes));
    }

    public string GetRedisDBStr(int type, string openId)
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
}

public struct Context
{
    public MyMessage message;

    public string inputContentStr;
    public string outputContentStr;
}

public interface ICommandHandler
{
    Task<Context> HandleAsync(MyMessage message, WebSocket webSocket);
}