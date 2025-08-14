// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IntegerFieldQuery
/// </summary>
public class IntegerFieldQuery : FieldQuery
{
    /// <summary>
    /// MinValue
    /// </summary>
    public long? Value { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    public NumberQueryType Type { get; set; } = NumberQueryType.Equal;

    public override bool IsEmpty()
    {
        return Value == null;
    }
}
