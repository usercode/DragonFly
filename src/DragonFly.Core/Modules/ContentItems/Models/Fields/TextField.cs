﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// TextField
/// </summary>
[Field]
[FieldOptions(typeof(TextFieldOptions))]
public partial class TextField : TextBaseField
{
    public TextField()
    {

    }

    public TextField(string? text)
    {
        Value = text;
    }
}
