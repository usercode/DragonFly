using DragonFly.Core.ContentItems.Models.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore
{
    public class DragonFlyApi : IDragonFlyApi
    {
        public DragonFlyApi(ContentFieldManager fieldManager)
        {
            Fields = fieldManager;
        }

        public ContentFieldManager Fields { get; private set; }

        public void Init()
        {
            
        }


    }
}
