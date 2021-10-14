using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Core.ContentStructures;
using DragonFly.Core.ContentStructures.Queries;
using DragonFly.Storage.MongoDB.Models.ContentStructures;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Data
{
    /// <summary>
    /// MongoStorage
    /// </summary>
    public partial class MongoStorage : IStructureStorage
    {
        public MongoStorage()
        {
            
        }

        public async Task CreateAsync(ContentStructure structure)
        {
            if (structure.Id == Guid.Empty)
            {
                structure.Id = Guid.NewGuid();
            }

            DateTime now = DateTimeService.Current();

            structure.CreatedAt = now;
            structure.ModifiedAt = now;

            MongoContentStructure mongo = structure.ToMongo();

            await ContentStructures.InsertOneAsync(mongo);

            structure.Id = mongo.Id;
        }

        public async Task UpdateAsync(ContentStructure entity)
        {
            entity.ModifiedAt = DateTimeService.Current();

            await ContentStructures.FindOneAndReplaceAsync(Builders<MongoContentStructure>.Filter.Eq(x => x.Id, entity.Id), entity.ToMongo());
        }

        public async Task<ContentStructure> GetStructureAsync(Guid id)
        {
            MongoContentStructure? structure = await ContentStructures.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

            if (structure == null)
            {
                throw new Exception($"Structure not found {id}");
            }

            return structure.ToModel();
        }

        public async Task<ContentStructure> GetStructureAsync(string name)
        {
            MongoContentStructure? structure = await ContentStructures.AsQueryable().FirstOrDefaultAsync(x => x.Name == name);

            if (structure == null)
            {
                throw new Exception($"Structure not found {name}");
            }

            return structure.ToModel();
        }

        public async Task<QueryResult<ContentStructure>> QueryStructuresAsync()
        {
            return new QueryResult<ContentStructure>()
            {
                Items = ContentStructures.AsQueryable()
                                            .OrderBy(x => x.Name)
                                            .ToList()
                                            .Select(x => x.ToModel())
                                            .ToList()
            };
        }

        public async Task<QueryResult<ContentNode>> QueryAsync(NodesQuery query)
        {
            IMongoQueryable<MongoContentNode> q = ContentNodes.AsQueryable()
                                                                .Where(x => x.StructureName == query.Structure);

            if (query.ParentId != null)
            {
                q = q.Where(x => x.Parent == query.ParentId.Value);
            }
            else
            {
                q = q.Where(x => x.Parent == null);
            }

            var result = await q.ToListAsync();

            return new QueryResult<ContentNode>()
            {
                Items = result
                                            .Select(x => x.ToModel())
                                            .ToList()
            };
        }

        public async Task CreateAsync(ContentNode node)
        {
            if (node.Id == Guid.Empty)
            {
                node.Id = Guid.NewGuid();
            }

            DateTime now = DateTimeService.Current();

            node.CreatedAt = now;
            node.ModifiedAt = now;

            MongoContentNode mongo = node.ToMongo();

            await ContentNodes.InsertOneAsync(mongo);

            node.Id = mongo.Id;
        }

        public async Task UpdateAsync(ContentNode node)
        {
            
        }
    }
}
