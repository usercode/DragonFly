using DragonFly.Content;
using DragonFly.ContentTypes;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Index
{
    /// <summary>
    /// CreateIndexAction
    /// </summary>
    class CreateIndexAction : IPostInitialize
    {
        public CreateIndexAction(MongoStorage mongoStorage, IDragonFlyApi api)
        {
            MongoStorage = mongoStorage;
            Api = api;
        }

        /// <summary>
        /// MongoStorage
        /// </summary>
        public MongoStorage MongoStorage { get; }

        /// <summary>
        /// Api
        /// </summary>
        public IDragonFlyApi Api { get; }

        public async Task ExecuteAsync(IDragonFlyApi api)
        {
            IList<MongoContentSchema> schemas = await MongoStorage.ContentSchemas.AsQueryable().ToListAsync();

            foreach (ContentSchema schema in schemas.Select(x=> x.ToModel()))
            {
                var collection = MongoStorage.GetMongoCollection(schema);

                //IEnumerable<ContentField> fields = Api.ContentField().CreateContentFields();

                //remove unused indices
                //var existingIndices = await collection.Indexes.ListAsync();

                //foreach (var f in existingIndices.ToList())
                //{
                //    string name = f.GetElement("name").Value.AsString;

                //    if (schema.Fields.Any(x => $"{x.Key}_Index" == name) == false)
                //    {
                //        await collection.Indexes.DropOneAsync(name);
                //    }
                //}

                foreach (var field in schema.Fields)
                {
                    Type fieldType = Api.ContentField().GetContentFieldType(field.Value.FieldType);

                    //add new indices
                    if (Api.Index().TryGetByType(fieldType, out FieldIndex? fieldIndex))
                    {
                        await collection.Indexes.CreateOneAsync(
                            new CreateIndexModel<MongoContentItem>(Builders<MongoContentItem>.IndexKeys.Ascending(fieldIndex.CreateIndexPath(field.Key)),
                            new CreateIndexOptions() { Name = fieldIndex.CreateIndexName(field.Key), Unique = fieldIndex.Unique }));
                    }
                }
            }
        }
    }
}
