﻿using DragonFly.AspNetCore.API.Models;
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
            ContentSchema contentSchema = new ContentSchema(restContentItem.Name);

            contentSchema.Id = restContentItem.Id;
            contentSchema.CreatedAt = restContentItem.CreatedAt;
            contentSchema.ModifiedAt = restContentItem.ModifiedAt;
            contentSchema.Version = restContentItem.Version;
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

        public static RestContentSchemaField ToRest(this ContentSchemaField schemaField)
        {
            RestContentSchemaField restContentFieldDefinition = new RestContentSchemaField();
            restContentFieldDefinition.Label = schemaField.Label;
            restContentFieldDefinition.SortKey = schemaField.SortKey;
            restContentFieldDefinition.FieldType = schemaField.FieldType;

            if (schemaField.Options != null)
            {
                restContentFieldDefinition.Options = JObject.FromObject(schemaField.Options, NewtonJsonExtensions.CreateSerializer());
            }

            return restContentFieldDefinition;
        }

        public static ContentSchemaField ToModel(this RestContentSchemaField definition)
        {
            Type optionsType = ContentFieldManager.Default.GetOptionsType(definition.FieldType);

            ContentFieldOptions options = (ContentFieldOptions)definition.Options.ToObject(optionsType, NewtonJsonExtensions.CreateSerializer());

            ContentSchemaField schemaField = new ContentSchemaField(definition.FieldType, options);
            schemaField.Label = definition.Label;
            schemaField.SortKey = definition.SortKey;

            return schemaField;
        }
    }
}
