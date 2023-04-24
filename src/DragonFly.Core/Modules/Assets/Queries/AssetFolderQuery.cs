// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core;

namespace DragonFly.Assets.Query;

public class AssetFolderQuery : QueryBase
{
    public AssetFolderQuery()
    {
    }

    public bool RootOnly { get; set; }

    public Guid? Parent { get; set; }
}
