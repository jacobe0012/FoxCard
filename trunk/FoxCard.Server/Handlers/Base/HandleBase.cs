using System.Collections.Concurrent;
using System.Net.WebSockets;
using FoxCard.Server.Datas;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Handlers;

public abstract class HandleBase
{
    const int Hour = 6;
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

    /// <summary>
    /// 设置服务器数据 每日设置/每周设置/GM后台设置等
    /// </summary>
    public async Task SetServerRootData()
    {
        var db = _redis.GetDatabase();
        var rv = await db.StringGetAsync(ServerConst.ServerRootName);

        // 获取今天的日期
        var utcNow = DateTimeOffset.UtcNow;
        DateTime currentTime = utcNow.DateTime;
        DateTime today6 =
            new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, Hour, 0, 0, DateTimeKind.Utc);
        // 获取今天是星期几 (1-7: 1=星期一, 7=星期日)
        int dayOfWeek = (int)today6.DayOfWeek;
        // 将数值转换为1-7的范围，星期一到星期天
        dayOfWeek = (dayOfWeek + 6) % 7 + 1;
        Console.WriteLine($"设置服务器 今天星期{dayOfWeek}");
        var serverData = JsonConvert.DeserializeObject<ServerRootData>(rv);
        serverData.MaxSignedDay = dayOfWeek;

        await db.StringSetAsync(ServerConst.ServerRootName, JsonConvert.SerializeObject(serverData));
    }

    public async Task<ServerRootData?> GetServerRootData()
    {
        var db = _redis.GetDatabase();
        var rv = await db.StringGetAsync(ServerConst.ServerRootName);


        if (rv.IsNullOrEmpty)
        {
            //TODO:初始化服务器Root数据
            // var serverdate = new ServerRootData
            // {
            //     Signed7GroupId = 2,
            //     MaxSignedDay = 7
            // };
            // db.StringSetAsync(ServerConst.ServerRootName, JsonConvert.SerializeObject(serverdate));
            // return serverdate;
        }

        var serverRootData = JsonConvert.DeserializeObject<ServerRootData>(rv);
        return serverRootData;
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