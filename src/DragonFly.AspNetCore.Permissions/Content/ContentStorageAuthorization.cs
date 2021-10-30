using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Content.Queries;
using DragonFly.Core.ContentItems;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Content
{
    /// <summary>
    /// ContentStorageAuthorization
    /// </summary>
    public class ContentStorageAuthorization : IContentStorage
    {
        public ContentStorageAuthorization(
            IContentStorage storage, 
            IDragonFlyApi api)
        {
            Api = api;
            Storage = storage;
        }

        /// <summary>
        /// Storage
        /// </summary>
        public IContentStorage Storage { get; }

        /// <summary>
        /// Authorization
        /// </summary>
        public IDragonFlyApi Api { get; }

        public async Task CreateAsync(ContentItem contentItem)
        {
            await Api.AuthorizeAsync(ContentItemPermissions.ContentCreate);

            await Storage.CreateAsync(contentItem);
        }

        public async Task DeleteAsync(string schema, Guid id)
        {
            await Api.AuthorizeAsync(ContentItemPermissions.ContentDelete);

            await Storage.DeleteAsync(schema, id);
        }

        public async Task<ContentItem> GetContentAsync(string schema, Guid id)
        {
            await Api.AuthorizeAsync(ContentItemPermissions.ContentRead);

            return await Storage.GetContentAsync(schema, id);
        }

        public async Task PublishAsync(string schema, Guid id)
        {
            await Api.AuthorizeAsync(ContentItemPermissions.ContentPublish);

            await Storage.PublishAsync(schema, id);
        }

        public async Task<QueryResult<ContentItem>> QueryAsync(ContentItemQuery query)
        {
            await Api.AuthorizeAsync(ContentItemPermissions.ContentRead);

            return await Storage.QueryAsync(query);
        }

        public async Task UnpublishAsync(string schema, Guid id)
        {
            await Api.AuthorizeAsync(ContentItemPermissions.ContentUnpublish);

            await Storage.UnpublishAsync(schema, id);
        }

        public async Task UpdateAsync(ContentItem entity)
        {
            await Api.AuthorizeAsync(ContentItemPermissions.ContentUpdate);

            await Storage.UpdateAsync(entity);
        }
    }
}
