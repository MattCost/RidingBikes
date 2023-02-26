using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models;


public class BikeRouteUpdateModel : UpdateModelBase
{
    [Range(0.1,999)] // is this smart enough to only fail if not null?
    public double? Distance { get; set; } = null;
    public string? RideWithGPSUrl { get; set; }

}