using FTS.Business.CommonList;
using FTS.Business.RoleMaster;
using FTS.Business.ZoneMaster;
using FTS.Data.RoleMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Master.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.HttpOverrides;

namespace FTS_Web.Controllers

{
    [AutoValidateAntiforgeryToken]
    public class ZoneMasterController : Controller
    {
        public IZoneMasterBl _Zonepository;
        public ICommonListBI _Commompository;
        public ZoneMasterController(IZoneMasterBl _Zonepository, ICommonListBI commompository)
        {
            this._Zonepository = _Zonepository;
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
                if (_ID != null && _ID != 0)
                {
                    PaginationRequest model = new PaginationRequest();
                    model.PageNumber = 1;
                    model.PageSize = 100;
                    model.SearchText = "";
                    int totalrecord = 0;
                    var List = _Zonepository.ZoneList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ZoneID.ToString());
                        }
                    }
                    totalrecord = List[0].TotalRecord;
                    return View(List);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ZoneMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }


        public ActionResult AddZoneMaster(string zoneid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try {
                if (_ID != null && _ID != 0)
                {
                    int ZoneID = 0;
                if (zoneid != null)
                {
                    ZoneID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(zoneid));
                }
                ZoneMasterModel ClsBundleBreak = new ZoneMasterModel();
                ClsBundleBreak = _Zonepository.ZoneRecord(ZoneID);
                ClsBundleBreak.ZoneIDEdit = ZoneID;
                return View("AddZoneMaster", ClsBundleBreak);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ZoneMasterController", "AddZoneMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public JsonResult SaveZoneRecord(ZoneMasterModel ObjZone)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    ZoneMasterModel ClsBundleBreak = new ZoneMasterModel();
                    ClsBundleBreak = _Zonepository.SaveZoneRecord(ObjZone);
                    return Json(new { data = ClsBundleBreak });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
                }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ZoneMasterController", "SaveZoneRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult DeleteZoneRecord(int ZoneID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int UserID = 1;
                    ZoneMasterModel ClsBundleBreak = new ZoneMasterModel();
                    ClsBundleBreak = _Zonepository.DeleteZoneRecord(UserID, ZoneID);
                    return Json(new { data = ClsBundleBreak });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ZoneMasterController", "DeleteZoneRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }



    }
}
