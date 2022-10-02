// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// TextField
/// </summary>
public abstract class TextBaseField : SingleValueField<string>
{
    public TextBaseField()
    {
    }

    public TextBaseField(string text)
    {
        Value = text;
    }

    public override string ToString()
    {
        return $"{Value}";
    }

    protected override void OnValueChanging(ref string? newValue)
    {
        if (string.IsNullOrWhiteSpace(newValue))
        {
            newValue = null;
        }
    }
}
