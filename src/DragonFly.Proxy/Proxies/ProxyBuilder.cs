using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using DragonFly.Content;

namespace DragonFly.Proxy;

internal class ProxyBuilder
{
    internal static ProxyGenerator Generator = new ProxyGenerator();
    
    public static object CreateProxy(ContentItem contentItem, Type type)
    {
        return Generator.CreateClassProxy(
                                type,
                                new Type[] { typeof(IContentItemProxy) },
                                new IInterceptor[] {
                                    new ContentItemProxyInterceptor(contentItem),
                                    new ContentItemInterceptor(contentItem)
                                });
    }

    public static T CreateProxy<T>(ContentItem contentItem)
        where T : class
    {
        return (T)CreateProxy(contentItem, typeof(T));
    }
}
