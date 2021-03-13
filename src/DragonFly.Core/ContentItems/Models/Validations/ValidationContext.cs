using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentItems.Models.Validations
{
    public class ValidationContext
    {
        public ValidationContext()
        {
            Errors = new List<ValidationError>();
        }

        /// <summary>
        /// Errors
        /// </summary>
        public IList<ValidationError> Errors { get; }
    }
}
