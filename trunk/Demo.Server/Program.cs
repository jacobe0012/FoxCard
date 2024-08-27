using System.Net;
using Demo.Server.Hubs;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// 添加 Redis 配置
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

builder.Services.AddSignalR()
    .AddMessagePackProtocol()
    .AddStackExchangeRedis(o =>
    {
        o.ConnectionFactory = async writer =>
        {
            var config = new ConfigurationOptions
            {
                AbortOnConnectFail = false
            };
            config.EndPoints.Add(IPAddress.Loopback, 6379);
            config.SetDefaultPorts();
            var connection = await ConnectionMultiplexer.ConnectAsync(config, writer);
            connection.ConnectionFailed += (_, e) => { Console.WriteLine("Connection to Redis failed."); };

            if (!connection.IsConnected)
            {
                Console.WriteLine("Did not connect to Redis.");
            }

            return connection;
        };
    });

var app = builder.Build();

// 配置中间件等
app.MapHub<LoginHub>("/LoginHub");

app.Run();