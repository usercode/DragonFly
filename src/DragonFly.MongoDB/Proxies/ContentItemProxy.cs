// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.MongoDB;

[Proxy]
internal partial class ContentItemProxy : ContentItem
{
    [Intercept]
    [IgnoreProperty(nameof(Id))]
    [IgnoreProperty(nameof(Schema))]
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
        IContentStorage storage = DragonFlyApi.Default.ServiceProvider.GetRequiredService<IContentStorage>();
        ContentItem? result = await storage.GetContentAsync(Schema.Name, Id).ConfigureAwait(false);

        if (result == null)
        {
            throw new Exception($"ContentItem '{Schema.Name}/{Id}' not found");
        }

        SetInvocationTarget(result);
    }
}
