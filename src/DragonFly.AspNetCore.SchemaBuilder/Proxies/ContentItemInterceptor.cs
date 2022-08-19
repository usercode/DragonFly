using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using DragonFly.Content;

namespace DragonFly.AspNetCore.SchemaBuilder.Proxies;

internal class ContentItemInterceptor : IInterceptor
{
    public ContentItemInterceptor(ContentItem contentItem)
    {
        ContentItem = contentItem;
    }

    private ContentItem ContentItem { get; }

    public void Intercept(IInvocation invocation)
    {
        if (invocation.Method.Name.StartsWith("get_"))
        {
            string propertyName = invocation.Method.Name.Substring(4);

            if (ContentItem.TryGetField(propertyName, out IContentField? field))
            {
                invocation.ReturnValue = field;

                return;
            }
        }
        else if (invocation.Method.Name.StartsWith("set_"))
        {
            string propertyName = invocation.Method.Name.Substring(4);

            if (ContentItem.TrySetField(propertyName, (IContentField)invocation.Arguments[0]))
            {
                return;
            }
        }

        invocation.Proceed();
    }
}
