using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Attributes;

namespace RidingBikes.Common.Models.Base;

public abstract class EntityModelBase : SerializableBase
{
    public Guid Id { get; set; }

    public virtual void Initialize(CreateModelBase createModel)
    {
        createModel.IsValid();
        this.Id = createModel.Id;
    }

    public virtual bool Update(UpdateModelBase updateModel)
    {
        updateModel.IsValid();
        return false;
    }
}

