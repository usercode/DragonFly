// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DragonFly.Contents.Content;

namespace DragonFly.Models;

/// <summary>
/// RestContentComponent
/// </summary>
public class RestContentComponent
{
    public RestContentComponent()
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
    /// Fields
    /// </summary>
    [JsonPropertyOrder(30)]
    public JsonObject Fields { get; set; }
}
