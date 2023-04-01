using Microsoft.AspNetCore.Mvc;



namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ReportController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportController(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProjectList()
        {
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\EmployeeList.frx";
  
   
            return View();
        }



    }
}
