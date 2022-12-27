// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;
using Microsoft.Extensions.Options;

namespace DragonFly.Proxy;

internal class ContentSchemaPostInitialize : IPostInitialize
{
    public ContentSchemaPostInitialize(IContentSchemaBuilder builder, IOptions<ContentSchemaBuilderOptions> options)
    {
        Builder = builder;
        Options = options.Value;
    }

    public IContentSchemaBuilder Builder { get; }

    public ContentSchemaBuilderOptions Options { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        foreach (Type type in Options.Types)
        {
            await Builder.AddAsync(type);
        }
    }
}
