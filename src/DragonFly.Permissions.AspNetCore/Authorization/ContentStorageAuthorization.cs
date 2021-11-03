using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Content.Queries;
using DragonFly.ContentItems;
using DragonFly.Core.ContentItems;
using DragonFly.Permissions.AspNetCore;
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
            IPermissionService permissionService)
        {
            PermissionService = permissionService;
            Storage = storage;
        }

        /// <summary>
        /// Storage
        /// </summary>
        public IContentStorage Storage { get; }

        /// <summary>
        /// Authorization
        /// </summary>
        public IPermissionService PermissionService { get; }

        public async Task CreateAsync(ContentItem contentItem)
        {
            await PermissionService.AuthorizeAsync(ContentItemPermissions.ContentCreate);

            await Storage.CreateAsync(contentItem);
        }

        public async Task DeleteAsync(string schema, Guid id)
        {
            await PermissionService.AuthorizeAsync(ContentItemPermissions.ContentDelete);

            await Storage.DeleteAsync(schema, id);
        }

        public async Task<ContentItem> GetContentAsync(string schema, Guid id)
        {
            await PermissionService.AuthorizeAsync(ContentItemPermissions.ContentRead);

            return await Storage.GetContentAsync(schema, id);
        }

        public async Task PublishAsync(string schema, Guid id)
        {
            await PermissionService.AuthorizeAsync(ContentItemPermissions.ContentPublish);

            await Storage.PublishAsync(schema, id);
        }

        public async Task<QueryResult<ContentItem>> QueryAsync(ContentItemQuery query)
        {
            await PermissionService.AuthorizeAsync(ContentItemPermissions.ContentQuery);

            return await Storage.QueryAsync(query);
        }

        public async Task UnpublishAsync(string schema, Guid id)
        {
            await PermissionService.AuthorizeAsync(ContentItemPermissions.ContentUnpublish);

            await Storage.UnpublishAsync(schema, id);
        }

        public async Task UpdateAsync(ContentItem entity)
        {
            await PermissionService.AuthorizeAsync(ContentItemPermissions.ContentUpdate);

            await Storage.UpdateAsync(entity);
        }
    }
}
