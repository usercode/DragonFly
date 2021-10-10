using DragonFly.AspNetCore.API.Models;
using DragonFly.Client;
using DragonFly.Content;
using DragonFly.Core.ContentStructures;
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
    public static class RestContentStructureConverter
    {
        public static ContentStructure ToModel(this RestContentStructure restContentItem)
        {
            ContentStructure contentSchema = new ContentStructure(restContentItem.Name);

            contentSchema.Id = restContentItem.Id;
            contentSchema.CreatedAt = restContentItem.CreatedAt;
            contentSchema.ModifiedAt = restContentItem.ModifiedAt;
            contentSchema.Version = restContentItem.Version;

            return contentSchema;
        }

        public static RestContentStructure ToRest(this ContentStructure contentSchema)
        {
            RestContentStructure restContentItem = new RestContentStructure();

            restContentItem.Id = contentSchema.Id;            
            restContentItem.CreatedAt = contentSchema.CreatedAt;
            restContentItem.ModifiedAt = contentSchema.ModifiedAt;
            restContentItem.Version = contentSchema.Version;
            restContentItem.Name = contentSchema.Name;

            return restContentItem;
        }

        public static ContentNode ToModel(this RestContentNode restContentNode)
        {
            ContentNode contentNode = new ContentNode();

            contentNode.Id = restContentNode.Id;
            contentNode.CreatedAt = restContentNode.CreatedAt;
            contentNode.ModifiedAt = restContentNode.ModifiedAt;
            contentNode.Version = restContentNode.Version;
            contentNode.StructureName = restContentNode.StructureName;

            if (restContentNode.Parent != null)
            {
                contentNode.Parent = restContentNode.Parent.ToModel();
            }

            return contentNode;
        }

        public static RestContentNode ToRest(this ContentNode contentNode)
        {
            RestContentNode restContentItem = new RestContentNode();

            restContentItem.Id = contentNode.Id;
            restContentItem.CreatedAt = contentNode.CreatedAt;
            restContentItem.ModifiedAt = contentNode.ModifiedAt;
            restContentItem.Version = contentNode.Version;
            restContentItem.StructureName = contentNode.StructureName;

            if (contentNode.Parent != null)
            {
                restContentItem.Parent = contentNode.Parent.ToRest();
            }

            return restContentItem;
        }
    }
}
