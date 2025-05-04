// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Core.Modules.Assets.Actions;
using SmartResults;

namespace DragonFly;

/// <summary>
/// IAssetStorage
/// </summary>
public interface IAssetStorage
{
    Task<Result<QueryResult<Asset>>> QueryAsync(AssetQuery query);

    Task<Result<Asset?>> GetAssetAsync(Guid id);

    Task<Result> CreateAsync(Asset asset);

    Task<Result> UpdateAsync(Asset asset);

    Task<Result> DeleteAsync(Asset asset);

    Task<Result> PublishAsync(Asset asset);

    Task<Result> UploadAsync(Guid assetId, string mimetype, Stream stream);

    Task<Result<Stream>> OpenStreamAsync(Guid assetId);

    Task<Result> ApplyMetadataAsync(Asset asset);

    Task<Result<BackgroundTaskInfo>> ApplyMetadataAsync(AssetQuery query);

    Task<Result> ApplyActionAsync(Guid assetId, string name);

    Task<Result<ActionItem[]>> GetActionsAsync(Guid assetId);
}
