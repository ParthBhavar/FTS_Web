using FTS.Business.AreaMaster;
using FTS.Business.CommonList;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AreaMasterController : Controller
    {
        public IAreaMasterBl _Areapository;
        public ICommonListBI _Commompository;

        public AreaMasterController(IAreaMasterBl _Areapository, ICommonListBI commompository)
        {

            this._Areapository = _Areapository;
            _Commompository = commompository;
        }

        public IActionResult Index()
        {
            PaginationRequest model = new PaginationRequest();
            model.PageNumber = 1;
            model.PageSize = 100;
            model.SearchText = "";
            //int totalrecord = 0;
            var List = _Areapository.AreaList();
            if (List.Count > 0)
            {
                foreach (var item in List)
                {
                    item.EncryptedId = Encrypt_Decrypt.Encrypt(item.AreaID.ToString());
                }
            }
            //totalrecord = List[0].TotalRecord;
            return View(List);
        }


        public ActionResult AddAreaMaster(string areaid)
        {
            int AreaID = 0;
            if (areaid != null)
            {
                AreaID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(areaid));
            }
            AreaMasterModel ClsAreaRecord = new AreaMasterModel();
            ClsAreaRecord = _Areapository.AreaRecord(AreaID);
            var DistrictList = _Commompository.DistrictList(AreaID);
            var ZoneList = _Commompository.ZoneList(AreaID);
            ClsAreaRecord.Districtlist = DistrictList;
            ClsAreaRecord.Zonelist = ZoneList;
            ClsAreaRecord.AreaIDEdit = AreaID;
            return View("AddAreaMaster", ClsAreaRecord);
        }
        public JsonResult SaveAreaRecord(AreaMasterModel ObjArea)
        {
            AreaMasterModel ClssaveRecord = new AreaMasterModel();
            ClssaveRecord.UserID = 1;
            ClssaveRecord = _Areapository.SaveAreaRecord(ObjArea);
            return Json(new { data = ClssaveRecord });
        }

        public JsonResult DeleteAreaRecord(int AreaID)
        {
            int UserID = 1;
            AreaMasterModel Clsdeleterecord = new AreaMasterModel();
            Clsdeleterecord = _Areapository.DeleteAreaRecord(UserID, AreaID);
            return Json(new { data = Clsdeleterecord });
        }
    }
}
