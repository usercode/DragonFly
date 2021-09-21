using DragonFly.Core.ContentItems.Queries;
using DragonFly.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Query
{
    public interface IQueryAction
    {
        void Apply(FieldQueryBase query, QueryActionContext context);
    }
}
