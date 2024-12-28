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

public sealed partial class Tbconstant
{
    private readonly Dictionary<string, config.constant> _dataMap;
    private readonly List<config.constant> _dataList;
    
    public Tbconstant(JSONNode _json)
    {
        _dataMap = new Dictionary<string, config.constant>();
        _dataList = new List<config.constant>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = config.constant.Deserializeconstant(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.constantName, _v);
        }
        PostInit();
    }

    public Dictionary<string, config.constant> DataMap => _dataMap;
    public List<config.constant> DataList => _dataList;

    public config.constant GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public config.constant Get(string key) => _dataMap[key];
    public config.constant this[string key] => _dataMap[key];

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