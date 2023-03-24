using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models.RideEventModels;

public class RideEventCreateModel : CreateModelBase
{

    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public DateOnly Date { get; set; }
    
    public List<Guid> RideIds { get; set; } = new List<Guid>();


}