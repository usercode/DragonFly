using DragonFly.Core.ContentItems.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Query
{
    /// <summary>
    /// QueryAction
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    public class QueryAction<TQuery> : IQueryAction
        where TQuery : FieldQueryBase
    {
        public virtual void Apply(TQuery query, QueryActionContext context)
        {

        }

        void IQueryAction.Apply(FieldQueryBase query, QueryActionContext context)
        {
            Apply((TQuery)query, context);
        }
    }
}
