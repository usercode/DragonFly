// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Runtime.CompilerServices;

namespace DragonFly;

/// <summary>
/// AsyncLazy
/// </summary>
/// <typeparam name="T"></typeparam>
public class AsyncLazy<T>
{
    public AsyncLazy(ContentReference reference, Func<Task<T?>> taskFactory)
    {
        ContentReference = reference;
        _lazy = new Lazy<Task<T?>>(taskFactory);
    }

    public AsyncLazy(T? value)
    {
        SetDirectValue(value);
    }

    private Lazy<Task<T?>> _lazy;
    public ContentReference? ContentReference { get; private set; }
    private T? _directValue;
    private bool _directValueChanged;

    public TaskAwaiter<T?> GetAwaiter()
    {
        return _lazy.Value.GetAwaiter();
    }

    public void SetDirectValue(T? value)
    {
        if (value is ContentItem content)
        {
            ContentReference = content.ToReference();
        }
        else if (value is Asset asset)
        {
            ContentReference = asset.ToReference();
        }
        else
        {
            ContentReference = null;
        }

        _directValue = value;
        _directValueChanged = true;

        _lazy = new Lazy<Task<T?>>(() => Task.FromResult(value));
    }

    public bool TryGetDirectValue(out T? value)
    {
        if (_directValueChanged)
        {
            value = _directValue;

            return true;
        }

        value = default;

        return false;
    }

    public static implicit operator AsyncLazy<T>(T? value)
    {
        return new AsyncLazy<T>(value);
    }
}
