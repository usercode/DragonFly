// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public static class DragonFlyApiExtensions
{
    public static string GetVersion(this IDragonFlyApi api)
    {
        return Assembly.GetExecutingAssembly()
                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion;
    }
}
