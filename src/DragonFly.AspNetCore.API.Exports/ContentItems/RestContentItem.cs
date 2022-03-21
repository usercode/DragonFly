using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DragonFly.AspNetCore.API.Models;
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
