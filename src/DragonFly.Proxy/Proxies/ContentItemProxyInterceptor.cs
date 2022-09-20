using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using DragonFly.Content;

namespace DragonFly.Proxy;

internal class ContentItemProxyInterceptor : IInterceptor
{
    public ContentItemProxyInterceptor(ContentItem contentItem)
    {
        ContentItem = contentItem;
    }

    private ContentItem ContentItem { get; }

    public void Intercept(IInvocation invocation)
    {
        if (invocation.Method.Name == $"get_{nameof(ContentItem.Id)}")
        {
            invocation.ReturnValue = ContentItem.Id;

            return;
        }

        if (invocation.Method.Name == $"get_{nameof(IContentItemProxy.ContentItem)}")
        {
            invocation.ReturnValue = ContentItem;
        }
        else
        {
            invocation.Proceed();
        }
    }
}
