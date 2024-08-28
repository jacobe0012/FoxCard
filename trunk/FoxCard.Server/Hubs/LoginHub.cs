using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace Demo.Server.Hubs;

public class LoginHub : Hub
{
    private readonly IConnectionMultiplexer _redis;

    public LoginHub(IConnectionMultiplexer redis)
    {
        this._redis = redis;
    }


    // public async void Login(MyData name)
    // {
    //     var db = _redis.GetDatabase();
    //
    //     long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    //     //Console.WriteLine(timestamp);
    //     await db.StringSetAsync(name.FirstName, timestamp.ToString());
    //
    //     Console.WriteLine($"{name} is called Login");
    // }
}