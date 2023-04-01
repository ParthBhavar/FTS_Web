using Microsoft.AspNetCore.Mvc;
using FTS.Business.LicenceApplication;
using FTS.Business.CommonList;
using FTS.Model.Common;
using FTS.Model.Entities;

using FileManager;
using System.Net;
using NPOI.XWPF.UserModel;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using Email;
using FastReport;
using FastReport.Export.PdfSimple;
using static FTS.Model.Constants.Constants;
using FTS.Model.Account;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class LicenceApplicationController : Controller
    {
        public ILicenceApplicationBl _licenceRepository;
        public ICommonListBI _Commompository;
        public IFileUploadService _FileUpload;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IEmailSender _emailSender;
        //private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LicenceApplicationController(ILicenceApplicationBl LicenceRepository, IFileUploadService FileUpload, ICommonListBI commompository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
        {
            this._licenceRepository = LicenceRepository;
            _Commompository = commompository;
            this.webHostEnvironment = webHostEnvironment;
            _FileUpload = FileUpload;
        }
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
        public IActionResult Index()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
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
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            if (_ID != null)
            {
                LicenceApplicationModel ClsAppliactionlst = new LicenceApplicationModel();
                ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                //ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                //ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                //ClsAppliactionlst.TalukaCode = Convert.ToInt32(_TalukaID);
                var List = _licenceRepository.LicenceIMWAppList(ClsAppliactionlst);
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
        #region Add LicenceApplication
        public ActionResult AddLicenceApplication(string id, int ISACL, int ISDCL, int ISView, int ISHO, int ISAmendment, int IsEdit, int stpid = 1)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            if (_ID != null)
            {
                int ApplicationID = 0;
                if (id != null)
                {
                    ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                }
                LicenceApplicationModel ClsAppliaction = new LicenceApplicationModel();
                ClsAppliaction.UserID = Convert.ToInt32(_ID);
                ClsAppliaction.UserMode = Convert.ToInt32(_UserMode);
                ClsAppliaction.ApplicationID = ApplicationID;
                ClsAppliaction.ISAmendment = ISAmendment;
                ClsAppliaction.IsEdit = IsEdit;

                ClsAppliaction = _licenceRepository.AppliactionRecord(ClsAppliaction);
                //var TalukaList = _Commompository.TalukaList(ApplicationID, ClsAppliaction.DistrictID);
                //var DistrictList = _Commompository.DistrictList(ClsAppliaction.Establishmentregistrationdetaillst[0].DistrictID);
                var DistrictList = _Commompository.DistrictList(ClsAppliaction.DistrictID);
                var TalukaList = _Commompository.TalukaList(ApplicationID, ClsAppliaction.DistrictID);
                var AreaList = _Commompository.AreaList(ClsAppliaction.ApplicationID, ClsAppliaction.DistrictID);

                var EDistrictList = _Commompository.DistrictList(ClsAppliaction.EDistrictID);
                var ETalukaList = _Commompository.TalukaList(ApplicationID, ClsAppliaction.EDistrictID);
                var EAreaList = _Commompository.AreaList(ClsAppliaction.ApplicationID, ClsAppliaction.EDistrictID);
                var TypeOfBusinessTradeList = _Commompository.TypeOfBusinessTradeList();
                //var GanderList = _Commompository.GanderList();
                ClsAppliaction.Talukalist = TalukaList;
                ClsAppliaction.Districtlist = DistrictList;
                ClsAppliaction.AreaList = AreaList;

                ClsAppliaction.EstablisTalukalist = ETalukaList;
                ClsAppliaction.EstablisDistrictlist = EDistrictList; 
                ClsAppliaction.EstablishAreaList = EAreaList;

                //ClsAppliaction.EstablishmentList = EstablishmentList;
                ClsAppliaction.PrincipalEmployerInformationdetaillst[0].Talukalist = TalukaList;
                ClsAppliaction.PrincipalEmployerInformationdetaillst[0].Districtlist = DistrictList;
                ClsAppliaction.PrincipalEmployerInformationdetaillst[0].AreaList = AreaList;
                ClsAppliaction.TypeOfEstablishmentList = TypeOfBusinessTradeList;
                ClsAppliaction.ApplicationnIDEdit = ApplicationID;
                ClsAppliaction.ISAmendment = ISAmendment;
                ClsAppliaction.ISACL = ISACL;
                ClsAppliaction.ISDCL = ISDCL;
                ClsAppliaction.ISView = ISView;
                ClsAppliaction.IsEdit = IsEdit;
                ClsAppliaction.ISHO = ISHO;
                ClsAppliaction.stpid = stpid;
                //ClsAppliaction.ISView = ISView;

                var i = 0;
                if (ClsAppliaction.PrincipalEmployerInformationdetaillst.Count > 0)
                {
                    for (i = 0; i < ClsAppliaction.PrincipalEmployerInformationdetaillst.Count; ++i)
                    {
                        ClsAppliaction.PrincipalEmployerInformationdetaillst[i].Districtlist = _Commompository.DistrictList(ClsAppliaction.PrincipalEmployerInformationdetaillst[i].PrincipalID);
                        ClsAppliaction.PrincipalEmployerInformationdetaillst[i].Talukalist = _Commompository.TalukaList(ClsAppliaction.PrincipalEmployerInformationdetaillst[i].PrincipalID, ClsAppliaction.PrincipalEmployerInformationdetaillst[i].DistrictID);
                        ClsAppliaction.PrincipalEmployerInformationdetaillst[i].AreaList = _Commompository.AreaList(ClsAppliaction.PrincipalEmployerInformationdetaillst[i].PrincipalID, ClsAppliaction.PrincipalEmployerInformationdetaillst[i].DistrictID);
                    }
                }
                //ClsAppliaction.Ganderlist = GanderList;
                //ClsAppliaction.AppliactionIDEdit = ApplicationID;
                return PartialView(ClsAppliaction);
                //return View("AddLicenceApplication", ClsAppliaction);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

        #region Save Contractor Details
        public JsonResult SaveContractorAppliactionRecord(LicenceApplicationModel Obj)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");

            if (_ID != null)
            {
                var IP = heserver.AddressList[1].ToString();
                Obj.UserID = Convert.ToInt32(_ID);
                Obj.UserMode = Convert.ToInt32(_UserMode);
                Obj.IPAddress = IP;
                LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
                ClssaveRecord = _licenceRepository.SaveContractorAppliactionRecord(Obj);
                ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.ApplicationID.ToString());
                return Json(new { data = ClssaveRecord });
                //var d = SessionExtensions[]
            }
            else
            {
                RedirectToAction("Index", "Home");
                return Json(new { data = "" });
            }
        }
        #endregion

        #region [HttpPost] Taluka Area list
        public JsonResult AllList(int mode, int DistrictID)
        {
            LicenceApplicationModel List = new LicenceApplicationModel();
            List.Talukalist = _Commompository.TalukaList(mode, DistrictID);                             
            List.AreaList = _Commompository.AreaList(mode, DistrictID);
            return Json(new { data = List });
        }
        #endregion

        #region Save Establishment Detail
        public JsonResult SaveEstablishmentLicenceRecord(LicenceApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.UserMode = Convert.ToInt32(_UserMode);
                    Obj.IP_Address = IP;
                    LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
                    ClssaveRecord = _licenceRepository.SaveEstablishmentLicenceRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.ApplicationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "LicenceApplication", "SaveEstablishmentLicenceRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #endregion

        #region Save Principle Employer Details
        public JsonResult SavePrincipalEmployerLicenceRecord(LicenceApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.UserMode = Convert.ToInt32(_UserMode);
                    Obj.IP_Address = IP;
                    LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
                    ClssaveRecord = _licenceRepository.SavePrincipalEmployerLicenceRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.ApplicationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "SavePrincipalEmployerRegistrationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #endregion

        #region Save Licence and Security Details
        public IActionResult SaveLicenceAndSecurityRecord(List<IFormFile> MyUploader, int ApplicationID, int ChallanNo, string ChallanDate, decimal LicenceFee, string TreasuryName, int SecurityChallanNo, string SecurityChallanDate,decimal SecurityDeposit, string TreasurName)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int i = 0;
                    int F = 0;
                    LicenceApplicationModel ClssaveEstablishment = new LicenceApplicationModel();
                    LicenceApplicationModel ClsBundleBreak = new LicenceApplicationModel();
                    var TreasurDoc = "";
                    var SecurityDoc = "";
                   
                    if (MyUploader.Count > 0)
                    {
                        for (i = 0; i < MyUploader.Count; i++)
                        {
                            //CessProjectDetailsModel ClsBundleBreak = new CessProjectDetailsModel();

                            string type = "";
                            if (MyUploader[i].ContentType == "application/pdf") { type = ".pdf"; }
                            else if (MyUploader[i].ContentType == "application/vnd.ms-word") { type = ".docx"; }
                            else if (MyUploader[i].ContentType == "application/vnd.ms-excel") { type = ".xls"; }
                            else if (MyUploader[i].ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { type = ".xlsx"; }
                            else if (MyUploader[i].ContentType == "image/jpeg") { type = ".jpeg"; }
                            else if (MyUploader[i].ContentType == "image/png") { type = ".png"; };


                            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "LicenceApplication");
                            //string webRootPath = webHostEnvironment.WebRootPath;
                            //string uploadsFolder = Path.Combine(webRootPath, "Documents", "LicenceApplication", "ChallanDoc");
                            string filePath = uploadsFolder;
                            string FileName;


                            // string output = "";
                            var output = false;
                            if (i == 0)
                            {
                                TreasurDoc = $"Licence{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                                output = _FileUpload.UploadDocument(MyUploader[i], TreasurDoc, filePath);
                            }
                            else if (i == 1)
                            {
                                SecurityDoc = $"Security{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                                output = _FileUpload.UploadDocument(MyUploader[i], SecurityDoc, filePath);
                            }
                          


                            ClsBundleBreak.ApplicationID = ApplicationID;
                            ClsBundleBreak.ChallanNo = ChallanNo;
                            ClsBundleBreak.DbInChallanDate = ChallanDate;
                            ClsBundleBreak.LicenceFee = LicenceFee;
                            ClsBundleBreak.TreasuryName = TreasuryName;
                            ClsBundleBreak.SecurityChallanNo = SecurityChallanNo;
                            ClsBundleBreak.DbInSecurityChallanDate = SecurityChallanDate;
                            ClsBundleBreak.TreasurName = TreasurName;
                            ClsBundleBreak.SecurityDeposit = SecurityDeposit;
                            //  ClsBundleBreak.fileName = FileName;


                        }
                        ClsBundleBreak.LicecneDoc = TreasurDoc;
                        ClsBundleBreak.SecurityDoc = SecurityDoc;
                       

                        var output1 = _licenceRepository.SaveLicenceAndSecurityRecord(ClsBundleBreak);
                        //ClsBundleBreak = _ProjectDetailspository.SaveCessCollectionDetails(ClsBundleBreak);
                        ClssaveEstablishment = output1;
                        if (output1.ErrorCode == 0)
                        {
                            F = i;
                        }

                    }
                    if (F != 2)
                    {
                        ClssaveEstablishment.ErrorCode = 1;
                        ClssaveEstablishment.ErrorMassage = "You are already upoaded Document if you need update So please select Document ";
                    }

                    //if file not selected than status is 3
                    return Json(new { status = ClssaveEstablishment });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "SavePrincipalEmployerRegistrationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
            //try
            //{

            //    if (_ID != null && _ID != 0)
            //    {
            //        Obj.UserID = Convert.ToInt32(_ID);
            //        Obj.UserMode = Convert.ToInt32(_UserMode);
            //        Obj.IP_Address = IP;
            //        LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
            //        ClssaveRecord = _licenceRepository.SaveLicenceAndSecurityRecord(Obj);
            //        ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.ApplicationID.ToString());
            //        return Json(new { data = ClssaveRecord });
            //    }
            //    else
            //    {
            //        RedirectToAction("Index", "Home");
            //        return Json(new { data = "" });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _Commompository.LogErrorintbl(ex, "LicenceApplication", "SaveLicenceAndSecurityRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
            //    return new JsonResult(ex.Message)
            //    {
            //        StatusCode = (int)HttpStatusCode.InternalServerError
            //    };
            //}

        }
        #endregion

        #region Save Documents
        [HttpPost]
        [RequestSizeLimit(60000000)]
        public IActionResult DocumentUploader(List<IFormFile> MyUploader, int ApplicationID, int IsSubmit, string URL, string AppID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int i = 0;
                    int F = 0;
                    LicenceApplicationModel ClsEstablishment = new LicenceApplicationModel();
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


                            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "LicenceApplication");
                            string filePath = uploadsFolder;
                            string FileName;
                            if (i == 0)
                            {
                                FileName = $"FactoryLicence_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (i == 1)
                            {
                                FileName = $"GumastaCertificate_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (i == 2)
                            {
                                FileName = $"EPFCodeCertificate_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (i == 3)
                            {
                                FileName = $"ESICodeCertificate_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (i == 4)
                            {
                                FileName = $"Pancard_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else
                            {
                                FileName = $"Others_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            //string FileName = MyUploader[i].FileName;
                            LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
                            ClssaveRecord.ApplicationID = ApplicationID;
                            ClssaveRecord.Filename = FileName;
                            ClssaveRecord.DocumentID = i;
                            ClssaveRecord.URL = URL;
                            ClssaveRecord.UserID = Convert.ToInt32(_ID);
                            ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                            ClssaveRecord.IP_Address = IP;
                            ClssaveRecord.AppID = AppID;

                            if (IsSubmit == 1) { ClssaveRecord.IsSubmit = true; } else { ClssaveRecord.IsSubmit = false; }

                            var output = _FileUpload.UploadDocument(MyUploader[i], FileName, filePath);
                            var output1 = _licenceRepository.SaveDocumnetandapplication(ClssaveRecord);
                            ClsEstablishment = output1;
                            ClsEstablishment.stpid = 5;
                            ClsEstablishment.TEMPID = Encrypt_Decrypt.Encrypt(output1.ApplicationID.ToString());
                            if (output == true && output1.ErrorCode == 0)
                            {
                                F = i;
                            }

                        }

                    }
                    //if (F != 3)
                    //{
                    //    ClsEstablishment.ErrorCode = 1;
                    //    ClsEstablishment.ErrorMassage = "You are already upoaded Document if you need update So please select Document ";
                    //}

                    //if file not selected than status is 3
                    return Json(new { status = ClsEstablishment });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "DocumentUploader", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }


        }
        #endregion

        #region List for ACL
        public ActionResult LicenceACLList()
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
                    LicenceApplicationModel ClsAppliactionlst = new LicenceApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _licenceRepository.LicenceACLList(ClsAppliactionlst);
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "LicenceACLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "SendMailApplicant", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                throw;
            }
        }

        #region Licence Approve Reject
        public ActionResult ApproveOrRejectLicenceFromACL(string id, int Status, string URL)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    if (id != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }


                    LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.ActionCode = Status;
                    ClssaveRecord = _licenceRepository.ApproveOrRejectLicenceByACL(ClssaveRecord);
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
                    //if (ClssaveRecord.ErrorCode == 0)
                    //{
                    //    SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                    //}
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "ApproveOrRejectLicenceFromACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #endregion

        #region Licence Query by ACL
        public ActionResult LicenceQueryByACL(string Comments, string id, string URL)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    if (id != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }


                    LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.QueryComments = Comments;
                    ClssaveRecord = _licenceRepository.LicenceQueryByACL(ClssaveRecord);
                    //if (ClssaveRecord.ErrorCode == 0)
                    //{
                    //    var test = SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                    //}

                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        int i = 0;
                        int v = 0;

                        if (ClssaveRecord.ApplicantMailDetail.Count > 0)
                        {
                            for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
                            {
                                if (ClssaveRecord.ApplicantMailDetail[i].Email != null)
                                {
                                    var test = SendMailApplicant(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, ClssaveRecord.ApplicantMailDetail[i].Body);
                                }
                            }
                        }

                        if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
                        {
                            for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
                            {
                                if (ClssaveRecord.EshtablishmentMailDetail[v].Email != null)
                                {
                                    var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, ClssaveRecord.EshtablishmentMailDetail[v].Body);
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "SendtoACLFromClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #endregion

        #region Licence Query by ACL
        public ActionResult LicenceQueryByDCLHO(string Comments, string id, string URL)   // Query to HO ACL
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    if (id != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }


                    LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.QueryComments = Comments;
                    ClssaveRecord = _licenceRepository.LicenceQueryByDCLHO(ClssaveRecord);
                    //if (ClssaveRecord.ErrorCode == 0)
                    //{
                    //    var test = SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                    //}

                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        int i = 0;
                        int v = 0;

                        if (ClssaveRecord.ApplicantMailDetail.Count > 0)
                        {
                            for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
                            {
                                if (ClssaveRecord.ApplicantMailDetail[i].Email != null)
                                {
                                    var test = SendMailApplicant(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, ClssaveRecord.ApplicantMailDetail[i].Body);
                                }
                            }
                        }

                        if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
                        {
                            for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
                            {
                                if (ClssaveRecord.EshtablishmentMailDetail[v].Email != null)
                                {
                                    var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, ClssaveRecord.EshtablishmentMailDetail[v].Body);
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "SendtoACLFromClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #endregion

        #region DCL Licence List
        public ActionResult LicenceApplicationHODCLList()
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
            var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
            var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
            var _RoleID = HttpContext.Session.GetInt32("_RoleID");

            try
            {

                if (_ID != null && _ID != 0)
                {
                    LicenceApplicationModel ClsAppliactionlst = new LicenceApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _licenceRepository.LicenceRegistrationDCLList(ClsAppliactionlst);
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "LicenceRegistrationDCLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region HO ACL Licence List
        public ActionResult LicenceApplicationHOACLList()
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
            var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
            var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
            var _RoleID = HttpContext.Session.GetInt32("_RoleID");

            try
            {

                if (_ID != null && _ID != 0)
                {
                    LicenceApplicationModel ClsAppliactionlst = new LicenceApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _licenceRepository.LicenceApplicationHOList(ClsAppliactionlst);
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "LicenceRegistrationDCLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Approve and Reject by ACL
        //public ActionResult LicenceApproveRejectByACL(LicenceApplicationModel obj)
        //{
        //    var _ID = HttpContext.Session.GetInt32("_ID");
        //    var _UserMode = HttpContext.Session.GetInt32("_UserMode");
        //    var IP = heserver.AddressList[1].ToString();
        //    try
        //    {
        //        if (_ID != null && _ID != 0)
        //        {

        //            LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();

        //            obj.UserID = Convert.ToInt32(_ID);
        //            obj.UserMode = Convert.ToInt32(_UserMode);
        //            obj.IP_Address = IP;
        //            ClssaveRecord = _licenceRepository.LicenceApproveRejectByACL(obj);

        //            return Json(new { data = ClssaveRecord });
        //        }
        //        else
        //        {
        //            RedirectToAction("Index", "Home");
        //            return Json(new { data = "" });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "SendtoCommentFromACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
        //        return new JsonResult(ex.Message)
        //        {
        //            StatusCode = (int)HttpStatusCode.InternalServerError
        //        };
        //    }

        //}
        [HttpPost]
        [RequestSizeLimit(20000000)]
        public IActionResult LicenceApproveRejectByACL(IFormFile MyUploader, LicenceApplicationModel obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "LicenceApplication");
                    string filePath = uploadsFolder;
                    string FileName = "";
                    bool output = false;

                    ClssaveRecord.IsCLRA_verified = obj.IsCLRA_verified;
                    ClssaveRecord.IsIMW_verified = obj.IsIMW_verified;
                    ClssaveRecord.ISMTW_verified = obj.ISMTW_verified;
                    ClssaveRecord.IMWNote = obj.IMWNote;
                    ClssaveRecord.MTWNote = obj.MTWNote;
                    ClssaveRecord.CLRANote = obj.CLRANote;
                    ClssaveRecord.IsCLRA = obj.IsCLRA;
                    ClssaveRecord.IsIMW = obj.IsIMW;
                    ClssaveRecord.ISMTW = obj.ISMTW;
                    ClssaveRecord.ApplicationID = obj.ApplicationID;
                    ClssaveRecord.DCL_Review_Comments = obj.DCL_Review_Comments;
                    ClssaveRecord.ACL_Review_Comments = obj.ACL_Review_Comments;
                    ClssaveRecord.URL = obj.URL;
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                    ClssaveRecord.IP_Address = IP;

                    if (MyUploader != null)
                    {
                        string type = "";
                        if (MyUploader.ContentType == "application/pdf") { type = ".pdf"; }
                        else if (MyUploader.ContentType == "application/vnd.ms-word") { type = ".docx"; }
                        else if (MyUploader.ContentType == "application/vnd.ms-excel") { type = ".xls"; }
                        else if (MyUploader.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { type = ".xlsx"; }
                        else if (MyUploader.ContentType == "image/jpeg") { type = ".jpeg"; }
                        else if (MyUploader.ContentType == "image/png") { type = ".png"; };

                        FileName = $"ApproveRejectDoc_{DateTime.Now:yyyyMMdd_hhmm}_{obj.ApplicationID}" + type;
                        ClssaveRecord.fileName = FileName;

                        output = _FileUpload.UploadDocument(MyUploader, FileName, filePath);
                        ClssaveRecord = _licenceRepository.LicenceApproveRejectByACL(ClssaveRecord);
                    }
                    else
                    {
                        ClssaveRecord = _licenceRepository.LicenceApproveRejectByACL(ClssaveRecord);
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "LicenceApproveRejectByACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #endregion

        #region forward Application from ACL HO To DCL HO 
        public ActionResult LicenceAppForwardToDCLHO(string id, string URL, string Comments)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    if (id != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }

                    LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.Comments = Comments;
                    ClssaveRecord = _licenceRepository.LicenceAppForwardToDCLHO(ClssaveRecord);

                    //if (ClssaveRecord.ErrorCode == 0)
                    //{
                    //    int i = 0;
                    //    int v = 0;

                    //    if (ClssaveRecord.ApplicantMailDetail.Count > 0)
                    //    {
                    //        for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
                    //        {
                    //            if (ClssaveRecord.ApplicantMailDetail[i].Email != null)
                    //            {
                    //                var test = SendMailApplicant(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, ClssaveRecord.ApplicantMailDetail[i].Body);
                    //            }
                    //        }
                    //    }

                    //    if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
                    //    {
                    //        for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
                    //        {
                    //            if (ClssaveRecord.EshtablishmentMailDetail[v].Email != null)
                    //            {
                    //                var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                    //            }
                    //        }
                    //    }
                    //}

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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "SendtoACLFromClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #endregion

        #region document download
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
                    var path = Path.Combine("wwwroot", "Documents", "LicenceApplication", FileName);
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "DownloadFile", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region final Document Submit
        public JsonResult LicenceApplicationUpdateSubmit(LicenceApplicationModel Obj)
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
                    LicenceApplicationModel ClssaveRecord = new LicenceApplicationModel();
                    ClssaveRecord = _licenceRepository.LicenceApplicationUpdateSubmit(Obj);
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
                _Commompository.LogErrorintbl(ex, "LicenceApplicationController", "ApplicationRegistrationUpdateSubmit", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region Licence Amandment List
        public ActionResult LicenceAmendmentList()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _MoNo = HttpContext.Session.GetString("_MoNo");
            var IP = heserver.AddressList[1].ToString();

            try
            {
                //var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                //var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                //var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");

                if (_ID != null && _ID != 0)
                {
                    LicenceApplicationModel ClsAppliactionlst = new LicenceApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.mobile = _MoNo;

                    //ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    //ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    //ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    var List = _licenceRepository.LicenceAmendmentList(ClsAppliactionlst);
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
                _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

    }
}

