using FTS.Business.CommonList;
using FTS.Business.EstablishmentMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class EstablishmentMasterController : Controller
    {

        public IEstablishmentMasterBI _EstablishmentMasterRepository;
        public ICommonListBI _Commompository;

        public EstablishmentMasterController(IEstablishmentMasterBI _EstablishmentMasterRepository, ICommonListBI commompository)
        {
            this._EstablishmentMasterRepository = _EstablishmentMasterRepository;
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
                    var List = _EstablishmentMasterRepository.EstablishmentMasterList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.EstablishmentID.ToString());
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
                _Commompository.LogErrorintbl(ex, "EstablishmentMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        //[HttpPost]

        //public JsonResult Index(int EmployeeID)
        //{
        //    var List = _EmployeeMasterRepository.EmployeeMasterList(EmployeeID);
        //    return Json(new { data = List });
        //}


        public ActionResult AddEstablishmentMaster(string establishmentid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int EstablishmentID = 0;
                    if (establishmentid != null)
                    {
                        EstablishmentID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(establishmentid));
                    }
                    EstablishmentMasterModel ClsBundleBreak = new EstablishmentMasterModel();
                    ClsBundleBreak = _EstablishmentMasterRepository.EstablishmentRecord(EstablishmentID);
                    var DesignationList = _Commompository.DesignationList(EstablishmentID);
                    //ClsBundleBreak.DesignationList = DesignationList;
                    var DistrictList = _Commompository.DistrictList(EstablishmentID);
                    ClsBundleBreak.Districtlist = DistrictList;
                    var TalukaList = _Commompository.TalukaList(EstablishmentID, ClsBundleBreak.DistrictID);
                    ClsBundleBreak.Talukalist = TalukaList;
                    ClsBundleBreak.EstablishmentIDEdit = EstablishmentID;
                    return View("AddEstablishmentMaster", ClsBundleBreak);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EstablishmentMasterController", "AddEstablishmentMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public JsonResult SaveEstablishmentRecord(EstablishmentMasterModel Objest)  
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    EstablishmentMasterModel ClsBundleBreak = new EstablishmentMasterModel();
                    ClsBundleBreak = _EstablishmentMasterRepository.SaveEstablishmentRecord(Objest);
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
                _Commompository.LogErrorintbl(ex, "EstablishmentMasterController", "SaveEstablishmentRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        public JsonResult DeleteEstablishmentRecord(int EstablishmentID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                { 
                    EstablishmentMasterModel ClsBundleBreak = new EstablishmentMasterModel();
                ClsBundleBreak = _EstablishmentMasterRepository.DeleteEstablishmentRecord(EstablishmentID);
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
                _Commompository.LogErrorintbl(ex, "EstablishmentMasterController", "DeleteEstablishmentRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
