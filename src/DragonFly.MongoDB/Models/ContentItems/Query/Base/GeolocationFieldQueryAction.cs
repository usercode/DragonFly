// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using MongoDB.Driver;

namespace DragonFly.MongoDB;

/// <summary>
/// GeolocationFieldQueryAction
/// </summary>
public class GeolocationFieldQueryAction : FieldQueryAction<GeolocationFieldQuery>
{
    public override void Apply(GeolocationFieldQuery query, FieldQueryActionContext context)
    {
        if (query.Longitude != null && query.Latitude != null)
        {
            context.Filters.Add(Builders<MongoContentItem>.Filter
                .NearSphere(
                        CreateFullFieldName(query.FieldName), 
                        query.Longitude.Value, 
                        query.Latitude.Value, 
                        query.MaxDistance, 
                        query.MinDistance));
        }
    }
}
