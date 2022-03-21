using DragonFly.Content;
using DragonFly.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Storage.MongoDB.Query;

/// <summary>
/// IFieldQueryAction
/// </summary>
public interface IFieldQueryAction
{
    void Apply(FieldQuery query, FieldQueryActionContext context);
}
