// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Proxy.Attributes;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DragonFly.Proxy;

/// <summary>
/// ContentSchemaBuilder
/// </summary>
public class ContentSchemaBuilder : IContentSchemaBuilder
{
    public ContentSchemaBuilder(ISchemaStorage schemaStorage)
    {
        Storage = schemaStorage;
    }

    /// <summary>
    /// Schema
    /// </summary>
    private ISchemaStorage Storage { get; }

    public async Task AddAsync(Type type)
    {
        ContentItemAttribute? schemaAttribute = type.GetCustomAttribute<ContentItemAttribute>();

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
        ContentSchema? schema = await Storage.GetSchemaAsync(schemaName);

        if (schema == null)
        {
            schema = new ContentSchema(schemaName);
        }

        schema.ListFields.Clear();

        foreach (PropertyInfo property in type.GetProperties())
        {
            BaseFieldAttribute? fieldAttribute = property.GetCustomAttribute<BaseFieldAttribute>(true);

            if (fieldAttribute == null)
            {
                continue;
            }

            fieldAttribute.ApplySchema(property.Name, schema);
        }

        if (schema.IsNew())
        {
            await Storage.CreateAsync(schema);
        }
        else
        {
            await Storage.UpdateAsync(schema);
        }

        ProxyTypeManager.Default.Add(type, schema);
    }
}
