// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// SlugQuery
/// </summary>
public class SlugQuery : FieldQuery
{
    /// <summary>
    /// Value
    /// </summary>
    public string? Value { get; set; }

    public override bool IsEmpty()
    {
        return string.IsNullOrWhiteSpace(Value);
    }
}
