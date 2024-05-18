using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeSplit.Pages
{
    public class RecipientsModel : PageModel
    {
        private readonly ILogger<RecipientsModel> _logger;

        public RecipientsModel(ILogger<RecipientsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
