using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RidingBikes.Website.PageModels;

namespace RidingBikes.Website.Pages;

public class IndexModel : PageModelBase
{

    public IndexModel(RidingBikes.APIClient.IClient client, ILogger<IndexModel> logger) : base(client, logger)
    {
    }

    public void OnGet()
    {

    }
}
