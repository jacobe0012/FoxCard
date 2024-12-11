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

public sealed partial class task :  Bright.Config.BeanBase 
{
    public task(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  id = _json["id"]; }
        { if(!_json["group"].IsNumber) { throw new SerializationException(); }  group = _json["group"]; }
        { var __json0 = _json["condition"]; if(!__json0.IsArray) { throw new SerializationException(); } condition = new System.Collections.Generic.List<UnityEngine.Vector3>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { UnityEngine.Vector3 __v0;  { var _json2 = __e0; if(!_json2.IsObject) { throw new SerializationException(); } float __x; { if(!_json2["x"].IsNumber) { throw new SerializationException(); }  __x = _json2["x"]; } float __y; { if(!_json2["y"].IsNumber) { throw new SerializationException(); }  __y = _json2["y"]; } float __z; { if(!_json2["z"].IsNumber) { throw new SerializationException(); }  __z = _json2["z"]; }  __v0 = new UnityEngine.Vector3(__x, __y,__z); }  condition.Add(__v0); }   }
        { var __json0 = _json["reward"]; if(!__json0.IsArray) { throw new SerializationException(); } reward = new System.Collections.Generic.List<UnityEngine.Vector3>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { UnityEngine.Vector3 __v0;  { var _json2 = __e0; if(!_json2.IsObject) { throw new SerializationException(); } float __x; { if(!_json2["x"].IsNumber) { throw new SerializationException(); }  __x = _json2["x"]; } float __y; { if(!_json2["y"].IsNumber) { throw new SerializationException(); }  __y = _json2["y"]; } float __z; { if(!_json2["z"].IsNumber) { throw new SerializationException(); }  __z = _json2["z"]; }  __v0 = new UnityEngine.Vector3(__x, __y,__z); }  reward.Add(__v0); }   }
        { if(!_json["type"].IsNumber) { throw new SerializationException(); }  type = _json["type"]; }
        { var __json0 = _json["para"]; if(!__json0.IsArray) { throw new SerializationException(); } para = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  para.Add(__v0); }   }
        { if(!_json["score"].IsNumber) { throw new SerializationException(); }  score = _json["score"]; }
        PostInit();
    }

    public task(int id, int group, System.Collections.Generic.List<UnityEngine.Vector3> condition, System.Collections.Generic.List<UnityEngine.Vector3> reward, int type, System.Collections.Generic.List<int> para, int score ) 
    {
        this.id = id;
        this.group = group;
        this.condition = condition;
        this.reward = reward;
        this.type = type;
        this.para = para;
        this.score = score;
        PostInit();
    }

    public static task Deserializetask(JSONNode _json)
    {
        return new config.task(_json);
    }

    /// <summary>
    /// 任务id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 所属组
    /// </summary>
    public int group { get; private set; }
    /// <summary>
    /// 任务接取条件
    /// </summary>
    public System.Collections.Generic.List<UnityEngine.Vector3> condition { get; private set; }
    /// <summary>
    /// 任务奖励
    /// </summary>
    public System.Collections.Generic.List<UnityEngine.Vector3> reward { get; private set; }
    /// <summary>
    /// 任务类型
    /// </summary>
    public int type { get; private set; }
    /// <summary>
    /// 任务参数
    /// </summary>
    public System.Collections.Generic.List<int> para { get; private set; }
    /// <summary>
    /// 点数
    /// </summary>
    public int score { get; private set; }

    public const int __ID__ = -27745103;
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
        + "group:" + group + ","
        + "condition:" + Bright.Common.StringUtil.CollectionToString(condition) + ","
        + "reward:" + Bright.Common.StringUtil.CollectionToString(reward) + ","
        + "type:" + type + ","
        + "para:" + Bright.Common.StringUtil.CollectionToString(para) + ","
        + "score:" + score + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
