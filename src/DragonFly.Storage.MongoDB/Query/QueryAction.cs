using DragonFly.Content;
using DragonFly.Models;
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
        where TQuery : FieldQuery
    {
        public virtual void Apply(TQuery query, QueryActionContext context)
        {

        }

        void IQueryAction.Apply(FieldQuery query, QueryActionContext context)
        {
            Apply((TQuery)query, context);
        }

        protected string CreateFullFieldName(string? name)
        {
            return $"{nameof(MongoContentItem.Fields)}.{name}";
        }

        protected string CreateFullReferenceFieldName(string? name)
        {
            return $"{nameof(MongoContentItem.Fields)}.{name}.Id";
        }
    }
}
