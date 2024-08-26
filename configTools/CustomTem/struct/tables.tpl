using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

{{
    name = x.name
    namespace = x.namespace
    tables = x.tables
}}

namespace cfg.blobstruct{
   
public static class GenGenBlobAssetReference
{
   
public struct ConfigData
{
{{~for table in tables ~}}
    public Config{{table.name}}s config{{table.name}}s;
{{~end~}} 
}
{{~for table in tables ~}}
public struct Config{{table.name}}s
{
    public BlobArray<Config{{table.name}}> config{{table.name}}s;
}
{{~end~}} 


public static BlobAssetReference<ConfigData> CreateBlob(Tables tables)
{
    var builder = new BlobBuilder(Allocator.Temp);
    ref var root = ref builder.ConstructRoot<ConfigData>();
    {{~for table in tables ~}}
    BlobBuilderArray<Config{{table.name}}> config{{table.name}}s = builder.Allocate(
        ref root.config{{table.name}}s.config{{table.name}}s,
        tables.{{table.name}}.DataList.Count);
    for (var i = 0; i < tables.{{table.name}}.DataList.Count; i++)
    {   {{ newVariable = table.name | string.slice(2) }}
        ConfigTb{{newVariable}}.Create(i,ref builder,ref config{{table.name}}s,tables);
    }
    {{~end~}}
    var result = builder.CreateBlobAssetReference<ConfigData>(Allocator.Persistent);
    builder.Dispose();
    return result;
}
}}