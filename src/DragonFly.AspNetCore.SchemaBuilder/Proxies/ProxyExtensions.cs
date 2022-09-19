using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.AspNetCore.SchemaBuilder.Proxies;
using DragonFly.AspNetCore.SchemaBuilder.SchemaStates;

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
        Type type = SchemaTypeManager.Default.GetTypeBySchema(contentItem.Schema);

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
