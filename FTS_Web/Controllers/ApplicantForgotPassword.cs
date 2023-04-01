using FTS.Business.ApplicantForgotPassword;
using FTS.Model.Common;
using FTS.Model.Entities;
using Dapper;
using Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTS_Web.Controllers
{
    public class ApplicantForgotPassword : Controller
    {
        public IApplicantForgotPasswordBl _applicantForgotPasswordRepository;
        private readonly IEmailSender _emailSender;
        public ApplicantForgotPassword(IApplicantForgotPasswordBl ApplicantForgotPasswordRepository, IEmailSender emailSender)
        {
            this._applicantForgotPasswordRepository = ApplicantForgotPasswordRepository;
            this._emailSender=emailSender;
        }
        public IActionResult Index(ApplicantMasterModel Applicantobj)
        {

            Random rand = new Random();
            /* HttpContext.Session.SetInt32("_EmpID", Employeeobj.EmployeeID)*/
            ;
            Applicantobj.OTP = Convert.ToString(rand.Next(100000, 999999));
            //var email = "parthbhavsar4u@gmail.com";
            //await _emailSender.SendEmailAsync(email, "Reset Password for COL Applcation", "OTP Is: 12345" );
            return View(Applicantobj);
        }

       
        public IActionResult SendMailApplicant(string EmailID,string Title,string Body)
        {
            HttpContext.Session.SetString("OTP", Body);
            ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
            var test =  _emailSender.SendEmailAsync(EmailID, Title, Body);
            Applicantobj.EncrptmailID = Encrypt_Decrypt.Encrypt(EmailID);
            return Json(new { data = Applicantobj });

        }
        public IActionResult VerifyOTP(string ID)
        {
            ApplicantMasterModel Applicantobj = new ApplicantMasterModel();

            Applicantobj.EmailID = Encrypt_Decrypt.Decrypt(ID);

            HttpContext.Session.SetString("email", Applicantobj.EmailID);
            return View("VerifyOTP", Applicantobj);

        }

        [HttpPost]
        public JsonResult CheckVerifyOTP(ApplicantMasterModel Applicantobj)
        {  
           
            var OTP = HttpContext.Session.GetString("OTP");
            if (OTP == Applicantobj.OTP)
            { 
                return Json(1);
                var mail= HttpContext.Session.GetString("email");

            }
            else
            {
                return Json(0);
            }          

        }      

        public JsonResult SaveApplicantForgotPasswordRecord(ApplicantMasterModel ObjFrgtPwd)
        {
            ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
            HttpContext.Session.SetString("email", ObjFrgtPwd.EmailID);
            Applicantobj = _applicantForgotPasswordRepository.SaveApplicantForgotPasswordRecord(ObjFrgtPwd);
           
          
            return Json(new { data = Applicantobj });
        }

        public JsonResult ChangePasswordApplicantForgotPassword(ApplicantMasterModel ObjFrgtPwd)
        {
            ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
            //string mail = 
            ObjFrgtPwd.EmailID = HttpContext.Session.GetString("email");
            Applicantobj = _applicantForgotPasswordRepository.ChangePasswordApplicantForgotPassword(ObjFrgtPwd);
           
            return Json(new { data = Applicantobj });
        }
        
    }
}
