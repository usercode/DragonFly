﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.Content;
using DragonFly.Core.ContentItems.Models.Validations;
using DragonFly.Data;

namespace DragonFly.Content
{
    /// <summary>
    /// ContentEmbed
    /// </summary>
    public class ContentEmbedded : IContentItemWithSchema
    {
        public ContentEmbedded(ContentSchema schema)
        {
            _schema = schema;
            _fields = new ContentFields();
        }

        private ContentSchema _schema;

        /// <summary>
        /// Type
        /// </summary>
        public virtual ContentSchema Schema { get => _schema; set => _schema = value; }

        private ContentFields _fields;

        /// <summary>
        /// Fields
        /// </summary>
        public virtual ContentFields Fields { get => _fields; set => _fields = value; }
      
    }
}
