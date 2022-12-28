﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Validations;

namespace DragonFly;

/// <summary>
/// XmlField
/// </summary>
[FieldOptions(typeof(XmlFieldOptions))]
public class XmlField : TextBaseField
{
    public XmlField()
    {

    }

    public XmlField(string? text)
    {
        Value = text;
    }

    public override void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
    {
        base.Validate(fieldName, options, context);
    }
}