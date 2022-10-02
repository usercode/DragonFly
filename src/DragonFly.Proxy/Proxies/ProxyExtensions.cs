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

    public static T ToModel<T>(this ContentItem contentItem)
        where T : class
    {
        return (T)ToModel(contentItem, typeof(T));
    }

    public static object ToModel(this ContentItem contentItem)
    {
        Type type = ProxyTypeManager.Default.GetTypeBySchema(contentItem.Schema);

        return ToModel(contentItem, type);
    }

    public static ContentItem ToContentItem<T>(this T obj)
        where T : class
    {
        if (obj is IContentItemProxy proxy)
        {
            return proxy.ContentItem;
        }

        throw new Exception("No valid ContentItem.");
    }
}
