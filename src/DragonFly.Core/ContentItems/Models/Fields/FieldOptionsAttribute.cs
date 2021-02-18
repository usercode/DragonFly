using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content
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
