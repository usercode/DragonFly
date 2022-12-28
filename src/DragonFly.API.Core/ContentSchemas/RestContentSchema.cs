// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly.API;

public class RestContentSchema : RestContentBase
{
    public RestContentSchema()
    {
        Name = string.Empty;
        Fields = new Dictionary<string, RestContentSchemaField>();
        ListFields = new List<string>();
        ReferenceFields = new List<string>();
        QueryFields = new List<string>();
        OrderFields = new List<FieldOrder>();
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }


    /// <summary>
    /// Parts
    /// </summary>
    public IDictionary<string, RestContentSchemaField> Fields { get; set; }

    /// <summary>
    /// ListFields
    /// </summary>
    public IList<string> ListFields { get; set; }

    /// <summary>
    /// ReferenceFields
    /// </summary>
    public IList<string> ReferenceFields { get; set; }

    /// <summary>
    /// QueryFields
    /// </summary>
    public IList<string> QueryFields { get; set; }

    /// <summary>
    /// OrderFields
    /// </summary>
    public IList<FieldOrder> OrderFields { get; set; }
}
