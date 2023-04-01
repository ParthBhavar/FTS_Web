using FTS.Business.CommonList;
using FTS.Business.Profile;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ProfileController : Controller
    {

        public IProfileBI _Profilepository;
        public ICommonListBI _Commompository;
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
        public ProfileController(IProfileBI Profilepository, ICommonListBI commompository)
        {

            this._Profilepository = Profilepository;
        }
        public IActionResult ChangeEmployeePassword()
        {
            return View();
        }
        public IActionResult Index()
        {

            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null)
                {
                    EmployeeMasterModel ClsAppliactionlst = new EmployeeMasterModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    //ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    var List = _Profilepository.ProfileList(Convert.ToInt32(_ID));
                    //if (List.Count > 0)
                    //{
                    //    foreach (var item in List)
                    //    {
                    //        item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ApplicationID.ToString());
                    //    }
                    //}
                    //totalrecord = List[0].TotalRecord;
                    return View(List);
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ProfileController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeEmployeePassword(EmployeeMasterModel ObjReglogin)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null)
                {
                    EmployeeMasterModel Employeeobj = new EmployeeMasterModel();
                    ObjReglogin.UserID = Convert.ToInt32(_ID);
                    Employeeobj = _Profilepository.ChangeEmployeePassword(ObjReglogin);




                    ViewBag.errormessage = Employeeobj.ErrorMassage;

                    return View("ChangeEmployeePassword");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }

            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ProfileController", "ChangeEmployeePassword", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }


        }

}
}
