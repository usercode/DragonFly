using DragonFly.Contents.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Models.ContentStructures
{
    /// <summary>
    /// MongoContentStructure
    /// </summary>
    public class MongoContentStructure : MongoContentBase
    {
        public MongoContentStructure()
        {
            Name = string.Empty;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }
}
