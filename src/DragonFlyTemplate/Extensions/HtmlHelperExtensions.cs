using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Fields.BlockField;
using DragonFly.Fields.BlockField.Storage.Serializers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using static MongoDB.Driver.WriteConcern;

namespace DragonFly.AspNetCore;

public static class HtmlHelperExtensions
{
    public static IHtmlContent DisplayBlocks<TModel>(this IHtmlHelper<TModel> htmlHelper, IBlocksContent content)
    {
        return htmlHelper.DisplayFor(m => content.Blocks);
    }

    public static async Task<IHtmlContent> DisplayBlockFieldAsync<TModel>(this IHtmlHelper<TModel> htmlHelper, BlockField blockField)
    {
        Document document = await DocumentSerializer.DeserializeAsync(blockField.Value);

        return htmlHelper.DisplayBlocks(document);
    }
}
