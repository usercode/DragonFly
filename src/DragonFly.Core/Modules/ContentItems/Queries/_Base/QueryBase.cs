// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public abstract class QueryBase
{
    /// <summary>
    /// Skip
    /// </summary>
    public int Skip { get; set; } = 0;

    /// <summary>
    /// Take
    /// </summary>
    public int Take { get; set; } = 50;

    /// <summary>
    /// Pattern
    /// </summary>
    public string Pattern { get; set; } = string.Empty;
}
