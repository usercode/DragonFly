using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentStructures.Queries
{
    public class NodesQuery
    {
        public Guid? Structure { get; set; }

        public Guid? ParentId { get; set; }
    }
}
