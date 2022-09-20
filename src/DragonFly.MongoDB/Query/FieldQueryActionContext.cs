using DragonFly.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.MongoDB.Query;

/// <summary>
/// FieldQueryActionContext
/// </summary>
public class FieldQueryActionContext
{
    public FieldQueryActionContext()
    {
        Filters = new List<FilterDefinition<MongoContentItem>>();
    }

    /// <summary>
    /// Filter
    /// </summary>
    public IList<FilterDefinition<MongoContentItem>> Filters { get; }
}
