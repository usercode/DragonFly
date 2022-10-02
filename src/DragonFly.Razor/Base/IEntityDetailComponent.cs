// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Content;
using DragonFly.Contents.Content;
using DragonFly.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Base;

public interface IEntityDetailComponent<T>
    where T : Entity
{

    [Inject]
    public ClientContentService ContentService { get; set; }

    public virtual bool IsNewEntity => EntityId == Guid.Empty;

    [Parameter]
    public Guid EntityId { get; set; }

    [Parameter]
    public string EntityType { get; set; }

    [Parameter]
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
