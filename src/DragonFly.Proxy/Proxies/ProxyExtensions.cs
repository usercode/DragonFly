// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Proxy;

namespace DragonFly;

public static class ProxyExtensions
{
    public static object ToModel(this ContentItem contentItem, Type type)
    {
        return ProxyBuilder.CreateProxy(contentItem, type);
    }

    public static TContentModel ToModel<TContentModel>(this ContentItem contentItem)
        where TContentModel : IContentModel
    {
        return (TContentModel)ToModel(contentItem, typeof(TContentModel));
    }

    public static object ToModel(this ContentItem contentItem)
    {
        Type type = ProxyTypeManager.Default.GetTypeBySchema(contentItem.Schema);

        return ToModel(contentItem, type);
    }

    public static ContentItem ToContentItem<TContentModel>(this TContentModel obj)
        where TContentModel : IContentModel
    {
        if (obj is IContentItemProxy proxy)
        {
            return proxy.ContentItem;
        }

        throw new Exception("No valid ContentItem.");
    }
}
