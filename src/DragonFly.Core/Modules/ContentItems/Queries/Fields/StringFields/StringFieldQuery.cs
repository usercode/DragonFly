// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// StringFieldQuery
/// </summary>
public class StringFieldQuery : FieldQuery
{
    /// <summary>
    /// Pattern
    /// </summary>
    public string? Pattern { get; set; }

    /// <summary>
    /// PatternType
    /// </summary>
    public StringFieldQueryType PatternType { get; set; }

    public override bool IsEmpty()
    {
        return string.IsNullOrWhiteSpace(Pattern);
    }
}
