//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using System.Text.Json;



namespace cfg.config
{

public sealed partial class task_group :  Bright.Config.BeanBase 
{
    public task_group(JsonElement _json) 
    {
        id = _json.GetProperty("id").GetInt32();
        tagFunc = _json.GetProperty("tag_func").GetInt32();
        day = _json.GetProperty("day").GetInt32();
        PostInit();
    }

    public task_group(int id, int tag_func, int day ) 
    {
        this.id = id;
        this.tagFunc = tag_func;
        this.day = day;
        PostInit();
    }

    public static task_group Deserializetask_group(JsonElement _json)
    {
        return new config.task_group(_json);
    }

    /// <summary>
    /// 任务组id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 所属模块id
    /// </summary>
    public int tagFunc { get; private set; }
    /// <summary>
    /// 天数
    /// </summary>
    public int day { get; private set; }

    public const int __ID__ = -1787169423;
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
        + "tagFunc:" + tagFunc + ","
        + "day:" + day + ","
        + "}";
    }

    partial void PostInit();
    partial void PostResolve();
}
}
