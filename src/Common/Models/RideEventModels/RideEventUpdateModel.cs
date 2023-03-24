using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models.RideEventModels;

public class RideEventUpdateModel : UpdateModelBase
{
    public string? Description { get; set; }
    public DateOnly? Date { get; set; }

}