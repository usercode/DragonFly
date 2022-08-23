using DragonFly.Content;
using DragonFly.Storage;
using DragonFlyTemplate.Models;
using DragonFly.AspNetCore.SchemaBuilder;
using DragonFly.AspNetCore.SchemaBuilder.Proxies;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using DragonFly.Core;

namespace DragonFlyTemplate.Startup;

public class DataSeeding
{
    public DataSeeding(
        IContentSchemaBuilder schemaBuilder, 
        IDataStorage dataStorage,
        IHostEnvironment env)
    {
        SchemaBuilder = schemaBuilder;
        DataStorage = dataStorage;
        HostEnvironment = env;
    }

    private IDataStorage DataStorage { get; }

    private IContentSchemaBuilder SchemaBuilder { get; }

    private IHostEnvironment HostEnvironment { get; }

    public async Task StartAsync()
    {
        //var r = await DataStorage.QueryAsync<AnimalModel>(new DragonFly.Content.Queries.ContentItemQuery() { Top = 1 });

        //if (r.Count > 0)
        //{
        //    return;
        //}

       
        //await DataStorage.CreateAsync(meerkat);
    }

    public async Task<Asset> CreateAssetAsync(IFileInfo filename)
    {
        if (filename.Exists == false)
        {
            throw new Exception();
        }

        Asset asset = new Asset();
        asset.Name = filename.Name;
        asset.Slug = Slugify.ToSlug(asset.Name);

        await DataStorage.CreateAsync(asset);

        using Stream stream = filename.CreateReadStream();

        await DataStorage.UploadAsync(asset.Id, MimeTypes.Jpeg, stream);

        return asset;
    }

}
