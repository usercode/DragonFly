﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.ContentStructures;

namespace DragonFly.Storage;

public interface IDataStorage : IContentStorage, ISchemaStorage, IAssetStorage, IWebHookStorage, IStructureStorage
{
}
