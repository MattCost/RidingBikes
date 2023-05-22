using Microsoft.AspNetCore.Mvc;
using RidingBikes.APIClient;
using RidingBikes.Common.Models;
using RidingBikes.Common.Models.BikeRouteModels;
using RidingBikes.Website.PageModels;

namespace RidingBikes.Website.Pages.BikeRoute
{
    public class CreatePageModel : PageModelBase
    {
        [BindProperty]
        public BikeRouteCreateModel createModel { get; set; } = new BikeRouteCreateModel();

        public CreatePageModel(RidingBikes.APIClient.IClient client, ILogger<CreatePageModel> logger) : base(client, logger)
        {

        }
        public async Task<ActionResult> OnPostAsync()
        {
            if(createModel.Id == Guid.Empty) createModel.Id = Guid.NewGuid();
            if(createModel != null)
            {
                try
                {
                    await _client.BikeRoutes_CreateBikeRouteAsync(createModel);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Unable to create bikeRoute");
                    PreviousPageAction = "BikeRoute/Create/OnPost";
                    PreviousPageErrorMessage = "Error when trying to create route";
                }
            }
            return RedirectToPage("Index");

        }        
    }
}
