// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly;

public static class ContentStorageExtensions
{
    /// <summary>
    /// Gets <typeparamref name="TContentModel"/> by id.
    /// </summary>
    /// <typeparam name="TContentModel"></typeparam>
    /// <param name="storage"></param>
    /// <param name="schema"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<TContentModel?> GetContentAsync<TContentModel>(this IContentStorage storage, string schema, Guid id)
        where TContentModel : class, IContentModel
    {
        var content = await storage.GetContentAsync(schema, id);

        if (content == null)
        {
            return null;
        }

        return (TContentModel)TContentModel.Create(content);
    }

    /// <summary>
    /// Creates a new <typeparamref name="TContentModel"/>.
    /// </summary>
    /// <typeparam name="TContentModel"></typeparam>
    /// <param name="storage"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static async Task CreateAsync<TContentModel>(this IContentStorage storage, TContentModel entity)
        where TContentModel : class, IContentModel
    {
        await storage.CreateAsync(entity.GetContentItem());
    }

    /// <summary>
    /// Updates an existing <typeparamref name="TContentModel"/>.
    /// </summary>
    /// <typeparam name="TContentModel"></typeparam>
    /// <param name="storage"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static async Task UpdateAsync<TContentModel>(this IContentStorage storage, TContentModel entity)
        where TContentModel : class, IContentModel
    {
        await storage.UpdateAsync(entity.GetContentItem());
    }

    /// <summary>
    /// Deletes an existing <typeparamref name="TContentModel"/>,
    /// </summary>
    /// <typeparam name="TContentModel"></typeparam>
    /// <param name="storage"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    public static async Task DeleteAsync<TContentModel>(this IContentStorage storage, TContentModel model)
        where TContentModel : class, IContentModel
    {
        await storage.DeleteAsync(model.GetContentItem());
    }

    /// <summary>
    /// Publishes an existing <typeparamref name="TContentModel"/>.
    /// </summary>
    /// <typeparam name="TContentModel"></typeparam>
    /// <param name="storage"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    public static async Task PublishAsync<TContentModel>(this IContentStorage storage, TContentModel model)
        where TContentModel : class, IContentModel
    {
        await storage.PublishAsync(model.GetContentItem());
    }

    /// <summary>
    /// Unpublishes an existing <typeparamref name="TContentModel"/>.
    /// </summary>
    /// <typeparam name="TContentModel"></typeparam>
    /// <param name="storage"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    public static async Task UnpublishAsync<TContentModel>(this IContentStorage storage, TContentModel model)
        where TContentModel : class, IContentModel
    {
        await storage.UnpublishAsync(model.GetContentItem());
    }

    /// <summary>
    /// Gets the first <typeparamref name="TContentModel"/>.
    /// </summary>
    /// <typeparam name="TContentModel"></typeparam>
    /// <param name="storage"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static async Task<TContentModel?> FirstOrDefaultAsync<TContentModel>(this IContentStorage storage, Action<ContentQuery<TContentModel>>? action = null)
        where TContentModel : class, IContentModel
    {
        var query = new ContentQuery<TContentModel>();

        action?.Invoke(query);

        query.Skip = 0;
        query.Top = 1;

        var result = await storage.QueryAsync(query);

        if (result.Items.Count == 0)
        {
            return null;
        }

        return (TContentModel)TContentModel.Create(result.Items[0]);
    }

    /// <summary>
    /// Starts a query for <typeparamref name="TContentModel"/>.
    /// </summary>
    public static async Task<QueryResult<TContentModel>> QueryAsync<TContentModel>(this IContentStorage storage, Action<ContentQuery<TContentModel>>? action = null)
        where TContentModel : class, IContentModel
    {
        var query = new ContentQuery<TContentModel>();

        action?.Invoke(query);

        var result = await storage.QueryAsync(query);

        return result.Convert(x => (TContentModel)TContentModel.Create(x));
    }
}
