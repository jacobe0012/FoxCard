//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using Cysharp.Threading.Tasks;
using SimpleJSON;


namespace cfg
{ 
   
public sealed partial class Tables
{
    public config.Tblanguage Tblanguage {get; private set; }
    public config.Tbcard_group Tbcard_group {get; private set; }
    public config.Tbitem Tbitem {get; private set; }

    public Tables() { }
    public Tables(System.Func<string, JSONNode> loader)
    {
        var tables = new System.Collections.Generic.Dictionary<string, object>();
        Tblanguage = new config.Tblanguage(loader("config_tblanguage")); 
        tables.Add("config.Tblanguage", Tblanguage);
        Tbcard_group = new config.Tbcard_group(loader("config_tbcard_group")); 
        tables.Add("config.Tbcard_group", Tbcard_group);
        Tbitem = new config.Tbitem(loader("config_tbitem")); 
        tables.Add("config.Tbitem", Tbitem);
        PostInit();

        Tblanguage.Resolve(tables); 
        Tbcard_group.Resolve(tables); 
        Tbitem.Resolve(tables); 
        PostResolve();
    }

    public async UniTask LoadAsync(System.Func<string, UniTask<JSONNode>> loader)
    {
        var tables = new System.Collections.Generic.Dictionary<string, object>();
        Tblanguage = new config.Tblanguage(await loader("config_tblanguage")); 
        tables.Add("config.Tblanguage", Tblanguage);
        Tbcard_group = new config.Tbcard_group(await loader("config_tbcard_group")); 
        tables.Add("config.Tbcard_group", Tbcard_group);
        Tbitem = new config.Tbitem(await loader("config_tbitem")); 
        tables.Add("config.Tbitem", Tbitem);
        PostInit();

        Tblanguage.Resolve(tables); 
        Tbcard_group.Resolve(tables); 
        Tbitem.Resolve(tables); 
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        Tblanguage.TranslateText(translator); 
        Tbcard_group.TranslateText(translator); 
        Tbitem.TranslateText(translator); 
    }
    
    partial void PostInit();
    partial void PostResolve();

    
}

}