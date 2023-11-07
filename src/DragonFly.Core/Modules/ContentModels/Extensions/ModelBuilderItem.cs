// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Model;

namespace DragonFly.Builders;

/// <summary>
/// ModelBuilderItem
/// </summary>
public class ModelBuilderItem
{
    public ModelBuilderItem(IDragonFlyBuilder builder)
    {
        Builder = builder;
    }

    /// <summary>
    /// Builder
    /// </summary>
    private IDragonFlyBuilder Builder { get; }

    /// <summary>
    /// Adds <typeparamref name="TModel"/> model for content items.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public ModelBuilderItem Add<TModel>()
         where TModel : IContentModel
    {
        Builder.Init<ContentModelInitializer<TModel>>();

        return this;
    }
}
