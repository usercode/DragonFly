﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.MongoDB.Storages;
using DragonFly.Query;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DragonFly.MongoDB;

/// <summary>
/// ContentStructureMongoStorage
/// </summary>
public class ContentStructureMongoStorage : MongoStorage, IStructureStorage
{
    public ContentStructureMongoStorage(MongoClient client, IDateTimeService dateTimeService)
        : base(client)
    {
        DateTimeService = dateTimeService;

        ContentStructures = Client.Database.GetCollection<MongoContentStructure>("ContentStructures");
        ContentNodes = Client.Database.GetCollection<MongoContentNode>("ContentNodes");
    }

    /// <summary>
    /// ContentStructures
    /// </summary>
    public IMongoCollection<MongoContentStructure> ContentStructures { get; }

    /// <summary>
    /// ContentNodes
    /// </summary>
    public IMongoCollection<MongoContentNode> ContentNodes { get; }

    /// <summary>
    /// DateTimeService
    /// </summary>
    private IDateTimeService DateTimeService { get; }

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

        await ContentStructures.InsertOneAsync(mongo).ConfigureAwait(false);

        structure.Id = mongo.Id;
    }

    public async Task UpdateAsync(ContentStructure entity)
    {
        entity.ModifiedAt = DateTimeService.Current();

        await ContentStructures.FindOneAndReplaceAsync(Builders<MongoContentStructure>.Filter.Eq(x => x.Id, entity.Id), entity.ToMongo()).ConfigureAwait(false);
    }

    public async Task<ContentStructure> GetStructureAsync(Guid id)
    {
        MongoContentStructure? structure = await ContentStructures.AsQueryable().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

        if (structure == null)
        {
            throw new Exception($"Structure not found {id}");
        }

        return structure.ToModel();
    }

    public async Task<ContentStructure> GetStructureAsync(string name)
    {
        MongoContentStructure? structure = await ContentStructures.AsQueryable().FirstOrDefaultAsync(x => x.Name == name).ConfigureAwait(false);

        if (structure == null)
        {
            throw new Exception($"Structure not found {name}");
        }

        return structure.ToModel();
    }

    public async Task<QueryResult<ContentStructure>> QueryAsync(StructureQuery query)
    {
        IList<MongoContentStructure> result = await ContentStructures.AsQueryable()
                                                                            .OrderBy(x => x.Name)
                                                                            .ToListAsync()
                                                                            .ConfigureAwait(false);

        return new QueryResult<ContentStructure>()
        {
            Items = result
                        .Select(x => x.ToModel())
                        .ToList()
        };
    }

    public async Task<QueryResult<ContentNode>> QueryAsync(NodesQuery query)
    {
        IQueryable<MongoContentNode> q = ContentNodes.AsQueryable()
                                                            .Where(x => x.Structure == query.Structure);

        if (query.ParentId != null)
        {
            q = q.Where(x => x.Parent == query.ParentId.Value);
        }
        else
        {
            q = q.Where(x => x.Parent == null);
        }

        var result = await q.ToListAsync().ConfigureAwait(false);

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

        await ContentNodes.InsertOneAsync(mongo).ConfigureAwait(false);

        node.Id = mongo.Id;
    }

    public Task UpdateAsync(ContentNode node)
    {
        return Task.CompletedTask;
    }
}
