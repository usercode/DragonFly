// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Pages.ContentItems;
using System.Threading.Tasks;

namespace DragonFly.Client.Core.Contents.ContentItems;

public interface IContentItemAction
{
    string Name { get; }

    bool CanUse(ContentItemDetailBase contentItemDetail);

    Task Execute(ContentItemDetailBase contentItemBase);
}
