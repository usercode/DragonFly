using DragonFly.Content.ContentParts;
using DragonFly.Data.Content.ContentParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentItems.Models.Fields.Base
{
    public class FloatFieldOptions : SingleValueContentFieldOptions<double>
    {
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }

        public override ContentField CreateContentField()
        {
            return new FloatField(DefaultValue);
        }
    }
}
