using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Rest.Exports
{
    public class QueryResult<T>
    {
        public QueryResult()
        {
            Items = new List<T>();
        }
        
        public IList<T> Items { get; set; }
        public long Offset { get; set; }
        public long Count { get; set; }

        public long TotalCount { get; set; }
    }
}
