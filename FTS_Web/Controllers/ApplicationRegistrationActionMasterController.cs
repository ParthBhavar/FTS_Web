using FTS.Business.ApplicationRegistrationActionMaster;
using FTS.Business.CommonList;
using FTS.Model.Common;
using FTS.Model.Entities;
using Logger;
using Microsoft.AspNetCore.Mvc;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ApplicationRegistrationActionMasterController : Controller
    {

        public IApplicationregistratationActionMasterBl _ApplicationregistratationActionpository;
        public ICommonListBI _Commompository;
        private ILogging _logger;

        public ApplicationRegistrationActionMasterController(IApplicationregistratationActionMasterBl _ApplicationregistratationActionpository, ICommonListBI commompository, ILogging logger)
        {

            this._ApplicationregistratationActionpository = _ApplicationregistratationActionpository;
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
            var List = _ApplicationregistratationActionpository.ApplicationRegistratationActionList();
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


        public ActionResult AddApplicationRegistrationActionMaster(string actionid)
        {
            int ActionID = 0;
            if (actionid != null)
            {
                ActionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(actionid));
            }
            ApplicationRegistrationActionMasterModel ClsApplicationRegistrationActionRecord = new ApplicationRegistrationActionMasterModel();
            ClsApplicationRegistrationActionRecord = _ApplicationregistratationActionpository.ApplicationRegistratationActionRecord(ActionID);
            var RoleList = _Commompository.RoleList(ActionID);
            ClsApplicationRegistrationActionRecord.Rolelist = RoleList;
            ClsApplicationRegistrationActionRecord.CanEdit = ActionID;
            return View("AddApplicationRegistrationActionMaster", ClsApplicationRegistrationActionRecord);
        }

        public JsonResult SaveAppRegActionRecord(ApplicationRegistrationActionMasterModel ObjAction)
        {
            ApplicationRegistrationActionMasterModel ClssaveRecord = new ApplicationRegistrationActionMasterModel();
            ClssaveRecord.UserID = 1;
            ClssaveRecord = _ApplicationregistratationActionpository.SaveAppRegActionRecord(ObjAction);
            return Json(new { data = ClssaveRecord });
        }

        public JsonResult DeleteAppREgActionRecord(int ActionID)
        {
            int UserID = 1;
            ApplicationRegistrationActionMasterModel Clsdeleterecord = new ApplicationRegistrationActionMasterModel();
            Clsdeleterecord = _ApplicationregistratationActionpository.DeleteAppREgActionRecord(UserID, ActionID);
            return Json(new { data = Clsdeleterecord });
        }
    }
   
}
