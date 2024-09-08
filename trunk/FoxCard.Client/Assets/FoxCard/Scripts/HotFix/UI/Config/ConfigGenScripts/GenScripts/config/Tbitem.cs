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

public sealed partial class Tbitem
{
    private readonly Dictionary<int, config.item> _dataMap;
    private readonly List<config.item> _dataList;
    
    public Tbitem(JSONNode _json)
    {
        _dataMap = new Dictionary<int, config.item>();
        _dataList = new List<config.item>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = config.item.Deserializeitem(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.id, _v);
        }
        PostInit();
    }

    public Dictionary<int, config.item> DataMap => _dataMap;
    public List<config.item> DataList => _dataList;

    public config.item GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public config.item Get(int key) => _dataMap[key];
    public config.item this[int key] => _dataMap[key];

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