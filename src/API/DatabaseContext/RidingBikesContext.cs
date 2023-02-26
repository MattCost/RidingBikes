using Microsoft.EntityFrameworkCore;
using RidingBikes.Common.Models;

namespace RidingBikes.API.DatabaseContext;

public class RidingBikesContext : DbContext
{
    public RidingBikesContext() { }
    public RidingBikesContext(DbContextOptions<RidingBikesContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // A bike route has many rides, where a ride has 1 route, and uses the route id foreign key.
        // I think this means i dont have to use annotations.
        // But also this might not be needed since EF is smart enough to figure this out.
        // modelBuilder.Entity<BikeRoute>()
        //     .HasMany(x => x.GroupRides).WithOne(x => x.BikeRoute).HasForeignKey(x => x.BikeRouteId);
    }
    public DbSet<GroupRideModel> GroupRides => Set<GroupRideModel>();
    public DbSet<BikeRouteModel> BikeRoutes => Set<BikeRouteModel>();
}