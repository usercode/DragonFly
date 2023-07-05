// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

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
    private async ValueTask OnMethodCalling(string name)
    {
        if (_loaded == true)
        {
            return;
        }

        _loaded = true;

        AssetFolder? result = await MongoStorage.Default.GetAssetFolderAsync(Id);

        if (result == null)
        {
            throw new Exception($"AssetFolder '{Id}' not found");
        }

        SetInvocationTarget(result);
    }
}
