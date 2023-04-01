using FTS.Business.CommonList;
using FTS.Business.EmpPositionMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Master.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class EmpPositionMasterController : Controller
    {
        public IEmpPositionMasterBl _EmpPositionMasterpository;
        public ICommonListBI _Commompository;

        public EmpPositionMasterController(IEmpPositionMasterBl _EmpPositionMasterpository, ICommonListBI commompository)
        {
            this._EmpPositionMasterpository = _EmpPositionMasterpository;
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

                if (_ID != null && _ID != 0)
                {
                    PaginationRequest model = new PaginationRequest();
                    model.PageNumber = 1;
                    model.PageSize = 100;
                    model.SearchText = "";
                    //int totalrecord = 0;
                    var List = _EmpPositionMasterpository.EmpPositionMasterList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.EmpPosID.ToString());
                        }
                    }
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
                _Commompository.LogErrorintbl(ex, "EmpPositionMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        public ActionResult AddEmpPositionMaster(string emppositionid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int EmpPosID = 0;
                    if (emppositionid != null)
                    {
                        EmpPosID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(emppositionid));
                    }
                 
                    EmpPositionMasterModel ClsEmpPositionRecord = new EmpPositionMasterModel();
                    ClsEmpPositionRecord = _EmpPositionMasterpository.EmpPositionMasterRecord(EmpPosID);
                    var ParentPositionList = _Commompository.PositionList(EmpPosID);
                    var EmployeeList = _Commompository.EmployeeList(EmpPosID);
                    //var EmployeeData = _Commompository.EmployeeData(EmpPosID , ClsEmpPositionRecord.EmployeeID);
                    //var MenuList = _Commompository.MenuList(EmpPosID);
                    //ClsEmpPositionRecord.MenuList = MenuList;
                    ClsEmpPositionRecord.EmpPosID = EmpPosID;
                    //ClsEmpPositionRecord.EmployeeData = EmployeeData;
                    ClsEmpPositionRecord.EmpPosIDEdit = EmpPosID;
                    ClsEmpPositionRecord.PositionList = ParentPositionList;
                    ClsEmpPositionRecord.EmployeeList = EmployeeList;
                    ClsEmpPositionRecord.IP_Address = IP;
                    ClsEmpPositionRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClsEmpPositionRecord.EmpPosID.ToString());
                    return View("AddEmpPositionMaster", ClsEmpPositionRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EmpPositionMasterController", "AddEmpPositionMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        public JsonResult SaveEmpPositionMasterRecord(EmpPositionMasterModel ObjEmpPosition)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try {

                {
                    if (_ID != null && _ID != 0)
                    {
                        EmpPositionMasterModel Clssave = new EmpPositionMasterModel();
                        ObjEmpPosition.UserID = Convert.ToInt32(_ID);
                        Clssave = _EmpPositionMasterpository.SaveEmpPositionMasterRecord(ObjEmpPosition);
                        Clssave.TEMPID = Encrypt_Decrypt.Encrypt(Clssave.EmpPosID.ToString());
                        return Json(new { data = Clssave });
                    }
                    else
                    {
                        RedirectToAction("Index", "Home");
                        return Json(new { data = "" });
                    }
                }

            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EmpPositionMasterController", "SaveEmpPositionMasterRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


        public JsonResult DeleteEmpPositionMasterRecord(int EmpPosID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int UserID = 1;
                    EmpPositionMasterModel ClsBundleBreak = new EmpPositionMasterModel();
                    ClsBundleBreak = _EmpPositionMasterpository.DeleteEmpPositionMasterRecord(UserID, EmpPosID);
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
                _Commompository.LogErrorintbl(ex, "EmpPositionMasterController", "DeleteEmpPositionMasterRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult SaveAssignPositionRecord(EmpPositionMasterModel ObjEmpPosition)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {

                    ObjEmpPosition.UserID = Convert.ToInt32(_ID);

                    EmpPositionMasterModel ClssaveRecord = new EmpPositionMasterModel();
                    ClssaveRecord = _EmpPositionMasterpository.SaveAssignPositionRecord(ObjEmpPosition);
                    return Json(new { data = ClssaveRecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EmpPositionMasterController", "SaveAssignPositionRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


        public JsonResult EmployeeData(int mode, int EmployeeID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            try
            {
                if (_ID != null && _ID != 0)
                {
                    var List = _Commompository.EmployeeData(mode, EmployeeID);
                    return Json(new { data = List });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }

            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EmpPositionMaster", "EmployeeData", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }



    }
}
