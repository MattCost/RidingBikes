using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RidingBikes.Website.PageModels
{
    public abstract class PageModelBase : PageModel
    {
        protected readonly ILogger<PageModelBase> _logger;
        protected readonly RidingBikes.APIClient.IClient _client;

        /// <summary>
        /// An error message from a previous page that redirects. 
        /// For example, in an OnPostHandler one can set this string, then redirect to a Get page, and display the error then
        /// </summary>
        [TempData]
        public string? PreviousPageErrorMessage { get; set; }

        /// <summary>
        /// The Action that caused the previous error message
        /// </summary>
        [TempData]
        public string? PreviousPageAction { get; set; }
        /// <summary>
        /// An error message from the current page. Not saved in temp data, so it can only be displayed on the current page
        /// </summary>
        public string? CurrentPageErrorMessage { get; set; }
        /// <summary>
        /// The Action that cuased the Current Error Message
        /// </summary>
        public string? CurrentPageAction { get; set; }

        public bool DisplayCurrentError
        {
            get
            {
                return !string.IsNullOrEmpty(CurrentPageErrorMessage);
            }
        }
        public bool DisplayPreviousError
        {
            get
            {
                return !string.IsNullOrEmpty(PreviousPageErrorMessage);
            }

        }

        public PageModelBase(RidingBikes.APIClient.IClient client, ILogger<PageModelBase> logger)
        {
            _logger = logger;
            _client = client;
        }

    }
}
