using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content;

public class XmlFieldOptions : ContentFieldOptions
{
    public XmlFieldOptions()
    {
        DefaultValue = string.Empty;
    }

    /// <summary>
    /// DefaultValue
    /// </summary>
    public string DefaultValue { get; set; }

    /// <summary>
    /// MinLength
    /// </summary>
    public int MinLength { get; set; }

    /// <summary>
    /// MaxLength
    /// </summary>
    public int MaxLength { get; set; }

    public override IContentField CreateContentField()
    {
        return new XmlField(DefaultValue);
    }
}
