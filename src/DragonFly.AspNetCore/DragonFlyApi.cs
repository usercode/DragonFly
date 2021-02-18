using DragonFly.Content;
using DragonFly.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore
{
    public class DragonFlyApi : IDragonFlyApi
    {
        public DragonFlyApi(ContentFieldManager fieldManager, IDataStorage dataStorage)
        {
            Fields = fieldManager;
            DataStorage = dataStorage;
        }

        public ContentFieldManager Fields { get; }

        public IDataStorage DataStorage { get; }

        public void Init()
        {
            
        }


    }
}
