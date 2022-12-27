// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// TextField
/// </summary>
[FieldOptions(typeof(TextFieldOptions))]
public class TextField : TextBaseField
{
    public TextField()
    {

    }

    public TextField(string? text)
    {
        Value = text;
    }
}
