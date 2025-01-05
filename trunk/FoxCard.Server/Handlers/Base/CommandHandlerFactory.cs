using System.Collections.Concurrent;
using System.Net.WebSockets;
using HotFix_UI;
using StackExchange.Redis;

namespace FoxCard.Server.Handlers;

public class CommandHandlerFactory
{
    // 注入必要的依赖
    private readonly IConnectionMultiplexer _redis;

    //private readonly IConnections _connections;
    private readonly ConcurrentDictionary<WebSocket, string> _connections;

    public CommandHandlerFactory(IConnectionMultiplexer redis, ConcurrentDictionary<WebSocket, string> connections)
    {
        _redis = redis;
        _connections = connections;
    }

    public ICommandHandler CreateHandler(int command)
    {
        return command switch
        {
            CMD.LOGIN => new LoginCommandHandler(_redis, _connections),
            CMD.QUERYRESOURCE => new QueryPlayerResourceHandler(_redis, _connections),
            CMD.RECEIVEDAILYSIGN => new ReceiveDailySignHandler(_redis, _connections),
            CMD.RECEIVEACHIEVEITEM => new ReceiveAchieveItemHandler(_redis, _connections),
            CMD.RECEIVEACHIEVEBOX => new ReceiveAchieveBoxHandler(_redis, _connections),
            CMD.RECEIVEMAILITEM => new ReceiveMailItemHandler(_redis, _connections),
            CMD.RECEIVEDAILYSIGN7 => new ReceiveDailySign7Handler(_redis, _connections),
            // 类似的命令可以继续添加
            _ => throw new NotImplementedException($"未实现命令 {command} 的处理程序。")
        };
    }
}