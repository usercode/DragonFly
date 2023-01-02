// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Castle.DynamicProxy;

namespace DragonFly.Proxy;

static class ProxyBuilder
{
    private static ProxyGenerator Generator = new ProxyGenerator();
    
    public static object CreateProxy(ContentItem contentItem, Type type)
    {
        return Generator.CreateClassProxy(
                                type,
                                new Type[] { typeof(IContentProxy) },
                                new IInterceptor[] {
                                    new ContentProxyInterceptor(contentItem),
                                    new ContentInterceptor(contentItem)
                                });
    }
}
