using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using DragonFly.Core.ContentItems.Models.Validations;
using DragonFly.Data.Content.ContentParts;

namespace DragonFly.Content.ContentParts
{
    /// <summary>
    /// ContentField
    /// </summary>
    public abstract class ContentField
    {
        public ContentField()
        {

        }

        public virtual bool CanSorting => false;

        public virtual ContentFieldOptions CreateOptions()
        {
            return null;
        }

        public virtual IList<ValidationError> Validate()
        {
            return new List<ValidationError>();
        }
    }
}
