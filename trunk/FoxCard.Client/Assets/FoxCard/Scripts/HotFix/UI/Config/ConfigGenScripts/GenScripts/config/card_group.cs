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

public sealed partial class card_group :  Bright.Config.BeanBase 
{
    public card_group(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  id = _json["id"]; }
        { if(!_json["lang_id"].IsString) { throw new SerializationException(); }  langId = _json["lang_id"]; }
        { if(!_json["is_disable"].IsNumber) { throw new SerializationException(); }  isDisable = _json["is_disable"]; }
        { if(!_json["init_add"].IsNumber) { throw new SerializationException(); }  initAdd = _json["init_add"]; }
        { if(!_json["init_mul"].IsNumber) { throw new SerializationException(); }  initMul = _json["init_mul"]; }
        { if(!_json["score_add"].IsNumber) { throw new SerializationException(); }  scoreAdd = _json["score_add"]; }
        { if(!_json["score_mul"].IsNumber) { throw new SerializationException(); }  scoreMul = _json["score_mul"]; }
        PostInit();
    }

    public card_group(int id, string lang_id, int is_disable, int init_add, int init_mul, int score_add, int score_mul ) 
    {
        this.id = id;
        this.langId = lang_id;
        this.isDisable = is_disable;
        this.initAdd = init_add;
        this.initMul = init_mul;
        this.scoreAdd = score_add;
        this.scoreMul = score_mul;
        
        PostInit();
    }

    public static card_group Deserializecard_group(JSONNode _json)
    {
        return new config.card_group(_json);
    }

    /// <summary>
    /// id
    /// </summary>
    public int id { get; private set; }
    public string langId { get; private set; }
    public int isDisable { get; private set; }
    public int initAdd { get; private set; }
    public int initMul { get; private set; }
    public int scoreAdd { get; private set; }
    public int scoreMul { get; private set; }
    public const int __ID__ = -2106808644;
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
        + "langId:" + langId + ","
        + "isDisable:" + isDisable + ","
        + "initAdd:" + initAdd + ","
        + "initMul:" + initMul + ","
        + "scoreAdd:" + scoreAdd + ","
        + "scoreMul:" + scoreMul + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}