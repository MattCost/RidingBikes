using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models.BikeRouteModels;

public class BikeRouteCreateModel : CreateModelBase
{
    [Range(0.1,999)]
    public double Distance { get; set; }

    [Required]
    public string RideWithGPSUrl { get; set; } = string.Empty;
}

