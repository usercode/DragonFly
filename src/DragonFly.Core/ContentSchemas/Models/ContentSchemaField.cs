﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content;

namespace DragonFly.Content
{
    /// <summary>
    /// ContentSchemaField
    /// </summary>
    public class ContentSchemaField : IContentFieldDefinition
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
        public ContentFieldOptions Options { get; set; }
    }
}