using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;

namespace DragonFly.AspNetCore.API.Models
{
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
}
