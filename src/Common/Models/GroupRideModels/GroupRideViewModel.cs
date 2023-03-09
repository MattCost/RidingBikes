using RidingBikes.Common.Models.Base;
using RidingBikes.Common.Models.BikeRouteModels;

namespace RidingBikes.Common.Models.GroupRideModels;

public class GroupRideViewModel : ViewModelBase
{
    public DateTime DateTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public BikeRouteViewModel BikeRoute { get; set; } = new BikeRouteViewModel();
    public Guid BikeRouteId { get { return BikeRoute.Id; } }
    public RideType RideType { get; set; }

    public static GroupRideViewModel Create(GroupRideModel groupRide)
    {

        var viewModel = new GroupRideViewModel
        {
            Id = groupRide.Id,
            DateTime = groupRide.DateTime,
            Location = groupRide.Location,
            RideType = groupRide.RideType
        };
        if (groupRide.BikeRoute != null)
        {
            viewModel.BikeRoute = BikeRouteViewModel.Create(groupRide.BikeRoute);
        }
        return viewModel;
    }
}