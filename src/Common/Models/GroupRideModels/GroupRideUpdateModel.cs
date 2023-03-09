using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using RidingBikes.Common.Attributes;
using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models.GroupRideModels;

public class GroupRideUpdateModel : UpdateModelBase
{
    public DateTime? DateTime { get; set; } = null;
    public string? Location { get; set; } = null;
    public RideType? RideType { get; set; } = null;
    public Guid? BikeRouteId { get; set; } = null;

}
