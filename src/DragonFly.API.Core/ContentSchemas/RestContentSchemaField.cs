// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Nodes;

namespace DragonFly.API;

public class RestContentSchemaField
{
    /// <summary>
    /// Label
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// SortKey
    /// </summary>
    public int SortKey { get; set; }

    /// <summary>
    /// FieldType
    /// </summary>
    public string FieldType { get; set; }

    /// <summary>
    /// Options
    /// </summary>
    public JsonNode? Options { get; set; }
}
