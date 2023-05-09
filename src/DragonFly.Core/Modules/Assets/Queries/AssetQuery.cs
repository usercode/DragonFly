﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core;

namespace DragonFly.Assets.Query;

public class AssetQuery : QueryBase
{
    public AssetQuery()
    {
        Pattern = string.Empty;
    }

    public Guid? Folder { get; set; }
}