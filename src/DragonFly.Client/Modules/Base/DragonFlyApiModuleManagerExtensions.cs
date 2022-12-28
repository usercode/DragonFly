// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;

namespace DragonFly;

public static class DragonFlyApiModuleManagerExtensions
{
    public static ModuleManager Module(this IDragonFlyApi api)
    {
        return ModuleManager.Default;
    }
}
