using System.ComponentModel.DataAnnotations;

namespace RidingBikes.Common.Attributes;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
sealed public class NotEmptyGuid : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if(value == null) return true;
        if(! (value is Guid )) return false;

        return !Guid.Equals((Guid)value, Guid.Empty);

    }
}