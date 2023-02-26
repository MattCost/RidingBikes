using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RidingBikes.Common.Models.Base;

public abstract class ValidatableBase : SerializableBase
{
    public bool IsValid()
    {
        ValidationContext vc = new ValidationContext(this);
        ICollection<ValidationResult> results = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(this, vc, results, true);
        if (!isValid)
        {
            var message = $"{results.Count} {(results.Count > 1 ? "properties" : "property")} failed validation\n" +
            string.Join("\n", results);
            throw new ValidationException(message);
        }
        return isValid;
    }
}
