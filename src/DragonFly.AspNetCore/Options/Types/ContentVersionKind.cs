// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore;

public enum ContentVersionKind
{
    /// <summary>
    /// Do not create version.
    /// </summary>
    None = 0,
    /// <summary>
    /// Create version for drafted and published content.
    /// </summary>
    All = 1,
    /// <summary>
    /// Create only version for published content.
    /// </summary>
    PublishedOnly = 2
}
