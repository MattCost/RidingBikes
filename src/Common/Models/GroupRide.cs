using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RidingBikes.Common.Models;
public class GroupRide
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Location { get; set; } = string.Empty;
    
    [ForeignKey("BikeRouteId")]
    public BikeRoute? BikeRoute { get; set; }
    public Guid? BikeRouteId { get; set; }
    public RideType RideType { get; set; }
}