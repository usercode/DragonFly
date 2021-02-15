using DragonFly.Core.ContentItems.Models.Fields;
using DragonFly.Core.ContentItems.Models.Fields.Base;
using DragonFly.Data.Content.ContentParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content.ContentParts
{
    /// <summary>
    /// BoolField
    /// </summary>
    [FieldOptions(typeof(BoolFieldOptions))]
    public class BoolField : SingleValueContentFieldNullable<bool>
    {
        public BoolField()
        {

        }

        public override bool CanSorting => true;

        public BoolField(bool? value)
        {
            Value = value;
        }

        public override ContentFieldOptions CreateOptions()
        {
            return new BoolFieldOptions();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
