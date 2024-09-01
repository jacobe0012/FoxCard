using System.Net.WebSockets;
using FoxCard.Server.Datas;
using HotFix_UI;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Controllers;

public class WebSocketController : ControllerBase
{
    private readonly IConnectionMultiplexer _redis;
    private readonly HttpClient _httpClient;

    public WebSocketController(IConnectionMultiplexer redis, HttpClient httpClient)
    {
        _redis = redis;
        _httpClient = httpClient;
    }

    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await Echo(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }


    private async Task Echo(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!receiveResult.CloseStatus.HasValue)
        {
            var receivedMessage = MessagePackSerializer.Deserialize<MyMessage>(buffer);


            //处理消息并生成回复
            var responseMessage = await ProcessMessage(receivedMessage);

            //使用 MessagePack 序列化回复消息
            var responseBuffer = MessagePackSerializer.Serialize(responseMessage);

            // 将回复发送回客户端
            await webSocket.SendAsync(
                new ArraySegment<byte>(responseBuffer, 0, responseBuffer.Length),
                receiveResult.MessageType,
                receiveResult.EndOfMessage,
                CancellationToken.None);

            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
    }

    private async Task<MyMessage> ProcessMessage(MyMessage message)
    {
        Console.WriteLine($"MyMessage:{JsonConvert.SerializeObject(message)}");
        if (message.MethodName == "Login")
        {
            var playerData = MessagePackSerializer.Deserialize<PlayerData>(message.Content);

            var db = _redis.GetDatabase();
            Console.WriteLine($"PlayerData:{JsonConvert.SerializeObject(playerData)}");
            //long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            //var playerdata = await db.StringGetAsync(player.Id.ToString());
            var wxCode2Session = await GetSessionJson(playerData.OtherData.code);
            //playerData.Id = userInfoJson.openid;
            Console.WriteLine($"wxCode2Session.openid:{wxCode2Session.openid}");
            //var userData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            playerData.ThirdId = wxCode2Session.openid;

            string jsonData = JsonConvert.SerializeObject(playerData);
            await db.StringSetAsync(playerData.ThirdId, jsonData);

            Console.WriteLine($"db:{db.StringGet(playerData.ThirdId)}");
        }


        return default;
    }

    public async Task<WXCode2Session>? GetSessionJson(string jsCode)
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

        var wxCode2Session = JsonConvert.DeserializeObject<WXCode2Session>(responseBody);
        //Console.WriteLine($"responseBody:{wxCode2Session.ToString()}");
        return wxCode2Session;
    }
}