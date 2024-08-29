using FoxCard.Server.Datas;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Hubs;

public class LoginHub : Hub
{
    private readonly IConnectionMultiplexer _redis;
    private readonly HttpClient _httpClient;

    public LoginHub(IConnectionMultiplexer redis, HttpClient httpClient)
    {
        _redis = redis;
        _httpClient = httpClient;
    }

    public async Task<string> GetSessionJson(string jsCode)
    {
        string appId = AppKeys.AppID;
        string appSecret = AppKeys.AppSecret;
        // 构建请求的URL
        string url =
            $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={appSecret}&js_code={jsCode}&grant_type=authorization_code";
        string responseBody = default;
        // 发起GET请求
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            // 读取响应内容
            responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"responseBody:{responseBody}");
        }
        else
        {
            Console.WriteLine($"response from server: {response.ReasonPhrase}");
        }

        return responseBody;
    }

    public async void Login(PlayerData player)
    {
        Console.WriteLine($"serverLogin:{player}");
        var db = _redis.GetDatabase();

        //long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        //var playerdata = await db.StringGetAsync(player.Id.ToString());
        var userInfoJson = await GetSessionJson(player.OtherData.code);
        Console.WriteLine($"userInfoJson:{userInfoJson}");
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

    public void Test()
    {
        Console.WriteLine($"Test:1");
    }
}