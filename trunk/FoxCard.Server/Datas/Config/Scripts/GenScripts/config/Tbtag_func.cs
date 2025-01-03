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

public sealed partial class Tbtag_func
{
    private readonly Dictionary<int, config.tag_func> _dataMap;
    private readonly List<config.tag_func> _dataList;
    
    public Tbtag_func(JSONNode _json)
    {
        _dataMap = new Dictionary<int, config.tag_func>();
        _dataList = new List<config.tag_func>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = config.tag_func.Deserializetag_func(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.id, _v);
        }
        PostInit();
    }

    public Dictionary<int, config.tag_func> DataMap => _dataMap;
    public List<config.tag_func> DataList => _dataList;

    public config.tag_func GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public config.tag_func Get(int key) => _dataMap[key];
    public config.tag_func this[int key] => _dataMap[key];

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