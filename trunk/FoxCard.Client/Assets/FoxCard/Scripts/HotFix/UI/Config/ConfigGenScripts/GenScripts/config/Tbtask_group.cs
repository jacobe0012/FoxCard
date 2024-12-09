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

public sealed partial class Tbtask_group
{
    private readonly Dictionary<int, config.task_group> _dataMap;
    private readonly List<config.task_group> _dataList;
    
    public Tbtask_group(JSONNode _json)
    {
        _dataMap = new Dictionary<int, config.task_group>();
        _dataList = new List<config.task_group>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = config.task_group.Deserializetask_group(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.id, _v);
        }
        PostInit();
    }

    public Dictionary<int, config.task_group> DataMap => _dataMap;
    public List<config.task_group> DataList => _dataList;

    public config.task_group GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public config.task_group Get(int key) => _dataMap[key];
    public config.task_group this[int key] => _dataMap[key];

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