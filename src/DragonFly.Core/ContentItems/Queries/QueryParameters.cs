using DragonFly.Core.ContentItems.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragonFly.Content.Queries
{
    /// <summary>
    /// QueryParameters
    /// </summary>
    public class QueryParameters
    {
        public QueryParameters()
        {
            Fields = new List<FieldQuery>();
            Fields2 = new List<FieldQueryBase>();
            OrderFields = new List<FieldOrder>();

            SearchPattern = string.Empty;
            Skip = 0;
            Top = 20;

            IncludeListFieldsOnly = false;
        }

        /// <summary>
        /// Skip
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Top
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// SearchPattern
        /// </summary>
        public string SearchPattern { get; set; }

        /// <summary>
        /// Fields
        /// </summary>
        public IList<FieldQuery> Fields { get; set; }

        /// <summary>
        /// Fields2
        /// </summary>
        public IList<FieldQueryBase> Fields2 { get; set; }

        /// <summary>
        /// OrderFields
        /// </summary>
        public IList<FieldOrder> OrderFields { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IncludeListFieldsOnly { get; set; }

        public IQueryable<T> Apply<T>(IQueryable<T> q)
        {
            return q.Skip(Skip).Take(Top);
        }
    }
}
