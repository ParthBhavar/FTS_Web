using FTS.Business.CommonList;
using FTS.Business.LicenceActionMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class LicenceActionMasterController : Controller
    {
        public ILicenceActionMasterBl _LicenceActionpository;
        public ICommonListBI _Commompository;
        public LicenceActionMasterController(ILicenceActionMasterBl _LicenceActionpository, ICommonListBI commompository)
        {

            this._LicenceActionpository = _LicenceActionpository;
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
                //int totalrecord = 0;
                var List = _LicenceActionpository.licenceActionList();
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
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "LicenceActionMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public ActionResult AddLicenceActionMaster(string actionid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                int ActionID = 0;
                if (actionid != null)
                {
                    ActionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(actionid));
                }
                LicenceActionMasterModel ClsLicenceActionRecord = new LicenceActionMasterModel();
                ClsLicenceActionRecord = _LicenceActionpository.licenceActionRecord(ActionID);
                var RoleList = _Commompository.RoleList(ActionID);
                ClsLicenceActionRecord.Rolelist = RoleList;
                ClsLicenceActionRecord.CanEdit = ActionID;
                return View("AddlicenceActionMaster", ClsLicenceActionRecord);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "LicenceActionMasterController", "AddLicenceActionMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }


        public JsonResult SaveLicenceActionRecord(LicenceActionMasterModel ObjAction)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                LicenceActionMasterModel ClssaveRecord = new LicenceActionMasterModel();
                ClssaveRecord.UserID = 1;
                ClssaveRecord = _LicenceActionpository.SavelicenceActionRecord(ObjAction);
                return Json(new { data = ClssaveRecord });
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "LicenceActionMasterController", "SaveLicenceActionRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }


        public JsonResult DeleteLicenceActionRecord(int ActionID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                int UserID = 1;
                LicenceActionMasterModel Clsdeleterecord = new LicenceActionMasterModel();
                Clsdeleterecord = _LicenceActionpository.DeletelicenceActionRecord(UserID, ActionID);
                return Json(new { data = Clsdeleterecord });
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "LicenceActionMasterController", "DeleteLicenceActionRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
