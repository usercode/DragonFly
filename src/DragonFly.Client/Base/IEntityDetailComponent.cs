// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace DragonFly.Client.Base;

public interface IEntityDetailComponent<T>
    where T : IEntity
{
    public virtual bool IsNewEntity => EntityId == Guid.Empty;

    public Guid EntityId { get; set; }

    public string EntityType { get; set; }

    public T Entity { get; set; }

    public ContentSchema Schema { get; set; }

    Task RefreshAsync();
    Task CreateAsync();
    Task UpdateAsync();
    Task SaveAsync();
    Task DeleteAsync();

    //Task PublishAsync();

    //Task UnpublishAsync();
   
}
