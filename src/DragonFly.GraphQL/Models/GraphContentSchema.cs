using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.AspNetCore.GraphQL.Models;

public class GraphContentSchema
{
    public GraphContentSchema()
    {
        Fields = new Dictionary<string, GraphContentFieldDefinition>();
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }


    /// <summary>
    /// Parts
    /// </summary>
    public IDictionary<string, GraphContentFieldDefinition> Fields { get; set; }
}
