using FTS.Business.CommonList;
using FTS.Business.Home;
using FTS.Model.Common;
using FTS.Model.Entities;
using DNTCaptcha.Core;
using Email;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]

    public class HomeController : Controller
    {
        public IHomeBI _homeRepository;
        const string SeID = "_ID";
        const string SeEmailID = "_EmailID";
        const string SeMobileNo = "_MoNo";
        const string SeGender = "_Gender";
        const string SeUserMode = "_UserMode";
        const string SeUserName = "_UserName";
        const string SeIP = "_IP";
        const string Seemail = "_email";


        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _catchaOptions;
        private readonly IEmailSender _emailSender;
        public ICommonListBI _Commompository;
        public HomeController(IHomeBI homeRepository, IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> catchaOptions, IEmailSender emailSender,ICommonListBI commompository)
        {
            this._homeRepository = homeRepository;
            _validatorService = validatorService;
            this._emailSender = emailSender;
            _Commompository = commompository;
            _catchaOptions = catchaOptions == null ? throw new ArgumentNullException(nameof(catchaOptions)) : catchaOptions.Value;
        }
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeApplicantPassword()

        {
            ApplicantMasterModel Employeeobj = new ApplicantMasterModel();
            return View("Applicantchangepassword", Employeeobj);
        }
        public IActionResult ForgotPassword(ApplicantMasterModel Applicantobj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                Random rand = new Random();
                /* HttpContext.Session.SetInt32("_EmpID", Employeeobj.EmployeeID)*/
                ;
                Applicantobj.OTP = Convert.ToString(rand.Next(100000, 999999));
                //var email = "parthbhavsar4u@gmail.com";
                //await _emailSender.SendEmailAsync(email, "Reset Password for COL Applcation", "OTP Is: 12345" );
                return View(Applicantobj);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "HomeController", "ForgotPassword", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult ResendEmailVerification(ApplicantMasterModel Applicantobj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                Random rand = new Random();
                Applicantobj.ApplicantOTP = (rand.Next(100000, 999999));
                return View(Applicantobj);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "HomeController", "ResendEmailVerification", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        public IActionResult SendMailApplicant(string EmailID, string Title, string Body)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                HttpContext.Session.SetString("OTP", Body);
                ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
                var test = _emailSender.SendEmailAsync(EmailID, Title, Body);
                Applicantobj.EncrptmailID = Encrypt_Decrypt.Encrypt(EmailID);
                return Json(new { data = Applicantobj });
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "HomeController", "SendMailApplicant", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult ReSendMailApplicant(string EmailID, string Title, string Body)
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
                _Commompository.LogErrorintbl(ex, "HomeController", "ReSendMailApplicant", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult ResendVerifyOTP(string ID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
                Applicantobj.EmailID = ID;
                return View("ResendVerifyOTP", Applicantobj);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "HomeController", "ResendVerifyOTP", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public IActionResult VerifyOTP(string ID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                ApplicantMasterModel Applicantobj = new ApplicantMasterModel();

                Applicantobj.EmailID = Encrypt_Decrypt.Decrypt(ID);

                HttpContext.Session.SetString("email", Applicantobj.EmailID);
                return View("VerifyOTP", Applicantobj);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "HomeController", "VerifyOTP", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult User(ApplicantMasterModel ObjReglogin)
       {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                ViewBag.wrongcaptcha = 0;
                if (!ModelState.IsValid)
                {
                    if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                    {
                        ViewBag.wrongcaptcha = 1;
                        this.ModelState.AddModelError(_catchaOptions.CaptchaComponent.CaptchaInputName, "Plese enter the security code as number");
                        return View("Index");
                    }
                }
                ApplicantMasterModel Userobj = new ApplicantMasterModel();
                Userobj = _homeRepository.SaveUserLoginRecord(ObjReglogin);

                if (Userobj.ErrorCode == 2 && Userobj.Islocked < 3)
                {

                    ApplicantMasterModel Userbj = new ApplicantMasterModel();
                    string LastLoginTime = $"{DateTime.Now}";
                    Userbj.IPAddress = IP;
                    Userbj.Islocked = Userobj.Islocked + 1;
                    Userbj.ApplicantID = Userobj.ApplicantID;
                    Userbj.LastLoginTime = Convert.ToDateTime(LastLoginTime);
                    _homeRepository.UpdateIslockedflageApp(Userbj);
                }

                if (Userobj.ErrorCode == 0)
                {
                    HttpContext.Session.SetInt32("_ID", Userobj.ApplicantID);
                    HttpContext.Session.SetString("_EmailID", Userobj.EmailID);
                    HttpContext.Session.SetString("_UserName", Userobj.Name);
                    HttpContext.Session.SetString("_MoNo", Userobj.MobileNo);
                    HttpContext.Session.SetInt32("_Gender", Userobj.Gender);
                    HttpContext.Session.SetInt32("_UserMode", 2);
                    HttpContext.Session.SetString("_IP", IP);
                    _Commompository.Applicantloginlogout(Userobj.ApplicantID, "Login", IP);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.errormessage = Userobj.ErrorMassage;
                    return View("Index");
                }
        }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "HomeController", "User", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
    }

}
        public IActionResult Logout()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {


                HttpContext.Session.SetInt32("_ID", 0);
                HttpContext.Session.SetInt32("_DesID", 0);
                HttpContext.Session.SetString("_EmailID", "");
                HttpContext.Session.SetString("_MoNo", "");
                HttpContext.Session.SetInt32("_Gender", 0);
                HttpContext.Session.SetInt32("_UserMode", 0);
                HttpContext.Session.SetInt32("_PositionID", 0);
                HttpContext.Session.SetInt32("_RoleID", 0);
                HttpContext.Session.SetInt32("_RegionID", 0);
                HttpContext.Session.SetInt32("_BranchID", 0);
                HttpContext.Session.SetInt32("_ZoneID", 0);
                HttpContext.Session.SetInt32("_DistrictID", 0);
                HttpContext.Session.SetInt32("_TalukaID", 0);
                HttpContext.Session.SetInt32("_EmpPosID", 0);
                HttpContext.Session.SetInt32("_IsDashoardList", 0);
                HttpContext.Session.Clear();
                HttpContext.SignOutAsync();
                    //officeloginlogouttable 
                if(_UserMode == 1)
                {
                    _Commompository.Officeloginlogout(Convert.ToInt16(_ID), "Logout", IP);
                }
                else
                {
                    _Commompository.Applicantloginlogout(Convert.ToInt16(_ID), "Logout", IP);
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "HomeController", "Logout", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }


        }

        public JsonResult CheckVerifyOTP(ApplicantMasterModel Applicantobj)
        {

            var OTP = HttpContext.Session.GetString("OTP");
            if (OTP == Applicantobj.OTP)
            {
                return Json(1);
                var mail = HttpContext.Session.GetString("email");

            }
            else
            {
                return Json(0);
            }

        }

        public JsonResult ResendCheckVerifyOTP(ApplicantMasterModel Applicantobj)
        {
            Applicantobj = _homeRepository.UserReVerifyOTP(Applicantobj);

            return Json(new { data = Applicantobj });

        }

        public JsonResult ChangeApplicantPassword1(ApplicantMasterModel Objreg)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
           
                ApplicantMasterModel ApplicantObj = new ApplicantMasterModel();
                Objreg.UserID = Convert.ToInt32(_ID);
                ApplicantObj = _homeRepository.ChangeApplicantPassword(Objreg);
               ViewBag.errormessage = ApplicantObj.ErrorMassage;
            return Json(new { data = ApplicantObj });

        }

        public JsonResult SaveApplicantForgotPasswordRecord(ApplicantMasterModel ObjFrgtPwd)
        {
            ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
            HttpContext.Session.SetString("_email", ObjFrgtPwd.EmailID);
            Applicantobj = _homeRepository.SaveApplicantForgotPasswordRecord(ObjFrgtPwd);


            return Json(new { data = Applicantobj });
        }
        public JsonResult SaveApplicantResendEmailRecord(ApplicantMasterModel ObjFrgtPwd)
        {
            ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
            Applicantobj = _homeRepository.SaveApplicantResendEmailRecord(ObjFrgtPwd);
            return Json(new { data = Applicantobj });
        }
        public JsonResult ChangePasswordApplicantForgotPassword(ApplicantMasterModel ObjFrgtPwd)
        {
            ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
            //string mail = 
            ObjFrgtPwd.EmailID = HttpContext.Session.GetString("_email");
            Applicantobj = _homeRepository.ChangePasswordApplicantForgotPassword(ObjFrgtPwd);

            return Json(new { data = Applicantobj });

        }
    }
}
