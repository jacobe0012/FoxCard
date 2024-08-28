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

public sealed partial class language :  Bright.Config.BeanBase 
{
    public language(JSONNode _json) 
    {
        { if(!_json["lang_id"].IsString) { throw new SerializationException(); }  langId = _json["lang_id"]; }
        { if(!_json["zh_cn"].IsString) { throw new SerializationException(); }  zhCn = _json["zh_cn"]; }
        { if(!_json["en"].IsString) { throw new SerializationException(); }  en = _json["en"]; }
        { if(!_json["de"].IsString) { throw new SerializationException(); }  de = _json["de"]; }
        { if(!_json["fr"].IsString) { throw new SerializationException(); }  fr = _json["fr"]; }
        { if(!_json["es"].IsString) { throw new SerializationException(); }  es = _json["es"]; }
        { if(!_json["jp"].IsString) { throw new SerializationException(); }  jp = _json["jp"]; }
        { if(!_json["current"].IsString) { throw new SerializationException(); }  current = _json["current"]; }
        PostInit();
    }

    public language(string lang_id, string zh_cn, string en, string de, string fr, string es, string jp, string current ) 
    {
        this.langId = lang_id;
        this.zhCn = zh_cn;
        this.en = en;
        this.de = de;
        this.fr = fr;
        this.es = es;
        this.jp = jp;
        this.current = current;
        
        PostInit();
    }

    public static language Deserializelanguage(JSONNode _json)
    {
        return new config.language(_json);
    }

    /// <summary>
    /// key
    /// </summary>
    public string langId { get; private set; }
    /// <summary>
    /// 简体中文
    /// </summary>
    public string zhCn { get; private set; }
    /// <summary>
    /// 英文
    /// </summary>
    public string en { get; private set; }
    /// <summary>
    /// 德语
    /// </summary>
    public string de { get; private set; }
    /// <summary>
    /// 法语
    /// </summary>
    public string fr { get; private set; }
    /// <summary>
    /// 西班牙
    /// </summary>
    public string es { get; private set; }
    /// <summary>
    /// 日语
    /// </summary>
    public string jp { get; private set; }
    /// <summary>
    /// 当前语言
    /// </summary>
    public string current { get; set; }
    public const int __ID__ = -611218300;
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
        + "langId:" + langId + ","
        + "zhCn:" + zhCn + ","
        + "en:" + en + ","
        + "de:" + de + ","
        + "fr:" + fr + ","
        + "es:" + es + ","
        + "jp:" + jp + ","
        + "current:" + current + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}