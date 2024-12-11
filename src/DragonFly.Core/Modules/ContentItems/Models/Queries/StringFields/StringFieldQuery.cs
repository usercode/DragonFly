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
    public StringQueryType PatternType { get; set; } = StringQueryType.Equal;

    public override bool IsEmpty()
    {
        return string.IsNullOrWhiteSpace(Pattern);
    }
}
