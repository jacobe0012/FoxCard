//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg.config
{ 

public sealed partial class sign_daily :  Bright.Config.BeanBase 
{
    public sign_daily(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  id = _json["id"]; }
        { var __json0 = _json["reward"]; if(!__json0.IsArray) { throw new SerializationException(); } reward = new System.Collections.Generic.List<UnityEngine.Vector3>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { UnityEngine.Vector3 __v0;  { var _json2 = __e0; if(!_json2.IsObject) { throw new SerializationException(); } float __x; { if(!_json2["x"].IsNumber) { throw new SerializationException(); }  __x = _json2["x"]; } float __y; { if(!_json2["y"].IsNumber) { throw new SerializationException(); }  __y = _json2["y"]; } float __z; { if(!_json2["z"].IsNumber) { throw new SerializationException(); }  __z = _json2["z"]; }  __v0 = new UnityEngine.Vector3(__x, __y,__z); }  reward.Add(__v0); }   }
        PostInit();
    }

    public sign_daily(int id, System.Collections.Generic.List<UnityEngine.Vector3> reward ) 
    {
        this.id = id;
        this.reward = reward;
        PostInit();
    }

    public static sign_daily Deserializesign_daily(JSONNode _json)
    {
        return new config.sign_daily(_json);
    }

    /// <summary>
    /// 天数
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 奖励
    /// </summary>
    public System.Collections.Generic.List<UnityEngine.Vector3> reward { get; private set; }

    public const int __ID__ = 54950723;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + id + ","
        + "reward:" + Bright.Common.StringUtil.CollectionToString(reward) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
