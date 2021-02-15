using Castle.DynamicProxy;
using DragonFly.ContentTypes;
using DragonFly.Core;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Data.Proxies
{
    class ContenItemInterceptor : IInterceptor
    {
        public ContenItemInterceptor(Guid id, ContentSchema schema)
        {
            Id = id;
            Schema = schema;

            _isLoaded = false;
            _firstLoad = false;
        }

        private Guid Id { get; }
        private ContentSchema Schema { get; }

        private bool _isLoaded;
        private bool _firstLoad;

        public void Intercept(IInvocation invocation)
        {
            ContentItem main = (ContentItem)invocation.InvocationTarget;

            if (_firstLoad == false)
            {
                main.Id = Id;
                main.Schema = Schema;

                _firstLoad = true;
            }

            if (_isLoaded == false)
            {
                if (invocation.Method.Name != $"get_{nameof(ContentItem.Id)}"
                && invocation.Method.Name != $"get_{nameof(ContentItem.Schema)}")
                {
                    LoadData(main).Wait();

                    _isLoaded = true;
                }
            }        

            invocation.Proceed();
        }

        private async Task LoadData(ContentItem main)
        {
            var result = await MongoStorage.Default.GetContentItemAsync(Schema.Name, Id);

            main.Id = result.Id;
            main.Schema = result.Schema;
            main.Fields = result.Fields;
            main.CreatedAt = result.CreatedAt;
            main.ModifiedAt = result.ModifiedAt;
        }
    }
}
