using FTS.Business.CommonList;
using FTS.Business.ConcilliationActionMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ConcilliationActionMasterController : Controller
    {

        public IConcilliationActionMasterBl _ConcilliationActionpository;
        public ICommonListBI _Commompository;

        public ConcilliationActionMasterController(IConcilliationActionMasterBl _ConcilliationActionpository, ICommonListBI commompository)
        {

            this._ConcilliationActionpository = _ConcilliationActionpository;
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
                    var List = _ConcilliationActionpository.ConcilliationActionList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ActionID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ConcilliationActionMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        public ActionResult AddConcilliationActionMaster(string actionid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int ActionID = 0;
                    if (actionid != null)
                    {
                        ActionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(actionid));
                    }
                    ConcilliationActionMasterModel ClsConcilliationActionRecord = new ConcilliationActionMasterModel();
                    ClsConcilliationActionRecord = _ConcilliationActionpository.ConcilliationActionRecord(ActionID);
                    var RoleList = _Commompository.RoleList(ActionID);
                    ClsConcilliationActionRecord.Rolelist = RoleList;
                    ClsConcilliationActionRecord.ConcActionIDEdit = ActionID;
                    return View("AddConcilliationActionMaster", ClsConcilliationActionRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConcilliationActionMasterController", "AddConcilliationActionMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        public JsonResult SaveConcilliationActionRecord(ConcilliationActionMasterModel ObjConcAction)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    ConcilliationActionMasterModel ClssaveRecord = new ConcilliationActionMasterModel();
                    ClssaveRecord.UserID = 1;
                    ClssaveRecord = _ConcilliationActionpository.SaveConcilliationActionRecord(ObjConcAction);
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
                _Commompository.LogErrorintbl(ex, "ConcilliationActionMasterController", "SaveConcilliationActionRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult DeleteConcilliationActionRecord(int ActionID)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int UserID = 1;
                    ConcilliationActionMasterModel Clsdeleterecord = new ConcilliationActionMasterModel();
                    Clsdeleterecord = _ConcilliationActionpository.DeleteConcilliationActionRecord(UserID, ActionID);
                    return Json(new { data = Clsdeleterecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
                }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConcilliationActionMasterController", "DeleteConcilliationActionRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

    }
}
