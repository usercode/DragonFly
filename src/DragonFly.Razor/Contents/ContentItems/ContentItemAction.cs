using DragonFly.Client.Pages.ContentItems;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Client.Core.Contents.ContentItems;

public abstract class ContentItemAction : IContentItemAction, IContentItemAction<ContentItemDetailBase>
{
    public ContentItemAction()
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
