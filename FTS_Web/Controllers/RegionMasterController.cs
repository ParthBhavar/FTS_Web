using FTS.Business.CommonList;
using FTS.Business.RegionMaster;
using FTS.Business.RoleMaster;
using FTS.Data.RoleMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Master.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class RegionMasterController : Controller
    {
        public IRegionMasterBl _Regionpository;
        public ICommonListBI _Commompository;
        public RegionMasterController(IRegionMasterBl _Regionpository, ICommonListBI commompository)
        {
            this._Regionpository = _Regionpository;
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
                int totalrecord = 0;
                var List = _Regionpository.RegionList();
                if (List.Count > 0)
                {
                    foreach (var item in List)
                    {
                        item.EncryptedId = Encrypt_Decrypt.Encrypt(item.RegionID.ToString());
                    }
                }
                totalrecord = List[0].TotalRecord;
                return View(List);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "RegionMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }


        public ActionResult AddRegionMaster(string regionid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                int RegionID = 0;
                if (regionid != null)
                {
                    RegionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(regionid));
                }
                RegionMasterModel ClsRegionRecord = new RegionMasterModel();
                ClsRegionRecord = _Regionpository.RegionRecord(RegionID);
                ClsRegionRecord.RegionIDEdit = RegionID;
                return View("AddRegionMaster", ClsRegionRecord);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "RegionMasterController", "AddRegionMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        //[HttpPost]
        //public JsonResult Index(int RegionID)
        //{
        //    var List = _Regionpository.RegionList(RegionID);
        //    return Json(new { data = List });
        //}

        //public ActionResult AddRegionMaster(int RegionID)
        //{
        //    RegionMasterModel ClsBundleBreak = new RegionMasterModel();
        //    ClsBundleBreak = _Regionpository.RegionRecord(RegionID);
        //    return View("AddRegionMaster", ClsBundleBreak);
        //}

        public JsonResult SaveRegionRecord(RegionMasterModel ObjRegion)
        {
            RegionMasterModel ClsBundleBreak = new RegionMasterModel();
            ClsBundleBreak = _Regionpository.SaveRegionRecord(ObjRegion);
            return Json(new { data = ClsBundleBreak });
        }

        public JsonResult DeleteRegionRecord(int RegionID)
        {
            int UserID = 1;
            RegionMasterModel ClsBundleBreak = new RegionMasterModel();
            ClsBundleBreak = _Regionpository.DeleteRegionRecord(UserID,RegionID);
            return Json(new { data = ClsBundleBreak });
        }
    }
}
