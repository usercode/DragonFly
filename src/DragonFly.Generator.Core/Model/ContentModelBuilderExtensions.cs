// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly;

public static class ContentModelBuilderExtensions
{
    public static async Task<TContentModel?> GetContentAsync<TContentModel>(this IContentStorage storage, string schema, Guid id)
        where TContentModel : class, IContentModel
    {
        ContentItem? content = await storage.GetContentAsync(schema, id);

        if (content == null)
        {
            return null;
        }

        return (TContentModel)TContentModel.Metadata.Create(content);
    }

    public static async Task CreateAsync<TContentModel>(this IContentStorage storage, TContentModel entity)
        where TContentModel : class, IContentModel
    {
        await storage.CreateAsync(entity.GetContentItem());
    }

    public static async Task UpdateAsync<TContentModel>(this IContentStorage storage, TContentModel entity)
        where TContentModel : class, IContentModel
    {
        await storage.UpdateAsync(entity.GetContentItem());
    }

    public static async Task DeleteAsync<TContentModel>(this IContentStorage storage, TContentModel model)
        where TContentModel : class, IContentModel
    {        
        await storage.DeleteAsync(model.GetContentItem());
    }

    public static async Task PublishAsync<TContentModel>(this IContentStorage storage, TContentModel model)
        where TContentModel : class, IContentModel
    {
        await storage.PublishAsync(model.GetContentItem());
    }

    public static async Task UnpublishAsync<TContentModel>(this IContentStorage storage, TContentModel model)
        where TContentModel : class, IContentModel
    {
        await storage.UnpublishAsync(model.GetContentItem());
    }

    public static async Task<TContentModel?> FirstOrDefaultAsync<TContentModel>(this IContentStorage storage, Action<ContentQuery<TContentModel>>? action = null)
        where TContentModel : class, IContentModel
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

        return (TContentModel)TContentModel.Metadata.Create(result.Items[0]);
    }

    public static async Task<QueryResult<TContentModel>> QueryAsync<TContentModel>(this IContentStorage storage, Action<ContentQuery<TContentModel>>? action = null)
        where TContentModel : class, IContentModel
    {
        ContentQuery<TContentModel> query = new ContentQuery<TContentModel>();

        action?.Invoke(query);

        QueryResult<ContentItem> result = await storage.QueryAsync(query);

        return result.Convert(x => (TContentModel)TContentModel.Metadata.Create(x));
    }
}
