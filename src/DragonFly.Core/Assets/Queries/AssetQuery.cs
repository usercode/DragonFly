// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Assets.Query;

public class AssetQuery
{
    public AssetQuery()
    {
        Pattern = string.Empty;
        Take = 50;
    }

    public int Skip { get; set; }

    public int Take { get; set; }

    public string Pattern { get; set; }

    public Guid? Folder { get; set; }
}
