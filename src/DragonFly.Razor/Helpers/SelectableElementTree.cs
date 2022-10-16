// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Collections.Generic;

namespace DragonFly.Razor.Helpers;

/// <summary>
/// SelectableElementTree
/// </summary>
/// <typeparam name="T"></typeparam>
public class SelectableElementTree<T> : SelectableElement<T>
{
    public SelectableElementTree(bool isSelected, T obj, IEnumerable<SelectableElementTree<T>> childs)
        : base(isSelected, obj)
    {
        Parent = Parent;
        Childs = childs;

        foreach (SelectableElementTree<T> child in childs)
        {
            child.Parent = this;
        }
    }

    /// <summary>
    /// Parent
    /// </summary>
    public SelectableElementTree<T> Parent { get; set; }


    /// <summary>
    /// Childs
    /// </summary>
    public IEnumerable<SelectableElementTree<T>> Childs { get; set; }
}
