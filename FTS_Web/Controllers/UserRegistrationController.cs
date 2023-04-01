using Microsoft.AspNetCore.Mvc;
using FTS.Business.UserRegistration;
using FTS.Data.UserRegistration;
using FTS.Model.Entities;
using Master.ViewModel;
using DNTCaptcha.Core;
using Microsoft.Extensions.Options;
using System.Net;
using FTS.Business.CommonList;
using FTS.Model.Common;
using Email;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class UserRegistrationController : Controller
    {
        public IUserRegistrationBl _userRegistrationRepository;
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _catchaOptions;
        public ICommonListBI _Commompository;
        private readonly IEmailSender _emailSender;
        public UserRegistrationController(IUserRegistrationBl UserRegistrationRepository, IDNTCaptchaValidatorService validatorService, IEmailSender emailSender, IOptions<DNTCaptchaOptions> catchaOptions , ICommonListBI commompository)
        {
            this._userRegistrationRepository = UserRegistrationRepository;
            _validatorService = validatorService;
            this._emailSender = emailSender;
            _catchaOptions = catchaOptions == null ? throw new ArgumentNullException(nameof(catchaOptions)) : catchaOptions.Value;
            _Commompository = commompository;
        }
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
        public IActionResult Index()
        {
            ApplicantMasterModel ObjReg = new ApplicantMasterModel();
            Random rand = new Random();
            ObjReg.ApplicantOTP = (rand.Next(100000, 999999));
            return View(ObjReg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Index(ApplicantMasterModel ObjReg)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            

            if (!ModelState.IsValid)
                {
                    if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                    {
                        this.ModelState.AddModelError(_catchaOptions.CaptchaComponent.CaptchaInputName, "Plese enter the security code as number");
                        return View(ObjReg);
                    }
                }
              
            try
            {
                ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
                Applicantobj = _userRegistrationRepository.SaveUserRegisterRecord(ObjReg);
                ViewBag.errormessage = Applicantobj.ErrorMassage;


                //return Json(new { data = Applicantobj });
               
                return View("Index");
            }
            catch (Exception ex)
            {

                _Commompository.LogErrorintbl(ex, "UserRegistrationController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }


            //if (Applicantobj.ErrorCode == 0)
            //{
            //    return RedirectToAction("Index", "Dashboard");
            //}
            //else
            //{
            //    ViewBag.errormessage = Applicantobj.ErrorMassage;
            //    return View("Index");
            //}
            //ViewBag.errormessage = Employeeobj.ErrorMassage;
            //return Json(new { data = Employeeobj });


        }
        public JsonResult SendMailApplicant(string EmailID, string Title, string Body)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
               
                ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
                var test = _emailSender.SendEmailAsync(EmailID, Title, Body);
                Applicantobj.EmailID = EmailID;
                return Json(new { data = Applicantobj });
            }
            catch (Exception ex)
            {
                //_Commompository.LogErrorintbl(ex, "HomeController", "SendMailApplicant", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                //return StatusCode(500, ex.Message);
                throw ex;
            }

        }

        public IActionResult VerifyOTP(string ID)
        //public IActionResult VerifyOTP(ApplicantMasterModel ObjApp)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                ApplicantMasterModel Applicantobj = new ApplicantMasterModel();

               
                Applicantobj.EmailID = ID;


                return View("VerifyOTP", Applicantobj);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "HomeController", "VerifyOTP", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public JsonResult CheckVerifyOTP(ApplicantMasterModel Applicantobj)
        {
            Applicantobj = _userRegistrationRepository.UserVerifyOTP(Applicantobj);
            
            return Json(new { data = Applicantobj });

        }
        public IActionResult SaveUserRegisterRecord(ApplicantMasterModel ObjApp)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                ApplicantMasterModel ClsBundleBreak = new ApplicantMasterModel();
                ClsBundleBreak = _userRegistrationRepository.SaveUserRegisterRecord(ObjApp);
                //var test = _emailSender.SendEmailAsync(ObjApp.EmailID, "Verified Email for COL Registratation", Convert.ToString(ObjApp.ApplicantOTP));
                ClsBundleBreak.EmailID = ObjApp.EmailID;
                //return RedirectToAction("VerifyOTP", ClsBundleBreak);
                //RedirectToAction("VerifyOTP", new {ID = ClsBundleBreak.EncrptmailID });
                return Json(new { data = ClsBundleBreak });
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "UserRegistrationController", "SaveUserRegisterRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }



    }
}
