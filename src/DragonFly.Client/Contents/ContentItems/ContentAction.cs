// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Pages.ContentItems;
using System.Threading.Tasks;

namespace DragonFly.Client;

public abstract class ContentAction : IContentAction, IContentAction<ContentItemDetail>
{
    public ContentAction()
    {

    }
    
    /// <summary>
    /// Name
    /// </summary>
    public virtual string Name { get; }

    /// <summary>
    /// CanUse
    /// </summary>
    public virtual bool CanUse(ContentItemDetail contentItemDetail)
    {
        return true;
    }

    /// <summary>
    /// Execute
    /// </summary>
    public abstract Task Execute(ContentItemDetail contentItemBase);
}
