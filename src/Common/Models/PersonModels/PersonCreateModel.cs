using System.ComponentModel.DataAnnotations;
using RidingBikes.Common.Models.Base;

namespace RidingBikes.Common.Models.PersonModels;

public class PersonCreateModel : CreateModelBase
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;


    [Required]
    public string Email { get; set; } = string.Empty;

    // TODO rbac app roles
    // TODO in app roles such as Rider, Marshal, etc. Flagged enum or list of enum values



}