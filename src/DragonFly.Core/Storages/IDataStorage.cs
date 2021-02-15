using DragonFly.Core.Assets;
using DragonFly.Core.WebHooks;
using DragonFly.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Core
{
    public interface IDataStorage : IContentStorage, ISchemaStorage, IAssetStorage, IWebHookStorage
    {
        
    }
}
