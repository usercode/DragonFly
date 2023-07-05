// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public class AssetFolderQuery : QueryBase
{
    public AssetFolderQuery()
    {
    }

    public bool RootOnly { get; set; }

    public Guid? Parent { get; set; }
}
