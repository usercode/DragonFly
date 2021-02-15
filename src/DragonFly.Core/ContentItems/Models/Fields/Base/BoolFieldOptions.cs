using DragonFly.Content.ContentParts;
using DragonFly.Data.Content.ContentParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentItems.Models.Fields.Base
{
    public class BoolFieldOptions : SingleValueContentFieldOptions<bool>
    {
        public override ContentField CreateContentField()
        {
            return new BoolField(DefaultValue);
        }
    }
}
