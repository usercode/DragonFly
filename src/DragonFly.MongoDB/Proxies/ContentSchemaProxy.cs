// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.MongoDB;

[Proxy]
internal partial class ContentSchemaProxy : ContentSchema
{
    [Intercept]
    [IgnoreProperty(nameof(Name))]
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
        ISchemaStorage storage = DragonFlyApi.Default.ServiceProvider.GetRequiredService<ISchemaStorage>();
        ContentSchema? result = await storage.GetSchemaAsync(Name).ConfigureAwait(false);

        if (result == null)
        {
            throw new Exception($"ContentSchema '{Name}' not found");
        }

        SetInvocationTarget(result);
    }
}
