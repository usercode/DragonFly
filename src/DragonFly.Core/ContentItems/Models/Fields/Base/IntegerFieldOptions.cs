using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Content;

public class IntegerFieldOptions : SingleValueContentFieldOptions<long>
{
    public long? MinValue { get; set; }
    public long? MaxValue { get; set; }

    public override IContentField CreateContentField()
    {
        return new IntegerField(DefaultValue);
    }
}
