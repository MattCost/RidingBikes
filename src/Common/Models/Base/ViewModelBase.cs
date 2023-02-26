
namespace RidingBikes.Common.Models.Base;
public abstract class ViewModelBase : EntityModelBase
{
    public ViewModelBase() : base()
    {
        this.Id = Guid.Empty;
    }
}
