using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models;

public class BikeRouteViewModel : ViewModelBase
{
    public double Distance { get; set; }
    public string RideWithGPSUrl { get; set; } = string.Empty;

    public static BikeRouteViewModel Create(BikeRouteModel bikeRoute)
    {
        var viewModel = new BikeRouteViewModel
        {
            Id = bikeRoute.Id,
            Distance = bikeRoute.Distance,
            RideWithGPSUrl = bikeRoute.RideWithGPSUrl
        };
        return viewModel;
    }
}