using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Events
{
    public class DragonFlyEvent
    {
        public DateTime? Created { get; set; }

        public string? Type { get; set; }

        public object? Data { get; set; }
    }
}
