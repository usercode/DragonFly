using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField
{
    /// <summary>
    /// Block
    /// </summary>
    public abstract class Block
    {
        public virtual string Type => GetType().Name;
    }
}
