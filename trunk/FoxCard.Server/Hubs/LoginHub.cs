using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Demo.Server.Hubs;

public class LoginHub : Hub
{
    private readonly IConnectionMultiplexer _redis;

    public LoginHub(IConnectionMultiplexer redis)
    {
        this._redis = redis;
    }


    public async void Login(PlayerData player)
    {
        var db = _redis.GetDatabase();

        //long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        //var playerdata = await db.StringGetAsync(player.Id.ToString());


        //var userData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
        string jsonData = JsonConvert.SerializeObject(player);
        await db.StringSetAsync(player.Id.ToString(), jsonData);

        //Clients.Caller.SendAsync("", player);
        //Console.WriteLine(timestamp);
        // await db.StringSetAsync(name.FirstName, timestamp.ToString());
        // db.KeyDelete("abc");
        // var value = await db.StringGetAsync("abc"); // 
        //
        // Console.WriteLine($"{name} is called Login");
    }
}