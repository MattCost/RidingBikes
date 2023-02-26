using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RidingBikes.Common.Models.Base;

public abstract class SerializableBase
{
    protected JsonSerializerOptions SerializerOptions { get; set; }
    public SerializableBase()
    {
        this.SerializerOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
    }
    public override string ToString()
    {
        if(SerializerOptions == null)
        {
            throw new ArgumentNullException(nameof(SerializerOptions));
        }
        var json = JsonSerializer.Serialize(this, this.GetType(), SerializerOptions);
        return json;
    }
}
