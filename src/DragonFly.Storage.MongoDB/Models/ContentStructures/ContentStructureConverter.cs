﻿using DragonFly.Content;
using DragonFly.Core.ContentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Models.ContentStructures
{
    static class ContentStructureConverter
    {
        public static MongoContentStructure ToMongo(this ContentStructure structure)
        {
            MongoContentStructure mongoContent = new MongoContentStructure();
            mongoContent.Id = structure.Id;
            mongoContent.Name = structure.Name;

            return mongoContent;
        }

        public static ContentStructure ToModel(this MongoContentStructure structure)
        {
            ContentStructure model = new ContentStructure(structure.Name);
            model.Id = structure.Id;

            return model;
        }

        public static MongoContentNode ToMongo(this ContentNode node)
        {
            MongoContentNode mongoNode = new MongoContentNode();
            mongoNode.Id = node.Id;
            mongoNode.CreatedAt = node.CreatedAt;
            mongoNode.ModifiedAt = node.ModifiedAt;
            mongoNode.Target = node.Target;
            mongoNode.StructureName = node.StructureName;
            mongoNode.Parent = node.Parent?.Id;

            return mongoNode;
        }

        public static ContentNode ToModel(this MongoContentNode mongoNode)
        {
            ContentNode node = new ContentNode();
            node.Id = mongoNode.Id;
            node.CreatedAt = mongoNode.CreatedAt;
            node.ModifiedAt = mongoNode.ModifiedAt;
            node.Target = mongoNode.Target;
            node.StructureName = mongoNode.StructureName;

            return node;
        }
    }
}
