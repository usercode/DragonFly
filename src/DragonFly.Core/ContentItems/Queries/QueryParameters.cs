﻿using DragonFly.Core.ContentItems.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragonFly.Core.Queries
{
    /// <summary>
    /// QueryParameters
    /// </summary>
    public class QueryParameters
    {
        public QueryParameters()
        {
            Fields = new List<FieldQuery>();
            OrderFields = new List<FieldOrder>();

            Skip = 0;
            Top = 50;
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
        /// OrderFields
        /// </summary>
        public IList<FieldOrder> OrderFields { get; set; }

        public IQueryable<T> Apply<T>(IQueryable<T> q)
        {
            return q.Skip(Skip).Take(Top);
        }
    }
}
