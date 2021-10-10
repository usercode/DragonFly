using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;

namespace DragonFly.Content
{
    /// <summary>
    /// IMongoStore
    /// </summary>
    public interface ISchemaStorage
    {
        //Schema
        Task<ContentSchema> GetSchemaAsync(Guid id);

        Task<ContentSchema> GetSchemaAsync(string name);

        Task CreateAsync(ContentSchema contentType);

        Task UpdateAsync(ContentSchema entity);

        //IList<ContentSchema> QueryContentSchemas()
        //{
        //    return QueryContentSchemas(new QueryParameters());
        //}

        Task<QueryResult<ContentSchema>> QuerySchemasAsync();
    }
}
