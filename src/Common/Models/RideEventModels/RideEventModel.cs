using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models.RideEventModels;

public class RideEventModel : EntityModelBase<RideEventCreateModel, RideEventUpdateModel>
{
    public string Description { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public List<Guid> RideIds { get; set; } = new List<Guid>();

    public override void Initialize(RideEventCreateModel createModel)
    {
        base.Initialize(createModel);
        this.Date = createModel.Date;
        this.Description = createModel.Description;
        this.RideIds = createModel.RideIds;
    }

    public override bool Update(RideEventUpdateModel updateModel)
    {
        var isUpdated = base.Update(updateModel);
        if(updateModel.Date.HasValue)
        {
            if(this.Date.CheckForUpdate(updateModel.Date.Value))
            {
                this.Date = updateModel.Date.Value;
                isUpdated = true;
            }
        }
        if(!string.IsNullOrEmpty(updateModel.Description))
        {
            if(this.Description.CheckForUpdate(updateModel.Description))
            {
                this.Description = updateModel.Description;
                isUpdated = true;
            }

        }

        return isUpdated;

    }

    // public void AddRideId(Guid rideId)
    // {
    //     if(!this.RideIds.Contains(rideId))
    //         this.RideIds.Add(rideId);
    // }
    
    // public void RemoveRide(Guid rideId)
    // {
    //     if(this.RideIds.Contains(rideId))
    //         this.RideIds.Remove(rideId);
    // }
}
