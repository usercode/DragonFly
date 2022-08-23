using DragonFly.Fields.BlockField;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DragonFly.AspNetCore;

public static class HtmlHelperExtensions
{
    public static IHtmlContent DisplayBlocks<TModel>(this IHtmlHelper<TModel> htmlHelper, IBlocksContent content)
    {
        return htmlHelper.DisplayFor(m => content.Blocks);
    }

    public static async Task<IHtmlContent> DisplayBlockFieldAsync<TModel>(this IHtmlHelper<TModel> htmlHelper, BlockField blockField)
    {
        Document document = await blockField.GetDocumentAsync();

        return htmlHelper.DisplayBlocks(document);
    }
}
