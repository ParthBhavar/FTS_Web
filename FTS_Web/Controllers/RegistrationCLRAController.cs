using FTS.Business.CommonList;
using FTS.Business.RegistrationCLRA;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class RegistrationCLRAController : Controller
    {

        public IRegistrationCLRABl _RegistrationCLRApository;
        public ICommonListBI _Commompository;

        public RegistrationCLRAController(IRegistrationCLRABl _RegistrationCLRApository, ICommonListBI commompository)
        {
            this._RegistrationCLRApository = _RegistrationCLRApository;
            _Commompository = commompository;
        }
        public IActionResult Index()
        {
            PaginationRequest model = new PaginationRequest();
            model.PageNumber = 1;
            model.PageSize = 100;
            model.SearchText = "";
            int totalrecord = 0;
            var List = _RegistrationCLRApository.RegistrationCLRAList();
            if (List.Count > 0)
            {
                foreach (var item in List)
                {
                    item.EncryptedId = Encrypt_Decrypt.Encrypt(item.RegistrationID.ToString());
                }
            }
            totalrecord = List[0].TotalRecord;
            return View(List);

        }
        public ActionResult AddRegistrationCLRA(string registrationid)
        {
            int RegistrationID = 0;
            if (registrationid != null)
            {
                RegistrationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(registrationid));
            }
            RegistrationCLRAModel ClsRegistrationCLRARecord = new RegistrationCLRAModel();
            ClsRegistrationCLRARecord = _RegistrationCLRApository.RegistrationCLRARecord(RegistrationID);
            
            var TalukaList = _Commompository.TalukaList(RegistrationID, ClsRegistrationCLRARecord.DistrictID);     
            var DistrictList = _Commompository.DistrictList(RegistrationID);
            ClsRegistrationCLRARecord.Talukalist = TalukaList;
            ClsRegistrationCLRARecord.Districtlist = DistrictList;
            ClsRegistrationCLRARecord.RegistrationIDEdit = RegistrationID;
            return View("AddRegistrationCLRA", ClsRegistrationCLRARecord);

        }

        public JsonResult TalukaList(int mode, int DistrictID)
        {
            var List = _Commompository.TalukaList(mode, DistrictID);
            return Json(new { data = List });
        }
        public JsonResult AllList(int mode, int DistrictID)
        {
            ConciliationApplicationModel List = new ConciliationApplicationModel();
            List.Talukalist = _Commompository.TalukaList(mode, DistrictID);
            return Json(new { data = List });
        }

        public JsonResult SaveRegistrationCLRA(RegistrationCLRAModel Objregclra)
        {
            RegistrationCLRAModel ClsBundleBreak = new RegistrationCLRAModel();
            ClsBundleBreak = _RegistrationCLRApository.SaveRegistrationCLRA(Objregclra);
            return Json(new { data = ClsBundleBreak });
        }
    }
}
