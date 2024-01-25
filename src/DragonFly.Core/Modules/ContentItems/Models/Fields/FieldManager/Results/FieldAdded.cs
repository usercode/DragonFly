// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FieldAdded
/// </summary>
public abstract class FieldAdded
{
    /// <summary>
    /// FieldType
    /// </summary>
    public abstract Type FieldType { get; }
}

/// <summary>
/// FieldAdded
/// </summary>
/// <typeparam name="T"></typeparam>
public class FieldAdded<T> : FieldAdded
    where T : ContentField
{
    public override Type FieldType => typeof(T);
}
