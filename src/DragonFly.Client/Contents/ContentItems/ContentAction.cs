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
    /// <param name="contentItemDetail"></param>
    /// <returns></returns>
    public virtual bool CanUse(ContentItemDetail contentItemDetail)
    {
        return true;
    }

    /// <summary>
    /// Execute
    /// </summary>
    /// <param name="contentItemBase"></param>
    /// <returns></returns>
    public abstract Task Execute(ContentItemDetail contentItemBase);
}
