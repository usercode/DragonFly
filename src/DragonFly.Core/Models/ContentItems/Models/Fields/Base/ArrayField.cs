﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;

namespace DragonFly;

/// <summary>
/// ArrayField
/// </summary>
[FieldOptions(typeof(ArrayFieldOptions))]
public class ArrayField : ContentField
{
    public ArrayField()
    {
        Items = new List<ArrayFieldItem>();
    }

    /// <summary>
    /// Items
    /// </summary>
    public IList<ArrayFieldItem> Items { get; set; }

    public override void Clear()
    {
        Items.Clear();
    }

    public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
    {
        if (options is ArrayFieldOptions fieldOptions)
        {
            if (fieldOptions.IsRequired && Items.Any() == false)
            {
                context.AddRequireValidation(fieldName);
            }

            if (Items.Count < fieldOptions.MinItems || Items.Count > fieldOptions.MaxItems)
            {
                context.AddRangeValidation(fieldName, fieldOptions.MinItems, fieldOptions.MaxItems);
            }
        }
    }
}
