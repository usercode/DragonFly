using DragonFly.Core.ContentStructures.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentStructures;

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
