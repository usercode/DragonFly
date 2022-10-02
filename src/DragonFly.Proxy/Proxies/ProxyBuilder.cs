// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Castle.DynamicProxy;

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
