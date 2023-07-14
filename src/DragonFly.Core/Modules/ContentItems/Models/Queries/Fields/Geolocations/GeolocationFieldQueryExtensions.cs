// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// GeolocationFieldQueryExtensions
/// </summary>
public static class GeolocationFieldQueryExtensions
{
    public static TContentQuery Geolocation<TContentQuery>(this TContentQuery query, string field, double longitude, double latitude, double? minDistance = null, double? maxDistance = null)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new GeolocationFieldQuery() { FieldName = field, Longitude = longitude, Latitude = latitude, MinDistance = minDistance, MaxDistance = maxDistance });

        return query;
    }
}
