using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Content
{
    /// <summary>
    /// MongoContentBase
    /// </summary>
    public abstract class MongoContentBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// CreatedAt
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// ModifiedAt
        /// </summary>
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// PublishedAt
        /// </summary>
        public DateTime? PublishedAt { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public int Version { get; set; }

        
    }
}
