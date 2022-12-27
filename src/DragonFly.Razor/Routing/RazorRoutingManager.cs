// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Collections.Generic;
using System.Reflection;

namespace DragonFly.Razor;

/// <summary>
/// RazorRoutingManager
/// </summary>
public sealed class RazorRoutingManager
{
    public static RazorRoutingManager Default { get; } = new RazorRoutingManager();

    private RazorRoutingManager()
    {
        Items = new List<Assembly>();
    }

    public IList<Assembly> Items { get; }
}
