using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

{{
    name = x.name
    parent_def_type = x.parent_def_type
    parent = x.parent
    export_fields = x.export_fields
    hierarchy_export_fields = x.hierarchy_export_fields
}}

namespace cfg.blobstruct{

{{~if x.comment != '' ~}}
/// <summary>
/// {{x.escape_comment}}
/// </summary>
{{~end~}}
public  struct ConfigTb{{name}}
{
    public static void Create(int i,ref BlobBuilder builder,ref BlobBuilderArray<ConfigTb{{name}}> configTb{{name}}s,Tables tables)
    { 
    {{~ for field in export_fields ~}}
    {{~if field.ctype.type_name == 'list'~}}
        var allocate{{field.convention_name}}s =
        builder.Allocate(ref configTb{{name}}s[i].{{field.convention_name}},
        tables.Tb{{name}}.DataList[i].{{field.convention_name}}.Count);
        for (var {{field.convention_name}}s = 0; {{field.convention_name}}s < tables.Tb{{name}}.DataList[i].{{field.convention_name}}.Count; {{field.convention_name}}s++)
        {
            {{~if field.ctype.element_type == 'Luban.Job.Common.Types.TVector2'~}}
            allocate{{field.convention_name}}s[{{field.convention_name}}s] = (int2) math.round(tables.Tb{{name}}.DataList[i].{{field.convention_name}}[{{field.convention_name}}s]);
            {{~else if field.ctype.element_type == 'Luban.Job.Common.Types.TVector3'~}}
            allocate{{field.convention_name}}s[{{field.convention_name}}s] = (int3) math.round(tables.Tb{{name}}.DataList[i].{{field.convention_name}}[{{field.convention_name}}s]);
            {{~else~}}
            allocate{{field.convention_name}}s[{{field.convention_name}}s] = tables.Tb{{name}}.DataList[i].{{field.convention_name}}[{{field.convention_name}}s];
            {{~end~}}
        }
    {{~else~}}
        configTb{{name}}s[i].{{field.convention_name}} = tables.Tb{{name}}.DataList[i].{{field.convention_name}};
    {{~end~}}
    {{~end~}}
    {{~if name=="skill_effect" ~}}
            var allocateskillEffectBuffNew =
            builder.Allocate(ref configTbskill_effects[i].skillEffectBuffNew,
            tables.Tbskill_effect.DataList[i].skillEffectBuffNew.Count);
            for (var skillEffectBuffNew = 0; skillEffectBuffNew < tables.Tbskill_effect.DataList[i].skillEffectBuffNew.Count; skillEffectBuffNew++)
            {
                allocateskillEffectBuffNew[skillEffectBuffNew] = tables.Tbskill_effect.DataList[i].skillEffectBuffNew[skillEffectBuffNew];
            }
    {{~end~}}     
    }


    {{~ for field in export_fields ~}}
{{~if field.comment != '' ~}}
    /// <summary>
    /// {{field.escape_comment}}
    /// </summary>
{{~end~}}
    {{~if field.ctype.type_name == 'string'~}}
        {{~if name == 'language'~}}
    public FixedString512Bytes {{field.convention_name}};
        {{~else~}}
    public FixedString128Bytes {{field.convention_name}};
        {{~end~}}
    {{~else if field.ctype.element_type == 'Luban.Job.Common.Types.TInt'~}}
    public BlobArray<int> {{field.convention_name}};    
    {{~else if field.ctype.element_type == 'Luban.Job.Common.Types.TVector2'~}}
    public BlobArray<int2> {{field.convention_name}};  
    {{~else if field.ctype.element_type == 'Luban.Job.Common.Types.TVector3'~}}
    public BlobArray<int3> {{field.convention_name}};  
    {{~else if field.ctype.element_type == 'Luban.Job.Common.Types.TString'~}}
    public BlobArray<FixedString128Bytes> {{field.convention_name}};  
    {{~else~}}
    public {{cs_define_type field.ctype}} {{field.convention_name}};
    {{~end~}}
    {{~end~}}
    {{~if name=="skill_effect" ~}}
    public BlobArray<int3x4> skillEffectBuffNew;
    {{~end~}} 
}
}