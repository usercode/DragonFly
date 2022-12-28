// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Client.Helpers;

public delegate void IsSelectedChangedHandler<T>(SelectableElement<T> selectableElement);

/// <summary>
/// SelectableObject
/// </summary>
/// <typeparam name="T"></typeparam>
public class SelectableElement<T>
{
    public SelectableElement(bool isSelected, T obj)
    {
        IsSelected = isSelected;
        Element = obj;
    }

    public event IsSelectedChangedHandler<T> IsSelectedChanged;

    private bool _isSelected;

    /// <summary>
    /// IsSelected
    /// </summary>
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;

                IsSelectedChanged?.Invoke(this);
            }
        }
    }

    /// <summary>
    /// Element
    /// </summary>
    public T Element { get; }
}
