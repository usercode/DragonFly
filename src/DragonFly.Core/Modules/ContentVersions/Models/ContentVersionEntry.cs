// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentVersionEntry
/// </summary>
public class ContentVersionEntry
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Version
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Modified
    /// </summary>
    public DateTime? Modified { get; set; }

    /// <summary>
    /// PublishedAt
    /// </summary>
    public DateTime? PublishedAt { get; set;}
}
