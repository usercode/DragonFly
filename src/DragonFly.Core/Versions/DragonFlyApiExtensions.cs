// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Reflection;

namespace DragonFly;

public static class DragonFlyApiExtensions
{
    public static string GetVersion(this IDragonFlyApi api)
    {
        return typeof(IDragonFlyApi).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion;
    }
}
