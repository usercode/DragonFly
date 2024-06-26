﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Generator;
using Microsoft.Extensions.DependencyInjection;

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
    [IgnoreMethod(nameof(ToString))]
    private async ValueTask OnMethodCalling(string name)
    {
        if (_loaded == true)
        {
            return;
        }

        _loaded = true;

        ISchemaStorage storage = DragonFlyApi.Default.ServiceProvider.GetRequiredService<ISchemaStorage>();
        ContentSchema? result = await storage.GetSchemaAsync(Name);

        if (result == null)
        {
            throw new Exception($"ContentSchema '{Name}' not found");
        }

        SetInvocationTarget(result);
    }
}
