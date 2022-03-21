using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content.Queries;
using DragonFly.Contents.Content;
using DragonFly.Data.Content.ContentTypes;
using MongoDB.Bson;

namespace DragonFly.ContentTypes;

/// <summary>
/// MongoContentSchema
/// </summary>
public class MongoContentSchema : MongoContentBase
{
    public MongoContentSchema()
    {
        Name = string.Empty;
        Fields = new Dictionary<string, MongoSchemaField>();
        ListFields = new List<string>();
        ReferenceFields = new List<string>();
        QueryFields = new List<string>();
        OrderFields = new List<FieldOrder>();            
    }

    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; set; }
    
    /// <summary>
    /// Parts
    /// </summary>
    public virtual IDictionary<string, MongoSchemaField> Fields { get; set; }

    /// <summary>
    /// ListFields
    /// </summary>
    public virtual IList<string> ListFields { get; set; }

    /// <summary>
    /// ReferenceFields
    /// </summary>
    public virtual IList<string> ReferenceFields { get; set; }

    /// <summary>
    /// QueryFields
    /// </summary>
    public virtual IList<string> QueryFields { get; set; }

    /// <summary>
    /// OrderFields
    /// </summary>
    public virtual IList<FieldOrder> OrderFields { get; set; }
}
