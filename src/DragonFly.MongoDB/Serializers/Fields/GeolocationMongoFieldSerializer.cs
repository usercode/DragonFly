// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Bson;
using DragonFly.Storage.Abstractions;
using MongoDB.Driver.GeoJsonObjectModel;
using MongoDB.Bson.Serialization;

namespace DragonFly.Storage.MongoDB.Fields;

/// <summary>
/// GeolocationMongoFieldSerializer
/// </summary>
/// <typeparam name="TContentField"></typeparam>
public class GeolocationMongoFieldSerializer : MongoFieldSerializer<GeolocationField>
{
    public override GeolocationField Read(SchemaField schemaField,  BsonValue bsonValue)
    {
        GeolocationField contentField = new GeolocationField();

        if (bsonValue is BsonDocument document)
        {
            GeoJsonPoint<GeoJson2DCoordinates> geo = BsonSerializer.Deserialize<GeoJsonPoint<GeoJson2DCoordinates>>(document);

            contentField.Longitude = geo.Coordinates.Values[0];
            contentField.Latitude = geo.Coordinates.Values[1];
        }

        return contentField;
    }

    public override BsonValue Write(GeolocationField contentField)
    {
        if (contentField.Longitude != null && contentField.Latitude != null)
        {
            GeoJsonPoint<GeoJson2DCoordinates> geo = new GeoJsonPoint<GeoJson2DCoordinates>(new GeoJson2DCoordinates(contentField.Longitude.Value, contentField.Latitude.Value));

            return geo.ToBsonDocument();
        }
        else
        {
            return BsonNull.Value;
        }
    }
}
