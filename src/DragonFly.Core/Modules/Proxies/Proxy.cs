// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly.Proxies;

public static class Proxy
{
    public static async Task<bool> LoadAsync(object? obj)
    {
        if (obj is IProxyObject proxyObject)
        {
            await proxyObject.LoadAsync();

            return true;
        }

        return false;
    }
}
