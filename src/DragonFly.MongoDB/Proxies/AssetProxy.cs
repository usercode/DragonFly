// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.MongoDB;

[Proxy]
internal partial class AssetProxy : Asset
{
    private bool _loaded = false;

    [Intercept]
    [IgnoreProperty(nameof(Id))]
    [IgnoreMethod(nameof(IsNew))]
    [IgnoreMethod(nameof(Equals))]
    [IgnoreMethod(nameof(GetHashCode))]
    [IgnoreMethod(nameof(ToString))]
    private async ValueTask OnMethodCalling(string name)
    {
        if (_loaded == true)
        {
            return;
        }

        _loaded = true;

        IAssetStorage storage = DragonFlyApi.Default.ServiceProvider.GetRequiredService<IAssetStorage>();
        Asset? result = await storage.GetAssetAsync(Id);

        if (result == null)
        {
            throw new Exception($"Asset '{Id}' not found");
        }

        SetInvocationTarget(result);
    }
}
