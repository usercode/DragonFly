// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Assets.Query;

public class AssetFolderQuery
{
    public AssetFolderQuery()
    {
        Pattern = string.Empty;
        Take = 50;
    }

    public int Skip { get; set; }

    public int Take { get; set; }

    public string Pattern { get; set; }

    public bool RootOnly { get; set; }

    public Guid? Parent { get; set; }
}
