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

public sealed partial class item :  Bright.Config.BeanBase 
{
    public item(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  id = _json["id"]; }
        { if(!_json["init_enable"].IsNumber) { throw new SerializationException(); }  initEnable = _json["init_enable"]; }
        PostInit();
    }

    public item(int id, int init_enable ) 
    {
        this.id = id;
        this.initEnable = init_enable;
        PostInit();
    }

    public static item Deserializeitem(JSONNode _json)
    {
        return new config.item(_json);
    }

    /// <summary>
    /// ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 初始号是否携带
    /// </summary>
    public int initEnable { get; private set; }

    public const int __ID__ = -28054977;
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
        + "initEnable:" + initEnable + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
