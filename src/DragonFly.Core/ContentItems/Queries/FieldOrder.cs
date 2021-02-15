using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Core.ContentItems.Queries
{
    public class FieldOrder
    {
        public FieldOrder()
        {
            Asc = true;
        }

        public FieldOrder(string name, bool asc)
        {
            Name = name;
            Asc = asc;
        }

        public string Name { get; set; }

        public bool Asc { get; set; }
    }
}
