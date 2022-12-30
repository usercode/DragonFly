// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;

namespace DragonFly;

/// <summary>
/// IStructureStorage
/// </summary>
public interface IStructureStorage
{
    //ContentStructure

    Task CreateAsync(ContentStructure structure);

    Task UpdateAsync(ContentStructure structure);

    Task<ContentStructure> GetStructureAsync(Guid id);

    Task<ContentStructure> GetStructureAsync(string name);

    Task<QueryResult<ContentStructure>> QueryAsync(StructureQuery query);

    
    //ContentNodes

    Task CreateAsync(ContentNode node);

    Task UpdateAsync(ContentNode node);

    Task<QueryResult<ContentNode>> QueryAsync(NodesQuery query);
}
