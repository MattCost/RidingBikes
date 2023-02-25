namespace RidingBikes.Common.Models;

public class BikeRoute
{
    public Guid Id { get; set; }
    public double Distance { get; set; }
    public string RideWithGPSUrl { get; set; } = string.Empty;

}