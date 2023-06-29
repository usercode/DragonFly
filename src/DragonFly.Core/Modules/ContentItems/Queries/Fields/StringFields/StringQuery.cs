// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// StringQuery
/// </summary>
public class StringQuery : FieldQuery
{
    /// <summary>
    /// Pattern
    /// </summary>
    public string? Pattern { get; set; }

    /// <summary>
    /// PatternType
    /// </summary>
    public StringQueryType PatternType { get; set; }

    public override bool IsEmpty()
    {
        return string.IsNullOrWhiteSpace(Pattern);
    }
}
