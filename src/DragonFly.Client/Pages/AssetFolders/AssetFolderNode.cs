// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Client.Pages.AssetFolders;

public class AssetFolderNode
{
    public AssetFolderNode(AssetFolder folder)
    {
        Folder = folder;
    }

    public AssetFolder Folder { get; set; }

    public bool IsExpanded { get; set; }
}
