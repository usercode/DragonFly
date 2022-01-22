using ImageWizard.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ImageWizard
{
    public class DragonFlyLoaderOptions : DataLoaderOptions
    {
        public DragonFlyLoaderOptions()
        {
            RefreshMode = DataLoaderRefreshMode.None;
        }
    }
}
