using DragonFly.Content;
using DragonFly.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DragonFly.Data.Models
{
    public static class RestContentItemConverter
    {
        public static ContentItem ToModel(this RestContentItem restContentItem)
        {
            return ToModel(restContentItem, null);
        }
        
        public static ContentItem ToModel(this RestContentItem restContentItem, ContentSchema schema)
        {
            if (schema == null)
            {
                schema = restContentItem.Schema.ToModel();
            }

            ContentItem contentItem = schema.CreateItem();

            contentItem.Id = restContentItem.Id;
            contentItem.Schema = schema;
            contentItem.CreatedAt = restContentItem.CreatedAt;
            contentItem.ModifiedAt = restContentItem.ModifiedAt;
            contentItem.PublishedAt = restContentItem.PublishedAt;
            contentItem.Version = restContentItem.Version;
            contentItem.SchemaVersion = restContentItem.SchemaVersion;

            foreach(var restField in restContentItem.Fields)
            {
                restField.Value.FromRestValue(restField.Key, contentItem, schema);
            }

            return contentItem;
        }

        public static RestContentItem ToRest(this ContentItem contentItem, bool includeNavigationProperties = true)
        {
            RestContentItem restContentItem = new RestContentItem();

            restContentItem.Id = contentItem.Id;
            restContentItem.Schema = contentItem.Schema.ToRest();
            restContentItem.CreatedAt = contentItem.CreatedAt;
            restContentItem.ModifiedAt = contentItem.ModifiedAt;
            restContentItem.PublishedAt = contentItem.PublishedAt;
            restContentItem.Version = contentItem.Version;
            restContentItem.SchemaVersion = contentItem.SchemaVersion;

            foreach (var field in contentItem.Fields)
            {
                restContentItem.Fields.Add(field.Key, field.Value.ToRestValue(includeNavigationProperties));
            }

            return restContentItem;
        }
    }
}
