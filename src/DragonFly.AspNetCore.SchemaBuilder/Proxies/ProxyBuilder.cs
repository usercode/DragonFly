using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.Proxies;

internal class ProxyBuilder
{
    internal static ProxyGenerator Generator = new ProxyGenerator();

    public static T CreateProxy<T>(ContentItem contentItem)
        where T : class
    {
        return (T)Generator.CreateClassProxy(
                                typeof(T),
                                new Type[] { typeof(IContentItemProxy) },
                                new IInterceptor[] { 
                                    new ContentItemProxyInterceptor(contentItem), 
                                    new ContentItemInterceptor(contentItem) 
                                });
    }
}
