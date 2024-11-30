// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public abstract class SingleValueFieldOptions<T> : FieldOptions
{ 

    public T? DefaultValue { get; set; }
}
