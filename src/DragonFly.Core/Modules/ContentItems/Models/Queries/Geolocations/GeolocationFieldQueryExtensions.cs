// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// GeolocationFieldQueryExtensions
/// </summary>
public static class GeolocationFieldQueryExtensions
{
    /// <summary>
    /// Geolocation
    /// </summary>
    /// <typeparam name="TContentQuery"></typeparam>
    /// <param name="query"></param>
    /// <param name="field">The field name</param>
    /// <param name="longitude">Valid longitude values are between -180 and 180, both inclusive.</param>
    /// <param name="latitude">Valid latitude values are between -90 and 90, both inclusive.</param>
    /// <param name="minDistance"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    public static TContentQuery Geolocation<TContentQuery>(this TContentQuery query, string field, double longitude, double latitude, double? minDistance = null, double? maxDistance = null)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new GeolocationFieldQuery() { FieldName = field, Longitude = longitude, Latitude = latitude, MinDistance = minDistance, MaxDistance = maxDistance });

        return query;
    }
}
