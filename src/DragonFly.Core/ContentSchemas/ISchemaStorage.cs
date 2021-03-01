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
        Task<ContentSchema> GetContentSchemaAsync(Guid id);

        Task<ContentSchema> GetContentSchemaAsync(string name);

        Task CreateAsync(ContentSchema contentType);

        Task UpdateAsync(ContentSchema entity);

        //IList<ContentSchema> QueryContentSchemas()
        //{
        //    return QueryContentSchemas(new QueryParameters());
        //}

        QueryResult<ContentSchema> QueryContentSchemas();
    }
}
