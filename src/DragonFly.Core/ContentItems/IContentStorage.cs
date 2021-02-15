using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.AspNetCore.Rest.Exports;
using DragonFly.ContentTypes;
using DragonFly.Core.Queries;
using DragonFly.Models;

namespace DragonFly.Data
{
    /// <summary>
    /// IContentStorage
    /// </summary>
    public interface IContentStorage
    {
        Task<QueryResult<ContentItem>> Query(string schema, QueryParameters queryParameters);
            
        Task<ContentItem> GetContentItemAsync(string schema, Guid id);

        Task CreateAsync(ContentItem contentItem);

        Task UpdateAsync(ContentItem entity);

        Task DeleteAsync(string schema, Guid id);

        Task PublishAsync(string schema, Guid id);

        Task UnpublishAsync(string schema, Guid id);
    }
}
