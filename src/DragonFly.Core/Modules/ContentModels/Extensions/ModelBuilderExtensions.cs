// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Builders;

namespace DragonFly.AspNetCore;

/// <summary>
/// ModelBuilderExtensions
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Adds source generated models. 
    /// <br /><br />
    /// At startup existing schemas will be overriden.
    /// </summary>
    public static TBuilder AddModels<TBuilder>(this TBuilder builder, Action<ModelBuilderItem> action)
        where TBuilder : IDragonFlyBuilder
    {
        ModelBuilderItem item = new ModelBuilderItem(builder);

        action(item);

        return builder;
    }
}
