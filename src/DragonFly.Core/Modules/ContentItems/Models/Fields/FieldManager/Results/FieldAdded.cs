// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// FieldAdded
/// </summary>
public class FieldAdded<T> : IFieldAdded<T>
    where T : ContentField
{
    public Type FieldType => typeof(T);
}

public interface IFieldAdded
{

}

public interface IFieldAdded<out T> : IFieldAdded
{

}
