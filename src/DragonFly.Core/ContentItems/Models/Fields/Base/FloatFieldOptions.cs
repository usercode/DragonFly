using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content;

public class FloatFieldOptions : SingleValueFieldOptions<double>
{
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }

    public override IContentField CreateContentField()
    {
        return new FloatField(DefaultValue);
    }
}
