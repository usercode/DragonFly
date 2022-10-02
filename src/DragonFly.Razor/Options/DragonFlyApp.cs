// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Razor.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor;

public class DragonFlyApp
{
    private static DragonFlyApp _default;

    public static DragonFlyApp Default
    {
        get
        {
            if (_default == null)
            {
                _default = new DragonFlyApp();
            }

            return _default;
        }
    }

    public DragonFlyApp()
    {
    }

    public Uri ApiBaseUrl { get; set; }

    public Uri ClientBaseUrl { get; set; }
}
