using Microsoft.AspNetCore.Mvc;
using WeSplit.Common.Entities.Org;
using WeSplit.Pages;
using WeSplit.SqlDatabase;

namespace WeSplit.Controllers
{
    [Route("/i")]
    public class IdentityController : Controller
    {
        [Route("/i/{pattern}")]
        public IActionResult Identity(string pattern)
        {
            Identity identity = null;

            if (Guid.TryParse(pattern, out var guid))
            {
                using var dbContext = new SqlDbContext();

                identity = dbContext.Identities.FirstOrDefault(i => i.Guid == guid);
            }

            if (identity is null)
            {
                var errorModel = new ErrorModel(new LoggerFactory().CreateLogger<ErrorModel>())
                {
                    ErrorMessage = "Object does not exist or you are not allowed to access it."
                };

                return RedirectToPage("/Error", errorModel);
            }

            return RedirectToPage(string.IsNullOrEmpty(pattern) ? "/" : pattern);
        }
    }
}
