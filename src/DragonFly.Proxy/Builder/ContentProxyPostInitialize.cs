// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;
using Microsoft.Extensions.Options;

namespace DragonFly.Proxy;

internal class ContentProxyPostInitialize : IPostInitialize
{
    public ContentProxyPostInitialize(ContentProxyBuilder builder, IOptions<ContentProxyBuilderOptions> options)
    {
        Builder = builder;
        Options = options.Value;
    }

    public ContentProxyBuilder Builder { get; }

    public ContentProxyBuilderOptions Options { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        foreach (Type type in Options.Types)
        {
            await Builder.AddAsync(type);
        }
    }
}
