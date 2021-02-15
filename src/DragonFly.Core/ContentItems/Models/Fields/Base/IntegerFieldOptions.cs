using DragonFly.Content.ContentParts;
using DragonFly.Data.Content.ContentParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentItems.Models.Fields.Base
{
    public class IntegerFieldOptions : SingleValueContentFieldOptions<long>
    {
        public long? MinValue { get; set; }
        public long? MaxValue { get; set; }

        public override ContentField CreateContentField()
        {
            return new IntegerField(DefaultValue);
        }
    }
}
