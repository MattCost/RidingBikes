using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using RidingBikes.Common.Attributes;
using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models;
public class GroupRideModel : EntityModelBase
{
    public DateTime DateTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public BikeRouteModel BikeRoute
    {
        set => _bikeRoute = value;
        get => _bikeRoute ?? throw new InvalidOperationException($"Uninitialized property {nameof(BikeRoute)}");
    }

    private BikeRouteModel? _bikeRoute;
    public Guid BikeRouteId { get; set; }
    public RideType RideType { get; set; }

    public static GroupRideModel Create(GroupRideCreateModel createModel)
    {
        var ride = new GroupRideModel();
        ride.Initialize(createModel);
        return ride;

    }
    public void Initialize(GroupRideCreateModel createModel)
    {
        base.Initialize(createModel);
        this.DateTime = createModel.DateTime;
        this.Location = createModel.Location;
        this.BikeRouteId = createModel.BikeRouteId;
        this.RideType = createModel.RideType;
    }

    public bool Update(GroupRideUpdateModel updateModel)
    {
        bool isUpdated = false;
        if (updateModel.BikeRouteId.HasValue)
        {
            if (this.BikeRouteId.CheckForUpdate(updateModel.BikeRouteId.Value))
            {
                this.BikeRouteId = updateModel.BikeRouteId.Value;
                isUpdated = true;
            }
        }
        if (updateModel.DateTime.HasValue)
        {
            if (this.DateTime.CheckForUpdate(updateModel.DateTime.Value))
            {
                this.DateTime = updateModel.DateTime.Value;
                isUpdated = true;
            }
        }

        if (!string.IsNullOrEmpty(updateModel.Location))
        {
            if (this.Location.CheckForUpdate(updateModel.Location))
            {
                this.Location = updateModel.Location;
                isUpdated = true;
            }
        }

        if (updateModel.RideType.HasValue)
        {
            if (this.RideType.CheckForUpdate(updateModel.RideType.Value))
            {
                this.RideType = updateModel.RideType.Value;
                isUpdated = true;
            }
        }
        return isUpdated;
    }
}
