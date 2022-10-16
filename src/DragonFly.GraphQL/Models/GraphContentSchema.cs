// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
