using RidingBikes.Common.Models;

namespace RidingBikes.API.DatabaseContext;

public static class DbInitializer
{
    public static async void Initialize(RidingBikesContext context)
    {
        if (!context.GroupRides.Any())
        {
            var route1 = new BikeRoute { Id = Guid.NewGuid(), Distance = Random.Shared.NextDouble() * 30.0, RideWithGPSUrl = "First Route" };
            var route2 = new BikeRoute { Id = Guid.NewGuid(), Distance = Random.Shared.NextDouble() * 20.0, RideWithGPSUrl = "Second Route" };
            var rides = new GroupRide[]
            {
                new GroupRide { Id = Guid.NewGuid(), DateTime = DateTime.Now, Location = "Elmwood", BikeRoute = route1, RideType = RideType.A },
                new GroupRide { Id = Guid.NewGuid(), DateTime = DateTime.Now, Location = "Niagara", BikeRoute = route2, RideType = RideType.BSweaty },
                new GroupRide { Id = Guid.NewGuid(), DateTime = DateTime.Now, Location = "Niagara", BikeRoute = route2, RideType = RideType.BChatty},
                new GroupRide { Id = Guid.NewGuid(), DateTime = DateTime.Now, Location = "Gravel Gravel", RideType = RideType.D }
            };
            context.GroupRides.AddRange(rides);
            await context.SaveChangesAsync();
        }
    }
}