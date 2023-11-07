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
    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        ISchemaStorage storage = api.ServiceProvider.GetRequiredService<ISchemaStorage>();

        ContentSchema schema = TContentModel.Schema;

        //override existing schema
        ContentSchema? existingSchema = await storage.GetSchemaAsync(schema.Name);

        if (existingSchema != null)
        {
            schema.Id = existingSchema.Id;

            await storage.UpdateAsync(schema);
        }
        else
        {
            await storage.CreateAsync(schema);
        }
    }
}
