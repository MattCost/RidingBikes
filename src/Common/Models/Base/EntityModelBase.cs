using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Attributes;

namespace RidingBikes.Common.Models.Base;

public abstract class EntityModelBase<TCreateModel, TUpdateModel> : SerializableBase
    where TCreateModel : CreateModelBase
    where TUpdateModel : UpdateModelBase
{
    public Guid Id { get; set; }

    public virtual void Initialize(TCreateModel createModel)
    {
        createModel.IsValid();
        this.Id = createModel.Id;
    }

    public virtual bool Update(TUpdateModel updateModel)
    {
        updateModel.IsValid();
        return false;
    }
}

