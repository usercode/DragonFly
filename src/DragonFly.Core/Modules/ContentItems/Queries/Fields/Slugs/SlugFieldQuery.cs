// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// SlugFieldQuery
/// </summary>
public class SlugFieldQuery : FieldQuery
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
