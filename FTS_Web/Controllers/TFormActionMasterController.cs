using FTS.Business.CommonList;
using FTS.Business.TFormActionMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class TFormActionMasterController : Controller
    {
        public ITFormActionMasterBl _TFormActionpository;
        public ICommonListBI _Commompository;

        public TFormActionMasterController(ITFormActionMasterBl _TFormActionpository, ICommonListBI commompository)
        {

            this._TFormActionpository = _TFormActionpository;
            _Commompository = commompository;
        }

        public IActionResult Index()
        {
            PaginationRequest model = new PaginationRequest();
            model.PageNumber = 1;
            model.PageSize = 100;
            model.SearchText = "";
            //int totalrecord = 0;
            var List = _TFormActionpository.TFormActionList();
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

        public ActionResult AddTFormActionMaster(string actionid)
        {
            int ActionID = 0;
            if (actionid != null)
            {
                ActionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(actionid));
            }
            TFormActionMasterModel ClsTFormActionRecord = new TFormActionMasterModel();
            ClsTFormActionRecord = _TFormActionpository.TFormActionRecord(ActionID);
            var RoleList = _Commompository.RoleList(ActionID);
            ClsTFormActionRecord.Rolelist = RoleList;
            ClsTFormActionRecord.TFormActionIDEdit = ActionID;
            return View("AddTFormActionMaster", ClsTFormActionRecord);
        }

        public JsonResult SaveTFormActionRecord(TFormActionMasterModel ObjTFormAction)
        {
            TFormActionMasterModel ClssaveRecord = new TFormActionMasterModel();
            ClssaveRecord.UserID = 1;
            ClssaveRecord = _TFormActionpository.SaveTFormActionRecord(ObjTFormAction);
            return Json(new { data = ClssaveRecord });
        }

        public JsonResult DeleteTFormActionRecord(int ActionID)
        {
            int UserID = 1;
            TFormActionMasterModel Clsdeleterecord = new TFormActionMasterModel();
            Clsdeleterecord = _TFormActionpository.DeleteTFormActionRecord(UserID, ActionID);
            return Json(new { data = Clsdeleterecord });
        }
    }
}
