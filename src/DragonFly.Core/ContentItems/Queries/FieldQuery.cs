using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content.Queries
{
    public class FieldQuery
    {
        public FieldQuery()
        {
            Name = string.Empty;
            Value = string.Empty;
        }

        public FieldQuery(string name, string value, QueryFieldType valueType)
        {
            Name = name;
            Value = value;
            ValueType = valueType;
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public QueryFieldType ValueType { get; set; }
    }
}
