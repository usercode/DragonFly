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
    /// IntegerField
    /// </summary>
    [FieldOptions(typeof(IntegerFieldOptions))]
    public class IntegerField : SingleValueContentFieldNullable<long>
    {
        public IntegerField()
        {

        }

        public override bool CanSorting => true;

        public IntegerField(long? number)
        {
            Value = number;
        }

        public override ContentFieldOptions CreateOptions()
        {
            return new IntegerFieldOptions();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
