// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Pages.ContentItems;
using System.Threading.Tasks;

namespace DragonFly.Client;

public abstract class ContentAction : IContentAction, IContentAction<ContentItemDetailBase>
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
    /// <param name="contentItemDetail"></param>
    /// <returns></returns>
    public virtual bool CanUse(ContentItemDetailBase contentItemDetail)
    {
        return true;
    }

    /// <summary>
    /// Execute
    /// </summary>
    /// <param name="contentItemBase"></param>
    /// <returns></returns>
    public abstract Task Execute(ContentItemDetailBase contentItemBase);
}
