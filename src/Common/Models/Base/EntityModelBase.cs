using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Attributes;

namespace RidingBikes.Common.Models.Base;

public abstract class EntityModelBase<TCreateModel, TUpdateModel> : SerializableBase
    where TCreateModel : CreateModelBase
    where TUpdateModel : UpdateModelBase
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public virtual void Initialize(TCreateModel createModel)
    {
        createModel.IsValid();
        this.Id = createModel.Id;
        this.Name = createModel.Name;
    }

    public virtual bool Update(TUpdateModel updateModel)
    {
        updateModel.IsValid();
        bool isUpdated = false;
        if(!string.IsNullOrEmpty(updateModel.Name))
        {
            if(this.Name.CheckForUpdate(updateModel.Name))
            {
                this.Name = updateModel.Name;
                isUpdated = true;
            } 
        }
        return isUpdated;
    }
}

