// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// BoolFieldQuery
/// </summary>
public class BoolFieldQuery : FieldQuery
{
    /// <summary>
    /// Value
    /// </summary>
    public bool? Value { get; set; }

    public override bool IsEmpty()
    {
        return Value == null;
    }
}
