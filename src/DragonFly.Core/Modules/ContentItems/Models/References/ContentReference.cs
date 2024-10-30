// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization;
using DragonFly.Core;

namespace DragonFly;

/// <summary>
/// ContentReference
/// </summary>
[JsonConverter(typeof(ContentReferenceConverter))]
public readonly struct ContentReference : IEquatable<ContentReference>
{
    public ContentReference(string schema, Guid id)
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

    public override bool Equals(object? obj)
    {
        if (obj is ContentReference other)
        {
            return Equals(other);
        }

        return false;
    }

    public bool Equals(ContentReference other)
    {
        return Schema == other.Schema && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Schema, Id);
    }

    public override string ToString()
    {
        return $"{Schema}/{Id}";
    }

    public static bool operator ==(ContentReference left, ContentReference right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ContentReference left, ContentReference right)
    {
        return !(left == right);
    }
}
