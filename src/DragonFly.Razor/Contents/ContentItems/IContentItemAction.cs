﻿using DragonFly.Client.Pages.ContentItems;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Client.Core.Contents.ContentItems
{
    public interface IContentItemAction
    {
        string Name { get; }

        bool CanUse(ContentItemDetailBase contentItemDetail);

        Task Execute(ContentItemDetailBase contentItemBase);
    }
}
