// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using DragonFly.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Proxy;

public static class ContentSchemaBuilderExtensions
{
    public static async Task<T> GetContentAsync<T>(this IContentStorage storage, string schema, Guid id)
        where T : class
    {
        ContentItem content = await storage.GetContentAsync(schema, id);

        return ProxyBuilder.CreateProxy<T>(content);
    }

    public static async Task CreateAsync<TContentType>(this IContentStorage storage, TContentType entity)
        where TContentType : class
    {
        await storage.CreateAsync(entity.ToContentItem());
    }

    public static async Task UpdateAsync<TContentType>(this IContentStorage storage, TContentType entity)
        where TContentType : class
    {
        await storage.UpdateAsync(entity.ToContentItem());
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

    public static async Task<QueryResult<TContentType>> QueryAsync<TContentType>(this IContentStorage storage, ContentItemQuery query)
        where TContentType : class
    {
        query.Schema = ProxyTypeManager.Default.Get<TContentType>().Name;

        QueryResult<ContentItem> result = await storage.QueryAsync(query);

        QueryResult<TContentType> result2 = new QueryResult<TContentType>();
        result2.Offset = result.Offset;
        result2.TotalCount = result.TotalCount;
        result2.Count = result.Count;
        result2.Items = result.Items.Select(x => x.ToModel<TContentType>()).ToList();

        return result2;
    }
}
