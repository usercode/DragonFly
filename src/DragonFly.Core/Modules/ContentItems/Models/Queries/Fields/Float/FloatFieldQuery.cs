// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FloatFieldQuery
/// </summary>
public class FloatFieldQuery : FieldQuery
{
    /// <summary>
    /// MinValue
    /// </summary>
    public double? Value { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    public NumberQueryType Type { get; set; } = NumberQueryType.Equal;

    public override bool IsEmpty()
    {
        return Value == null;
    }
}
