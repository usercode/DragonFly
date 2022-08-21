﻿using DragonFly.AspNetCore.SchemaBuilder.Attributes;
using DragonFly.AspNetCore.SchemaBuilder.SchemaStates;
using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder;

/// <summary>
/// ContentSchemaBuilder
/// </summary>
public class ContentSchemaBuilder : IContentSchemaBuilder
{
    public ContentSchemaBuilder(ISchemaStorage schemaStorage)
    {
        Storage = schemaStorage;

        _schema = new Dictionary<Type, ContentSchema>();
    }

    /// <summary>
    /// Schema
    /// </summary>
    private ISchemaStorage Storage { get; }

    private IDictionary<Type, ContentSchema> _schema;

    public async Task AddAsync(Type type)
    {
        SchemaAttribute? schemaAttribute = type.GetCustomAttribute<SchemaAttribute>();

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

        IEnumerable<Type> allFieldTypes = ContentFieldManager.Default.GetAllFieldTypes();

        int sort = 100;

        foreach (PropertyInfo property in type.GetProperties())
        {
            BaseFieldAttribute? fieldAttribute = property.GetCustomAttribute<BaseFieldAttribute>(true);

            if (fieldAttribute == null)
            {
                continue;
            }

            schema.AddField(property.Name, fieldAttribute.FieldType, options: fieldAttribute.CreateOptions(), sortkey: sort++);         
        }

        if (schema.IsNew())
        {
            await Storage.CreateAsync(schema);
        }
        else
        {
            await Storage.UpdateAsync(schema);
        }

        SchemaTypeManager.Default.Add(type, schema);

        if (_schema.TryAdd(type, schema) == false)
        {
            throw new Exception($"The type '{type.Name}' already exists.");
        }
    }
}
