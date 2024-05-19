// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// References content by schema and id.
/// </summary>
public readonly struct ContentId
{
    public ContentId(string schema, Guid id)
    {
        Schema = schema;
        Id = id;
    }

    /// <summary>
    /// Schema
    /// </summary>
    public string Schema { get; }

    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; }

    public override string ToString()
    {
        return $"{Schema}/{Id}";
    }
}
