// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly.MongoDB;

[Proxy]
internal partial class ContentSchemaProxy : ContentSchema
{
    private bool _loaded = false;

    [Intercept]
    [IgnoreProperty(nameof(Name))]
    [IgnoreMethod(nameof(IsNew))]
    [IgnoreMethod(nameof(Equals))]
    [IgnoreMethod(nameof(GetHashCode))]
    private async Task OnMethodCalling(string name)
    {
        if (_loaded == true)
        {
            return;
        }

        _loaded = true;

        ContentSchema? result = await MongoStorage.Default.GetSchemaAsync(Name);

        if (result == null)
        {
            throw new Exception($"ContentSchema '{Name}' not found");
        }

        SetInvocationTarget(result);
    }
}
