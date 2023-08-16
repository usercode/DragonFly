// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API;

public class RestContentSchema : RestContentBase
{
    public RestContentSchema()
    {
        Name = string.Empty;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }


    /// <summary>
    /// Parts
    /// </summary>
    public IDictionary<string, RestContentSchemaField> Fields { get; set; } = new Dictionary<string, RestContentSchemaField>();

    /// <summary>
    /// ListFields
    /// </summary>
    public IList<string> ListFields { get; set; } = new List<string>();

    /// <summary>
    /// ReferenceFields
    /// </summary>
    public IList<string> ReferenceFields { get; set; } = new List<string>();

    /// <summary>
    /// QueryFields
    /// </summary>
    public IList<string> QueryFields { get; set; } = new List<string>();

    /// <summary>
    /// OrderFields
    /// </summary>
    public IList<FieldOrder> OrderFields { get; set; } = new List<FieldOrder>();

    /// <summary>
    /// Previews
    /// </summary>
    public PreviewItem Preview { get; set; } = new PreviewItem();
}
