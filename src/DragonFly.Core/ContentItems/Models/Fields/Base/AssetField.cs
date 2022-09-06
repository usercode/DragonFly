using DragonFly.Core.ContentItems.Models.Validations;
using DragonFly.Query;
using System;
using System.Collections.Generic;
using System.Text;

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

    /// <summary>
    /// Asset
    /// </summary>
    public Asset? Asset { get; set; }
}
