// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;

namespace DragonFly;

/// <summary>
/// ValidationExtensions
/// </summary>
public static class ValidationExtensions
{
    public static ValidationContext Validate(this ContentItem contentItem)
    {
        ValidationContext validationContext = new ValidationContext();

        foreach (var field in contentItem.Fields)
        {
            SchemaField f = contentItem.Schema.Fields[field.Key];

            if (f.Options != null)
            {
                field.Value.Validate(field.Key, f.Options, validationContext);
            }
        }

        return validationContext;
    }
}
