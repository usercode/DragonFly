// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// GeolocationFieldQueryExtensions
/// </summary>
public static class GeolocationFieldQueryExtensions
{
    public static TContentQuery Geolocation<TContentQuery>(this TContentQuery query, double longitude, double latitude)
        where TContentQuery : ContentQuery
    {
        query.Fields.Add(new GeolocationFieldQuery() { Longitude = longitude, Latitude = latitude });

        return query;
    }
}
