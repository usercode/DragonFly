// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Proxy;

namespace DragonFly;

public static class SchemaTypeManagerExtensions
{
    public static ProxyTypeManager ProxyTypes(this IDragonFlyApi api)
    {
        return ProxyTypeManager.Default;
    }
}
