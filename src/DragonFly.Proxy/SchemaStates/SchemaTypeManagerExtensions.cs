// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Proxy;

namespace DragonFly;

public static class SchemaTypeManagerExtensions
{
    public static ProxyTypeManager ProxyTypes(this IDragonFlyApi api)
    {
        return ProxyTypeManager.Default;
    }
}
