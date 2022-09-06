using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// IMongoStore
/// </summary>
public interface ISchemaStorage
{
    //Schema
    Task<ContentSchema?> GetSchemaAsync(Guid id);

    Task<ContentSchema?> GetSchemaAsync(string name);

    Task CreateAsync(ContentSchema contentType);

    Task UpdateAsync(ContentSchema entity);

    //IList<ContentSchema> QueryContentSchemas()
    //{
    //    return QueryContentSchemas(new QueryParameters());
    //}

    Task<QueryResult<ContentSchema>> QuerySchemasAsync();
}
