using DragonFly.Content;
using DragonFly.Core.Assets;
using DragonFly.Core.ContentStructures;
using DragonFly.Core.WebHooks;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Storage;

public interface IDataStorage : IContentStorage, ISchemaStorage, IAssetStorage, IWebHookStorage, IStructureStorage
{
}
