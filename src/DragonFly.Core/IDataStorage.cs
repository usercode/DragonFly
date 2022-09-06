using DragonFly.Core.ContentStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Storage;

public interface IDataStorage : IContentStorage, ISchemaStorage, IAssetStorage, IWebHookStorage, IStructureStorage
{
}
