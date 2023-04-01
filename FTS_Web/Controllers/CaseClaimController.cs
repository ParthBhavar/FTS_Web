using Microsoft.AspNetCore.Mvc;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CaseClaimController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CaseClaimDtl()
        {
            return View();
        }
    }
}
