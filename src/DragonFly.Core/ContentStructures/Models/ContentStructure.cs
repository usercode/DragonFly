using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

/// <summary>
/// ContentStructure
/// </summary>
public class ContentStructure : ContentBase
{
    public ContentStructure()
        : this(string.Empty)
    {

    }

    public ContentStructure(string name)
    {
        Name = name;
    }


    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }
}
