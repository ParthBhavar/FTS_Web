using FTS.Business.CommonList;
using FTS.Business.TalukaMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class TalukaMasterController : Controller
    {
        public ITalukaMasterBl _Talukapository;
        public ICommonListBI _Commompository;

        public TalukaMasterController(ITalukaMasterBl _Talukapository, ICommonListBI commompository)
        {

            this._Talukapository = _Talukapository;
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
                    //int totalrecord = 0;
                    var List = _Talukapository.TalukaList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.TalukaID.ToString());
                        }
                    }
                    //totalrecord = List[0].TotalRecord;
                    return View(List);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "TalukaMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public ActionResult AddTalukaMaster(string talukaid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int TalukaId = 0;
                    if (talukaid != null)
                    {
                        TalukaId = Convert.ToInt32(Encrypt_Decrypt.Decrypt(talukaid));
                    }
                    TalukaMasterModel ClsTalukaRecord = new TalukaMasterModel();
                    ClsTalukaRecord = _Talukapository.TalukaRecord(TalukaId);
                    var DistrictList = _Commompository.DistrictList(TalukaId);
                    ClsTalukaRecord.Districtlist = DistrictList;
                    ClsTalukaRecord.TalukaIDEdit = TalukaId;
                    return View("AddTalukaMaster", ClsTalukaRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "TalukaMasterController", "AddTalukaMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public JsonResult SaveTalukaRecord(TalukaMasterModel ObjTaluka)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    TalukaMasterModel ClssaveRecord = new TalukaMasterModel();
                    ClssaveRecord.UserID = 1;
                    ClssaveRecord = _Talukapository.SaveTalukaRecord(ObjTaluka);
                    return Json(new { data = ClssaveRecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "TalukaMasterController", "SaveTalukaRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult DeleteTalukaRecord(int TalukaId)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int UserID = 1;
                    TalukaMasterModel Clsdeleterecord = new TalukaMasterModel();
                    Clsdeleterecord = _Talukapository.DeleteTalukaRecord(UserID, TalukaId);
                    return Json(new { data = Clsdeleterecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "TalukaMasterController", "DeleteTalukaRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
