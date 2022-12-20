﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Proxy.Query;
using DragonFly.Query;
using DragonFly.Storage;

namespace DragonFly.Proxy;

public static class ContentSchemaBuilderExtensions
{
    public static async Task<T?> GetContentAsync<T>(this IContentStorage storage, string schema, Guid id)
        where T : class, IContentModel, new()
    {
        ContentItem? content = await storage.GetContentAsync(schema, id);

        if (content == null)
        {
            return null;
        }

        return ProxyBuilder.CreateProxy<T>(content);
    }

    public static async Task CreateAsync<TContentModel>(this IContentStorage storage, TContentModel entity)
        where TContentModel : class, IContentModel
    {
        await storage.CreateAsync(entity.ToContentItem());
    }

    public static async Task UpdateAsync<TContentModel>(this IContentStorage storage, TContentModel entity)
        where TContentModel : class, IContentModel
    {
        await storage.UpdateAsync(entity.ToContentItem());
    }

    public static async Task DeleteAsync<TContentModel>(this IContentStorage storage, TContentModel model)
        where TContentModel : class, IContentModel
    {
        ContentSchema schema = ProxyTypeManager.Default.GetSchema<TContentModel>();

        await storage.DeleteAsync(schema.Name, model.Id);
    }

    public static async Task PublishAsync<TContentModel>(this IContentStorage storage, TContentModel model)
        where TContentModel : class, IContentModel
    {
        ContentSchema schema = ProxyTypeManager.Default.GetSchema<TContentModel>();

        await storage.PublishAsync(schema.Name, model.Id);
    }

    public static async Task UnpublishAsync<TContentModel>(this IContentStorage storage, TContentModel model)
        where TContentModel : class, IContentModel
    {
        ContentSchema schema = ProxyTypeManager.Default.GetSchema<TContentModel>();

        await storage.UnpublishAsync(schema.Name, model.Id);
    }

    public static async Task<TContentModel?> FirstOrDefaultAsync<TContentModel>(this IContentStorage storage, Action<IContentQuery<TContentModel>>? action = null)
        where TContentModel : class, IContentModel, new()
    {
        ContentQuery<TContentModel> query = new ContentQuery<TContentModel>();

        action?.Invoke(query);

        query.Skip = 0;
        query.Top = 1;

        QueryResult<ContentItem> result = await storage.QueryAsync(query);

        if (result.Items.Count == 0)
        {
            return null;
        }

        return result.Items[0].ToModel<TContentModel>();
    }

    public static async Task<QueryResult<TContentModel>> QueryAsync<TContentModel>(this IContentStorage storage, Action<IContentQuery<TContentModel>>? action = null)
        where TContentModel : class, IContentModel, new()
    {
        ContentQuery<TContentModel> query = new ContentQuery<TContentModel>();

        action?.Invoke(query);

        QueryResult<ContentItem> result = await storage.QueryAsync(query);

        QueryResult<TContentModel> result2 = new QueryResult<TContentModel>();
        result2.Offset = result.Offset;
        result2.TotalCount = result.TotalCount;
        result2.Count = result.Count;
        result2.Items = result.Items.Select(x => x.ToModel<TContentModel>()).ToList();

        return result2;
    }
}
