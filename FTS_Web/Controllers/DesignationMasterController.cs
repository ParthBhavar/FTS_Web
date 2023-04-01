using FTS.Business.CommonList;
using FTS.Business.DesignationMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Kendo.Mvc.UI;
using Master.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class DesignationMasterController : Controller
    {
        public IDesignationMasterBI _Designationpository;
        public ICommonListBI _Commompository;

        public DesignationMasterController(IDesignationMasterBI _Designationpository , ICommonListBI commompository)
        {
            this._Designationpository = _Designationpository;
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
                    int totalrecord = 0;
                    var List = _Designationpository.DesignationList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.DesignationID.ToString());
                        }
                    }
                    totalrecord = List[0].TotalRecord;
                    return View(List);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
               _Commompository.LogErrorintbl(ex, "DesignationMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        public ActionResult AddDesignationMaster(string designationid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int DesignationID = 0;
                    if (designationid != null)
                    {
                        DesignationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(designationid));
                    }
                    DesignationMasterModel ClsBundleBreak = new DesignationMasterModel();
                    ClsBundleBreak = _Designationpository.DesignationRecord(DesignationID);
                    ClsBundleBreak.DesignationIDEdit = DesignationID;
                    return View("AddDesignationMaster", ClsBundleBreak);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "DesignationMasterController", "AddDesignationMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public JsonResult SaveDesignationRecord(DesignationMasterModel ObjDes)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    DesignationMasterModel ClsBundleBreak = new DesignationMasterModel();
                    ClsBundleBreak = _Designationpository.SaveDesignationRecord(ObjDes);
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
                _Commompository.LogErrorintbl(ex, "DesignationMasterController", "SaveDesignationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult DeleteDesignationRecord(int DesignationID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int UserID = 1;
                    DesignationMasterModel ClsBundleBreak = new DesignationMasterModel();
                    ClsBundleBreak = _Designationpository.DeleteDesignationRecord(UserID, DesignationID);
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
                _Commompository.LogErrorintbl(ex, "DesignationMasterController", "DeleteDesignationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            
        }
        }
    }
}
