using RidingBikes.APIClient;
using RidingBikes.Common.Models;
using RidingBikes.Website.PageModels;

namespace RidingBikes.Website.Pages.BikeRoute
{
    public class IndexPageModel : PageModelBase
    {
        public IndexPageModel(RidingBikes.APIClient.IClient client, ILogger<IndexPageModel> logger) : base(client, logger)
        {

        }

        public List<BikeRouteViewModel> routes { get; set; } = new List<BikeRouteViewModel>();

        public async Task OnGetAsync()
        {
            try
            {
                var routesResult = await _client.BikeRoutes_GetAllBikeRoutesAsync();
                this.routes = routesResult.ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unable to get all routes");
                CurrentPageAction = "BikeRoute/Index/OnGet";
                CurrentPageErrorMessage = "Error trying to get all routes";
            }
        }
    }
}
