// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Nodes;

namespace DragonFly.API;

/// <summary>
/// SingleValueFieldSerializer
/// </summary>
public class SingleValueJsonFieldSerializer<TContentField> : JsonFieldSerializer<TContentField>
    where TContentField : ContentField, ISingleValueField, new()
{
    public override TContentField Read(SchemaField schemaField,  JsonNode? jsonNode)
    {
        TContentField contentField = new TContentField();

        if (jsonNode is JsonValue jsonValue)
        {
            if (contentField is SingleValueField<string> stringField)
            {
                if (jsonValue.TryGetValue(out string? stringValue))
                {
                    stringField.Value = stringValue;
                }
            }
            else if (contentField is SingleValueField<bool?> boolField)
            {
                if (jsonValue.TryGetValue(out bool? boolValue))
                {
                    boolField.Value = boolValue;
                }
            }
            else if (contentField is SingleValueField<short?> shortField)
            {
                if (jsonValue.TryGetValue(out short? shortValue))
                {
                    shortField.Value = shortValue;
                }
            }
            else if (contentField is SingleValueField<int?> intField)
            {
                if (jsonValue.TryGetValue(out int? intValue))
                {
                    intField.Value = intValue;
                }
            }
            else if (contentField is SingleValueField<long?> longField)
            {
                if (jsonValue.TryGetValue(out long? longValue))
                {
                    longField.Value = longValue;
                }
            }
            else if (contentField is SingleValueField<float?> floatField)
            {
                if (jsonValue.TryGetValue(out float? floatValue))
                {
                    floatField.Value = floatValue;
                }
            }
            else if (contentField is SingleValueField<double?> doubleField)
            {
                if (jsonValue.TryGetValue(out double? doubleValue))
                {
                    doubleField.Value = doubleValue;
                }
            }           
            else if (contentField is SingleValueField<DateTime?> dateField)
            {
                if (jsonValue.TryGetValue(out DateTime? dateValue))
                {
                    dateField.Value = dateValue;
                }
            }
            else if (contentField is SingleValueField<Guid?> guidField)
            {
                if (jsonValue.TryGetValue(out Guid? guidValue))
                {
                    guidField.Value = guidValue;
                }
            }
        }

        return contentField;
    }

    public override JsonNode? Write(TContentField contentField, bool includeNavigationProperty)
    {
        if (contentField.HasValue)
        {
            return contentField switch
            {
                SingleValueField<string> stringField => JsonValue.Create(stringField.Value),
                SingleValueField<bool?> boolField => JsonValue.Create(boolField.Value),
                SingleValueField<short?> shortField => JsonValue.Create(shortField.Value),
                SingleValueField<int?> intField => JsonValue.Create(intField.Value),
                SingleValueField<long?> longField => JsonValue.Create(longField.Value),
                SingleValueField<float?> floatField => JsonValue.Create(floatField.Value),
                SingleValueField<double?> doubleField => JsonValue.Create(doubleField.Value),                
                SingleValueField<DateTime?> dateField => JsonValue.Create(dateField.Value),
                SingleValueField<Guid?> guidField => JsonValue.Create(guidField.Value),
                _ => throw new Exception($"Unknown field type: {contentField.Value.GetType().Name}")
            };
        }
        else
        {
            return null;
        }
    }
}
