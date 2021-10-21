﻿using DragonFly.Content;
using DragonFly.Core.Builders;
using DragonFly.Fields.BlockField.Blocks;
using DragonFly.Fields.BlockField.Razor.Pages;
using DragonFly.Fields.BlockField.Razor.Pages.Blocks;
using DragonFly.Razor.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Razor
{
    public static class DragonFlyBuilderExtensions
    {
        public static IDragonFlyBuilder AddBlockField(this IDragonFlyBuilder builder)
        {
            builder.Services.AddSingleton(BlockFieldManager.Default);

            builder.Init(api =>
            {
                api.RegisterField<BlockField, BlockFieldView>();
                api.RegisterBlock<ColumnBlock, ColumnBlockView>();
                api.RegisterBlock<ImageBlock, ImageBlockView>();
                api.RegisterBlock<TextBlock, TextBlockView>();
                api.RegisterBlock<HtmlBlock, HtmlBlockView>();
            });           

            return builder;
        }
    }
}