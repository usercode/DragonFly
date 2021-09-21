using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// FieldQueryAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FieldQueryAttribute : Attribute
    {
        public FieldQueryAttribute(Type queryType)
        {
            QueryType = queryType;
        }

        /// <summary>
        /// QueryType
        /// </summary>
        public Type QueryType { get; }
    }
}
