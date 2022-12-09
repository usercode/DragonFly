// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFlyTemplate.Models;

namespace DragonFlyTemplate;

public class DataCache
{

    /// <summary>
    /// Elements
    /// </summary>
    public IList<PageLayoutModel> Elements { get; set; } = new List<PageLayoutModel>();
}
