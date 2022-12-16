// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Proxy.Query;
using DragonFly.Query;
using DragonFly.Storage;

namespace DragonFly.Proxy;

public static class ContentSchemaBuilderExtensions
{
    public static async Task<T?> GetContentAsync<T>(this IContentStorage storage, string schema, Guid id)
        where T : class
    {
        ContentItem? content = await storage.GetContentAsync(schema, id);

        if (content == null)
        {
            return null;
        }

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
        ContentSchema schema = ProxyTypeManager.Default.GetSchema<TContentType>();

        await storage.DeleteAsync(schema.Name, id);
    }

    public static async Task PublishAsync<TContentType>(this IContentStorage storage, Guid id)
        where TContentType : class
    {
        ContentSchema schema = ProxyTypeManager.Default.GetSchema<TContentType>();

        await storage.PublishAsync(schema.Name, id);
    }

    public static async Task UnpublishAsync<TContentType>(this IContentStorage storage, Guid id)
        where TContentType : class
    {
        ContentSchema schema = ProxyTypeManager.Default.GetSchema<TContentType>();

        await storage.UnpublishAsync(schema.Name, id);
    }

    public static async Task<TModel?> FirstOrDefaultAsync<TModel>(this IContentStorage storage, Action<IContentQuery<TModel>>? action = null)
        where TModel : class
    {
        ContentQuery<TModel> query = new ContentQuery<TModel>();

        action?.Invoke(query);

        query.Top = 1;

        QueryResult<ContentItem> result = await storage.QueryAsync(query);

        if (result.Items.Count == 0)
        {
            return null;
        }

        return result.Items[0].ToModel<TModel>();
    }

    public static async Task<QueryResult<TModel>> QueryAsync<TModel>(this IContentStorage storage, Action<IContentQuery<TModel>>? action = null)
        where TModel : class
    {
        ContentQuery<TModel> query = new ContentQuery<TModel>();

        action?.Invoke(query);

        QueryResult<ContentItem> result = await storage.QueryAsync(query);

        QueryResult<TModel> result2 = new QueryResult<TModel>();
        result2.Offset = result.Offset;
        result2.TotalCount = result.TotalCount;
        result2.Count = result.Count;
        result2.Items = result.Items.Select(x => x.ToModel<TModel>()).ToList();

        return result2;
    }
}
