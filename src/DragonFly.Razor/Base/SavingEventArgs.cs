using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Base;

public class SavingEventArgs : EventArgs
{
    public SavingEventArgs()
    {
        CanSave = true;
    }

    public bool CanSave { get; set; }
}
