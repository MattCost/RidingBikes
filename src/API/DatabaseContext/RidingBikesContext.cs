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


    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
        
    // }
    public DbSet<GroupRide> GroupRides => Set<GroupRide>();
    public DbSet<BikeRoute> BikeRoutes => Set<BikeRoute>();
}