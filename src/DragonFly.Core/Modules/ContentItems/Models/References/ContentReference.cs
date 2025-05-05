// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using DragonFly.Core;

namespace DragonFly;

/// <summary>
/// ContentReference
/// </summary>
[JsonConverter(typeof(ContentReferenceJsonConverter))]
[TypeConverter(typeof(ContentReferenceTypeConverter))]
public readonly struct ContentReference : IEquatable<ContentReference>, IParsable<ContentReference>
{
    public static ContentReference Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out ContentReference result))
        {
            return result;
        }

        throw new Exception();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ContentReference result)
    {
        if (s == null)
        {
            result = default;

            return false;
        }

        int pos = s.IndexOf('/');

        if (pos == -1)
        {
            result = default;

            return false;
        }

        result = new ContentReference(s[..pos], Guid.Parse(s[(pos + 1)..]));

        return true;
    }

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
