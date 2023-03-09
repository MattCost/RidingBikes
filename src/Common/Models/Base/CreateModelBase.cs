using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Attributes;

namespace RidingBikes.Common.Models.Base;

public class CreateModelBase : ValidatableBase
{
    [Required]
    [NotEmptyGuid]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = string.Empty;
}
