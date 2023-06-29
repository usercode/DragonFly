// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class ContentItemExtensions
{
    /// <summary>
    /// ToModel
    /// </summary>
    /// <typeparam name="TContentModel"></typeparam>
    /// <param name="contentItem"></param>
    /// <returns></returns>
    public static TContentModel ToModel<TContentModel>(this ContentItem contentItem)
        where TContentModel : IContentModel
    {
        return (TContentModel)TContentModel.Create(contentItem);
    }
}
