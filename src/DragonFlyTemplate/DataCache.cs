// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFlyTemplate.Models;

namespace DragonFlyTemplate;

public class DataCache
{

    /// <summary>
    /// PageLayouts
    /// </summary>
    public IList<PageLayoutModel> PageLayouts { get; set; } = new List<PageLayoutModel>();

    /// <summary>
    /// FooterPages
    /// </summary>
    public IList<StandardPageModel> FooterPages { get; set; } = new List<StandardPageModel>();
}
