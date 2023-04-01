using Microsoft.AspNetCore.Mvc;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class PositionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
