using FTS.Business.CommonList;
using FTS.Business.ReinstatementActionMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Logger;
using Microsoft.AspNetCore.Mvc;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ReinstatementActionMasterController : Controller
    {

        public IReinstatementActionMasterBl _ReinstatemementActionpository;
        public ICommonListBI _Commompository;
        private ILogging _logger;

        public ReinstatementActionMasterController(IReinstatementActionMasterBl _ReinstatemementActionpository, ICommonListBI commompository, ILogging logger)
        {

            this._ReinstatemementActionpository = _ReinstatemementActionpository;
            _Commompository = commompository;
            _logger = logger;
        }


        public IActionResult Index()
        {
            PaginationRequest model = new PaginationRequest();
            model.PageNumber = 1;
            model.PageSize = 100;
            model.SearchText = "";
            //int totalrecord = 0;
            var List = _ReinstatemementActionpository.ReinstatementActionList();
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


        public ActionResult AddReinstatementActionMaster(string actionid)
        {
            int ActionID = 0;
            if (actionid != null)
            {
                ActionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(actionid));
            }
            ReinstatementActionMasterModel ClsReinstatementActionRecord = new ReinstatementActionMasterModel();
            ClsReinstatementActionRecord = _ReinstatemementActionpository.ReinstatementActionRecord(ActionID);
            var RoleList = _Commompository.RoleList(ActionID);           
            ClsReinstatementActionRecord.Rolelist = RoleList;           
            ClsReinstatementActionRecord.ReinstatementActionIDEdit = ActionID;
            return View("AddReinstatementActionMaster", ClsReinstatementActionRecord);
        }

        public JsonResult SaveReinstatementActionRecord(ReinstatementActionMasterModel ObjAction)
        {
            ReinstatementActionMasterModel ClssaveRecord = new ReinstatementActionMasterModel();
            ClssaveRecord.UserID = 1;
            ClssaveRecord = _ReinstatemementActionpository.SaveReinstatementActionRecord(ObjAction);
            return Json(new { data = ClssaveRecord });
        }

        public JsonResult DeleteReinstatementActionRecord(int ActionID)
        {
            int UserID = 1;
            ReinstatementActionMasterModel Clsdeleterecord = new ReinstatementActionMasterModel();
            Clsdeleterecord = _ReinstatemementActionpository.DeleteReinstatementActionRecord(UserID, ActionID);
            return Json(new { data = Clsdeleterecord });
        }
    }
}
