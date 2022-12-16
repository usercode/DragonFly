// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Proxy;

namespace DragonFly;

public static class ProxyTypeManagerExtensions
{
    public static ProxyTypeManager Proxy(this IDragonFlyApi api)
    {
        return ProxyTypeManager.Default;
    }
}
