using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.AspNetCore.SchemaBuilder.Proxies;
using DragonFly.Content;

namespace DragonFly;

public static class ProxyExtensions
{
    public static T ToModel<T>(this ContentItem contentItem) where T : class
    {
        return ProxyBuilder.CreateProxy<T>(contentItem);
    }

    public static ContentItem ToContentItem<T>(this T item)
    {
        throw new Exception();
    }
}
