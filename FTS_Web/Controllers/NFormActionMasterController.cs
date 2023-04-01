using FTS.Business.CommonList;
using FTS.Business.NFormActionMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class NFormActionMasterController : Controller
    {

        public INFormActionMasterBl _NFormActionpository;
        public ICommonListBI _Commompository;

        public NFormActionMasterController(INFormActionMasterBl _NFormActionpository, ICommonListBI commompository)
        {

            this._NFormActionpository = _NFormActionpository;
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
                var List = _NFormActionpository.NFormActionList();
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
                _Commompository.LogErrorintbl(ex, "NFormActionMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        public ActionResult AddNFormActionMaster(string actionid)
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
                NFormActionMasterModel ClsNFormActionRecord = new NFormActionMasterModel();
                ClsNFormActionRecord = _NFormActionpository.NFormActionRecord(ActionID);
                var RoleList = _Commompository.RoleList(ActionID);
                ClsNFormActionRecord.Rolelist = RoleList;
                ClsNFormActionRecord.NFormIDEdit = ActionID;
                return View("AddNFormActionMaster", ClsNFormActionRecord);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "NFormActionMasterController", "AddNFormActionMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public JsonResult SaveNFormActionRecord(NFormActionMasterModel ObjAction)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                NFormActionMasterModel ClssaveRecord = new NFormActionMasterModel();
                ClssaveRecord.UserID = 1;
                ClssaveRecord = _NFormActionpository.SaveNFormActionRecord(ObjAction);
                return Json(new { data = ClssaveRecord });
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "NFormActionMasterController", "SaveNFormActionRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public JsonResult DeleteNFormActionRecord(int ActionID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                int UserID = 1;
                NFormActionMasterModel Clsdeleterecord = new NFormActionMasterModel();
                Clsdeleterecord = _NFormActionpository.DeleteNFormActionRecord(UserID, ActionID);
                return Json(new { data = Clsdeleterecord });
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "NFormActionMasterController", "DeleteNFormActionRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
