using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content;

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

    public abstract ContentField CreateContentField();
}
