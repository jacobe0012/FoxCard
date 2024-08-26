using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;

{{ 
    name = x.name
    key_type = x.key_ttype
    key_type1 =  x.key_ttype1
    key_type2 =  x.key_ttype2
    value_type =  x.value_ttype
}}

{{cs_start_name_space_grace x.namespace_with_top_module}} 

{{~if x.comment != '' ~}}
/// <summary>
/// {{x.escape_comment}}
/// </summary>
{{~end~}}
{{~if name == 'Tblanguage' ~}}
public sealed partial class Tblanguage
{
    private readonly Dictionary<string, config.language> _dataMap;
    private readonly List<config.language> _dataList;
    
    public Tblanguage(JSONNode _json)
    {
        _dataMap = new Dictionary<string, config.language>();
        _dataList = new List<config.language>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = config.language.Deserializelanguage(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.langId, _v);
        }
        PostInit();
    }

    public Dictionary<string, config.language> DataMap => _dataMap;
    public List<config.language> DataList => _dataList;

    public config.language GetOrDefault(string key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public config.language Get(string key) {if(_dataMap.TryGetValue(key,out var v)){return v;}else{UnityEngine.Debug.LogError($"当前key值不存在,请确认多语言表配置!key:{key}");var newlang = new config.language(key,key,key,key,key,key,key,key);return newlang;}}
    public config.language this[string key] => _dataMap[key];

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
    
    public void SwitchL10N(L10N l10N)
    {
        foreach(var v in _dataList)
        {
            switch (l10N)
            {
                case L10N.zh_cn:
                    v.current=IsEmptyStr(v.zhCn, v.langId);
                    break;
                case L10N.en:
                    v.current=IsEmptyStr(v.en, v.langId);
                    break;
                case L10N.de:
                    v.current=IsEmptyStr(v.de, v.langId);
                    break;
                case L10N.fr:
                    v.current=IsEmptyStr(v.fr, v.langId);
                    break;
                case L10N.es:
                    v.current=IsEmptyStr(v.es, v.langId);
                    break;
                case L10N.jp:
                    v.current=IsEmptyStr(v.jp, v.langId);
                    break;
            }
        }

        foreach (var v in _dataMap)
        {
            switch (l10N)
            {
                case L10N.zh_cn:
                    v.Value.current=IsEmptyStr(v.Value.zhCn, v.Value.langId);
                    break;
                case L10N.en:
                    v.Value.current=IsEmptyStr(v.Value.en, v.Value.langId);
                    break;
                case L10N.de:
                    v.Value.current=IsEmptyStr(v.Value.de, v.Value.langId);
                    break;
                case L10N.fr:
                    v.Value.current=IsEmptyStr(v.Value.fr, v.Value.langId);
                    break;
                case L10N.es:
                    v.Value.current=IsEmptyStr(v.Value.es, v.Value.langId);
                    break;
                case L10N.jp:
                    v.Value.current=IsEmptyStr(v.Value.jp, v.Value.langId);
                    break;
            }
        }
    }
    private string IsEmptyStr(string targetStr, string langid)
    {
        string current;
        if (targetStr == "")
        {
            return current = langid;
        }

        return current = targetStr;
    }
    public enum L10N
    {
        en=1,
        zh_cn=2,
        zh_HK=3,
        kr=4,
        jp=5,
        fr=6,
        de=7,
        ru=8,
        th=9,
        es=10,
    }
}
{{~else~}}
public sealed partial class {{name}}
{
    {{~if x.is_map_table ~}}
    private readonly Dictionary<{{cs_define_type key_type}}, {{cs_define_type value_type}}> _dataMap;
    private readonly List<{{cs_define_type value_type}}> _dataList;
    
    public {{name}}(JSONNode _json)
    {
        _dataMap = new Dictionary<{{cs_define_type key_type}}, {{cs_define_type value_type}}>();
        _dataList = new List<{{cs_define_type value_type}}>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = {{cs_define_type value_type}}.Deserialize{{value_type.bean.name}}(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.{{x.index_field.convention_name}}, _v);
        }
        PostInit();
    }

    public Dictionary<{{cs_define_type key_type}}, {{cs_define_type value_type}}> DataMap => _dataMap;
    public List<{{cs_define_type value_type}}> DataList => _dataList;

{{~if value_type.is_dynamic~}}
    public T GetOrDefaultAs<T>({{cs_define_type key_type}} key) where T : {{cs_define_type value_type}} => _dataMap.TryGetValue(key, out var v) ? (T)v : null;
    public T GetAs<T>({{cs_define_type key_type}} key) where T : {{cs_define_type value_type}} => (T)_dataMap[key];
{{~end~}}
    public {{cs_define_type value_type}} GetOrDefault({{cs_define_type key_type}} key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public {{cs_define_type value_type}} Get({{cs_define_type key_type}} key) => _dataMap[key];
    public {{cs_define_type value_type}} this[{{cs_define_type key_type}} key] => _dataMap[key];

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
    
        {{~else if x.is_list_table ~}}
    private readonly List<{{cs_define_type value_type}}> _dataList;
    
    {{~if x.is_union_index~}}
    private {{cs_table_union_map_type_name x}} _dataMapUnion;
    {{~else if !x.index_list.empty?~}}
    {{~for idx in x.index_list~}}
    private Dictionary<{{cs_define_type idx.type}}, {{cs_define_type value_type}}> _dataMap_{{idx.index_field.name}};
    {{~end~}}
    {{~end~}}

    public {{name}}(JSONNode _json)
    {
        _dataList = new List<{{cs_define_type value_type}}>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = {{cs_define_type value_type}}.Deserialize{{value_type.bean.name}}(_row);
            _dataList.Add(_v);
        }
    {{~if x.is_union_index~}}
        _dataMapUnion = new {{cs_table_union_map_type_name x}}();
        foreach(var _v in _dataList)
        {
            _dataMapUnion.Add(({{cs_table_key_list x "_v"}}), _v);
        }
    {{~else if !x.index_list.empty?~}}
    {{~for idx in x.index_list~}}
        _dataMap_{{idx.index_field.name}} = new Dictionary<{{cs_define_type idx.type}}, {{cs_define_type value_type}}>();
    {{~end~}}
    foreach(var _v in _dataList)
    {
    {{~for idx in x.index_list~}}
        _dataMap_{{idx.index_field.name}}.Add(_v.{{idx.index_field.convention_name}}, _v);
    {{~end~}}
    }
    {{~end~}}
        PostInit();
    }

    public List<{{cs_define_type value_type}}> DataList => _dataList;
   

    {{~if x.is_union_index~}}
    public {{cs_define_type value_type}} Get({{cs_table_get_param_def_list x}}) => _dataMapUnion.TryGetValue(({{cs_table_get_param_name_list x}}), out {{cs_define_type value_type}} __v) ? __v : null;
    {{~else if !x.index_list.empty? ~}}
        {{~for idx in x.index_list~}}
    public {{cs_define_type value_type}} GetBy{{idx.index_field.convention_name}}({{cs_define_type idx.type}} key) => _dataMap_{{idx.index_field.name}}.TryGetValue(key, out {{cs_define_type value_type}} __v) ? __v : null;
        {{~end~}}
    {{~end~}}


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

    {{~else~}}

     private readonly {{cs_define_type value_type}} _data;

    public {{name}}(JSONNode _json)
    {
        if(!_json.IsArray)
        {
            throw new SerializationException();
        }
        if (_json.Count != 1) throw new SerializationException("table mode=one, but size != 1");
        _data = {{cs_define_type value_type}}.Deserialize{{value_type.bean.name}}(_json[0]);
        PostInit();
    }

    {{~ for field in value_type.bean.hierarchy_export_fields ~}}

{{~if field.comment != '' ~}}
    /// <summary>
    /// {{field.escape_comment}}
    /// </summary>
{{~end~}}
     public {{cs_define_type field.ctype}} {{field.convention_name}} => _data.{{field.convention_name}};
    {{~end~}}



    public void Resolve(Dictionary<string, object> _tables)
    {
        _data.Resolve(_tables);
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        _data.TranslateText(translator);
    }

    {{~end~}}
    
    partial void PostInit();
    partial void PostResolve();
    
    {{~if name == 'Tblanguage' ~}}
    public void SwitchL10N(L10N l10N)
    {
        foreach(var v in _dataList)
        {
            switch (l10N)
            {
                case L10N.zh_cn:
                    v.current = v.zhCn;
                    break;
                case L10N.en:
                    v.current = v.en;
                    break;
                case L10N.de:
                    v.current = v.de;
                    break;
                case L10N.fr:
                    v.current = v.fr;
                    break;
                case L10N.es:
                    v.current = v.es;
                    break;
                case L10N.jp:
                    v.current = v.jp;
                    break;
            }
        }

        foreach (var v in _dataMap)
        {
            switch (l10N)
            {
                case L10N.zh_cn:
                    v.Value.current = v.Value.zhCn;
                    break;
                case L10N.en:
                    v.Value.current = v.Value.en;
                    break;
                case L10N.de:
                    v.Value.current = v.Value.de;
                    break;
                case L10N.fr:
                    v.Value.current = v.Value.fr;
                    break;
                case L10N.es:
                    v.Value.current = v.Value.es;
                    break;
                case L10N.jp:
                    v.Value.current = v.Value.jp;
                    break;
            }
        }
    }
   
    public enum L10N
    {
        zh_cn,
        en,
        de,
        fr,
        es,
        jp
    }
    {{~end~}}
}
{{~end~}}
{{cs_end_name_space_grace x.namespace_with_top_module}}