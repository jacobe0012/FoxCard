using System.Net;
using HotFix_UI;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var config = new ConfigurationOptions
    {
        AbortOnConnectFail = false
    };
    config.EndPoints.Add(IPAddress.Loopback, 6379); // 默认 Redis 端口是 6379
    config.SetDefaultPorts();
    var connection = ConnectionMultiplexer.Connect(config);
    connection.ConnectionFailed += (_, e) => { Console.WriteLine("Connection to Redis failed."); };

    if (!connection.IsConnected)
    {
        Console.WriteLine("Did not connect to Redis.");
    }

    return connection;
});

var app = builder.Build();
// <snippet_UseWebSockets>
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};

app.UseWebSockets(webSocketOptions);
// </snippet_UseWebSockets>

//app.UseDefaultFiles();
//app.UseStaticFiles();

app.MapControllers();
var url = $"http://{MyUrl.urlipv4}";
app.Run(url);