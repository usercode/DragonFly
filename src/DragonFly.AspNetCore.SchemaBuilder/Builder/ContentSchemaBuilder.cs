using DragonFly.AspNetCore.SchemaBuilder.Attributes;
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

    public async Task BuildAsync<T>()
    {
        Type type = typeof(T);

        ContentSchemaAttribute? schemaAttribute = type.GetCustomAttribute<ContentSchemaAttribute>();

        if (schemaAttribute == null)
        {
            throw new Exception($"The type {type.Name} needs the ContentSchemaAttribute.");
        }

        string schemaName = type.Name;

        if (schemaAttribute.SchemaName != null)
        {
            schemaName = schemaAttribute.SchemaName;
        }

        //load schema
        ContentSchema schema = await Storage.GetSchemaAsync(schemaName);

        if (schema == null)
        {
            schema = new ContentSchema(type.Name);
        }
        else
        {
            schema.Fields.Clear();
        }

        IEnumerable<Type> allFieldTypes = ContentFieldManager.Default.GetAllFieldTypes();

        foreach (PropertyInfo property in type.GetProperties())
        {
            ContentFieldAttribute? fieldAttribute = property.GetCustomAttribute<ContentFieldAttribute>();

            if (fieldAttribute == null)
            {
                continue;
            }

            Type fieldType = property.PropertyType;

            if (fieldAttribute.FieldType != null)
            {
                fieldType = fieldAttribute.FieldType;
            }

            if (allFieldTypes.Contains(fieldType))
            {
                schema.AddField(property.Name, property.PropertyType);
            }
        }

        if (schema.IsNew())
        {
            await Storage.CreateAsync(schema);
        }
        else
        {
            await Storage.UpdateAsync(schema);
        }

        if (_schema.TryAdd(type, schema) == false)
        {
            throw new Exception($"The type '{type.Name}' already exists.");
        }
    }
}
