using FTS.Business.ApplicantLoginMaster;
using FTS.Model.Entities;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FTS_Web.Controllers
{
  
    public class AppliantLoginController : Controller
    {
        public IApplicantLoginMasterBl _applicantLoginRepository;

        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _catchaOptions;
        public AppliantLoginController(IApplicantLoginMasterBl ApplicantLoginRepository, IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> catchaOptions)
        {
            this._applicantLoginRepository = ApplicantLoginRepository;
            _validatorService = validatorService;
            _catchaOptions = catchaOptions == null ? throw new ArgumentNullException(nameof(catchaOptions)) : catchaOptions.Value;
        }
        public IActionResult Index()

        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Applicant(ApplicantMasterModel ObjReglogin)
        {
            if (ModelState.IsValid)
            {
                if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                {
                    this.ModelState.AddModelError(_catchaOptions.CaptchaComponent.CaptchaInputName, "Plese enter the security code as number");
                    return View("Index");
                }
            }

            ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
            Applicantobj = _applicantLoginRepository.SaveApplicantLoginRecord(ObjReglogin);
            return Json(new { data = Applicantobj });
           

            ViewBag.errormessage = Applicantobj.ErrorMassage;


            return View("Index");
        }


    }
}

