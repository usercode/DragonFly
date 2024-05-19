// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;
using Microsoft.Extensions.DependencyInjection;

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
    [IgnoreMethod(nameof(ToString))]
    private async ValueTask OnMethodCalling(string name)
    {
        if (_loaded == true)
        {
            return;
        }

        _loaded = true;

        IContentStorage storage = DragonFlyApi.Default.ServiceProvider.GetRequiredService<IContentStorage>();
        ContentItem? result = await storage.GetContentAsync(new ContentId(Schema.Name, Id));

        if (result == null)
        {
            throw new Exception($"ContentItem '{Schema.Name}/{Id}' not found");
        }

        SetInvocationTarget(result);
    }
}
