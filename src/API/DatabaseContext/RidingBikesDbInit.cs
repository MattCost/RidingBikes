using RidingBikes.Common.Models;
using RidingBikes.Common.Models.BikeRouteModels;
using RidingBikes.Common.Models.GroupRideModels;

namespace RidingBikes.API.DatabaseContext;

public static class DbInitializer
{
    public static async void Initialize(RidingBikesContext context)
    {
        if (!context.GroupRides.Any())
        {
            var route1 = new BikeRouteModel { Id = Guid.NewGuid(), Distance = Random.Shared.NextDouble() * 30.0, RideWithGPSUrl = "First Route" };
            var route2 = new BikeRouteModel { Id = Guid.NewGuid(), Distance = Random.Shared.NextDouble() * 20.0, RideWithGPSUrl = "Second Route" };
            var rides = new GroupRideModel[]
            {
                GroupRideModel.Create( new GroupRideCreateModel{ StartTime = TimeOnly.FromDateTime(DateTime.UtcNow), Location = "Elmwood", BikeRouteId = route1.Id, RideType = RideType.A}),
                GroupRideModel.Create( new GroupRideCreateModel{ StartTime = TimeOnly.FromDateTime(DateTime.UtcNow), Location = "Niagara", BikeRouteId = route2.Id, RideType = RideType.BSweaty}),
                GroupRideModel.Create( new GroupRideCreateModel{ StartTime = TimeOnly.FromDateTime(DateTime.UtcNow), Location = "Niagara", BikeRouteId = route2.Id, RideType = RideType.BChatty}),
                GroupRideModel.Create( new GroupRideCreateModel{ StartTime = TimeOnly.FromDateTime(DateTime.UtcNow), Location = "Gravel Gravel", BikeRouteId = Guid.Empty, RideType = RideType.D})
            };
            context.BikeRoutes.Add(route1);
            context.BikeRoutes.Add(route2);
            context.GroupRides.AddRange(rides);
            await context.SaveChangesAsync();
        }
    }
}