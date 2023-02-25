using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly RidingBikes.APIClient.IClient _client;

    public IndexModel(ILogger<IndexModel> logger, RidingBikes.APIClient.IClient client)
    {
        _logger = logger;
        _client = client;
    }

    public IEnumerable<RidingBikes.APIClient.GroupRide> Rides { get; set; } = new List<RidingBikes.APIClient.GroupRide>();

    public async Task OnGetAsync()
    {
        var result = await _client.GroupRides_GetGroupRidesAsync();
        Rides = result;
    }
}
