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

public sealed partial class Tbmail
{
    private readonly Dictionary<int, config.mail> _dataMap;
    private readonly List<config.mail> _dataList;
    
    public Tbmail(JSONNode _json)
    {
        _dataMap = new Dictionary<int, config.mail>();
        _dataList = new List<config.mail>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = config.mail.Deserializemail(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.telempateId, _v);
        }
        PostInit();
    }

    public Dictionary<int, config.mail> DataMap => _dataMap;
    public List<config.mail> DataList => _dataList;

    public config.mail GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public config.mail Get(int key) => _dataMap[key];
    public config.mail this[int key] => _dataMap[key];

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