using FTS.Business.ApplicantProfile;
using FTS.Business.CommonList;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FTS_Web.Controllers
{
    public class ApplicantProfileController : Controller
    {
        public IApplicantProfileBl _ApplicantProfileRepository;
        public ICommonListBI _Commompository;
        public ApplicantProfileController(IApplicantProfileBl applicantprofileRepository, ICommonListBI commompository)
        {
            this._ApplicantProfileRepository = applicantprofileRepository;
            _Commompository = commompository;
        }

        public IActionResult Index()
        { 
          
            return View();
        }

        public IActionResult ChangeApplicantPassword()
        {
            return View();
        }

        public ActionResult GetUpdatedUser(string applicantid)
        {
            int ApplicantID = 0;
            if (applicantid != null)
            {
                ApplicantID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(applicantid));
            }
            ApplicantMasterModel ClsUSerRecord = new ApplicantMasterModel();
            ClsUSerRecord = _ApplicantProfileRepository.GetuserRecord(ApplicantID);
            //ClsUSerRecord.RegionIDEdit = RegionID;
            return View("SaveUserRecord", ClsUSerRecord);
        }


        //[HttpPost]
        //public IActionResult Index(ApplicantMasterModel ObjAP)
        //{
        //    ApplicantMasterModel ApplivantObj = new ApplicantMasterModel();
        //    var EmailID = HttpContext.Session.GetString("EmailID");
        //    EmailID = ObjAP.EmailID;
        //    var MobileNo = HttpContext.Session.GetString("MobileNo");
        //    MobileNo = ObjAP.MobileNo;

        //    return View();
        //}
        //public ActionResult GetuserUpdatedprofile(string applicantid)
        //{
        //    int ApplicantID = 0;
        //    if (applicantid != null)
        //    {
        //        ApplicantID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(applicantid));
        //    }
        //    ApplicantMasterModel ClsUserRecord = new ApplicantMasterModel();
        //    ClsUserRecord = _ApplicantProfileRepository.GetuserRecord(ApplicantID);
        //    //ClsUserRecord.userIDEdit = ApplicantID;
        //    return View("AddRegionMaster", ClsUserRecord);
        //}

        public JsonResult SaveuserRecord(ApplicantMasterModel Objreg)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
         
                Objreg.ApplicantID = Convert.ToInt32(_ID);
                ApplicantMasterModel ClsBundleBreak = new ApplicantMasterModel();
                ClsBundleBreak = _ApplicantProfileRepository.SaveuserRecord(Objreg);          
       
                 return Json(new { data = ClsBundleBreak });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeApplicantPassword(ApplicantMasterModel Objreg)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            if (_ID != null)
            {
                ApplicantMasterModel ApplicantObj = new ApplicantMasterModel();
                Objreg.UserID = Convert.ToInt32(_ID);
                ApplicantObj = _ApplicantProfileRepository.ChangeApplicantPassword(Objreg);




                ViewBag.errormessage = ApplicantObj.ErrorMassage;

                return View("ChangeApplicantPassword");
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }
    }
}
