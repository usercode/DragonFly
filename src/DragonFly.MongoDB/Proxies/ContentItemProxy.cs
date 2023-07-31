// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;

namespace DragonFly.MongoDB;

[Proxy]
internal partial class ContentItemProxy : ContentItem
{
    private bool _loaded = false;

    [Intercept]
    [IgnoreProperty(nameof(Id))]
    [IgnoreProperty(nameof(Schema))]
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

        ContentItem? result = await MongoStorage.Default.GetContentAsync(Schema.Name, Id);

        if (result == null)
        {
            throw new Exception($"ContentItem '{Schema.Name}/{Id}' not found");
        }

        SetInvocationTarget(result);
    }
}
