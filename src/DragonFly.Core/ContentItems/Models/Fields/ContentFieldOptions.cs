using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// ContentFieldOptions
/// </summary>
public abstract class ContentFieldOptions
{
    public string Type => GetType().Name;

    /// <summary>
    /// IsRequired
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// IsSearchable
    /// </summary>
    public bool IsSearchable { get; set; }

    public abstract IContentField CreateContentField();
}
