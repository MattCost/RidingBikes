using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using RidingBikes.Common.Attributes;
using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models.GroupRideModels;
public class GroupRideCreateModel : CreateModelBase
{
    [Required]
    public DateTime DateTime { get; set; }

    [Required]
    public string Location { get; set; } = string.Empty;

    [Required]
    //ValidEnumAttribute
    public RideType RideType { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid BikeRouteId { get; set; }
}
