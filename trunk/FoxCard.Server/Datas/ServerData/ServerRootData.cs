namespace FoxCard.Server.Datas;

public class ServerRootData
{
    /// <summary>
    /// 七天签到奖励组id
    /// </summary>
    public int Signed7GroupId { get; set; }

    /// <summary>
    /// 服务器签到最大天数   1-7
    /// </summary>
    public int MaxSignedDay { get; set; }
}