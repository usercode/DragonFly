// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor;

/// <summary>
/// RazorRoutingManager
/// </summary>
public class RazorRoutingManager
{
    private static RazorRoutingManager? _default;

    public static RazorRoutingManager Default
    {
        get
        {
            if (_default == null)
            {
                _default = new RazorRoutingManager();
            }

            return _default;
        }
    }

    private RazorRoutingManager()
    {
        Items = new List<Assembly>();
    }

    public IList<Assembly> Items { get; }
}
