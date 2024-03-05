// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;

namespace DragonFly;

/// <summary>
/// AssetField
/// </summary>
[Field]
[FieldOptions(typeof(AssetFieldOptions))]
[FieldQuery(typeof(AssetFieldQuery))]
public partial class AssetField
{
    public AssetField()
    {

    }

    public AssetField(Asset asset)
    {
        Asset = asset;
    }

    private Asset? _asset;

    /// <summary>
    /// Asset
    /// </summary>
    public Asset? Asset
    {
        get => _asset;
        set
        {
            if (value?.IsNew() == true)
            {
                throw new Exception("It's not allowed to assign an unsaved asset.");
            }

            _asset = value;
        }
    }

    public override void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
        base.Validate(fieldName, options, context);

        if (options is AssetFieldOptions assetOptions)
        {
            if (assetOptions.IsRequired && Asset == null)
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
