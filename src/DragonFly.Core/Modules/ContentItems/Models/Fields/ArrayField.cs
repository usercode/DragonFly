﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ArrayField
/// </summary>
[Field]
[FieldOptions(typeof(ArrayFieldOptions))]
public partial class ArrayField : IReferencedContent
{
    public ArrayField()
    {
    }

    /// <summary>
    /// Items
    /// </summary>
    public IList<ArrayFieldItem> Items { get; set; }= [];

    public override void Clear()
    {
        Items.Clear();
    }

    public ContentReference[] GetReferences()
    {
        return Items
            .SelectMany(x => x.Fields
                                    .Select(i => i.Value)
                                    .OfType<IReferencedContent>()
                                    .SelectMany(f => f.GetReferences()))
            .ToArray();
    }

    public override void Validate(string fieldName, FieldOptions options, ValidationContext context)
    {
        base.Validate(fieldName, options, context);

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
