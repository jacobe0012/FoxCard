namespace FoxCard.Server.Datas;

public class WXCode2Session
{
    public string session_key { get; set; }
    public string unionid { get; set; }
    public string errmsg { get; set; }
    public string openid { get; set; }
    public int errcode { get; set; }
}