// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Collections.Generic;
using System.Reflection;

namespace DragonFly.Client;

/// <summary>
/// RazorRoutingManager
/// </summary>
public sealed class RazorRoutingManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static RazorRoutingManager Default { get; } = new RazorRoutingManager();

    /// <summary>
    /// Items
    /// </summary>
    public IList<Assembly> Items { get; } = new List<Assembly>();
}
