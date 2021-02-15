using DragonFly.Content.ContentParts;
using DragonFly.Data.Content.ContentParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Core.Content.ContentTypes
{
    public interface IContentFieldDefinition
    {
        /// <summary>
        /// SortKey
        /// </summary>
        public int SortKey { get; set; }


    }
}
