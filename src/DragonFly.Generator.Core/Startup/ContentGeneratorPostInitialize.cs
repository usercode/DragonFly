// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Reflection;
using DragonFly.API;

namespace DragonFly.Generator;

/// <summary>
/// ContentGeneratorPostInitialize
/// </summary>
public class ContentGeneratorPostInitialize : IPostInitialize
{
    public ContentGeneratorPostInitialize(ISchemaStorage schemaStorage)
    {
        Storage = schemaStorage;
    }

    /// <summary>
    /// Schema
    /// </summary>
    private ISchemaStorage Storage { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        //foreach (Type type in Options.Types)
        //{
        //    await Builder.AddAsync(type);
        //}
    }

    private async Task AddAsync(Type type)
    {
        var schemaAttribute = type.GetCustomAttribute<ContentItemAttribute>();

        string? schemaName = null;

        if (schemaAttribute?.SchemaName != null)
        {
            schemaName = schemaAttribute.SchemaName;
        }

        if (schemaName == null)
        {
            schemaName = type.Name;
        }

        //load schema
        var schema = await Storage.GetSchemaAsync(schemaName);

        if (schema == null)
        {
            schema = new ContentSchema(schemaName);
        }

        schema.Fields.Clear();
        schema.ListFields.Clear();

        foreach (var property in type.GetProperties())
        {
            var fieldAttribute = property.GetCustomAttribute<BaseFieldAttribute>(true);

            if (fieldAttribute == null)
            {
                continue;
            }

            fieldAttribute.AddToSchema(schema, property.Name);
        }

        if (schema.IsNew())
        {
            await Storage.CreateAsync(schema);
        }
        else
        {
            await Storage.UpdateAsync(schema);
        }
    }
}
