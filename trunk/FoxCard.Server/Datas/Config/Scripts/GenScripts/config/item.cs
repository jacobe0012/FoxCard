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

public sealed partial class item :  Bright.Config.BeanBase 
{
    public item(JsonElement _json) 
    {
        id = _json.GetProperty("id").GetInt32();
        buyValue = _json.GetProperty("buy_value").GetInt32();
        sellValue = _json.GetProperty("sell_value").GetInt32();
        quality = _json.GetProperty("quality").GetInt32();
        PostInit();
    }

    public item(int id, int buy_value, int sell_value, int quality ) 
    {
        this.id = id;
        this.buyValue = buy_value;
        this.sellValue = sell_value;
        this.quality = quality;
        PostInit();
    }

    public static item Deserializeitem(JsonElement _json)
    {
        return new config.item(_json);
    }

    /// <summary>
    /// ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 购买价值
    /// </summary>
    public int buyValue { get; private set; }
    /// <summary>
    /// 出售价值
    /// </summary>
    public int sellValue { get; private set; }
    /// <summary>
    /// 道具品质
    /// </summary>
    public int quality { get; private set; }

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
        + "buyValue:" + buyValue + ","
        + "sellValue:" + sellValue + ","
        + "quality:" + quality + ","
        + "}";
    }

    partial void PostInit();
    partial void PostResolve();
}
}
