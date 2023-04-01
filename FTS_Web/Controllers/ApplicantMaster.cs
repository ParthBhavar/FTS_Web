using Microsoft.AspNetCore.Mvc;

using COL.Business.ApplicantMaster;
using COL.Data.ApplicantMaster;
using COL.Model.Entities;
using Master.ViewModel;

namespace Col_Web.Controllers
{
    public class ApplicantMaster : Controller
    {
        public IApplicantMasterBl _applicantRepository;
        public ApplicantMaster(IApplicantMasterBl ApplicantRepository)
        {
            this._applicantRepository = ApplicantRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public ActionResult AddBranchMaster(int BranchID)
        //{
        //    ApplicantMasterModel branchobj = new ApplicantMasterModel();
        //    branchobj = _applicantRepository.GetBranchRecord(BranchID);
        //    return View("AddBranchMaster", branchobj);
        //}

        public JsonResult SaveUserRegisterRecord(ApplicantMasterModel ObjReg)
        {
            ApplicantMasterModel Applicantobj = new ApplicantMasterModel();
            Applicantobj = _applicantRepository.SaveUserRegisterRecord(ObjReg);
            return Json(new { data = Applicantobj });
        }
    }
}
