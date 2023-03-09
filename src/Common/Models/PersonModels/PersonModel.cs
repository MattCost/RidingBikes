using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models.PersonModels;

public class PersonModel : EntityModelBase<PersonCreateModel, PersonUpdateModel>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public override void Initialize(PersonCreateModel createModel)
    {
        base.Initialize(createModel);
        this.FirstName = createModel.FirstName ?? string.Empty;
        this.LastName = createModel.LastName ?? string.Empty;
        this.Email = createModel.Email ?? string.Empty;
    }
    public override bool Update(PersonUpdateModel updateModel)
    {
        var isUpdated = base.Update(updateModel);

        if(!string.IsNullOrEmpty(updateModel.FirstName))
        {
            if(this.FirstName.CheckForUpdate(updateModel.FirstName))
            {
                this.FirstName = updateModel.FirstName;
                isUpdated = true;
            }
        }

        if(!string.IsNullOrEmpty(updateModel.LastName))
        {
            if(this.LastName.CheckForUpdate(updateModel.LastName))
            {
                this.LastName = updateModel.LastName;
                isUpdated = true;
            }
        }
        return isUpdated;
    }

}