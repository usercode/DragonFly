using DragonFly.AspNetCore.REST.Models;
using DragonFly.Client;
using DragonFly.Content;
using DragonFly.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DragonFly.Data.Models
{
    public static class RestContentSchemaConverter
    {
        public static ContentSchema ToModel(this RestContentSchema restContentItem)
        {
            ContentSchema contentSchema = new ContentSchema();

            contentSchema.Id = restContentItem.Id;
            contentSchema.CreatedAt = restContentItem.CreatedAt;
            contentSchema.ModifiedAt = restContentItem.ModifiedAt;
            contentSchema.Version = restContentItem.Version;
            contentSchema.Name = restContentItem.Name;
            contentSchema.ListFields = restContentItem.ListFields.ToList();
            contentSchema.ReferenceFields = restContentItem.ReferenceFields.ToList();
            contentSchema.OrderFields = restContentItem.OrderFields.ToList();

            foreach (var mongoField in restContentItem.Fields)
            {
                contentSchema.Fields.Add(mongoField.Key, mongoField.Value.ToModel());
            }

            return contentSchema;
        }

        public static RestContentSchema ToRest(this ContentSchema contentSchema)
        {
            RestContentSchema restContentItem = new RestContentSchema();

            restContentItem.Id = contentSchema.Id;
            restContentItem.Name = contentSchema.Name;
            restContentItem.CreatedAt = contentSchema.CreatedAt;
            restContentItem.ModifiedAt = contentSchema.ModifiedAt;
            restContentItem.Version = contentSchema.Version;
            restContentItem.ListFields = contentSchema.ListFields.ToList();
            restContentItem.ReferenceFields = contentSchema.ReferenceFields.ToList();
            restContentItem.OrderFields = contentSchema.OrderFields.ToList();

            foreach (var field in contentSchema.Fields)
            {
                restContentItem.Fields.Add(field.Key, field.Value.ToRest());
            }

            return restContentItem;
        }

        public static RestContentFieldDefinition ToRest(this ContentFieldDefinition definition)
        {
            RestContentFieldDefinition restContentFieldDefinition = new RestContentFieldDefinition();
            restContentFieldDefinition.Label = definition.Label;
            restContentFieldDefinition.SortKey = definition.SortKey;
            restContentFieldDefinition.FieldType = definition.FieldType;

            if (definition.Options != null)
            {
                restContentFieldDefinition.Options = JObject.FromObject(definition.Options, NewtonJsonExtensions.CreateSerializer());
            }

            return restContentFieldDefinition;
        }

        public static ContentFieldDefinition ToModel(this RestContentFieldDefinition definition)
        {
            ContentFieldDefinition restContentFieldDefinition = new ContentFieldDefinition();
            restContentFieldDefinition.Label = definition.Label;
            restContentFieldDefinition.SortKey = definition.SortKey;
            restContentFieldDefinition.FieldType = definition.FieldType;

            if (definition.Options != null)
            {
                ContentFieldOptions options = ContentFieldManager.Default.CreateField(definition.FieldType).CreateOptions();

                restContentFieldDefinition.Options = (ContentFieldOptions)definition.Options.ToObject(options.GetType(), NewtonJsonExtensions.CreateSerializer());
            }

            return restContentFieldDefinition;
        }
    }
}
