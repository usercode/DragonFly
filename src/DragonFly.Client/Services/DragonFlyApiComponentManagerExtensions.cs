// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;

namespace DragonFly;

/// <summary>
/// DragonFlyApiComponentManagerExtensions
/// </summary>
public static class DragonFlyApiComponentManagerExtensions
{
    public static ComponentManager Component(this IDragonFlyApi api)
    {
        return ComponentManager.Default;
    }
}
