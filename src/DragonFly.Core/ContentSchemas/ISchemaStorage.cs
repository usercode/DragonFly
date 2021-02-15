using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.AspNetCore.Rest.Exports;
using DragonFly.ContentTypes;
using DragonFly.Models;

namespace DragonFly.Data
{
    /// <summary>
    /// IMongoStore
    /// </summary>
    public interface ISchemaStorage
    {
        //Schema
        Task<ContentSchema> GetContentSchemaByNameAsync(string name);

        Task CreateAsync(ContentSchema contentType);

        Task UpdateAsync(ContentSchema entity);

        //IList<ContentSchema> QueryContentSchemas()
        //{
        //    return QueryContentSchemas(new QueryParameters());
        //}

        QueryResult<ContentSchema> QueryContentSchemas();
    }
}
