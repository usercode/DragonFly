using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using DragonFly.Core.ContentItems.Models.Validations;

namespace DragonFly.Content
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

        public virtual void Validate(string fieldName, ContentFieldOptions options, ValidationContext context)
        {
        }
    }
}
