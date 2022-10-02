// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.ContentStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Storage;

public interface IDataStorage : IContentStorage, ISchemaStorage, IAssetStorage, IWebHookStorage, IStructureStorage
{
}
