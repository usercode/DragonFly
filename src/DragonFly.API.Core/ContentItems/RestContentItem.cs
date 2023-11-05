// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DragonFly.Validations;

namespace DragonFly.API;

/// <summary>
/// ContentItem
/// </summary>
public class RestContentItem : RestContentBase
{
    public RestContentItem()
    {
        Fields = new JsonObject();
    }

    /// <summary>
    /// Schema
    /// </summary>
    [JsonPropertyOrder(3)]
    public JsonNode? Schema { get; set; }

    /// <summary>
    /// SchemaVersion
    /// </summary>
    [JsonPropertyOrder(2)]
    public int SchemaVersion { get; set; }

    /// <summary>
    /// ValidationContext
    /// </summary>
    [JsonPropertyOrder(4)]
    public ValidationContext? ValidationContext { get; set; }

    /// <summary>
    /// Fields
    /// </summary>
    [JsonPropertyOrder(30)]
    //[JsonConverter(typeof(MyConverter))]
    public JsonObject Fields { get; set; }

}
