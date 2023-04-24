// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;
using DragonFly.Query;

namespace DragonFly;

/// <summary>
/// AssetField
/// </summary>
[FieldOptions(typeof(AssetFieldOptions))]
[FieldQuery(typeof(AssetFieldQuery))]
public class AssetField : ContentField
{
    public AssetField()
    {

    }

    public AssetField(Asset asset)
    {
        Asset = asset;
    }

    /// <summary>
    /// Asset
    /// </summary>
    public Asset? Asset { get; set; }

    public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
    {
        if (options is AssetFieldOptions fieldOptions)
        {
            if (fieldOptions.IsRequired && Asset == null)
            {
                context.AddRequireValidation(fieldName);
            }
        }
    }

    public override void Clear()
    {
        Asset = null;
    }    
}
