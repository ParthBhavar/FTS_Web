using Microsoft.AspNetCore.Mvc;

namespace FTS_Web.Controllers
{
    public class ApplicantDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
