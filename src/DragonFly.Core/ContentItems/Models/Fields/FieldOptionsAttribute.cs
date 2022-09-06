using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// FieldOptionsAttribute
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class FieldOptionsAttribute : Attribute
{
    public FieldOptionsAttribute(Type optionsType)
    {
        OptionsType = optionsType;
    }

    /// <summary>
    /// OptionsType
    /// </summary>
    public Type OptionsType { get; }
}
