// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class StringFieldOptions : FieldOptions
{
    public const int DefaultMaxLength = 2048;

    public StringFieldOptions()
    {
        MinLength = 0;
        MaxLength = DefaultMaxLength;
    }

    /// <summary>
    /// DefaultValue
    /// </summary>
    public string? DefaultValue { get; set; }

    /// <summary>
    /// MinLength
    /// </summary>
    public int? MinLength { get; set; }

    /// <summary>
    /// MaxLength
    /// </summary>
    public int? MaxLength { get; set; }

    public override ContentField CreateContentField()
    {
        return new StringField(DefaultValue);
    }
}
