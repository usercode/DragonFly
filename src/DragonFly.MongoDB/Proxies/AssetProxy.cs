// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

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
    private async ValueTask OnMethodCalling(string name)
    {
        if (_loaded == true)
        {
            return;
        }

        _loaded = true;

        Asset? result = await MongoStorage.Default.GetAssetAsync(Id);

        if (result == null)
        {
            throw new Exception($"Asset '{Id}' not found");
        }

        SetInvocationTarget(result);
    }
}
