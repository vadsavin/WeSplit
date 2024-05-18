using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeSplit.Pages
{
    public class SessionsModel : PageModel
    {
        private readonly ILogger<SessionsModel> _logger;

        public SessionsModel(ILogger<SessionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
