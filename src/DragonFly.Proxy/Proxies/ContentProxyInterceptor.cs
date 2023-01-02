// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Castle.DynamicProxy;

namespace DragonFly.Proxy;

internal class ContentProxyInterceptor : IInterceptor
{
    public ContentProxyInterceptor(ContentItem contentItem)
    {
        ContentItem = contentItem;
    }

    private ContentItem ContentItem { get; }

    public void Intercept(IInvocation invocation)
    {
        if (invocation.Method.Name == $"get_{nameof(ContentItem.Id)}")
        {
            invocation.ReturnValue = ContentItem.Id;
        }
        else if (invocation.Method.Name == $"get_{nameof(IContentProxy.ContentItem)}")
        {
            invocation.ReturnValue = ContentItem;
        }
        else
        {
            invocation.Proceed();
        }
    }
}
