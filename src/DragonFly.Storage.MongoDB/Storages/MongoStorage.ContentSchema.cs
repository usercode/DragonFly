﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Contents.Assets;
using DragonFly.ContentTypes;
using DragonFly.Core;
using DragonFly.Core.Assets;
using DragonFly.Data.Content.ContentTypes;
using DragonFly.Data.Models;
using DragonFly.Data.Models.Assets;
using DragonFly.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace DragonFly.Data
{
    /// <summary>
    /// MongoStore
    /// </summary>
    public partial class MongoStorage : ISchemaStorage
    {
        public async Task CreateAsync(ContentSchema schema)
        {
            if (schema.Id == Guid.Empty)
            {
                schema.Id = Guid.NewGuid();
            }

            DateTime now = DateTimeService.Current();

            schema.CreatedAt = now;
            schema.ModifiedAt = now;

            var mongo = schema.ToMongo();

            await ContentSchemas.InsertOneAsync(mongo);

            schema.Id = mongo.Id;
        }

        public async Task UpdateAsync(ContentSchema entity)
        {
            entity.ModifiedAt = DateTimeService.Current();

            await ContentSchemas.FindOneAndReplaceAsync(Builders<MongoContentSchema>.Filter.Where(x => x.Id == entity.Id), entity.ToMongo());
        }

        public QueryResult<ContentSchema> QueryContentSchemas()
        {
            return new QueryResult<ContentSchema>()
            {
                Items = ContentSchemas.AsQueryable()
                        .OrderBy(x => x.Name)
                        .ToList()
                        .Select(x => x.ToModel())
                        .ToList()
            };
        }

        public Task<ContentSchema> GetContentSchemaAsync(string name)
        {
            return Task.Run(() => ContentSchemas.AsQueryable().FirstOrDefault(x => x.Name == name).ToModel());
        }

        public Task<ContentSchema> GetContentSchemaAsync(Guid id)
        {
            return Task.Run(() => ContentSchemas.AsQueryable().FirstOrDefault(x => x.Id == id).ToModel());
        }
    }
}
