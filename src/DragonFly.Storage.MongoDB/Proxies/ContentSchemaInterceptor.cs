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

class ContentSchemaInterceptor : IInterceptor
{
    public ContentSchemaInterceptor(string schema)
    {
        Schema = schema;

        _isLoaded = false;
        _firstLoad = false;
    }

    private string Schema { get; }

    private bool _isLoaded;
    private bool _firstLoad;

    public void Intercept(IInvocation invocation)
    {
        ContentSchema main = (ContentSchema)invocation.InvocationTarget;

        if (_firstLoad == false)
        {
            //main.Name = Schema;

            _firstLoad = true;
        }

        if (invocation.Method.Name != $"get_{nameof(ContentSchema.Name)}")
        {
            if (_isLoaded == false)
            {
                LoadData(main).Wait();

                _isLoaded = true;
            }
        }

        invocation.Proceed();
    }

    private async Task LoadData(ContentSchema main)
    {
        var result = await MongoStorage.Default.GetSchemaAsync(Schema);

        main.Id = result.Id;
        main.Name = result.Name;
        main.CreatedAt = result.CreatedAt;
        main.ModifiedAt = result.ModifiedAt;
        main.Version = result.Version;
        main.Fields = result.Fields;
        main.ListFields = result.ListFields;
        main.ReferenceFields = result.ReferenceFields;
    }
}
