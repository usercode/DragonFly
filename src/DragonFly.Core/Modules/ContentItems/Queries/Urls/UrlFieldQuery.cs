// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// UrlFieldQuery
/// </summary>
public class UrlFieldQuery : FieldQuery
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
