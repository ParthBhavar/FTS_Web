using FTS.Business.CommonList;
using FTS.Business.TFormApplication;
using FTS.Model.Common;
using FTS.Model.Entities;
using FileManager;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Email;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class TFormApplicationController : Controller
    {
        public ITFormApplicationBl _tFormApplicationpository;
        public ICommonListBI _Commompository;
        public IFileUploadService _FileUpload;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly IHttpContextAccessor httpContextAccessor;
        public TFormApplicationController(ITFormApplicationBl _TFormApplicationpository, ICommonListBI commompository, IWebHostEnvironment webHostEnvironment, IFileUploadService fileUpload, IEmailSender emailSender)
        {
            this._tFormApplicationpository = _TFormApplicationpository;
            _Commompository = commompository;
            this.webHostEnvironment = webHostEnvironment;
            _FileUpload = fileUpload;
            _emailSender = emailSender;
        }
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());

        public IActionResult Index()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
                if (_ID != null && _ID != 0)
                {
                    TFormAppealApplicationModel ClsAppliactionlst = new TFormAppealApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    var List = _tFormApplicationpository.TFormApplicationList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.AppealID.ToString());
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
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
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

        #region Add T Form Application for Employee
        public ActionResult AddTFormForEmployee(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int AppealID = 0;
                    if (id != null)
                    {
                        AppealID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }
                    TFormAppealApplicationModel ClsAppliaction = new TFormAppealApplicationModel();
                    ClsAppliaction = _tFormApplicationpository.TFormForEmployeeRecord(AppealID);

                    ClsAppliaction.ApplicationIDEdit = AppealID;

                    
                    if (ClsAppliaction.ApplicationID != 0)
                    {
                        return View("AddTFormForEmployee", ClsAppliaction);
                    }
                    else
                    {
                        TempData["message"] = "You can not create Appeal Request !";
                        return RedirectToAction("TFormEmployerList", "TFormApplication");
                    }

                }
                else
   
                {
                    return RedirectToAction("Index", "Home");

                }

            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "AddTFormForEmployee", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }


        }
        #endregion

        #region Add T Form Application for Employer
        public ActionResult AddTFormForEmployer(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            try
            {

                if (_ID != null && _ID != 0)
                {
                    int AppealID = 0;
                    if (id != null)
                    {
                        AppealID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }
                    TFormAppealApplicationModel ClsAppliaction = new TFormAppealApplicationModel();
                    ClsAppliaction = _tFormApplicationpository.TFormForEmployerRecord(AppealID);
                    ClsAppliaction.ApplicationIDEdit = AppealID;
                    if (ClsAppliaction.ApplicationID != 0)
                    {
                        return View("AddTFormForEmployer", ClsAppliaction);
                    }
                    else
                    {
                        return View("AddTFormForEmployer", ClsAppliaction);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "AddTFormForEmployer", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }




        }
        #endregion

        #region T Form Employer List
        public IActionResult TFormEmployerList()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
                if (_ID != null && _ID != 0)
                {
                    TFormAppealApplicationModel ClsAppliactionlst = new TFormAppealApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(1);
                    var List = _tFormApplicationpository.TFormApplicationList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.AppealID.ToString());
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
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "AddTFormForEmployer", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region Save T Form Application for Employee
        public JsonResult SaveTFormForEmployee(TFormAppealApplicationModel Obj)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                  
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.UserMode = Convert.ToInt32(_UserMode);
                    Obj.IPAddress = IP;
                    TFormAppealApplicationModel ClssaveRecord = new TFormAppealApplicationModel();
                    ClssaveRecord = _tFormApplicationpository.SaveTFormForEmployee(Obj);
                    if (ClssaveRecord.ErrorCode == 0)
                    {
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
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "SaveTFormForEmployee", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region Save T Form Application for Employer
        public IActionResult SaveTFormForEmployer(IFormFile MyUploader,string ApplicantName, int RefNApplicationID, int AppealID, string ChallanNo, int ChallanAmount, DateTime ChallanDate, int CaseFavourIn, string AppealReason, string URL)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    string type = "";
                    if (MyUploader.ContentType == "application/pdf") { type = ".pdf"; }
                    else if (MyUploader.ContentType == "application/vnd.ms-word") { type = ".docx"; }
                    else if (MyUploader.ContentType == "application/vnd.ms-excel") { type = ".xls"; }
                    else if (MyUploader.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { type = ".xlsx"; }
                    else if (MyUploader.ContentType == "image/jpeg") { type = ".jpeg"; }
                    else if (MyUploader.ContentType == "image/png") { type = ".png"; };


                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath,"Documents", "Appeal", "Challan");
                    string filePath = uploadsFolder;
                    string FileName = $"ChallanDoc_{DateTime.Now:yyyyMMdd_hhmm}_{AppealID}" + type;

                    //string FileName = MyUploader[i].FileName;
                    TFormAppealApplicationModel ClssaveRecord = new TFormAppealApplicationModel();
                    ClssaveRecord.ApplicantName = ApplicantName;
                    ClssaveRecord.RefNApplicationID = RefNApplicationID;
                    ClssaveRecord.AppealID = AppealID;
                    ClssaveRecord.ChallanFile = FileName;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.UserMode = Convert.ToInt32(1);
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.ChallanNo = ChallanNo;
                    ClssaveRecord.ChallanAmount = ChallanAmount;
                    ClssaveRecord.DateOfChallan = ChallanDate;
                    ClssaveRecord.CaseFavourIn = CaseFavourIn;
                    ClssaveRecord.AppealReason = AppealReason;
                    ClssaveRecord.URL = URL;


                    var output = _FileUpload.UploadDocument(MyUploader, FileName, filePath);
                    ClssaveRecord = _tFormApplicationpository.SaveTFormForEmployer(ClssaveRecord);

                    if (ClssaveRecord.ErrorCode == 0)
                    {
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
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "SaveTFormForEmployer", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Appeal List for DCL
        public ActionResult TFormApplicationDCLList()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
                var _RoleID = HttpContext.Session.GetInt32("_RoleID");

                if (_ID != null && _ID != 0)
                {
                    TFormAppealApplicationModel ClsAppliactionlst = new TFormAppealApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _tFormApplicationpository.TFormApplicationDCLList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.AppealID.ToString());
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
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "TFormApplicationDCLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Add N Form Status update by DCL
        public ActionResult AddTFormDCLStatusUpdate(string id, int ISNew)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int AppealID = 0;
                    int IsACL = 0;
                    if (id != null)
                    {
                        AppealID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }
                    TFormAppealApplicationModel ClsRecord = new TFormAppealApplicationModel();
                    if (ISNew == 0)
                    {
                        ClsRecord = _tFormApplicationpository.TFormDCLStatusUpdate(AppealID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.AppealID, IsACL);

                    }
                    else if (ISNew == 1)
                    {
                        ClsRecord = _tFormApplicationpository.TFormDCLStatusUpdate(AppealID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.AppealID, IsACL);

                    }
                    else if (ISNew == 2)
                    {
                        ClsRecord = _tFormApplicationpository.TFormDCLStatusUpdate(AppealID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.AppealID, IsACL);

                    }
                    else if (ISNew == 3)
                    {
                        ClsRecord = _tFormApplicationpository.TFormDCLStatusUpdate(AppealID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.AppealID, IsACL);

                    }
                    var B = 0;
                    if (ClsRecord.HearingdetailACL.Count > 0)
                    {
                        for (B = 0; B < ClsRecord.HearingdetailACL.Count; ++B)
                        {
                            ClsRecord.HearingdetailACL[B].HearingReasonList = ClsRecord.HearingReasonList;
                        }
                    }
                    //ClsRecord = _conciliationRepository.ConciliationACLRecord(ApplicationID);
                    return View("AddFormDCLStatusUpdate", ClsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "AddTFormDCLStatusUpdate", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Update DCL Hearing Status
        public JsonResult SaveTFormHearingDCLRecord(TFormAppealApplicationModel Obj)
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
                    TFormAppealApplicationModel ClssaveRecord = new TFormAppealApplicationModel();
                    ClssaveRecord = _tFormApplicationpository.SaveTFormHearingDCLRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "SaveTFormHearingDCLRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region Update DCL Remand Back Status
        public IActionResult SaveTFormResolutionDCLRecord(IFormFile MyUploader, int AppealID, string RemandBackDate, string RemandRemark, string URL, int ResolutionReasonId, int RefNApplicationID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    string type = "";
                    if (MyUploader.ContentType == "application/pdf") { type = ".pdf"; }
                    else if (MyUploader.ContentType == "application/vnd.ms-word") { type = ".docx"; }
                    else if (MyUploader.ContentType == "application/vnd.ms-excel") { type = ".xls"; }
                    else if (MyUploader.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { type = ".xlsx"; }
                    else if (MyUploader.ContentType == "image/jpeg") { type = ".jpeg"; }
                    else if (MyUploader.ContentType == "image/png") { type = ".png"; };


                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "Appeal", "Resolution");
                    string filePath = uploadsFolder;
                    string FileName = $"AppealDoc_{DateTime.Now:yyyyMMdd_hhmm}_{AppealID}" + type;

                    //string FileName = MyUploader[i].FileName;
                    TFormAppealApplicationModel ClssaveRecord = new TFormAppealApplicationModel();
                    ClssaveRecord.AppealID = AppealID;
                    ClssaveRecord.fileName = FileName;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.ResolutionDateIn = RemandBackDate;
                    ClssaveRecord.RemandRemark = RemandRemark;
                    ClssaveRecord.ResolutionReasonID = ResolutionReasonId;
                    ClssaveRecord.RefNApplicationID = RefNApplicationID;


                    var output = _FileUpload.UploadDocument(MyUploader, FileName, filePath);
                    ClssaveRecord = _tFormApplicationpository.SaveTFormResolutionBackDCLRecord(ClssaveRecord);
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
                                var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.EshtablishmentMailDetail[v].Email.ToString() }, ClssaveRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                            }
                        }
                    }
                    //Obj.UserID = Convert.ToInt32(_ID);
                    //Obj.IP_Address = IP;
                    //TFormAppealApplicationModel ClssaveRecord = new TFormAppealApplicationModel();
                    //ClssaveRecord = _tFormApplicationpository.SaveTFormRemandBackDCLRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "SaveTFormHearingDCLRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region Update DCL Confirm Status
        public JsonResult SaveTFormConfirmDCLRecord(TFormAppealApplicationModel Obj)
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
                    TFormAppealApplicationModel ClssaveRecord = new TFormAppealApplicationModel();
                    ClssaveRecord = _tFormApplicationpository.SaveTFormConfirmDCLRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "SaveTFormHearingDCLRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region Update DCL Hearing Status
        public JsonResult SaveTFormDismissDCLRecord(TFormAppealApplicationModel Obj)
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
                    TFormAppealApplicationModel ClssaveRecord = new TFormAppealApplicationModel();
                    ClssaveRecord = _tFormApplicationpository.SaveTFormDismissDCLRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "SaveTFormHearingDCLRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #endregion
        #region GetTFormHistory
        public IActionResult TFormDCLHistory(TFormAppealApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    TFormAppealApplicationModel ClssaveRecord = new TFormAppealApplicationModel();
                    ClssaveRecord = _tFormApplicationpository.TFormDCLHistory(Obj);
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
                _Commompository.LogErrorintbl(ex, "TFormApplicationController", "TFormDCLHistory", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        #endregion

        #region Applicant Status History
        public ActionResult StatusHistory(string id, int ISNew ,string apealid, int ISView)
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
                        ApplicationID = Convert.ToInt32(id);
                        //AppealID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }
                    TFormAppealApplicationModel ClsRecord = new TFormAppealApplicationModel();
                    if (ISNew == 0) //1 Mins Hearing Date 
                    {
                        ClsRecord = _tFormApplicationpository.TFormACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID,IsACL);

                    }
                    else if (ISNew == 1) //1 Mins Close Case 
                    {
                        ClsRecord = _tFormApplicationpository.TFormACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID, IsACL);
                    }

                    var B = 0;
                    if (ClsRecord.HearingdetailACL.Count > 0)
                    {
                        for (B = 0; B < ClsRecord.HearingdetailACL.Count; ++B)
                        {
                            ClsRecord.HearingdetailACL[B].HearingReasonList = ClsRecord.HearingReasonList;
                        }
                    }

                    ClsRecord.ApplicationID = ApplicationID;
                    ClsRecord.ISView = ISView;
                    ClsRecord.IP_Address = IP;
                    return View("StatusHistory", ClsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "AddNFormHearingALC", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }


        }
        #endregion 

    }
}
