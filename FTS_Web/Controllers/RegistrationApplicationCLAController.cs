using COL.Business.CommonList;
using COL.Business.RegistrationApplicationCLA;
using COL.Model.Common;
using COL.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Col_Web.Controllers
{
    public class RegistrationApplicationCLAController : Controller
    {
        public IRegistrationApplicationCLABI _RegistrationApplicationCLApository;
        public ICommonListBI _Commompository;

        public RegistrationApplicationCLAController(IRegistrationApplicationCLABI _RegistrationApplicationCLApository, ICommonListBI commompository)
        {
            this._RegistrationApplicationCLApository = _RegistrationApplicationCLApository;
            _Commompository = commompository;
        }

        public IActionResult Index()
        {
            PaginationRequest model = new PaginationRequest();
            model.PageNumber = 1;
            model.PageSize = 100;
            model.SearchText = "";
            int totalrecord = 0;
            var List = _RegistrationApplicationCLApository.RegistrationApplicationCLAList();
            if (List.Count > 0)
            {
                foreach (var item in List)
                {
                    item.EncryptedId = Encrypt_Decrypt.Encrypt(item.RegistrationCLAID.ToString());
                }
            }
            totalrecord = List[0].TotalRecord;
            return View(List);
        }

        public ActionResult AddRegistrationApplicationCLA(string registrationclaid)
        {
            int RegistrationCLAID = 0;
            if (registrationclaid != null)
            {
                RegistrationCLAID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(registrationclaid));
            }
            RegistrationApplicationCLAModel ClsRegistrationApplicationCLARecord = new RegistrationApplicationCLAModel();
            ClsRegistrationApplicationCLARecord = _RegistrationApplicationCLApository.RegistrationApplicationCLARecord(RegistrationCLAID);
            
            var TalukaList = _Commompository.TalukaList(RegistrationCLAID, ClsRegistrationApplicationCLARecord.DistrictID);
            var DistrictList = _Commompository.DistrictList(RegistrationCLAID);
            var AreaList = _Commompository.AreaList(RegistrationCLAID, ClsRegistrationApplicationCLARecord.DistrictID);
            ClsRegistrationApplicationCLARecord.AreaList = AreaList;
            ClsRegistrationApplicationCLARecord.Talukalist = TalukaList;
            ClsRegistrationApplicationCLARecord.Districtlist = DistrictList;
            ClsRegistrationApplicationCLARecord.RegistrationCLAIDEdit = RegistrationCLAID;
            return View("AddRegistrationApplicationCLA", ClsRegistrationApplicationCLARecord);

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

        public JsonResult SaveRegistrationApplicationCLA(RegistrationApplicationCLAModel Objregappclra)
        {
            RegistrationApplicationCLAModel ClsBundleBreak = new RegistrationApplicationCLAModel();
            ClsBundleBreak = _RegistrationApplicationCLApository.SaveRegistrationApplicationCLA(Objregappclra);
            return Json(new { data = ClsBundleBreak });
        }
    }
}
