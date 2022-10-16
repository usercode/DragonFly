// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Proxy;

/// <summary>
/// ContentSchemaBuilder
/// </summary>
public interface IContentSchemaBuilder
{
    Task AddAsync(Type type);
    
}
