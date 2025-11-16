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
    extension(IDragonFlyApi api)
    {
        public ComponentManager Components => ComponentManager.Default;
    }
}
