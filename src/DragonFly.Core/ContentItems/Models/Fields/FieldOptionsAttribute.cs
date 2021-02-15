using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentItems.Models.Fields
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FieldOptionsAttribute : Attribute
    {
        public FieldOptionsAttribute(Type optionsType)
        {
            OptionsType = optionsType;
        }

        public Type OptionsType { get; }
    }
}
