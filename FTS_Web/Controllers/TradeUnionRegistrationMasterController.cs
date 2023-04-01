using FTS.Business.CommonList;
using FTS.Business.TradeUnionRegistrationMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class TradeUnionRegistrationMasterController : Controller
    {

        public ITradeUnionRegistrationMasterBl _TradeUnionRegistrationMasterRepository;
        public ICommonListBI _Commompository;

        public TradeUnionRegistrationMasterController(ITradeUnionRegistrationMasterBl _TradeUnionRegistrationMasterRepository, ICommonListBI commompository)
        {

            this._TradeUnionRegistrationMasterRepository = _TradeUnionRegistrationMasterRepository;
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
                    var List = _TradeUnionRegistrationMasterRepository.TradeUnionRegistrationList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.TradunionID.ToString());
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
                _Commompository.LogErrorintbl(ex, "TradeUnionRegistrationMasterController", "AddTradeUnionRegistrationMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        public ActionResult AddTradeUnionRegistrationMaster(string tradeunionid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int TradunionID = 0;
                    if (tradeunionid != null)
                    {
                        TradunionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(tradeunionid));
                    }
                    TradeUnionRegistrationMasterModel ClsTradeUnionRegistrationRecord = new TradeUnionRegistrationMasterModel();
                    ClsTradeUnionRegistrationRecord = _TradeUnionRegistrationMasterRepository.TradeUnionRegistrationRecord(TradunionID);
                    var DistrictList = _Commompository.DistrictList(TradunionID);
                    ClsTradeUnionRegistrationRecord.Districtlist = DistrictList;
                    var TalukaList = _Commompository.TalukaList(TradunionID, ClsTradeUnionRegistrationRecord.DistrictID);
                    ClsTradeUnionRegistrationRecord.Talukalist = TalukaList;
                    ClsTradeUnionRegistrationRecord.TradeIDEdit = TradunionID;
                    return View("AddTradeUnionRegistrationMaster", ClsTradeUnionRegistrationRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "TradeUnionRegistrationMasterController", "AddTradeUnionRegistrationMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        public JsonResult SaveTradeUnionRegistrationRecord(TradeUnionRegistrationMasterModel ObjTradeUnionRegistration)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            
            try
            {
                if (_ID != null && _ID != 0)
                {
                    TradeUnionRegistrationMasterModel ClssaveRecord = new TradeUnionRegistrationMasterModel();

                    ClssaveRecord = _TradeUnionRegistrationMasterRepository.SaveTradeUnionRegistrationRecord(ObjTradeUnionRegistration);
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
                _Commompository.LogErrorintbl(ex, "TradeUnionRegistrationMasterController", "SaveTradeUnionRegistrationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult DeleteTradeUnionRegistrationRecord(int TradunionID)
        {
                var _ID = HttpContext.Session.GetInt32("_ID");
                var _UserMode = HttpContext.Session.GetInt32("_UserMode");
                var IP = heserver.AddressList[1].ToString();
                
            try
            {
                if (_ID != null && _ID != 0)
                {
                int UserID = 1;
                TradeUnionRegistrationMasterModel Clsdeleterecord = new TradeUnionRegistrationMasterModel();
                Clsdeleterecord = _TradeUnionRegistrationMasterRepository.DeleteTradeUnionRegistrationRecord(UserID, TradunionID);
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
                _Commompository.LogErrorintbl(ex, "TradeUnionRegistrationMasterController", "DeleteTradeUnionRegistrationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult TalukaList(int mode, int DistrictID)
        {
                    var _ID = HttpContext.Session.GetInt32("_ID");
                    var _UserMode = HttpContext.Session.GetInt32("_UserMode");
                    var IP = heserver.AddressList[1].ToString();
                   
            try
            {
                if (_ID != null && _ID != 0)
                {
                    var List = _Commompository.TalukaList(mode, DistrictID);
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
                _Commompository.LogErrorintbl(ex, "TradeUnionRegistrationMasterController", "TalukaList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

    }
}
