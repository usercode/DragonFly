// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Proxy;

namespace DragonFly;

public static class ProxyExtensions
{
    private static IContentModel ToModelInternal(this ContentItem contentItem, Type? type = null)
    {
        Type schemaType = ProxyTypeManager.Default.GetTypeBySchema(contentItem.Schema);

        if (type != null && schemaType != type)
        {
            throw new Exception($"The content item is not compatible with model type {type.Name}!");
        }

        return (IContentModel)ProxyBuilder.CreateProxy(contentItem, schemaType);
    }

    /// <summary>
    /// Creates a proxy model of type <typeparamref name="TContentModel"/> for the content item.
    /// </summary>
    /// <typeparam name="TContentModel"></typeparam>
    /// <param name="contentItem"></param>
    /// <returns></returns>
    public static TContentModel ToModel<TContentModel>(this ContentItem contentItem)
        where TContentModel : class, IContentModel, new()
    {
        return (TContentModel)ToModelInternal(contentItem, typeof(TContentModel));
    }

    /// <summary>
    /// Creates a proxy model of suitable type for the content item.
    /// </summary>
    /// <param name="contentItem"></param>
    /// <returns></returns>
    public static IContentModel ToModel(this ContentItem contentItem)
    {
        return ToModelInternal(contentItem, null);
    }

    /// <summary>
    /// Creates a proxy model of <paramref name="type"/> for the content item.
    /// </summary>
    /// <typeparam name="TContentModel"></typeparam>
    /// <param name="contentItem"></param>
    /// <returns></returns>
    public static IContentModel ToModel(this ContentItem contentItem, Type type)
    {
        return ToModelInternal(contentItem, type);
    }

    /// <summary>
    /// Gets the content item of the proxy model.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static ContentItem GetContentItem(this IContentModel model)
    {
        if (model is IContentProxy proxy)
        {
            return proxy.ContentItem;
        }

        throw new Exception("No valid ContentItem.");
    }
}
