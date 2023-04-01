using Microsoft.AspNetCore.Mvc;
using FTS.Business.ConciliationApplication;
using FTS.Business.CommonList;
using FTS.Model.Common;
using FTS.Model.Entities;
using FileManager;
using System.Net;
using System.Data;
using MySql.Data.MySqlClient;
using Email;
using FastReport.Export.PdfSimple;
using FastReport;
using System.Text;
using FTS.Data.Common;
using System.Net.Http.Headers;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ConciliationApplicationController : Controller
    {
        public IConciliationApplicationBl _conciliationRepository;
        public ICommonListBI _Commompository;
        public IFileUploadService _FileUpload;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string _connectionString;
        private readonly IEmailSender _emailSender;

        public ConciliationApplicationController(IConciliationApplicationBl ConciliationRepository, IFileUploadService FileUpload, ICommonListBI commompository, IEmailSender emailSender,IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this._conciliationRepository = ConciliationRepository;
            _Commompository = commompository;
            this.webHostEnvironment = webHostEnvironment;
            _FileUpload = FileUpload;
            _emailSender = emailSender;
            _connectionString = configuration.GetConnectionString("DbConnection");
        }
        public IDbConnection Connection => new MySqlConnection(_connectionString);
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());

        public ActionResult Index()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                var _DesID = HttpContext.Session.GetInt32("_DesID");
                var _EmailID = HttpContext.Session.GetInt32("_EmailID");
                var _MoNo = HttpContext.Session.GetInt32("_MoNo");
                var _Gender = HttpContext.Session.GetInt32("_Gender");
                var _PositionID = HttpContext.Session.GetInt32("_PositionID");
                var _RegionID = HttpContext.Session.GetInt32("_RegionID");
                var _BranchID = HttpContext.Session.GetInt32("_BranchID");
                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");


                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClsAppliactionlst = new ConciliationApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.PositionID = Convert.ToInt32(_PositionID);
                    ClsAppliactionlst.PositionDistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.TalukaCode = Convert.ToInt32(_TalukaID);
                    var List = _conciliationRepository.ConciliationList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ApplicationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        public ActionResult AddConciliationApplication(int id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    if (id != 0)
                    {
                        //ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                        ApplicationID = id;

                    }
                    ConciliationApplicationModel ClsAppliaction = new ConciliationApplicationModel();
                    ClsAppliaction = _conciliationRepository.AppliactionRecord(ApplicationID);
                    var TalukaList = _Commompository.TalukaList(ApplicationID, ClsAppliaction.DistrictID);
                    var DistrictList = _Commompository.DistrictList(ApplicationID);
                    var AreaList = _Commompository.AreaList(ApplicationID, ClsAppliaction.DistrictID);
                    //var GanderList = _Commompository.GanderList();
                    ClsAppliaction.Talukalist = TalukaList;
                    ClsAppliaction.Districtlist = DistrictList;
                    ClsAppliaction.AreaList = AreaList;
                    //ClsAppliaction.Ganderlist = GanderList;
                    ClsAppliaction.AppliactionIDEdit = ApplicationID;
                    return View("AddConciliationApplication", ClsAppliaction);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConciliationApplication", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }


        public ActionResult AddConciliationEstablishment(int ApplicationID, int ISNew)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    //int AppliactionID = 0;

                    //if (id != null)
                    //{
                    //    AppliactionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    //}
                    ConciliationApplicationModel ClsEstablishment = new ConciliationApplicationModel();
                    ClsEstablishment.ClEstablish = _conciliationRepository.ConcilEstablishmentRecord(ApplicationID, ISNew);
                    var DistrictList = _Commompository.DistrictList(ClsEstablishment.ClEstablish[0].EstablisDetailID);
                    //var TalukaList = _Commompository.TalukaList(ClsEstablishment.EstablisDetailID, ClsEstablishment.DistrictID);
                    //var AreaList = _Commompository.AreaList(ClsEstablishment.EstablisDetailID, ClsEstablishment.DistrictID);
                    var EstablishmentList = _Commompository.EstablishmentList(ClsEstablishment.ClEstablish[0].EstablisDetailID);
                    //ClsEstablishment.Talukalist = TalukaList;
                    ClsEstablishment.Districtlist = DistrictList;
                    //ClsEstablishment.AreaList = AreaList;
                    ClsEstablishment.EstablishmentList = EstablishmentList;
                    //ClsTradunion.EditId = AppliactionID;
                    ClsEstablishment.ApplicationID = ApplicationID;
                    ClsEstablishment.ISNew = ISNew;
                    ClsEstablishment.IP_Address = heserver.AddressList[1].ToString();
                    ClsEstablishment.isReqiredTradDetail = ClsEstablishment.ClEstablish[0].isReqiredTradDetail;
                    ClsEstablishment.IsSubmit = ClsEstablishment.ClEstablish[0].IsSubmit;
                    return View("AddConciliationEstablishment", ClsEstablishment);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConciliationEstablishment", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        public ActionResult AddConciliationTradunion(int ApplicationID, int ISNew)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClsTradunion = new ConciliationApplicationModel();
                    ClsTradunion.ClTradunion = _conciliationRepository.ConciliationTradunionRecord(ApplicationID, ISNew);
                    var DistrictList = _Commompository.DistrictList(ClsTradunion.ClTradunion[0].TradunionID);
                    var TradunionList = _Commompository.TradunionList(ClsTradunion.ClTradunion[0].TradunionID);
                    //var TalukaList = _Commompository.TalukaList(ClsTradunion.TradunionID, ClsTradunion.TradeUnionDistrict);
                    //var AreaList = _Commompository.AreaList(ClsTradunion.TradunionID, ClsTradunion.TradeUnionDistrict);
                    var DepartmentList = _Commompository.DepartmentList(ClsTradunion.ClTradunion[0].TradunionID);
                    var WorkTypeList = _Commompository.WorkTypeList(ClsTradunion.ClTradunion[0].TradunionID);
                    //ClsTradunion.Talukalist = TalukaList;
                    ClsTradunion.Districtlist = DistrictList;
                    ClsTradunion.TradunionList = TradunionList;
                    ClsTradunion.DepartmentList = DepartmentList;
                    ClsTradunion.WorkTypeList = WorkTypeList;
                    ClsTradunion.ApplicationID = ApplicationID;
                    ClsTradunion.IsSubmit = ClsTradunion.ClTradunion[0].IsSubmit;
                    //ClsTradunion.AreaList = AreaList;
                    //ClsTradunion.EditId = AppliactionID;
                    return View("AddConciliationTradunion", ClsTradunion);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConciliationTradunion", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public JsonResult ConciliationTradunion(int ApplicationID, int ISNew)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClsEstablishment = new ConciliationApplicationModel();
                    //  ClsEstablishment.ClsEstablish = _Reinstatementpository.ReiniEstablishmentRecord(ApplicationID, ISNew);
                    var ClsEstablish = _conciliationRepository.ConciliationTradunionRecord(ApplicationID, ISNew);
                    //var DistrictList = _Commompository.DistrictList(ClsEstablishment.EstablisDetailID);
                    //var TalukaList = _Commompository.TalukaList(ClsEstablishment.EstablisDetailID, ClsEstablishment.DistrictID);
                    //var AreaList = _Commompository.AreaList(ClsEstablishment.EstablisDetailID, ClsEstablishment.DistrictID);
                    //var EstablishmentList = _Commompository.EstablishmentList(ClsEstablishment.EstablisDetailID);
                    //ClsEstablishment.Talukalist = TalukaList;
                    //ClsEstablishment.Districtlist = DistrictList;
                    //ClsEstablishment.AreaList = AreaList;
                    //ClsEstablishment.EstablishmentList = EstablishmentList;

                    var i = 0;
                    if (ClsEstablish.Count > 0)
                    {
                        for (i = 0; i < ClsEstablish.Count; ++i)
                        {
                            ClsEstablish[i].Districtlist = _Commompository.DistrictList(ClsEstablish[i].TradunionID);
                            ClsEstablish[i].Talukalist = _Commompository.TalukaList(ClsEstablish[i].TradunionID, ClsEstablish[i].DistrictID);
                            ClsEstablish[i].AreaList = _Commompository.AreaList(ClsEstablish[i].TradunionID, ClsEstablish[i].DistrictID);
                            ClsEstablish[i].TradunionList = _Commompository.TradunionList(ClsEstablish[i].TradunionID);
                        }
                    }
                    return Json(new { data = ClsEstablish });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "ConciliationEstablishment", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


        [HttpPost]
        public JsonResult ConciliationEstablishment(int ApplicationID, int ISNew)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClsEstablishment = new ConciliationApplicationModel();
                    //  ClsEstablishment.ClsEstablish = _Reinstatementpository.ReiniEstablishmentRecord(ApplicationID, ISNew);
                    var ClsEstablish = _conciliationRepository.ConcilEstablishmentRecord(ApplicationID, ISNew);
                    //var DistrictList = _Commompository.DistrictList(ClsEstablishment.EstablisDetailID);
                    //var TalukaList = _Commompository.TalukaList(ClsEstablishment.EstablisDetailID, ClsEstablishment.DistrictID);
                    //var AreaList = _Commompository.AreaList(ClsEstablishment.EstablisDetailID, ClsEstablishment.DistrictID);
                    //var EstablishmentList = _Commompository.EstablishmentList(ClsEstablishment.EstablisDetailID);
                    //ClsEstablishment.Talukalist = TalukaList;
                    //ClsEstablishment.Districtlist = DistrictList;
                    //ClsEstablishment.AreaList = AreaList;
                    //ClsEstablishment.EstablishmentList = EstablishmentList;

                    var i = 0;
                    if (ClsEstablish.Count > 0)
                    {
                        for (i = 0; i < ClsEstablish.Count; ++i)
                        {
                            ClsEstablish[i].Districtlist = _Commompository.DistrictList(ClsEstablish[i].EstablisDetailID);
                            ClsEstablish[i].Talukalist = _Commompository.TalukaList(ClsEstablish[i].EstablisDetailID, ClsEstablish[i].DistrictID);
                            ClsEstablish[i].AreaList = _Commompository.AreaList(ClsEstablish[i].EstablisDetailID, ClsEstablish[i].DistrictID);
                            ClsEstablish[i].EstablishmentList = _Commompository.EstablishmentList(ClsEstablish[i].EstablisDetailID);
                        }
                    }
                    return Json(new { data = ClsEstablish });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "ConciliationEstablishment", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


        #region Get Applicant status
        public ActionResult ApplicantStatusHistory(string id, int ISNew)
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    if (id != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }
                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();
                    ClsRecord.ISACL = 1;
                    if (ISNew == 0)
                    {
                        ClsRecord = _conciliationRepository.ConciliationACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID,ClsRecord.ISACL);

                    }
                    else if (ISNew == 1)
                    {
                        ClsRecord = _conciliationRepository.ConciliationACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID,ClsRecord.ISACL);

                    }
                    else if (ISNew == 2)
                    {
                        ClsRecord = _conciliationRepository.ConciliationACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID, ClsRecord.ISACL);

                    }
                    //var i = 0;
                    //if (ClsRecord.EstalishmentdetailACL.Count > 0)
                    //{
                    //    for (i = 0; i < ClsRecord.EstalishmentdetailACL.Count; ++i)
                    //    {
                    //        ClsRecord.EstalishmentdetailACL[i].Districtlist = _Commompository.DistrictList(0);
                    //        ClsRecord.EstalishmentdetailACL[i].Talukalist = _Commompository.TalukaList(0, ClsRecord.EstalishmentdetailACL[i].DistrictID);
                    //        ClsRecord.EstalishmentdetailACL[i].AreaList = _Commompository.AreaList(0, ClsRecord.EstalishmentdetailACL[i].DistrictID);
                    //    }
                    //}
                    var B = 0;
                    if (ClsRecord.HearingdetailACL.Count > 0)
                    {
                        for (B = 0; B < ClsRecord.HearingdetailACL.Count; ++B)
                        {
                            ClsRecord.HearingdetailACL[B].HearingReasonList = ClsRecord.HearingReasonList;
                        }
                    }
                    //ClsRecord = _conciliationRepository.ConciliationACLRecord(ApplicationID);
                    return View("ApplicantStatusHistory", ClsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConciliationHearingACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion
        public JsonResult SaveConcilEstablishmentRecord(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.IP_Address = IP;
                    ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
                    ClssaveRecord = _conciliationRepository.SaveConcilEstablishmentRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "SaveConcilEstablishmentRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
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
                var List = _Commompository.TalukaList(mode, DistrictID);
                return Json(new { data = List });
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "TalukaList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        public JsonResult AllList(int mode, int DistrictID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                ConciliationApplicationModel List = new ConciliationApplicationModel();
                List.Talukalist = _Commompository.TalukaList(mode, DistrictID);
                List.AreaList = _Commompository.AreaList(mode, DistrictID);
                return Json(new { data = List });
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AllList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult SaveApplicationRecord(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _PositionID = HttpContext.Session.GetInt32("_PositionID");
            var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.UserMode = Convert.ToInt32(_UserMode);
                    Obj.PositionID = Convert.ToInt32(_PositionID);
                    Obj.PositionDistrictID = Convert.ToInt32(_DistrictID);
                    ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
                    ClssaveRecord = _conciliationRepository.SaveAppliactionRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "SaveApplicationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult SaveConciliationTradunionRecord(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.IP_Address = IP;
                    ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
                    ClssaveRecord = _conciliationRepository.SaveConciliationTradunionRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "SaveConciliationTradunionRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        [HttpPost]
        [RequestSizeLimit(60000000)]
        public IActionResult OnPostMyUploader(List<IFormFile> MyUploader, int ApplicationID, int IsSubmit, string URL, string AppID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int i = 0;
                    int F = -1;
                    ConciliationApplicationModel ClsEstablishment = new ConciliationApplicationModel();
                    if (MyUploader.Count > 0)
                    {
                        for (i = 0; i < MyUploader.Count; i++)
                        {

                            //if (MyUploader[i].ContentType == "application/pdf" || MyUploader[i].ContentType == "image/png"  || MyUploader[i].ContentType == "application/vnd.ms-word" || MyUploader[i].ContentType == "application/vnd.ms-excel" ||
                            //    MyUploader[i].ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || MyUploader[i].ContentType == "image/jpeg")
                            //{
                            string type = "";
                            if (MyUploader[i].ContentType == "application/pdf") { type = ".pdf"; }
                            else if (MyUploader[i].ContentType == "application/vnd.ms-word") { type = ".docx"; }
                            else if (MyUploader[i].ContentType == "application/vnd.ms-excel") { type = ".xls"; }
                            else if (MyUploader[i].ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { type = ".xlsx"; }
                            else if (MyUploader[i].ContentType == "image/jpeg") { type = ".jpeg"; }
                            else if (MyUploader[i].ContentType == "image/png") { type = ".png"; };



                            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath,"Documents", "Conciliation");
                            string filePath = uploadsFolder;
                            string FileName;
                            if (i == 0)
                            {
                                FileName = $"ChrDemand_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (i == 1)
                            {
                                FileName = $"Intervention_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (i == 2)
                            {
                                FileName = $"OwnerChartered_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (i == 3)
                            {
                                FileName = $"MemberVerify_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else
                            {
                                FileName = $"AnnualReturn_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            //string FileName = MyUploader[i].FileName;
                            ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
                            ClssaveRecord.ApplicationID = ApplicationID;
                            ClssaveRecord.fileName = FileName;
                            ClssaveRecord.DocumentID = i;
                            ClssaveRecord.URL = URL;
                            ClssaveRecord.UserID = Convert.ToInt32(_ID);
                            ClssaveRecord.IP_Address = IP;
                            ClssaveRecord.AppID = AppID;

                            if (IsSubmit == 1) { ClssaveRecord.IsSubmit = true; } else { ClssaveRecord.IsSubmit = false; }

                            var output = _FileUpload.UploadDocument(MyUploader[i], FileName, filePath);
                            var output1 = _conciliationRepository.SaveDocumnetandapplication(ClssaveRecord);
                            ClsEstablishment = output1;

                            if (ClssaveRecord.ErrorCode == 0)
                            {
                                SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                            }
                            if (output == true && output1.ErrorCode == 0)
                            {
                                F = i;
                            }

                        }

                    }
                    
                    //if (F == -1)
                    //{
                    ////    ClsEstablishment.ErrorCode = 0;
                    ////    ClsEstablishment.ErrorMassage = "Document upload Succefully";
                    ////}
                    ////else
                    ////{
                    //    ClsEstablishment.ErrorCode = 1;
                    //    ClsEstablishment.ErrorMassage = "You are already upoaded Document if you need update So please select Document ";
                    //}
                    ////if file not selected than status is 3
                    return Json(new { status = ClsEstablishment });
                }

                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "OnPostMyUploader", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }


        public ActionResult AddConcilUploadDocument(int ApplicationID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClsEstablishment = new ConciliationApplicationModel();
                    ClsEstablishment = _conciliationRepository.ConcilAppliactionDocumentURLRecord(ApplicationID);
                    return View("AddConcilUploadDocument", ClsEstablishment);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConcilUploadDocument", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public async Task<IActionResult> DownloadFile(string FileName)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    if (FileName == null)
                        return Content("filename not present");

                    //var path = "https://localhost:44314/images/logo.png";
                    var path = Path.Combine("wwwroot", "Documents", "Conciliation", FileName);
                    //var path = "wwwroot/images/logo.png";
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    return File(memory, _FileUpload.GetContentType(path), Path.GetFileName(path));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "DownloadFile", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        #region Download Settlement Document
        public async Task<IActionResult> DownloadSettelementFile(string FileName)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    if (FileName == null)
                        return Content("filename not present");

                    //var path = "https://localhost:44314/images/logo.png";
                    var path = Path.Combine("wwwroot", "Documents", "Conciliation","Settlement", FileName);
                    //var path = "wwwroot/images/logo.png";
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    return File(memory, _FileUpload.GetContentType(path), Path.GetFileName(path));
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "DownloadSettelementFile", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        public JsonResult ConciliationAppliactionUpdateSubmit(ConciliationApplicationModel Obj)
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {
                if (_ID != null && _ID != 0)
                {
                    Obj.UserMode = Convert.ToInt32(_UserMode);
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.IP_Address = IP;
                    Obj.URL = Obj.URL;
                    ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
                    ClssaveRecord = _conciliationRepository.ConciliationAppliactionUpdateSubmit(Obj);
                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        //SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                        int i = 0;
                        int v = 0;

                        if (ClssaveRecord.ApplicantMailDetail.Count > 0)
                        {
                            for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
                            {
                                var test = SendMailApplicant(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, ClssaveRecord.ApplicantMailDetail[i].Body);
                            }
                        }

                        if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
                        {
                            for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
                            {
                                var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                            }
                        }
                    }
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "SaveRoleRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #region List for ACL
        public ActionResult ConciliationACLList()
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {
                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
                var _RoleID = HttpContext.Session.GetInt32("_RoleID");

                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClsAppliactionlst = new ConciliationApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _conciliationRepository.ConciliationACLList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ApplicationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "ConciliationACLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region List for DCL Clerk
        public ActionResult ConciliationDCLClerkList()
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {
                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
                var _RoleID = HttpContext.Session.GetInt32("_RoleID");

                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClsAppliactionlst = new ConciliationApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _conciliationRepository.ConciliationDCLClerkList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ApplicationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "ConciliationACLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion
        #region AddConciliation ACL
        public ActionResult AddConciliationHearingACL(string id, int ISNew, int IsView = 0)
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    int IsACL = 1;
                    if (id != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }
                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();
                    if (ISNew == 0)
                    {
                        ClsRecord = _conciliationRepository.ConciliationACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID, IsACL);

                    }
                    else if (ISNew == 1)
                    {
                         ClsRecord = _conciliationRepository.ConciliationACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID, IsACL);

                    }
                    else if (ISNew == 2)
                    {
                        ClsRecord = _conciliationRepository.ConciliationACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID, IsACL);

                    }
                    var i = 0;
                    if (ClsRecord.EstalishmentdetailACL.Count > 0)
                    {
                        for (i = 0; i < ClsRecord.EstalishmentdetailACL.Count; ++i)
                        {
                            ClsRecord.EstalishmentdetailACL[i].Districtlist = _Commompository.DistrictList(0);
                            ClsRecord.EstalishmentdetailACL[i].Talukalist = _Commompository.TalukaList(0, ClsRecord.EstalishmentdetailACL[i].DistrictID);
                            ClsRecord.EstalishmentdetailACL[i].AreaList = _Commompository.AreaList(0, ClsRecord.EstalishmentdetailACL[i].DistrictID);
                        }
                    }
                    ClsRecord.ISView = IsView;
                    //var B = 0;
                    //if (ClsRecord.HearingdetailACL.Count > 0)
                    //{
                    //    for (B = 0; B < ClsRecord.HearingdetailACL.Count; ++B)
                    //    {
                    //        ClsRecord.HearingdetailACL[B].HearingReasonList = ClsRecord.HearingReasonList;
                    //    }
                    //}
                    //ClsRecord = _conciliationRepository.ConciliationACLRecord(ApplicationID);
                    return View("AddConciliationHearingACL", ClsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConciliationHearingACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Update ACL Hearing Status
        public JsonResult SaveConciliaitonHearingACLRecord(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.IP_Address = IP;
                    ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
                    ClssaveRecord = _conciliationRepository.SaveConciliationHearingACLRecord(Obj);

                    EmailReportModel EmailReportDetail = new EmailReportModel();
                    EmailReportDetail.DictrictACLOffice = ClssaveRecord.EmailReportDetail[0].DictrictACLOffice;
                    EmailReportDetail.Date = ClssaveRecord.EmailReportDetail[0].Date;
                    EmailReportDetail.Establishmentdetail = ClssaveRecord.EmailReportDetail[0].Establishmentdetail;
                    EmailReportDetail.ApplicantDetail = ClssaveRecord.EmailReportDetail[0].ApplicantDetail;
                    EmailReportDetail.AppID = ClssaveRecord.EmailReportDetail[0].AppID;
                    EmailReportDetail.HearingDate = ClssaveRecord.EmailReportDetail[0].HearingDate;
                    EmailReportDetail.HearingTime = ClssaveRecord.EmailReportDetail[0].HearingTime;
                    EmailReportDetail.ACLDistrict = ClssaveRecord.EmailReportDetail[0].ACLDistrict;

                    //MemoryStream ms1 = new MemoryStream();
                    //string[] rr1 = new string[2000000];
                    //rr1 = ConciliationHearingEmailPDF(EmailReportDetail);
                    //byte[] bytes = System.Convert.FromBase64String(rr1[1]);
                    //Stream stream = new MemoryStream(bytes);

                    //List<MemoryStream> _FileStrem = new List<MemoryStream> { GenerateStreamFromString(ConciliationHearingEmailPDF(EmailReportDetail)) };
                    string webRootPath = webHostEnvironment.WebRootPath;
                    string uploadsFolder = Path.Combine(webRootPath, "Documents", "Report", "EmailPDF");

                    List<Attachments> AttachedFile = new List<Attachments>();
                    if (ClssaveRecord.EmailReportDetail[0].AppID != null && ClssaveRecord.EmailReportDetail[0].AppID != "")
                    {
                        MemoryStream ms1 = new MemoryStream();
                        string[] rr1 = new string[2000000];
                        rr1 = ConciliationHearingEmailPDF(EmailReportDetail);
                        byte[] bytes = System.Convert.FromBase64String(rr1[1]);
                        Stream stream = new MemoryStream(bytes);

                        string filePath = Path.Combine("wwwroot", "Documents", "Report", "EmailPDF");
                        var filename = $"Hearing_{DateTime.Now:yyyyMMdd_hhmm}_{ClssaveRecord.EmailReportDetail[0].AppID}" + ".pdf";
                        System.IO.File.WriteAllBytes(filePath + "\\" + filename, bytes);
                        AttachedFile.Add(new Attachments() { FileName = uploadsFolder + "\\" + filename });
                    }

                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        //SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                        int i = 0;
                        int v = 0;

                        if (ClssaveRecord.ApplicantMailDetail.Count > 0)
                        {
                            for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
                            {
                                if (ClssaveRecord.ApplicantMailDetail[i].Email != null)
                                {
                                    //var test = SendMailApplicant(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, stream, ClssaveRecord.ApplicantMailDetail[i].Body);
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.ApplicantMailDetail[i].Email.ToString() }, ClssaveRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClssaveRecord.ApplicantMailDetail[i].Body);

                                }
                            }
                        }

                        if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
                        {
                            for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
                            {

                                if (ClssaveRecord.EshtablishmentMailDetail[v].Email != null)
                                {
                                    //var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, stream, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.EshtablishmentMailDetail[v].Email.ToString() }, ClssaveRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClssaveRecord.EshtablishmentMailDetail[v].Body);

                                }
                            }
                        }
                    }
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "SaveConciliaitonHearingACLRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        #endregion

        public Report report = new Report();
        public string[] ConciliationHearingEmailPDF(EmailReportModel EmailReportDetail)
        {
            MemoryStream ms1 = new MemoryStream();
            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder

            DataSet ds = new DataSet();
            DataTable Dt1 = ds.Tables.Add("Table1");

            // create fields
            Dt1.Columns.Add("DictrictACLOffice", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("Establishmentdetail", typeof(string));
            Dt1.Columns.Add("ApplicantDetail", typeof(string));
            Dt1.Columns.Add("AppID", typeof(string));
            Dt1.Columns.Add("HearingDate", typeof(string));
            Dt1.Columns.Add("HearingTime", typeof(string));
            Dt1.Columns.Add("ACLDistrict", typeof(string));

            //insert row values
            Dt1.Rows.Add(new Object[]{
                 EmailReportDetail.DictrictACLOffice,
                 EmailReportDetail.Date,
                 EmailReportDetail.Establishmentdetail,
                 EmailReportDetail.ApplicantDetail,
                 EmailReportDetail.AppID,
                 EmailReportDetail.HearingDate,
                 EmailReportDetail.HearingTime,
                 EmailReportDetail.ACLDistrict
            });

            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/ConciliaitonACLHearing.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/ConciliaitonACLHearing.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            report.Report.Load(webRootPath + "/Documents/Report/CouncilationACLHearing.frx");
            report.Prepare();

            //PDFExport export = new PDFExport();
            string[] rr = new string[2000000];
            PDFSimpleExport export = new PDFSimpleExport();
            using (MemoryStream ms = new MemoryStream())
            {
                export.Export(report, ms);
                rr[1] = Convert.ToBase64String(ms.ToArray());
                rr[0] = Encoding.ASCII.GetString(ms.ToArray());
                return rr;
            }

        }

        #region Send to Labour court
        public JsonResult SaveConciliationSendtolabourACLRecord(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.IP_Address = IP;
                    ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
                    ClssaveRecord = _conciliationRepository.SaveConciliationSendtolabourACLRecord(Obj);
                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                    }
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
                _Commompository.LogErrorintbl(ex, "RoleMasterController", "SaveRoleRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        #endregion

        public async Task SendMailApplicant(string EmailID, string Title, string Body)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                await _emailSender.SendEmailAsync(EmailID, Title, Body);
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SendMailApplicant", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                throw;
            }
        }

        #region Settlment by ACL
        [HttpPost]
        [RequestSizeLimit(20000000)]
        public IActionResult SaveConciliationACLSettlementrecord(IFormFile MyUploader, int ApplicationID, string SettlementDate, string SettlementRemark,
            string URL, int HearingReasonID, string OrderOutwardDate, int OrderOutwardNo)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
             var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {

                    ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();

                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "Conciliation", "Settlement");
                    string filePath = uploadsFolder;
                    string FileName = "";
                    bool output = false;

                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.fileName = FileName;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.SettlementDateIn = SettlementDate;
                    ClssaveRecord.SettlementRemark = SettlementRemark;
                    ClssaveRecord.ResolutionReasonID = HearingReasonID;
                    ClssaveRecord.OrderOutwardDatee = OrderOutwardDate;
                    ClssaveRecord.OrderOutwardNo = OrderOutwardNo;

                    if (MyUploader != null)
                    {
                        string type = "";
                        if (MyUploader.ContentType == "application/pdf") { type = ".pdf"; }
                        else if (MyUploader.ContentType == "application/vnd.ms-word") { type = ".docx"; }
                        else if (MyUploader.ContentType == "application/vnd.ms-excel") { type = ".xls"; }
                        else if (MyUploader.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { type = ".xlsx"; }
                        else if (MyUploader.ContentType == "image/jpeg") { type = ".jpeg"; }
                        else if (MyUploader.ContentType == "image/png") { type = ".png"; };

                        FileName = $"SettlementDoc_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                        ClssaveRecord.fileName = FileName;


                         output = _FileUpload.UploadDocument(MyUploader, FileName, filePath);
                        ClssaveRecord = _conciliationRepository.SaveConciliationACLSettlementrecord(ClssaveRecord);
                    }
                    else
                    {
                        ClssaveRecord = _conciliationRepository.SaveConciliationACLSettlementrecord(ClssaveRecord);
                    }

                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        //SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                        int i = 0;
                        int v = 0;

                        List<Attachments> AttachedFile = new List<Attachments>();
                        AttachedFile.Add(new Attachments() { FileName = uploadsFolder + "\\" + FileName });

                        if (ClssaveRecord.ApplicantMailDetail.Count > 0)
                        {
                            for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
                            {
                                //string path = Path.Combine(uploadsFolder, FileName);
                                //var test = _emailSender.SendEmailAsync(new List<string>(ClssaveRecord.ApplicantMailDetail[i].Email.ToString() }, ClssaveRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClssaveRecord.ApplicantMailDetail[i].Body);
                                 var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.ApplicantMailDetail[i].Email.ToString() }, ClssaveRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClssaveRecord.ApplicantMailDetail[i].Body);
                            }
                        }

                        if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
                        {
                            for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
                            {
                                var test = _emailSender.SendEmailAsync(new List<string> {ClssaveRecord.EshtablishmentMailDetail[v].Email.ToString() }, ClssaveRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                            }
                        }
                    }
                    if (output == true)
                    {
                        return Json(new { status = ClssaveRecord });
                    }


                    //if file not selected than status is 3
                    return Json(new { status = ClssaveRecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "SaveConciliationACLSettlementrecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Conciliation History
        public IActionResult ConciliationHistory(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
                    ClssaveRecord = _conciliationRepository.ConciliationHistory(Obj);
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "ConciliationHistory", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion
        //public WebReport GetReport(ConciliationApplicationModel Obj)
        //{
        //    string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder
        //    WebReport webReport = new WebReport();
        //    //string connetion = "Server=10.10.0.42;User ID=developer;Password=D#v#l0p#r;Database=col;port=3306;Pooling=false;";
        //    MySqlConnection con = new MySqlConnection(Connection.ConnectionString);
        //    con.Open();
        //    MySqlCommand cmd = new MySqlCommand("SP_GetRecordConciliationApplicationHistory", con);
        //    cmd.Parameters.AddWithValue("p_ApplicationId", Obj.ApplicationID);
        //    cmd.Parameters.AddWithValue("p_UserID", 1);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    sda.Fill(ds);

        //    //cmd.ExecuteNonQuery();
        //    //con.Close();
        //    FastReport.Utils.RegisteredObjects.AddConnection(typeof(MySqlDataConnection));
        //    //webReport.Report.Load(webRootPath + "/Report/Conciliation.frx");

        //    webReport.Report.Load(webRootPath + "/Report/ConciliationTest.frx");

        //    ds.WriteXmlSchema(webRootPath + "/Report/ApplicationHistory.xsd");
        //    webReport.Report.RegisterData(ds, Connection.ConnectionString); // Register the data source in the report
        //    //ds.Tables[1].TableName = "Table";
        //    //ds.Tables[2].TableName = "Temp1";




        //    //var reportToLoad = model.ReportsList[0];
        //    //if (reportIndex >= 0 && reportIndex < model.ReportsList.Length)
        //    //    reportToLoad = model.ReportsList[reportIndex.Value];

        //    //model.WebReport.Report.Load(Path.Combine(ReportsFolder, $"{reportToLoad}.frx"));

        //    //var dataSet = new DataSet();
        //    //dataSet.ReadXml(Path.Combine(ReportsFolder, "nwind.xml"));
        //    //model.WebReport.Report.RegisterData(dataSet, "NorthWind");

        //    //return View(model);


        //    //return webReport;

        //    //string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder
        //    //WebReport webReport = new WebReport();
        //    //ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
        //    //ClssaveRecord = _conciliationRepository.ConciliationHistory(Obj);
        //    //FastReport.Utils.RegisteredObjects.AddConnection(typeof(MySqlDataConnection));
        //    //webReport.Report.Load(webRootPath + "/Report/conciliation1.frx");
        //    //webReport.Report.RegisterData(ClssaveRecord.basicdetailtlst, Connection.ConnectionString); // Register the data source in the report

        //    return webReport;

        //    //return View("AddConcilUploadDocument", ClsEstablishment);
        //}


        //public IActionResult Pdf(int ApplicationId)
        //{
        //    ConciliationApplicationModel Obj = new ConciliationApplicationModel();
        //    Obj.ApplicationID = ApplicationId;
        //    var webReport = GetReport(Obj);
        //    webReport.Report.Prepare();

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        PDFSimpleExport pdfExport = new PDFSimpleExport();
        //        pdfExport.Export(webReport.Report, ms);
        //        ms.Flush();
        //        return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension("Conciliation-Application") + ".pdf");
        //    }
        //}

        public IActionResult Pdf(int ApplicationId)
        {
            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder
            MySqlConnection con = new MySqlConnection(Connection.ConnectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(SPConstants.UpdateConciliationApplicationHistory, con);
            cmd.Parameters.AddWithValue("p_ApplicationId", ApplicationId);
            cmd.Parameters.AddWithValue("p_UserID", 1);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/ConciliationApp.xsd");
             //ds.WriteXmlSchema(webRootPath + "/Documents/Report/ConciliationApp.xml");
            report.Report.RegisterData(ds.Tables[1], "Table1");
            report.Report.RegisterData(ds.Tables[2], "Table2");
            report.Report.Load(webRootPath + "/Documents/Report/Conciliation.frx");
            report.Prepare();

            //PDFExport export = new PDFExport();
            PDFSimpleExport export = new PDFSimpleExport();
            using (MemoryStream ms = new MemoryStream())
            {
                export.Export(report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension("Conciliation Application") + ".pdf");
            }
        }

        #region update PUS status by ACL clerk
        public JsonResult UpdateClerkStatustoACL(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    //int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    //if (id != null)
                    //{
                    //    ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    //}
                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();
                    ClsRecord.UserID = Convert.ToInt32(_ID);

                    ClsRecord = _conciliationRepository.ConcilupdateClerkStatustoACL(Obj);
                    EmailReportModel EmailReportDetail = new EmailReportModel();
                    EmailReportDetail.DictrictACLOffice = ClsRecord.EmailReportDetail[0].DictrictACLOffice;
                    EmailReportDetail.AppID = ClsRecord.EmailReportDetail[0].AppID;
                    EmailReportDetail.ApplicantDetail = ClsRecord.EmailReportDetail[0].ApplicantDetail;
                    EmailReportDetail.Establishmentdetail = ClsRecord.EmailReportDetail[0].Establishmentdetail;
                    EmailReportDetail.ACLName = ClsRecord.EmailReportDetail[0].ACLName;
                    EmailReportDetail.ACLDistrict = ClsRecord.EmailReportDetail[0].ACLDistrict;
                    EmailReportDetail.Date = ClsRecord.EmailReportDetail[0].Date;
                    EmailReportDetail.SendLCDate = ClsRecord.EmailReportDetail[0].SendLCDate;
                    EmailReportDetail.EstablishmentName = ClsRecord.EmailReportDetail[0].EstablishmentName;  

                    string webRootPath = webHostEnvironment.WebRootPath;
                    string uploadsFolder = Path.Combine(webRootPath, "Documents", "Report", "EmailPDF");

                    List<Attachments> AttachedFile = new List<Attachments>();
                    if (ClsRecord.EmailReportDetail[0].AppID != null && ClsRecord.EmailReportDetail[0].AppID != "")
                    {
                        MemoryStream ms1 = new MemoryStream();
                        string[] rr1 = new string[2000000];
                        rr1 = PUSEmailPDF(EmailReportDetail);
                        byte[] bytes = System.Convert.FromBase64String(rr1[1]);
                        Stream stream = new MemoryStream(bytes);

                        string filePath = Path.Combine("wwwroot", "Documents", "Report", "EmailPDF");
                        var filename = $"Conciliatin(PUS)Verify_{DateTime.Now:yyyyMMdd_hhmm}_{ClsRecord.EmailReportDetail[0].AppID}" + ".pdf";
                        System.IO.File.WriteAllBytes(filePath + "\\" + filename, bytes);
                        AttachedFile.Add(new Attachments() { FileName = uploadsFolder + "\\" + filename });
                    }
                    if (ClsRecord.ErrorCode == 0)
                    {
                        int i = 0;
                        int v = 0;


                        if (ClsRecord.ApplicantMailDetail.Count > 0)
                        {
                            for (i = 0; i < ClsRecord.ApplicantMailDetail.Count; i++)
                            {
                                if (ClsRecord.ApplicantMailDetail[i].Email != null)
                                {
                                    //var test = SendMailApplicant(ClsRecord.ApplicantMailDetail[i].Email, ClsRecord.ApplicantMailDetail[i].Subject, ClsRecord.ApplicantMailDetail[i].Body);
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClsRecord.ApplicantMailDetail[i].Email.ToString() }, ClsRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClsRecord.ApplicantMailDetail[i].Body);
                                }
                            }
                        }

                        if (ClsRecord.EshtablishmentMailDetail.Count > 0)
                        {
                            for (v = 0; v < ClsRecord.EshtablishmentMailDetail.Count; v++)
                            {
                                if (ClsRecord.EshtablishmentMailDetail[v].Email != null)
                                {
                                    //var test = SendMailApplicant(ClsRecord.EshtablishmentMailDetail[v].Email, ClsRecord.EshtablishmentMailDetail[v].Subject, ClsRecord.EshtablishmentMailDetail[v].Body);
                                    //var test = SendMailApplicant(ClsRecord.EshtablishmentMailDetail[v].Email, ClsRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClsRecord.EshtablishmentMailDetail[v].Body);
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClsRecord.EshtablishmentMailDetail[v].Email.ToString() }, ClsRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClsRecord.EshtablishmentMailDetail[v].Body);
                                }
                            }
                        }
                    }
                    return Json(new { data = ClsRecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "UpdateClerkStatustoACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        #endregion

        #region update Query by ACL clerk
        public JsonResult UpdateQueryByACLClerk(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    //int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    //if (id != null)
                    //{
                    //    ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    //}
                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();
                    ClsRecord.UserID = Convert.ToInt32(_ID);

                    ClsRecord = _conciliationRepository.UpdateQueryByACLClerk(Obj);
                    if (ClsRecord.ErrorCode == 0)
                    {
                        SendMailApplicant(ClsRecord.EMailIDList, ClsRecord.EmailSubject, ClsRecord.EmailBody);
                    }
                    return Json(new { data = ClsRecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "UpdateQueryByACLClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        #endregion

        #region status by ACL clerk
        public ActionResult AddConciliationACLClerk(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    //if (id != null)
                    //{
                    //    ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    //}
                   
                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();

                    ClsRecord = _conciliationRepository.ConcilupdateACLClerk(id);

                    //TempData["message"] = "Forwarded To ACL Successfully!";

                    //return RedirectToAction("Index", "ConciliationApplication");
                    return View("AddConciliationACLClerk", ClsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConciliationACLClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        #endregion

        #region Get Detail by DCL clerk
        public ActionResult AddConciliationDCLClerk(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    if (id != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }

                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();

                    ClsRecord = _conciliationRepository.ConciliationDCLClerkRecord(ApplicationID);
                    ClsRecord.ReferStatuList = _Commompository.ReferActionList(ClsRecord.ApplicationID);

                    //TempData["message"] = "Forwarded To ACL Successfully!";

                    //return RedirectToAction("Index", "ConciliationApplication");
                    return View("AddConciliationDCLClerk", ClsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConciliationACLClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        #endregion

        #region update Status by DCL clerk
        public JsonResult UpdateStatusByDCLClerk(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    //int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    //if (id != null)
                    //{
                    //    ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    //}
                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();
                    ClsRecord.UserID = Convert.ToInt32(_ID);

                    ClsRecord = _conciliationRepository.UpdateStatusByDCLClerk(Obj);

                    //TempData["message"] = "Forwarded To ACL Successfully!";
                    if (ClsRecord.ErrorCode == 0)
                    {
                        //SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                        int i = 0;
                        int v = 0;


                        if (ClsRecord.ApplicantMailDetail.Count > 0)
                        {
                            for (i = 0; i < ClsRecord.ApplicantMailDetail.Count; i++)
                            {
                                var test = SendMailApplicant(ClsRecord.ApplicantMailDetail[i].Email, ClsRecord.ApplicantMailDetail[i].Subject, ClsRecord.ApplicantMailDetail[i].Body);
                            }
                        }

                        if (ClsRecord.EshtablishmentMailDetail.Count > 0)
                        {
                            for (v = 0; v < ClsRecord.EshtablishmentMailDetail.Count; v++)
                            {
                                var test = SendMailApplicant(ClsRecord.EshtablishmentMailDetail[v].Email, ClsRecord.EshtablishmentMailDetail[v].Subject, ClsRecord.EshtablishmentMailDetail[v].Body);
                            }
                        }
                    }

                    return Json(new { data = ClsRecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }

            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "UpdateClerkStatustoACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        #endregion

        #region List for DCL
        public ActionResult ConciliationDCLList()
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {
                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
                var _RoleID = HttpContext.Session.GetInt32("_RoleID");

                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClsAppliactionlst = new ConciliationApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _conciliationRepository.ConciliationDCLList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ApplicationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "ConciliationACLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Get Detail by DCL
        public ActionResult AddConciliationDCL(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    if (id != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }

                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();

                    ClsRecord = _conciliationRepository.ConciliationDCLRecord(ApplicationID);
                    ClsRecord.ReferStatuList = _Commompository.ReferActionList(ClsRecord.ApplicationID);

                    //TempData["message"] = "Forwarded To ACL Successfully!";

                    //return RedirectToAction("Index", "ConciliationApplication");
                    return View("AddConciliationDCL", ClsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConciliationACLClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        #endregion

        #region update Status by DCL
        public JsonResult UpdateStatusByDCL(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    //int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    int DCLReferStatusID = Obj.DCLReferStatusID;
                    //if (id != null)
                    //{
                    //    ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    //}
                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();
                    ClsRecord.UserID = Convert.ToInt32(_ID);

                    ClsRecord = _conciliationRepository.UpdateStatusByDCL(Obj);

                    EmailReportModel EmailReportDetail = new EmailReportModel();
                    EmailReportDetail.DictrictACLOffice = ClsRecord.EmailReportDetail[0].DictrictACLOffice;
                    EmailReportDetail.AppID = ClsRecord.EmailReportDetail[0].AppID;
                    EmailReportDetail.ApplicantDetail = ClsRecord.EmailReportDetail[0].ApplicantDetail;
                    EmailReportDetail.Establishmentdetail = ClsRecord.EmailReportDetail[0].Establishmentdetail;
                    EmailReportDetail.ACLName = ClsRecord.EmailReportDetail[0].ACLName;
                    EmailReportDetail.LCAddress = ClsRecord.EmailReportDetail[0].LCAddress;
                    EmailReportDetail.Date = ClsRecord.EmailReportDetail[0].Date;
                    EmailReportDetail.SendLCDate = ClsRecord.EmailReportDetail[0].SendLCDate;

                    //MemoryStream ms1 = new MemoryStream();
                    //string[] rr1 = new string[2000000];
                    //if (DCLReferStatusID == 10)
                    //{
                    //    rr1 = SendToHOByDCLEmailPDF(EmailReportDetail);
                    //}
                    //else
                    //{
                    //    rr1 = SendToLCByDCLEmailPDF(EmailReportDetail);
                    //}
                    //byte[] bytes = System.Convert.FromBase64String(rr1[1]);
                    //Stream stream = new MemoryStream(bytes);

                    //TempData["message"] = "Forwarded To ACL Successfully!";
                    string webRootPath = webHostEnvironment.WebRootPath;
                    string uploadsFolder = Path.Combine(webRootPath, "Documents", "Report", "EmailPDF");

                    List<Attachments> AttachedFile = new List<Attachments>();
                    if (ClsRecord.EmailReportDetail[0].AppID != null && ClsRecord.EmailReportDetail[0].AppID != "")
                    {
                        MemoryStream ms1 = new MemoryStream();
                        string[] rr1 = new string[2000000];
                        rr1 = SendToHOByDCLEmailPDF(EmailReportDetail);
                        byte[] bytes = System.Convert.FromBase64String(rr1[1]);
                        Stream stream = new MemoryStream(bytes);

                        string filePath = Path.Combine("wwwroot", "Documents", "Report", "EmailPDF");
                        var filename = $"Hearing_{DateTime.Now:yyyyMMdd_hhmm}_{ClsRecord.EmailReportDetail[0].AppID}" + ".pdf";
                        System.IO.File.WriteAllBytes(filePath + "\\" + filename, bytes);
                        AttachedFile.Add(new Attachments() { FileName = uploadsFolder + "\\" + filename });
                    }
                    if (ClsRecord.ErrorCode == 0)
                    {
                        int i = 0;
                        int v = 0;


                        if (ClsRecord.ApplicantMailDetail.Count > 0)
                        {
                            for (i = 0; i < ClsRecord.ApplicantMailDetail.Count; i++)
                            {
                                if (ClsRecord.ApplicantMailDetail[i].Email != null)
                                {
                                    //var test = SendMailApplicant(ClsRecord.ApplicantMailDetail[i].Email, ClsRecord.ApplicantMailDetail[i].Subject, ClsRecord.ApplicantMailDetail[i].Body);
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClsRecord.ApplicantMailDetail[i].Email.ToString() }, ClsRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClsRecord.ApplicantMailDetail[i].Body);
                                }
                            }
                        }

                        if (ClsRecord.EshtablishmentMailDetail.Count > 0)
                        {
                            for (v = 0; v < ClsRecord.EshtablishmentMailDetail.Count; v++)
                            {
                                if (ClsRecord.EshtablishmentMailDetail[v].Email != null)
                                {
                                    //var test = SendMailApplicant(ClsRecord.EshtablishmentMailDetail[v].Email, ClsRecord.EshtablishmentMailDetail[v].Subject, ClsRecord.EshtablishmentMailDetail[v].Body);
                                    //var test = SendMailApplicant(ClsRecord.EshtablishmentMailDetail[v].Email, ClsRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClsRecord.EshtablishmentMailDetail[v].Body);
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClsRecord.EshtablishmentMailDetail[v].Email.ToString() }, ClsRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClsRecord.EshtablishmentMailDetail[v].Body);
                                }
                            }
                        }
                    }
                    return Json(new { data = ClsRecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "UpdateClerkStatustoACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        #endregion
        //public Report report = new Report();
        public string[] SendToHOByDCLEmailPDF(EmailReportModel EmailReportDetail)
        //public IActionResult SendToLabourEmailPDF()
        {
            MemoryStream ms1 = new MemoryStream();
            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder

            DataSet ds = new DataSet();
            DataTable Dt1 = ds.Tables.Add("Table1");

            // create fields
            Dt1.Columns.Add("ACLName", typeof(string));
            Dt1.Columns.Add("DictrictACLOffice", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("Shakhano", typeof(string));
            Dt1.Columns.Add("Establishmentdetail", typeof(string));
            Dt1.Columns.Add("AppID", typeof(string));
            Dt1.Columns.Add("ApplicantName", typeof(string));

            //insert row values
            Dt1.Rows.Add(new Object[]{
                 EmailReportDetail.ACLName,
                 EmailReportDetail.DictrictACLOffice,
                 EmailReportDetail.Date,
                 EmailReportDetail.ShakhaNo,
                 EmailReportDetail.Establishmentdetail,
                 EmailReportDetail.AppID,
                 EmailReportDetail.ApplicantName
             });

            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/ConciliationSendToHO.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/ConciliationSendToHO.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            report.Report.Load(webRootPath + "/Documents/Report/ConciliaitonHO.frx");
            //report.Report.Load(path);
            report.Prepare();

            //PDFExport export = new PDFExport();
            PDFSimpleExport export = new PDFSimpleExport();
            string[] rr = new string[2000000];

            using (MemoryStream ms = new MemoryStream())
            {
                export.Export(report, ms);
                rr[1] = Convert.ToBase64String(ms.ToArray());
                rr[0] = Encoding.ASCII.GetString(ms.ToArray());
                return rr;
            }
        }

        public string[] SendToLCByDCLEmailPDF(EmailReportModel EmailReportDetail)
        //public IActionResult SendToLabourEmailPDF()
        {
            MemoryStream ms1 = new MemoryStream();
            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder

            DataSet ds = new DataSet();
            DataTable Dt1 = ds.Tables.Add("Table1");

            // create fields
            Dt1.Columns.Add("ACLName", typeof(string));
            Dt1.Columns.Add("DictrictACLOffice", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("ApplicantName", typeof(string));
            Dt1.Columns.Add("Establishmentdetail", typeof(string));
            Dt1.Columns.Add("AppID", typeof(string));

            //insert row values
            Dt1.Rows.Add(new Object[]{
                 EmailReportDetail.ACLName,
                 EmailReportDetail.DictrictACLOffice,
                 EmailReportDetail.Date,
                 EmailReportDetail.ApplicantName,
                 EmailReportDetail.Establishmentdetail,
                 EmailReportDetail.AppID

            });

           
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/ConciliationSendToLC.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/ConciliationSendToLC.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            report.Report.Load(webRootPath + "/Documents/Report/ConcilliationLC.frx");
            //report.Report.Load(path);
            report.Prepare();

            //PDFExport export = new PDFExport();
            PDFSimpleExport export = new PDFSimpleExport();
            string[] rr = new string[2000000];

            using (MemoryStream ms = new MemoryStream())
            {
                export.Export(report, ms);
                rr[1] = Convert.ToBase64String(ms.ToArray());
                rr[0] = Encoding.ASCII.GetString(ms.ToArray());
                return rr;
            }
        }

        #region List for HO Clerk
        public ActionResult ConciliationHOClerkList()
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {
                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
                var _RoleID = HttpContext.Session.GetInt32("_RoleID");

                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClsAppliactionlst = new ConciliationApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _conciliationRepository.ConciliationHOClerkList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ApplicationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "ConciliationACLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Get Detail by HO Clerk
        public ActionResult AddConciliationHOClerk(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    if (id != null && _ID != 0)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }

                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();

                    ClsRecord = _conciliationRepository.ConciliationHOClerkRecord(ApplicationID);
                    ClsRecord.HOReferStatuList = _Commompository.HOReferActionList(ClsRecord.ApplicationID);

                    //TempData["message"] = "Forwarded To ACL Successfully!";

                    //return RedirectToAction("Index", "ConciliationApplication");
                    return View("AddConciliationHOClerk", ClsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConciliationACLClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        #endregion

        #region update PUS and Refer status by HO clerk
        public JsonResult UpdateClerkStatustoHO(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    //int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    //if (id != null)
                    //{
                    //    ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    //}
                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();
                    ClsRecord.UserID = Convert.ToInt32(_ID);

                    ClsRecord = _conciliationRepository.ConcilupdateHOClerkStatus(Obj);

                    //TempData["message"] = "Forwarded To ACL Successfully!";

                    return Json(new { data = ClsRecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "UpdateClerkStatustoACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        #endregion
        #region Query by HO clerk
        public JsonResult UpdateQueryByHOClerk(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    //int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    //if (id != null)
                    //{
                    //    ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    //}
                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();
                    ClsRecord.UserID = Convert.ToInt32(_ID);

                    ClsRecord = _conciliationRepository.UpdateQueryByHOClerk(Obj);
                    if (ClsRecord.ErrorCode == 0)
                    {
                        SendMailApplicant(ClsRecord.EMailIDList, ClsRecord.EmailSubject, ClsRecord.EmailBody);
                    }
                    return Json(new { data = ClsRecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "UpdateQueryByHOClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        #endregion

        #region List for HO
        public ActionResult ConciliationHOList()
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {
                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
                var _RoleID = HttpContext.Session.GetInt32("_RoleID");

                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClsAppliactionlst = new ConciliationApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _conciliationRepository.ConciliationHOList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ApplicationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "ConciliationACLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Get Detail by HO
        public ActionResult AddConciliationHO(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    if (id != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }

                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();

                    ClsRecord = _conciliationRepository.ConciliationHORecord(ApplicationID);
                    ClsRecord.HOReferStatuList = _Commompository.HOReferActionList(ClsRecord.ApplicationID);

                    //TempData["message"] = "Forwarded To ACL Successfully!";

                    //return RedirectToAction("Index", "ConciliationApplication");
                    return View("AddConciliationHO", ClsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "AddConciliationACLClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        #endregion

        #region update status by HO
        public JsonResult UpdateStatusByHO(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    //int ApplicationID = 0;
                    int UserId = Convert.ToInt32(_ID);
                    //if (id != null)
                    //{
                    //    ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    //}
                    ConciliationApplicationModel ClsRecord = new ConciliationApplicationModel();
                    ClsRecord.UserID = Convert.ToInt32(_ID);

                    ClsRecord = _conciliationRepository.UpdateStatusByHO(Obj);

                    //TempData["message"] = "Forwarded To ACL Successfully!";

                    return Json(new { data = ClsRecord });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ConciliationApplicationController", "UpdateClerkStatustoACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        #endregion

        #region updateEstablishmentDetail BY ACL
        public ActionResult UpdateEshtablishmentdetailByACL(ConciliationApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.IP_Address = IP;
                    ClssaveRecord = _conciliationRepository.UpdateEshtablishmentdetailByACL(Obj);
                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                    }
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
                _Commompository.LogErrorintbl(ex, "ConciliationApplication", "UpdateEshtablishmentdetailByACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        public static MemoryStream GenerateStreamFromString(string[] value)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(value[0] ?? ""));
        }

        public IActionResult SendToLabourEmailPDF1()
        {

            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder

            DataSet ds = new DataSet();
            DataTable Dt1 = ds.Tables.Add("Table1");

            //Dt1.Columns.Add("ACLName", typeof(string));
            //Dt1.Columns.Add("DictrictACLOffice", typeof(string));
            //Dt1.Columns.Add("Date", typeof(string));
            //Dt1.Columns.Add("ApplicantName", typeof(string));
            //Dt1.Columns.Add("Establishmentdetail", typeof(string));
            //Dt1.Columns.Add("AppID", typeof(string));
            Dt1.Columns.Add("DictrictACLOffice", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("Establishmentdetail",typeof(string));
            Dt1.Columns.Add("ApplicantDetail", typeof(string));
            Dt1.Columns.Add("AppID", typeof(string));
            Dt1.Columns.Add("HearingDate", typeof(string));
            Dt1.Columns.Add("HearingTime", typeof(string));
            Dt1.Columns.Add("ACLDistrict", typeof(string));

            //insert row values
            Dt1.Rows.Add(new Object[]{
                 "સેવા સદન-ર-સી અમદાવાદ ૩૬૫૬૦૧",
                 "14/05/2022",
                 "abhi b/108 amar tenament vatva near Mahalami corner ahmedabad",
                 "Manohar singh ahmedabad",
                 "CON00042",
                 "20/04/2022",
                 "20:41",
                 "Ahmedabad"
             });

            // create fields
            //Dt1.Columns.Add("DictrictACLOffice", typeof(string));
            //Dt1.Columns.Add("AppID", typeof(string));
            //Dt1.Columns.Add("ApplicantDetail", typeof(string));
            //Dt1.Columns.Add("Establishmentdetail", typeof(string));
            //Dt1.Columns.Add("ACLName", typeof(string));
            //Dt1.Columns.Add("LCAddress", typeof(string));
            //Dt1.Columns.Add("Date", typeof(string));
            //Dt1.Columns.Add("SendLCDate", typeof(string));

          

            //// insert row values
            //Dt1.Rows.Add(new Object[]{
            //     "Test Chirag",
            //     "Test Chirag",
            //     "Test Chirag",
            //     "Test Chirag",
            //     "Test Chirag",
            //     "Test Chirag",
            //     "Test Chirag",
            //     "Test Chirag"
            //});

           // ds.WriteXmlSchema(webRootPath + "/Documents/Report/ConciliaitonSendToLC.xsd");
           // ds.WriteXmlSchema(webRootPath + "/Documents/Report/ConciliaitonSendToLC.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            report.Report.Load(webRootPath + "/Documents/Report/CouncilationACLHearing.frx");
            //report.Report.Load(path);
            report.Prepare();

            //PDFExport export = new PDFExport();
            PDFSimpleExport export = new PDFSimpleExport();

            using (MemoryStream ms = new MemoryStream())
            {
                export.Export(report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension("Simple List") + ".pdf");
            }

        }
        public string[] PUSEmailPDF(EmailReportModel EmailReportDetail)
        {



            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder



            DataSet ds = new DataSet();
            DataTable Dt1 = ds.Tables.Add("Table1");



            // create fields
            Dt1.Columns.Add("DictrictACLOffice", typeof(string));
            Dt1.Columns.Add("ApplicantDetail", typeof(string));
            Dt1.Columns.Add("Establishmentdetail", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("ACLDistrict", typeof(string));
            Dt1.Columns.Add("EstablishmentName", typeof(string));
            Dt1.Columns.Add("ACLName", typeof(string));

            //Dt1.Columns.Add("ACLName", typeof(string));

            //Dt1.Columns.Add("EstablishmentName", typeof(string));


            //insert row values
            Dt1.Rows.Add(new Object[]{
                 EmailReportDetail.DictrictACLOffice,
                 //EmailReportDetail.AppID,
                 EmailReportDetail.ApplicantDetail,
                 EmailReportDetail.Establishmentdetail,
                 EmailReportDetail.Date,
                 EmailReportDetail.ACLDistrict,
                  EmailReportDetail.EstablishmentName,
                  EmailReportDetail.ACLName,
                 //EmailReportDetail.ACLName,
                 //EmailReportDetail.LCAddress,
               
                 //EmailReportDetail,
                 //EmailReportDetail.ApplicantName,
                
                 //"શ્રમ ભવન,રૂસ્‍તમ કામા માર્ગ, ખાનપુર, અમદાવાદ",
                 //"બાબુભાઈ બળદેવભાઈ ઠાકોર",
                 //"ભુપેન્દ્ર એન્ટરપ્રાઈઝ, C/o, એડીયન્ટ ઈન્ડીયા પ્રા.લી.",
                 //"આર.ડી.પટેલ",
                 //"૧૦-૦૧-૨૦૨૨",
                 //"એડીયન્ટ ઈન્ડીયા પ્રા.લી",
                 //"અમદાવાદ"




            });



            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/pusupadte.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/pusupadte.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            report.Report.Load(webRootPath + "/Documents/Report/pusupadte.frx");

            //report.Report.Load(path);
            report.Prepare();
            string[] rr = new string[2000000];
            //PDFExport export = new PDFExport();
            PDFSimpleExport export = new PDFSimpleExport();
            using (MemoryStream ms = new MemoryStream())
            {
                export.Export(report, ms);
                rr[1] = Convert.ToBase64String(ms.ToArray());
                rr[0] = Encoding.ASCII.GetString(ms.ToArray());



                return rr;
            }
        }

        public IActionResult MultiFile()
        {
            ConciliationApplicationModel ClssaveRecord = new ConciliationApplicationModel();
            return View(ClssaveRecord);
        }

        [HttpPost]
        public IActionResult Upload(ConciliationApplicationModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;
                if (model.Files.Count > 0)
                {
                    foreach (var file in model.Files)
                    {

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);


                        string fileNameWithPath = Path.Combine(path, file.FileName);

                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    model.IsSuccess = true;
                    model.Message = "Files upload successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Please select files";
                }
            }
            return View("MultiFile", model);
        }

        [HttpPost]
        [RequestSizeLimit(60000000)]
        public ActionResult OnPostUpload(List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                string folderName = "Files";
                string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                    }
                }
                return this.Content("Success");
            }
            return this.Content("Fail");
        }
    }
}
