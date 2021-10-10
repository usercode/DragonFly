using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Content.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder
{
    public static class ContentSchemaBuilderExtensions
    {
        public static T ToModel<T>(this ContentItem contentItem)
        {
            T instance = Activator.CreateInstance<T>();



            return instance;
        }

        public static ContentItem GetContentItem(this object obj)
        {
            return null;
        }

        public static async Task CreateAsync<TContentType>(this IContentStorage storage, TContentType entity)
            where TContentType : class
        {
            await storage.CreateAsync(entity.GetContentItem());
        }

        public static async Task UpdateAsync<TContentType>(this IContentStorage storage, TContentType entity)
            where TContentType : class
        {
            await storage.UpdateAsync(entity.GetContentItem());
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

        public static async Task<QueryResult<TContentType>> QueryResult<TContentType>(this IContentStorage storage, ContentItemQuery query)
        {
            QueryResult<ContentItem> result = await storage.QueryAsync(query);

            QueryResult<TContentType> result2 = new QueryResult<TContentType>();
            result2.Offset = result.Offset;
            result2.TotalCount = result.TotalCount;
            result2.Count = result.Count;
            result2.Items = result.Items.Select(x => x.ToModel<TContentType>()).ToList();

            return result2;
        }
    }
}
