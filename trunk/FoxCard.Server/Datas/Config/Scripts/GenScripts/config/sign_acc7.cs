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

public sealed partial class sign_acc7 :  Bright.Config.BeanBase 
{
    public sign_acc7(JSONNode _json) 
    {
        { if(!_json["assist"].IsNumber) { throw new SerializationException(); }  assist = _json["assist"]; }
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  id = _json["id"]; }
        { if(!_json["group_id"].IsNumber) { throw new SerializationException(); }  groupId = _json["group_id"]; }
        { var __json0 = _json["reward"]; if(!__json0.IsArray) { throw new SerializationException(); } reward = new System.Collections.Generic.List<UnityEngine.Vector3>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { UnityEngine.Vector3 __v0;  { var _json2 = __e0; if(!_json2.IsObject) { throw new SerializationException(); } float __x; { if(!_json2["x"].IsNumber) { throw new SerializationException(); }  __x = _json2["x"]; } float __y; { if(!_json2["y"].IsNumber) { throw new SerializationException(); }  __y = _json2["y"]; } float __z; { if(!_json2["z"].IsNumber) { throw new SerializationException(); }  __z = _json2["z"]; }  __v0 = new UnityEngine.Vector3(__x, __y,__z); }  reward.Add(__v0); }   }
        PostInit();
    }

    public sign_acc7(int assist, int id, int group_id, System.Collections.Generic.List<UnityEngine.Vector3> reward ) 
    {
        this.assist = assist;
        this.id = id;
        this.groupId = group_id;
        this.reward = reward;
        PostInit();
    }

    public static sign_acc7 Deserializesign_acc7(JSONNode _json)
    {
        return new config.sign_acc7(_json);
    }

    /// <summary>
    /// 辅助主键id
    /// </summary>
    public int assist { get; private set; }
    /// <summary>
    /// 星期id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 奖励组id
    /// </summary>
    public int groupId { get; private set; }
    /// <summary>
    /// 奖励
    /// </summary>
    public System.Collections.Generic.List<UnityEngine.Vector3> reward { get; private set; }

    public const int __ID__ = 2079894892;
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
        + "assist:" + assist + ","
        + "id:" + id + ","
        + "groupId:" + groupId + ","
        + "reward:" + Bright.Common.StringUtil.CollectionToString(reward) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
