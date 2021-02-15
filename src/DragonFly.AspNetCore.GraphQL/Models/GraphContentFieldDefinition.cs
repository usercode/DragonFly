using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.AspNetCore.GraphQL.Models
{
    public class GraphContentFieldDefinition
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


    }
}
