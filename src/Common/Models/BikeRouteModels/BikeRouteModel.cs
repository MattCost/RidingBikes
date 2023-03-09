using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Models.Base;
using RidingBikes.Common.Models.GroupRideModels;

namespace RidingBikes.Common.Models.BikeRouteModels;

public class BikeRouteModel : EntityModelBase<BikeRouteCreateModel, BikeRouteUpdateModel>
{
    public double Distance { get; set; }
    public string RideWithGPSUrl { get; set; } = string.Empty;
    public ICollection<GroupRideModel> GroupRides { get; set; } = new HashSet<GroupRideModel>();

    public override void Initialize(BikeRouteCreateModel createModel)
    {
        base.Initialize(createModel);
        this.Distance = createModel.Distance;
        this.RideWithGPSUrl = createModel.RideWithGPSUrl;
    }

    public override bool Update(BikeRouteUpdateModel updateModel)
    {
        // Call base.Update first, to do validation
        bool isUpdated = base.Update(updateModel);

        if(updateModel.Distance.HasValue)
        {
            if(this.Distance.CheckForUpdate(updateModel.Distance.Value))
            {
                this.Distance = updateModel.Distance.Value;
                isUpdated = true;
            }
        }

        if(!string.IsNullOrEmpty(updateModel.RideWithGPSUrl))
        {
            if(this.RideWithGPSUrl.CheckForUpdate(updateModel.RideWithGPSUrl))
            {
                this.RideWithGPSUrl = updateModel.RideWithGPSUrl;
                isUpdated = true;
            }
        }
        
        return isUpdated;
    }

}
