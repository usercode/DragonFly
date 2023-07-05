﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class AssetQuery : QueryBase
{
    public AssetQuery()
    {
        Pattern = string.Empty;
    }

    public Guid? Folder { get; set; }
}
