// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.MongoDB;

[Proxy]
internal partial class AssetFolderProxy : AssetFolder
{
    [Intercept]
    [IgnoreProperty(nameof(Id))]
    [IgnoreMethod(nameof(IsNew))]
    [IgnoreMethod(nameof(Equals))]
    [IgnoreMethod(nameof(GetHashCode))]
    [IgnoreMethod(nameof(ToString))]
    private async Task OnMethodCalling(string name)
    {
        await LoadAsync().ConfigureAwait(false);
    }

    public async Task LoadAsync()
    {
        IAssetFolderStorage storage = DragonFlyApi.Default.ServiceProvider.GetRequiredService<IAssetFolderStorage>();
        AssetFolder? result = await storage.GetAssetFolderAsync(Id).ConfigureAwait(false);

        if (result == null)
        {
            throw new Exception($"AssetFolder '{Id}' not found");
        }

        SetInvocationTarget(result);
    }
}
