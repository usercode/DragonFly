using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Content
{
    public abstract class RestContentBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public DateTime? PublishedAt { get; set; }

        public int Version { get; set; }
    }
}
