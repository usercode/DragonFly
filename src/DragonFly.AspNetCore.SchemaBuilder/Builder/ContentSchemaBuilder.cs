using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.SchemaBuilder
{
    /// <summary>
    /// ContentSchemaBuilder
    /// </summary>
    public class ContentSchemaBuilder : IContentSchemaBuilder
    {
        public ContentSchemaBuilder(ISchemaStorage schemaStorage)
        {
            Storage = schemaStorage;

            _schemaByName = new Dictionary<string, Type>();
            _schema = new Dictionary<Type, ContentSchema>();
        }

        /// <summary>
        /// Schema
        /// </summary>
        private ISchemaStorage Storage { get; }

        private IDictionary<string, Type> _schemaByName;
        private IDictionary<Type, ContentSchema> _schema;

        public async Task BuildAsync<T>()
        {
            Type type = typeof(T);

            ContentSchema schema = await Storage.GetContentSchemaAsync(type.Name);

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
                if (allFieldTypes.Contains(property.PropertyType))
                {
                    schema.AddField(property.Name, property.PropertyType);
                }
            }

            if (schema.IsNew)
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
}
