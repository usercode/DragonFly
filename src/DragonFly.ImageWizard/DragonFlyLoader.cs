// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using ImageWizard;
using ImageWizard.Loaders;
using Microsoft.Extensions.Options;

namespace DragonFly.ImageWizard;

/// <summary>
/// DragonFlyLoader
/// </summary>
public class DragonFlyLoader : Loader<DragonFlyLoaderOptions>
{
    public DragonFlyLoader(IDragonFlyApi api, IAssetStorage storage, IOptions<DragonFlyLoaderOptions> options)
        : base(options)
    {
        Api = api;
        Storage = storage;
    }

    /// <summary>
    /// Api
    /// </summary>
    private IDragonFlyApi Api { get; }

    /// <summary>
    /// Storage
    /// </summary>
    private IAssetStorage Storage { get; }

    public override async Task<LoaderResult> GetAsync(string source, CachedData? existingCachedImage)
    {
        int pos = source.IndexOf('?');

        Guid id;

        //remove query parameter
        if (pos != -1)
        {
            id = Guid.Parse(source.AsSpan(0, pos));
        }
        else
        {
            id = Guid.Parse(source);
        }

        using (Api.DisableAuthorization())
        {
            Asset? asset = await Storage.GetAssetAsync(id);

            if (asset == null)
            {
                return LoaderResult.Failed();
            }

            Stream stream = await Storage.OpenStreamAsync(asset.Id);

            return LoaderResult.Success(new OriginalData(asset.MimeType, stream));
        }
    }
}
