﻿using DragonFly.Content.ContentParts;
using DragonFly.Data.Content.ContentParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentItems.Models.Fields.Base
{
    public class XmlFieldOptions : ContentFieldOptions
    {
        /// <summary>
        /// IsRequired
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// DefaultValue
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// MinLength
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// MaxLength
        /// </summary>
        public int MaxLength { get; set; }

        public override ContentField CreateContentField()
        {
            return new XmlField(DefaultValue);
        }
    }
}
