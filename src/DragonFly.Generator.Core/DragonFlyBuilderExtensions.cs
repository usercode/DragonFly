// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Builders;
using DragonFly.Generator;

namespace DragonFly.AspNetCore;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds a source generated model.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IDragonFlyBuilder AddContentModel<TContentModel>(this IDragonFlyBuilder builder)
        where TContentModel : IContentModel
    {
        builder.PostInit<ContentModelInitializer<TContentModel>>();

        return builder;
    }
}
