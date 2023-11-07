// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;

namespace DragonFly.API;

public static class RestAssetConverter
{
    public static Asset ToModel(this RestAsset restAsset)
    {
        Asset asset = new Asset();
        asset.Id = restAsset.Id;
        asset.CreatedAt = restAsset.CreatedAt;
        asset.ModifiedAt = restAsset.ModifiedAt;
        asset.PublishedAt = restAsset.PublishedAt;
        asset.Version = restAsset.Version;
        asset.Name = restAsset.Name;
        asset.Hash = restAsset.Hash;
        asset.MimeType = restAsset.MimeType;
        asset.Size = restAsset.Size;
        asset.PreviewUrl = restAsset.PreviewUrl;
        asset.Slug = restAsset.Slug;
        asset.Alt = restAsset.Alt;
        asset.Description = restAsset.Description;

        if (restAsset.Folder != null)
        {
            asset.Folder = restAsset.Folder.ToModel();
        }

        foreach (var item in restAsset.Metaddata)
        {
            AssetMetadata? assetMetadata = (AssetMetadata?)item.Value.Deserialize(AssetMetadataManager.Default.GetMetadataType(item.Key), ApiJsonSerializerDefault.Options);

            if (assetMetadata != null)
            {
                asset.SetMetadata(assetMetadata);
            }
        }

        return asset;
    }

    public static RestAsset ToRest(this Asset asset)
    {
        RestAsset restAsset = new RestAsset();
        restAsset.Id = asset.Id;
        restAsset.CreatedAt = asset.CreatedAt;
        restAsset.ModifiedAt = asset.ModifiedAt;
        restAsset.PublishedAt = asset.PublishedAt;
        restAsset.Version = asset.Version;
        restAsset.Name = asset.Name;
        restAsset.Hash = asset.Hash;
        restAsset.MimeType = asset.MimeType;
        restAsset.Size = asset.Size;
        restAsset.PreviewUrl = asset.PreviewUrl;
        restAsset.Slug = asset.Slug;
        restAsset.Alt = asset.Alt;
        restAsset.Description = asset.Description;

        if (asset.Folder != null)
        {
            restAsset.Folder = asset.Folder.ToRest();
        }

        foreach (var metadata in asset.Metaddata)
        {
            restAsset.Metaddata.Add(metadata.Key, JsonSerializer.SerializeToNode(metadata.Value, metadata.Value.GetType(), ApiJsonSerializerDefault.Options));
        }

        return restAsset;
    }

    public static AssetFolder ToModel(this RestAssetFolder restFolder)
    {
        AssetFolder folder = new AssetFolder();
        folder.Id = restFolder.Id;
        folder.CreatedAt = restFolder.CreatedAt;
        folder.ModifiedAt = restFolder.ModifiedAt;
        folder.Version = restFolder.Version;
        folder.Name = restFolder.Name;

        if (restFolder.Parent != null)
        {
            folder.Parent = new AssetFolder(restFolder.Parent.Value);
        }

        return folder;
    }

    public static RestAssetFolder ToRest(this AssetFolder folder)
    {
        RestAssetFolder restFolder = new RestAssetFolder();
        restFolder.Id = folder.Id;
        restFolder.CreatedAt = folder.CreatedAt;
        restFolder.ModifiedAt = folder.ModifiedAt;
        restFolder.Version = folder.Version;
        restFolder.Name = folder.Name;
        restFolder.Parent = folder.Parent?.Id;

        return restFolder;
    }
}
