// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Pages.ContentItems;
using System.Threading.Tasks;

namespace DragonFly.Client;

public interface IContentAction
{
    string Name { get; }

    bool CanUse(ContentItemDetail contentItemDetail);

    Task Execute(ContentItemDetail contentItemBase);
}
