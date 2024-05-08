// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.MongoDB;

[Proxy]
internal partial class AssetFolderProxy : AssetFolder
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

        IAssetFolderStorage storage = DragonFlyApi.Default.ServiceProvider.GetRequiredService<IAssetFolderStorage>();
        AssetFolder? result = await storage.GetAssetFolderAsync(Id);

        if (result == null)
        {
            throw new Exception($"AssetFolder '{Id}' not found");
        }

        SetInvocationTarget(result);
    }
}
