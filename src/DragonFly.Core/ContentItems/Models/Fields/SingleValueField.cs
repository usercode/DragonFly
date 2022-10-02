// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// SingleValueField
/// </summary>
public abstract class SingleValueField<T> : ContentField, ISingleValueField       
{
    public SingleValueField()
    {

    }

    private T? _value;

    /// <summary>
    /// Value
    /// </summary>
    public virtual T? Value 
    {
        get => _value;
        set
        {
            OnValueChanging(ref value);

            _value = value;
        }
    }

    [MemberNotNullWhen(returnValue: true, member: nameof(Value))]
    public bool HasValue => Value != null;

    object? ISingleValueField.Value
    {
        get => _value;
        set => _value = (T?)value;
    }

    public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
    {
        if (options.IsRequired && HasValue == false)
        {
            context.AddRequireValidation(fieldName);
        }
    }

    protected virtual void OnValueChanging(ref T? newValue)
    {
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}
