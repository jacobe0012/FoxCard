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

public sealed partial class Tbsign_daily
{
    private readonly Dictionary<int, config.sign_daily> _dataMap;
    private readonly List<config.sign_daily> _dataList;
    
    public Tbsign_daily(JSONNode _json)
    {
        _dataMap = new Dictionary<int, config.sign_daily>();
        _dataList = new List<config.sign_daily>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = config.sign_daily.Deserializesign_daily(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.id, _v);
        }
        PostInit();
    }

    public Dictionary<int, config.sign_daily> DataMap => _dataMap;
    public List<config.sign_daily> DataList => _dataList;

    public config.sign_daily GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public config.sign_daily Get(int key) => _dataMap[key];
    public config.sign_daily this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    
    partial void PostInit();
    partial void PostResolve();
}

}