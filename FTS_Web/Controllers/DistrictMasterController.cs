using FTS.Business.CommonList;
using FTS.Business.DistrictMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class DistrictMasterController : Controller
    {
        public IDistrictMasterBl _Districtpository;
        public ICommonListBI _Commompository;


        public DistrictMasterController(IDistrictMasterBl _Districtpository, ICommonListBI commompository)
        {
            this._Districtpository = _Districtpository;
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
                    var List = _Districtpository.DistrictList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.DistrictID.ToString());
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
                _Commompository.LogErrorintbl(ex, "DistrictMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public ActionResult AddDistrictMaster(string districtid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int DistrictId = 0;
                    if (districtid != null)
                    {
                        DistrictId = Convert.ToInt32(Encrypt_Decrypt.Decrypt(districtid));
                    }
                    DistrictMasterModel ClsBundleBreak = new DistrictMasterModel();
                    ClsBundleBreak = _Districtpository.DistrictRecord(DistrictId);
                    ClsBundleBreak.DistrictIDEdit = DistrictId;
                    return View("AddDistrictMaster", ClsBundleBreak);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "DistrictMasterController", "AddDistrictMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public JsonResult SaveDistrictRecord(DistrictMasterModel ObjDistrict)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    DistrictMasterModel ClsBundleBreak = new DistrictMasterModel();
                    ClsBundleBreak = _Districtpository.SaveDistrictRecord(ObjDistrict);
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
                _Commompository.LogErrorintbl(ex, "DistrictMasterController", "SaveDistrictRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


        public JsonResult DeleteDistrictRecord(int DistrictId)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int UserID = 1;
                    DistrictMasterModel ClsBundleBreak = new DistrictMasterModel();
                    ClsBundleBreak = _Districtpository.DeleteDistrictRecord(DistrictId, UserID);
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
                _Commompository.LogErrorintbl(ex, "DistrictMasterController", "DeleteDistrictRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

    }
}
