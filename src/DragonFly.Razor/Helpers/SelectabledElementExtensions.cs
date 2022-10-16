// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Collections.Generic;

namespace DragonFly.Razor.Helpers;

public static class SelectabledElementExtensions
{
    public static SelectableElementTree<T> EnableActivePath<T>(this SelectableElementTree<T> selectableElement)
    {
        selectableElement.IsSelectedChanged += (element) =>
        {
            //select parent elements
            if (selectableElement.IsSelected == true)
            {
                SelectableElementTree<T> parent = selectableElement.Parent;

                if (parent != null)
                {
                    parent.IsSelected = true;
                }
            }
            else
            {
                //deselect child elements
                foreach (SelectableElementTree<T> child in selectableElement.Childs)
                {
                    child.IsSelected = false;
                }
            }
        };

        return selectableElement;
    }

    public static IEnumerable<T> ToFlatList<T>(this IEnumerable<SelectableElementTree<T>> items, bool isSelected = true)
    {
        foreach (SelectableElementTree<T> p in items)
        {
            if (p.IsSelected == isSelected)
            {
                yield return p.Element;
            }

            IEnumerable<T> childs = ToFlatList(p.Childs, isSelected);

            foreach (T c in childs)
            {
                yield return c;
            }
        }
    }
}
