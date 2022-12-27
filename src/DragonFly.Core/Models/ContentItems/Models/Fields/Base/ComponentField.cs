// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.ContentItems.Models.Validations;

namespace DragonFly;

/// <summary>
/// ComponentField
/// </summary>
[FieldOptions(typeof(ComponentFieldOptions))]
public class ComponentField : ContentField
{
    public ComponentField()
    {
    }

    /// <summary>
    /// ContentComponent
    /// </summary>
    public ContentComponent? ContentComponent { get; set; }

    public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
    {
        if (options is ComponentFieldOptions fieldOptions)
        {
            if (fieldOptions.IsRequired && ContentComponent == null)
            {
                context.AddRequireValidation(fieldName);
            }
        }
    }

    public override string ToString()
    {
        if (ContentComponent == null)
        {
            return "no embedded content";
        }
        else
        {
            return $"{ContentComponent}";
        }
    }
}
