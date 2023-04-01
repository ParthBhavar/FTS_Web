using FTS.Business.CommonList;
using FTS.Business.EmployeeMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Master.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class EmployeeMasterController : Controller
    {

        public IEmployeeMasterBI _EmployeeMasterRepository;
        public ICommonListBI _Commompository;

        public EmployeeMasterController(IEmployeeMasterBI _EmployeeMasterRepository, ICommonListBI commompository)
        {
            this._EmployeeMasterRepository = _EmployeeMasterRepository;
            _Commompository = commompository; 
        }
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
        public IActionResult Index()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {


                PaginationRequest model = new PaginationRequest();
                model.PageNumber = 1;
                model.PageSize = 100;
                model.SearchText = "";
                int totalrecord = 0;
                var List = _EmployeeMasterRepository.EmployeeMasterList();
                if (List.Count > 0)
                {
                    foreach (var item in List)
                    {
                        item.EncryptedId = Encrypt_Decrypt.Encrypt(item.EmployeeID.ToString());
                    }
                }
                //totalrecord = List[0].TotalRecord;
                return View(List);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EmployeeMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }






        }
            //[HttpPost]

            //public JsonResult Index(int EmployeeID)
            //{
            //    var List = _EmployeeMasterRepository.EmployeeMasterList(EmployeeID);
            //    return Json(new { data = List });
            //}


     public ActionResult AddEmployeeMaster(string employeeid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int EmployeeID = 0;
                
                //string Password = "";
                if (employeeid != null)
                {
                    EmployeeID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(employeeid));
                }
                EmployeeMasterModel ClsBundleBreak = new EmployeeMasterModel();
                ClsBundleBreak = _EmployeeMasterRepository.EmployeeRecord(EmployeeID);
                var DesignationList = _Commompository.DesignationList(EmployeeID);
                ClsBundleBreak.DesignationList = DesignationList;
                var RegionList = _Commompository.RegionList(EmployeeID);
                ClsBundleBreak.Regionlist = RegionList;
                var GanderList = _Commompository.GanderList();
                ClsBundleBreak.Ganderlist = GanderList;
                ClsBundleBreak.EmployeeIDEdit = EmployeeID;
                return View("AddEmployeeMaster", ClsBundleBreak);
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EmployeeMasterController", "AddEmployeeMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }


        }
            
        public JsonResult SaveEmployeeRecord(EmployeeMasterModel ObjEmp)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    EmployeeMasterModel ClsBundleBreak = new EmployeeMasterModel();
                ClsBundleBreak = _EmployeeMasterRepository.SaveEmployeeRecord(ObjEmp);
                return Json(new { data = ClsBundleBreak });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EmployeeMasterController", "SaveEmployeeRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }


        }
        public JsonResult DeleteEmployeeRecord(int EmployeeID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int UserID = 1;
                    EmployeeMasterModel ClsBundleBreak = new EmployeeMasterModel();
                    ClsBundleBreak = _EmployeeMasterRepository.DeleteEmployeeRecord(UserID, EmployeeID);
                    return Json(new { data = ClsBundleBreak });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EmployeeMasterController", "DeleteEmployeeRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
