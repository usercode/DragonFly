using DragonFly.Content;
using DragonFly.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public class DragonFlyApi : IDragonFlyApi
    {
        private static DragonFlyApi? _default;

        public static DragonFlyApi Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new DragonFlyApi();
                }

                return _default;
            }
        }

        public DragonFlyApi()
        {
        }
        

        public void Init()
        {
            
        }


    }
}
