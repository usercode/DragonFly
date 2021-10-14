using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Core.ContentStructures.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.ContentStructures
{
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

        Task<QueryResult<ContentStructure>> QueryStructuresAsync();

        
        //ContentNodes

        Task CreateAsync(ContentNode node);

        Task UpdateAsync(ContentNode node);

        Task<QueryResult<ContentNode>> QueryAsync(NodesQuery query);
    }
}
