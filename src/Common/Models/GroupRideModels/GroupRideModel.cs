using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using RidingBikes.Common.Attributes;
using RidingBikes.Common.Models.Base;
using RidingBikes.Common.Models.BikeRouteModels;

namespace RidingBikes.Common.Models.GroupRideModels;

public class GroupRideModel : EntityModelBase<GroupRideCreateModel, GroupRideUpdateModel>
{
    public string Description { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
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
    public override void Initialize(GroupRideCreateModel createModel)
    {
        base.Initialize(createModel);
        this.StartTime = createModel.StartTime;
        this.Location = createModel.Location;
        this.BikeRouteId = createModel.BikeRouteId;
        this.RideType = createModel.RideType;
    }

    public override bool Update(GroupRideUpdateModel updateModel)
    {
        bool isUpdated = base.Update(updateModel);
        
        if (updateModel.BikeRouteId.HasValue)
        {
            if (this.BikeRouteId.CheckForUpdate(updateModel.BikeRouteId.Value))
            {
                this.BikeRouteId = updateModel.BikeRouteId.Value;
                isUpdated = true;
            }
        }
        if (updateModel.StartTime.HasValue)
        {
            if (this.StartTime.CheckForUpdate(updateModel.StartTime.Value))
            {
                this.StartTime = updateModel.StartTime.Value;
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
