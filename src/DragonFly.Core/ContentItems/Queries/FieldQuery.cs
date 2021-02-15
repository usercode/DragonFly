﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Core.ContentItems.Queries
{
    public class FieldQuery
    {
        public FieldQuery()
        {

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
