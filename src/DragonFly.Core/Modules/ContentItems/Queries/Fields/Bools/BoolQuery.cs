// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// BoolQuery
/// </summary>
public class BoolQuery : FieldQuery
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
