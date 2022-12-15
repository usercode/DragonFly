// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// IContentQuery
/// </summary>
public interface IContentQuery
{
    /// <summary>
    /// Skip
    /// </summary>
    int Skip { get; set; }

    /// <summary>
    /// Top
    /// </summary>
    int Top { get; set; }

    /// <summary>
    /// SearchPattern
    /// </summary>
    string SearchPattern { get; set; }

    /// <summary>
    /// Fields
    /// </summary>
    IList<FieldQuery> Fields { get; set; }

    /// <summary>
    /// OrderFields
    /// </summary>
    IList<FieldOrder> OrderFields { get; set; }

    /// <summary>
    /// 
    /// </summary>
    bool IncludeListFieldsOnly { get; set; }

    /// <summary>
    /// UsedAsset
    /// </summary>
    Guid? UsedAsset { get; set; }

    /// <summary>
    /// Published
    /// </summary>
    bool Published { get; set; }
}
