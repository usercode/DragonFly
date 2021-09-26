using DragonFly.Core.ContentItems.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// IntegerField
    /// </summary>
    [FieldOptions(typeof(IntegerFieldOptions))]
    [FieldQuery(typeof(IntegerFieldQuery))]
    public class IntegerField : SingleValueContentField<long?>
    {
        public IntegerField()
        {

        }

        public override bool CanSorting => true;

        public IntegerField(long? number)
        {
            Value = number;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
