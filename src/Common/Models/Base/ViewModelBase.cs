
namespace RidingBikes.Common.Models.Base;
public abstract class ViewModelBase : SerializableBase
{
    public Guid Id { get; set; } = Guid.Empty;
}
