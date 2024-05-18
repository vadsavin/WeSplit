using Microsoft.AspNetCore.Mvc.RazorPages;
using WeSplit.Common.Entities.Org;

namespace WeSplit
{
    public class BaseUserModel<T> : PageModel where T : BaseUserModel<T>
    {
        private const string _authCoocieName = "auth-token";

        protected readonly ILogger<T> _logger;

        protected User User { get; private set; }

        public BaseUserModel(ILogger<T> logger)
        {
            _logger = logger;
        }

        public virtual void OnGet()
        {
            if (!Request.Cookies.TryGetValue(_authCoocieName, out var coockieValue))
            {

            }

            if (coockieValue is null)
            {

            }


        }
    }
}
