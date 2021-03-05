using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder
{
    public static class ContentSchemaBuilderExtensions
    {
        public static async Task CreateAsync<TContentType>(this IContentStorage storage, TContentType entity)
            where TContentType : class
        {
           
        }

        public static async Task UpdateAsync<TContentType>(this IContentStorage storage, TContentType entity)
            where TContentType : class
        {

        }

        public static async Task DeleteAsync<TContentType>(this IContentStorage storage, Guid id)
            where TContentType : class
        {

        }

        public static async Task PublishAsync<TContentType>(this IContentStorage storage, Guid id)
            where TContentType : class
        {

        }

        public static async Task UnpublishAsync<TContentType>(this IContentStorage storage, Guid id)
            where TContentType : class
        {

        }
    }
}
