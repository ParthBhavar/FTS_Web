using FTS.Business.AppealDayMaster;
using FTS.Business.CommonList;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AppealDayMasterController : Controller
    {
        public IAppealDayMasterBl _AppealDayRepository;
        public ICommonListBI _Commompository;
        public AppealDayMasterController(IAppealDayMasterBl appealdayRepository, ICommonListBI commompository)
        {
            this._AppealDayRepository = appealdayRepository;
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
                    var List = _AppealDayRepository.AppealDayMasterList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ID.ToString());
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
                _Commompository.LogErrorintbl(ex, "AppealDayMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public ActionResult AddAppealDayMaster(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int ID = 0;
                    if (id != null)
                    {
                        ID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }

                    AppealDayMasterModel appealobj = new AppealDayMasterModel();
                    appealobj = _AppealDayRepository.GetAppealDayRecord(ID);
                    appealobj.AppealMasterIDEdit = ID;
                    // var AppealDayList = _Commompository.AppealDayList(ID);
                    //appealobj.AppealDayList = AppealDayList;

                    return View("AddAppealDayMaster", appealobj);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "AppealDayMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        public JsonResult SaveAppealDayRecord(AppealDayMasterModel ObjAppeal)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null)
                {
                    AppealDayMasterModel appealobj = new AppealDayMasterModel();
                    appealobj = _AppealDayRepository.SaveAppealDayRecord(ObjAppeal);
                    return Json(new { data = appealobj });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "AppealDayMasterController", "SaveAppealDayRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public JsonResult DeleteAppealDayRecord(int ID)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null)
                {
                    int UserID = 1;
                    AppealDayMasterModel ClsBundleBreak = new AppealDayMasterModel();
                    ClsBundleBreak = _AppealDayRepository.DeleteAppealDayRecord(UserID, ID);
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
                _Commompository.LogErrorintbl(ex, "AppealDayMasterController", "DeleteAppealDayRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
    }
}
