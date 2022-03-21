using Castle.DynamicProxy;
using DragonFly.Content;
using DragonFly.ContentTypes;
using DragonFly.Core;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Data.Proxies;

class ContenItemInterceptor : IInterceptor
{
    public ContenItemInterceptor()
    {
        _isLoaded = false;
    }

    private bool _isLoaded;

    private ContentItem? ContentItem;

    public void Intercept(IInvocation invocation)
    {
        ContentItem = (ContentItem)invocation.InvocationTarget;

        if (_isLoaded == false)
        {
            if (invocation.Method.Name != $"get_{nameof(ContentItem.Id)}"
            && invocation.Method.Name != $"get_{nameof(ContentItem.Schema)}")
            {
                LoadDataAsync(ContentItem).GetAwaiter().GetResult();

                _isLoaded = true;
            }
        }

        invocation.Proceed();
    }

    private async Task LoadDataAsync(ContentItem main)
    {
        ContentItem result = await MongoStorage.Default.GetContentAsync(main.Schema.Name, main.Id);

        main.Id = result.Id;
        main.Schema = result.Schema;
        main.Fields = result.Fields;
        main.CreatedAt = result.CreatedAt;
        main.ModifiedAt = result.ModifiedAt;
        main.PublishedAt = result.PublishedAt;
    }
}
