// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Builders;

namespace DragonFly.AspNetCore;

public static class DragonFlyModelBuilderExtensions
{
    /// <summary>
    /// Adds a source generated model for <typeparamref name="TContentModel"/>. 
    /// <br /><br />
    /// At startup an existing schema for <typeparamref name="TContentModel"/> will be overriden.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IDragonFlyBuilder AddModel<TContentModel>(this IDragonFlyBuilder builder)
        where TContentModel : IContentModel
    {
        builder.PostInit<ContentModelInitializer<TContentModel>>();

        return builder;
    }
}
