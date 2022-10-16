// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DragonFly.Content;
using DragonFly.Contents.Content;
namespace DragonFly.Models;

/// <summary>
/// ContentItem
/// </summary>
public class RestContentItem : RestContentBase
{
    public RestContentItem()
    {
        Fields = new RestContentFields();
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
    /// Fields
    /// </summary>
    [JsonPropertyOrder(30)]
    public RestContentFields Fields { get; set; }
}
