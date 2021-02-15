using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Content
{
    public abstract class ContentBase
    {
        protected Guid _id;

        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get => _id; set => _id = value; }

        public virtual DateTime? CreatedAt { get; set; }

        public virtual DateTime? ModifiedAt { get; set; }

        public virtual DateTime? PublishedAt { get; set; }

        public virtual int Version { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
