// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Init;
using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Model;

/// <summary>
/// ContentModelInitializer
/// </summary>
public class ContentModelInitializer<TContentModel> : IInitialize
    where TContentModel : IContentModel
{
    public ContentModelInitializer(ISchemaStorage schemaStorage)
    {
        SchemaStorage = schemaStorage;
    }

    /// <summary>
    /// SchemaStorage
    /// </summary>
    private ISchemaStorage SchemaStorage { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        ContentSchema schema = TContentModel.Schema;

        //override existing schema
        ContentSchema? existingSchema = await SchemaStorage.GetSchemaAsync(schema.Name);

        if (existingSchema != null)
        {
            schema.Id = existingSchema.Id;

            await SchemaStorage.UpdateAsync(schema);
        }
        else
        {
            await SchemaStorage.CreateAsync(schema);
        }
    }
}
