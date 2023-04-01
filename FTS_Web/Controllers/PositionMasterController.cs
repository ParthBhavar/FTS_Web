using FTS.Business.CommonList;
using FTS.Business.PositionMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class PositionMasterController : Controller
    {
        public IPositionMasterBI _Positionrpository;
        public ICommonListBI _Commompository;

        public PositionMasterController(IPositionMasterBI Positionrpository, ICommonListBI commompository)
        {

            this._Positionrpository = Positionrpository;
            _Commompository = commompository;
        }
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());

        public ActionResult Index()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                //PaginationRequest model = new PaginationRequest();
                //model.PageNumber = 1;
                //model.PageSize = 100;
                //model.SearchText = "";
                //int totalrecord = 0;
                var List = _Positionrpository.PositionList();
                if (List.Count > 0)
                {
                    foreach (var item in List)
                    {
                        item.EncryptedId = Encrypt_Decrypt.Encrypt(item.PositionID.ToString());
                    }
                }
                //totalrecord = List[0].TotalRecord;
                return View(List);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "PositionMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public ActionResult AddPositionMaster(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                int PositionID = 0;
                if (id != null)
                {
                    PositionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                }
                PositionMasterModel ClsPositionRecord = new PositionMasterModel();
                ClsPositionRecord = _Positionrpository.PositionRecord(PositionID);
                var ParentPositionList = _Commompository.PositionList(PositionID);
                var ZoneList = _Commompository.ZoneList(PositionID);
                var RoleList = _Commompository.RoleList(PositionID);
                var BranchList = _Commompository.BranchList(PositionID, ClsPositionRecord.DistrictID);
                var RegionList = _Commompository.RegionList(PositionID);
                ClsPositionRecord.Districtlist = _Commompository.DistrictList(PositionID);
                ClsPositionRecord.Talukalist = _Commompository.TalukaList(PositionID, ClsPositionRecord.DistrictID);
                ClsPositionRecord.ParentPositionlist = ParentPositionList;
                ClsPositionRecord.Zonelist = ZoneList;
                ClsPositionRecord.Rolelist = RoleList;
                ClsPositionRecord.Branchlist = BranchList;
                ClsPositionRecord.Regionlist = RegionList;
                ClsPositionRecord.PositionIDEdit = PositionID;
                return View("AddPositionMaster", ClsPositionRecord);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "PositionMasterController", "AddPositionMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public JsonResult SavePositionRecord(PositionMasterModel Obj)
        {
            PositionMasterModel ClssaveRecord1 = new PositionMasterModel();
            ClssaveRecord1 = _Positionrpository.SavePositionRecord(Obj);
            return Json(new { data = ClssaveRecord1 });
        }

        public JsonResult DeletePositionRecord(int PositionID)
        {
            int UserID = 1;
            PositionMasterModel Clsdeleterecord = new PositionMasterModel();
            Clsdeleterecord = _Positionrpository.DeletePositionRecord(UserID, PositionID);
            return Json(new { data = Clsdeleterecord });
        }

        public IActionResult AllList(int mode, int DistrictID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            try
            {
                if (_ID != null && _ID != 0)
                {
                    PositionMasterModel List = new PositionMasterModel();
                    List.Talukalist = _Commompository.TalukaList(mode, DistrictID);
                    List.BranchList = _Commompository.BranchList(mode, DistrictID);
                    return Json(new { data = List });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "PositionMasterController", "AllList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

    }
}
