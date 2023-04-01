using FTS.Business.CommonList;
using FTS.Business.INFormApplication;
using FTS.Model;
using FTS.Model.Account;
using FTS.Model.Common;
using FTS.Model.Entities;
using Email;
using FastReport;
using FastReport.Export.PdfSimple;
using FileManager;
using Microsoft.AspNetCore.Mvc;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using static FTS.Model.Constants.Constants;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class NFormApplicationController : Controller
    {
        public INFormApplicationBI _NFormApplicationpository;
        public ICommonListBI _Commompository;
        public IFileUploadService _FileUpload;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public NFormApplicationController(INFormApplicationBI _NFormApplicationpository, ICommonListBI commompository, IFileUploadService FileUpload, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
        {

            this._NFormApplicationpository = _NFormApplicationpository;
            _Commompository = commompository;
            _FileUpload = FileUpload;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }

        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());

        public ActionResult Index()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {

                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
                var _PositionID = HttpContext.Session.GetInt32("_PositionID");
                var _RoleID = HttpContext.Session.GetInt32("_RoleID");

                if (_ID != null && _ID != 0)
                {
                    NFormApplicationModel ClsAppliactionlst = new NFormApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.PositionID = Convert.ToInt32(_PositionID);
                    ClsAppliactionlst.PositionDistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    var List = _NFormApplicationpository.INFormList(ClsAppliactionlst);
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public ActionResult AddNFormApplication(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int AppliactionID = 0;
                    if (id != null)
                    {
                        AppliactionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }

                    NFormApplicationModel ClsAppliaction = new NFormApplicationModel();
                    ClsAppliaction = _NFormApplicationpository.NFormAppliactionRecord(AppliactionID);
                    var TalukaList = _Commompository.TalukaList(AppliactionID, ClsAppliaction.DistrictID);
                    var DistrictList = _Commompository.DistrictList(AppliactionID);
                    var GanderList = _Commompository.GanderList();
                    var AreaList = _Commompository.AreaList(AppliactionID, ClsAppliaction.DistrictID);
                    ClsAppliaction.Talukalist = TalukaList;
                    ClsAppliaction.Districtlist = DistrictList;
                    ClsAppliaction.Ganderlist = GanderList;
                    ClsAppliaction.DepartmentList = GanderList;
                    ClsAppliaction.AppliactionIDEdit = AppliactionID;
                    ClsAppliaction.AreaList = AreaList;
                    return View("AddNFormApplication", ClsAppliaction);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "AddNFormApplication", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public JsonResult SaveNFormApplicationRecord(NFormApplicationModel Obj)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
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
                    Obj.IP_Address = IP;
                    NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                    ClssaveRecord = _NFormApplicationpository.SaveNFormAppliactionRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SaveNFormApplicationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public ActionResult AddnFormEstablishment(int ApplicationID, int ISNew)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                //int AppliactionID = 0;

                //if (id != null)
                //{
                //    AppliactionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                //}
                if (_ID != null && _ID != 0)
                {
                    NFormApplicationModel ClsEstablishment = new NFormApplicationModel();
                    ClsEstablishment.ClsEstablish = _NFormApplicationpository.NFormEstablishmentRecord(ApplicationID, ISNew);
                    var DistrictList = _Commompository.DistrictList(ClsEstablishment.ClsEstablish[0].EstablisDetailID);
                    var EstablishmentList = _Commompository.EstablishmentList(ClsEstablishment.ClsEstablish[0].EstablisDetailID);
                    ClsEstablishment.Districtlist = DistrictList;
                    ClsEstablishment.EstablishmentList = EstablishmentList;
                    ClsEstablishment.ApplicationID = ApplicationID;
                    ClsEstablishment.ISNew = ISNew;
                    ClsEstablishment.IP_Address = heserver.AddressList[1].ToString();
                    ClsEstablishment.isReqiredTradDetail = ClsEstablishment.ClsEstablish[0].isReqiredTradDetail;
                    ClsEstablishment.IsSubmit = ClsEstablishment.ClsEstablish[0].IsSubmit;
                    ClsEstablishment.EncryptedId = Encrypt_Decrypt.Encrypt(ApplicationID.ToString());
                    return View("AddnFormEstablishment", ClsEstablishment);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "AddnFormEstablishment", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public JsonResult NFormEstablishment(int ApplicationID, int ISNew)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    NFormApplicationModel ClsEstablishment = new NFormApplicationModel();

                    var ClsEstablish = _NFormApplicationpository.NFormEstablishmentRecord(ApplicationID, ISNew);
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "NFormEstablishment", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


        public JsonResult SaveNFormEstablishmentRecord(NFormApplicationModel Obj)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.IP_Address = IP;
                    NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                    ClssaveRecord = _NFormApplicationpository.SaveNFormEstablishmentRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SaveNFormEstablishmentRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public ActionResult AddUploadDocment(int ApplicationID)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    NFormApplicationModel ClsEstablishment = new NFormApplicationModel();
                    ClsEstablishment = _NFormApplicationpository.NFormAppliactionDocumentURLRecord(ApplicationID);
                    return View("AddUploadDocment", ClsEstablishment);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "AddUploadDocment", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
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

        public Report report = new Report();

        public static MemoryStream GenerateStreamFromString(string[] value)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(value[0] ?? ""));
        }

        [HttpPost]
        [RequestSizeLimit(60000000)]
        public IActionResult UploadNFormDocument(List<IFormFile> MyUploader, int ApplicationID, int IsSubmit, string URL, string AppID)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {

                    int i = 0;
                    int F = -1;
                    int DocumentID = 0;
                    NFormApplicationModel ClsEstablishment = new NFormApplicationModel();
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


                            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "Reinstatement", "NForm");
                            string filePath = uploadsFolder;
                            string FileName;
                            if (MyUploader[i].FileName == "0")
                            {
                                FileName = $"Other1_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (MyUploader[i].FileName == "1")
                            {
                                FileName = $"Other2_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (MyUploader[i].FileName == "2")
                            {
                                FileName = $"Doc1_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                                //DocumentID = 2;
                            }
                            else if (MyUploader[i].FileName == "3")
                            {
                                FileName = $"Doc2_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                                //DocumentID = 3;
                            }
                            else
                            {
                                FileName = $"Doc3_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                                //DocumentID = 4;
                            }
                            //string FileName = MyUploader[i].FileName;
                            NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                            ClssaveRecord.ApplicationID = ApplicationID;
                            ClssaveRecord.fileName = FileName;
                            ClssaveRecord.DocumentID = Convert.ToInt32(MyUploader[i].FileName); 
                            ClssaveRecord.URL = URL;
                            ClssaveRecord.UserID = Convert.ToInt32(_ID);
                            ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                            ClssaveRecord.IP_Address = IP;
                            ClssaveRecord.AppID = AppID;

                            if (IsSubmit == 1) { ClssaveRecord.IsSubmit = true; } else { ClssaveRecord.IsSubmit = false; }

                            var output = _FileUpload.UploadDocument(MyUploader[i], FileName, filePath);
                            var output1 = _NFormApplicationpository.SaveDocumnetandapplication(ClssaveRecord);
                            //if (output1.ErrorCode == 0 && output1.CID == 1)
                            //{
                            //    var test = SendMailApplicant(output1.EMailIDList, output1.EmailSubject, output1.EmailSubject);
                            //}

                            if (output1.ErrorCode == 0 && output1.CID == 1)
                            {
                                int b = 0;
                                int v = 0;

                                if (output1.ApplicantMailDetail.Count > 0)
                                {
                                    for (b = 0; b < output1.ApplicantMailDetail.Count; b++)
                                    {
                                        if (output1.ApplicantMailDetail[b].Email != null)
                                        {
                                            var test = SendMailApplicant(output1.ApplicantMailDetail[b].Email, output1.ApplicantMailDetail[b].Subject, output1.ApplicantMailDetail[b].Body);
                                        }
                                    }
                                }

                                if (output1.EshtablishmentMailDetail.Count > 0)
                                {
                                    for (v = 0; v < output1.EshtablishmentMailDetail.Count; v++)
                                    {
                                        if (output1.EshtablishmentMailDetail[v].Email != null)
                                        {
                                            var test = SendMailApplicant(output1.EshtablishmentMailDetail[v].Email, output1.EshtablishmentMailDetail[v].Subject, output1.EshtablishmentMailDetail[v].Body);
                                        }
                                    }
                                }
                            }

                            ClsEstablishment = output1;
                            if (output == true && output1.ErrorCode == 0)
                            {
                                F = i;
                            }

                        }

                    }
                    if (F == -1)
                    {
                        ClsEstablishment.ErrorCode = 1;
                        ClsEstablishment.ErrorMassage = "You are already upoaded Document if you need update So please select Document ";
                    }

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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "UploadNFormDocument", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public async Task<IActionResult> DownloadApplicationFile(string FileName)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    if (FileName == null)
                        return Content("filename not present");

                    //var path = "https://localhost:44314/images/logo.png";
                    var path = Path.Combine("wwwroot", "Documents", "Reinstatement", "NForm", FileName);
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
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "DownloadApplicationFile", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public JsonResult NFormAppliactionUpdateSubmit(NFormApplicationModel Obj)
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            if (_ID != null && _ID != 0)
            {

                Obj.UserMode = Convert.ToInt32(_UserMode);
                Obj.UserID = Convert.ToInt32(_ID);
                Obj.IP_Address = IP;
                Obj.URL = Obj.URL;
                NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                ClssaveRecord = _NFormApplicationpository.NFormAppliactionUpdateSubmit(Obj);
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

        public ActionResult NFormACLList()
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
                    NFormApplicationModel ClsAppliactionlst = new NFormApplicationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _NFormApplicationpository.NFormACLList(ClsAppliactionlst);
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SaveNFormEstablishmentRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public ActionResult AddNFormHearingALC(string id, int ISNew, int ISView)
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
                    NFormApplicationModel ClsRecord = new NFormApplicationModel();
                    if (ISNew == 0) //1 Mins Hearing Date 
                    {
                        ClsRecord = _NFormApplicationpository.NFormACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID, IsACL);
                    }
                    else if (ISNew == 1) //1 Mins Close Case 
                    {
                        ClsRecord = _NFormApplicationpository.NFormACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID, IsACL);
                    }
                    else if (ISNew == 2) //1 Mins Close Case 
                    {
                        ClsRecord = _NFormApplicationpository.NFormACLRecord(ApplicationID, ISNew);
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

                    var B = 0;
                    if (ClsRecord.HearingdetailACL.Count > 0)
                    {
                        for (B = 0; B < ClsRecord.HearingdetailACL.Count; ++B)
                        {
                            ClsRecord.HearingdetailACL[B].HearingReasonList = ClsRecord.HearingReasonList;
                        }
                    }


                    ClsRecord.ISView = ISView;
                    ClsRecord.IP_Address = IP;
                    return View("AddNFormHearingALC", ClsRecord);
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

        public ActionResult StatusHistory(string id, int ISNew, int ISView)
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {

                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    int IsACL = 0;
                    if (id != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }
                    NFormApplicationModel ClsRecord = new NFormApplicationModel();

                    if (ISNew == 0) //1 Mins Hearing Date 
                    {
                        ClsRecord = _NFormApplicationpository.NFormACLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID, IsACL);

                    }
                    else if (ISNew == 1) //1 Mins Close Case 
                    {
                        ClsRecord = _NFormApplicationpository.NFormACLRecord(ApplicationID, ISNew);
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
        //public JsonResult SaveNFormHearingACLRecord(NFormApplicationModel Obj)
        //{
        //    var IP = heserver.AddressList[1].ToString();
        //    var _ID = HttpContext.Session.GetInt32("_ID");
        //    var _UserMode = HttpContext.Session.GetInt32("_UserMode");
        //    try
        //    {
        //        if (_ID != null && _ID != 0)
        //        {
        //            Obj.UserID = Convert.ToInt32(_ID);
        //            Obj.IP_Address = IP;
        //            NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
        //            ClssaveRecord = _NFormApplicationpository.SaveNFormHearingACLRecord(Obj);
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
        //        _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SaveNFormHearingACLRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
        //        return new JsonResult(ex.Message)
        //        {
        //            StatusCode = (int)HttpStatusCode.InternalServerError
        //        };
        //    }

        //}

        public JsonResult SaveNFormHearingACLRecord(NFormApplicationModel Obj)
        {
            var IP = heserver.AddressList[1].ToString();
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            try
            {
                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.IP_Address = IP;
                    NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                    ClssaveRecord = _NFormApplicationpository.SaveNFormHearingACLRecord(Obj);
                    EmailReportModel EmailReportDetail = new EmailReportModel();

                    EmailReportDetail.DictrictACLOffice = ClssaveRecord.EmailReportDetail[0].DictrictACLOffice;
                    EmailReportDetail.AppID = ClssaveRecord.EmailReportDetail[0].AppID;
                    EmailReportDetail.ApplicantDetail = ClssaveRecord.EmailReportDetail[0].ApplicantDetail;
                    EmailReportDetail.Establishmentdetail = ClssaveRecord.EmailReportDetail[0].Establishmentdetail;
                    EmailReportDetail.Date = ClssaveRecord.EmailReportDetail[0].Date;
                    EmailReportDetail.HearingDate = ClssaveRecord.EmailReportDetail[0].HearingDate;
                    EmailReportDetail.HearingTime = ClssaveRecord.EmailReportDetail[0].HearingTime;
                    EmailReportDetail.IsNotice = ClssaveRecord.IsNotice;
                    EmailReportDetail.IsReviewHearing = ClssaveRecord.IsReviewHearing;


                    string webRootPath = webHostEnvironment.WebRootPath;
                    string uploadsFolder = Path.Combine(webRootPath, "Documents", "Report", "EmailPDF");

                    List<Attachments> AttachedFile = new List<Attachments>();
                    if (ClssaveRecord.EmailReportDetail[0].AppID != null && ClssaveRecord.EmailReportDetail[0].AppID != "")
                    {
                        MemoryStream ms1 = new MemoryStream();
                        string[] rr1 = new string[2000000];
                        rr1 = SendToHearingEmailPDF(EmailReportDetail);
                        byte[] bytes = System.Convert.FromBase64String(rr1[1]);
                        Stream stream = new MemoryStream(bytes);

                        string filePath = Path.Combine("wwwroot", "Documents", "Report", "EmailPDF");
                       
                        if (EmailReportDetail.IsReviewHearing == 1)
                        {
                            var filename = $"ReviewHearing_{DateTime.Now:yyyyMMdd_hhmm}_{ClssaveRecord.EmailReportDetail[0].AppID}" + ".pdf";
                            System.IO.File.WriteAllBytes(filePath + "\\" + filename, bytes);
                            AttachedFile.Add(new Attachments() { FileName = uploadsFolder + "\\" + filename });
                        }
                        else
                        {
                            var filename = $"Hearing_{DateTime.Now:yyyyMMdd_hhmm}_{ClssaveRecord.EmailReportDetail[0].AppID}" + ".pdf";
                            System.IO.File.WriteAllBytes(filePath + "\\" + filename, bytes);
                            AttachedFile.Add(new Attachments() { FileName = uploadsFolder + "\\" + filename });
                        }
                        
                    }

                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        //var test = SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                        int i = 0;
                        int v = 0;

                        if (ClssaveRecord.ApplicantMailDetail.Count > 0)
                        {
                            for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
                            {
                                if (ClssaveRecord.ApplicantMailDetail[i].Email != null)
                                {
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.ApplicantMailDetail[i].Email.ToString() }, ClssaveRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClssaveRecord.ApplicantMailDetail[i].Body);
                                    //var test = _emailSender.SendEmailAsync(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClssaveRecord.ApplicantMailDetail[i].Body);
                                }
                            }
                        }

                        if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
                        {
                            for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
                            {
                                if (ClssaveRecord.EshtablishmentMailDetail[v].Email != null)
                                {
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.EshtablishmentMailDetail[v].Email.ToString() }, ClssaveRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                                    //var test = _emailSender.SendEmailAsync(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClssaveRecord.EshtablishmentMailDetail[v].Body);
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SaveNFormHearingACLRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        [HttpPost]
        [RequestSizeLimit(20000000)]
        public IActionResult SaveNFormACLSettlementRecord(IFormFile MyUploader, int ApplicationID, string SettlementDate, string SettlementNote
            , string URL, int HearingReasonID, int CaseFavourIn, bool IsReview,string OrderOutwardDate,int OrderOutwardNo)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "Reinstatement", "NForm", "Settlement");
                    string filePath = uploadsFolder;
                    string FileName = "";
                    bool output = false;

                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.fileName = FileName;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.upSettlementDate = SettlementDate;
                    ClssaveRecord.SettlementNote = SettlementNote;
                    ClssaveRecord.CaseFavourIn = CaseFavourIn;
                    ClssaveRecord.ResolutionReasonID = HearingReasonID;
                    ClssaveRecord.IsReviewResolution = IsReview;
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

                        if(ClssaveRecord.IsReviewResolution ==true)
                        {
                            FileName = $"ReviewOrderDoc_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            ClssaveRecord.fileName = FileName;
                        }
                        else
                        {
                            FileName = $"OrderDoc_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            ClssaveRecord.fileName = FileName;
                        }
                        
                        //string FileName = MyUploader[i].FileName;

                        //ClssaveRecord.ApplicationID = ApplicationID;
                        //ClssaveRecord.fileName = FileName;
                        //ClssaveRecord.URL = URL;
                        //ClssaveRecord.UserID = Convert.ToInt32(_ID);
                        //ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                        //ClssaveRecord.IP_Address = IP;
                        //ClssaveRecord.upSettlementDate = SettlementDate;
                        //ClssaveRecord.SettlementNote = SettlementNote;
                        //ClssaveRecord.CaseFavourIn = CaseFavourIn;
                        //ClssaveRecord.ResolutionReasonID = HearingReasonID;
                        //ClssaveRecord.IsReviewResolution = IsReview;
                        output = _FileUpload.UploadDocument(MyUploader, FileName, filePath);
                        ClssaveRecord = _NFormApplicationpository.SaveNFormSettlementrecord(ClssaveRecord);
                    }
                    else
                    {
                        ClssaveRecord = _NFormApplicationpository.SaveNFormSettlementrecord(ClssaveRecord);
                    }
                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        //var test = SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                        int i = 0;
                        int v = 0;

                        List<Attachments> AttachedFile = new List<Attachments>();
                        AttachedFile.Add(new Attachments() { FileName = uploadsFolder + "\\" + FileName });

                        if (ClssaveRecord.ApplicantMailDetail.Count > 0)
                        {
                            for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
                            {
                                if (ClssaveRecord.ApplicantMailDetail[i].Email != null)
                                {
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.ApplicantMailDetail[i].Email.ToString() }, ClssaveRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClssaveRecord.ApplicantMailDetail[i].Body);
                                    //var test = SendMailApplicant(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, ClssaveRecord.ApplicantMailDetail[i].Body);
                                }
                            }
                        }

                        if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
                        {
                            for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
                            {
                                if (ClssaveRecord.EshtablishmentMailDetail[v].Email != null)
                                {
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.EshtablishmentMailDetail[v].Email.ToString() }, ClssaveRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                                    //var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                                }
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SaveNFormACLSettlementRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public async Task<IActionResult> DownloadSettlementFile(string FileName)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (FileName == null)
                    return Content("filename not present");

                //var path = "https://localhost:44314/images/logo.png";
                var path = Path.Combine("wwwroot", "Reinstatement", "Documents", "NForm", "Settlement", FileName);
                //var path = "wwwroot/images/logo.png";
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, _FileUpload.GetContentType(path), Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "DownloadSettlementFile", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult NFormHistory(NFormApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                    ClssaveRecord = _NFormApplicationpository.NFormHistory(Obj);
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "NFormHistory", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public ActionResult SendtoACLFromClerk(string id, string URL)
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

                    NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord = _NFormApplicationpository.SendtoACLFromClerk(ClssaveRecord);

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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SendtoACLFromClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #region Send Review to ACL
        public IActionResult SendReviewtoACL(List<IFormFile> MyUploader, string IDs, string URL, string ReviewReason, string ReviewDoc)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int ApplicationID = 0;
                    if (IDs != null)
                    {
                        ApplicationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(IDs));
                    }
                    int i = 0;
                    int F = 0;
                    NFormApplicationModel ClassReviewApply = new NFormApplicationModel();
                    NFormApplicationModel ClsBundleBreak = new NFormApplicationModel();
                    var ReviewDocument = "";
                    //var SecurityDoc = "";

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


                            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "Review", "ApplicantDoc");
                            //string webRootPath = webHostEnvironment.WebRootPath;
                            //string uploadsFolder = Path.Combine(webRootPath, "Documents", "LicenceApplication", "ChallanDoc");
                            string filePath = uploadsFolder;
                            string FileName;


                            // string output = "";
                            var output = false;
                            if (i == 0)
                            {
                                ReviewDoc = $"Review{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                                output = _FileUpload.UploadDocument(MyUploader[i], ReviewDoc, filePath);
                            }

                            ClsBundleBreak.UserID = Convert.ToInt32(_ID);
                            ClsBundleBreak.ApplicationID = ApplicationID;
                            ClsBundleBreak.URL = URL;
                            ClsBundleBreak.IP_Address = IP;
                            ClsBundleBreak.ReviewReason = ReviewReason;
                            ClsBundleBreak.ReviewDoc = ReviewDoc;

                        }
                        //ClsBundleBreak.ReviewDoc = ReviewDoc;
                        //ClsBundleBreak.SecurityDoc = SecurityDoc;


                        var output1 = _NFormApplicationpository.SendReviewtoACL(ClsBundleBreak);
                        //ClsBundleBreak = _ProjectDetailspository.SaveCessCollectionDetails(ClsBundleBreak);
                        ClassReviewApply = output1;
                        if (output1.ErrorCode == 0)
                        {
                            F = i;
                        }

                    }
                    //if (F != 2)
                    //{
                    //    ClssaveEstablishment.ErrorCode = 1;
                    //    ClssaveEstablishment.ErrorMassage = "You are already upoaded Document if you need update So please select Document ";
                    //}

                    //if file not selected than status is 3
                    return Json(new { status = ClassReviewApply });
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

        }
        #endregion

        public ActionResult SendtoCommentFromClerk(string Comments, string id, string URL)
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


                    NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.Comments = Comments;
                    ClssaveRecord = _NFormApplicationpository.SendtoCommentFromClerk(ClssaveRecord);
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SendtoACLFromClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public ActionResult ApproveOrRejectReviewFromACL(string id, int Status, string URL)
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


                    NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.ActionCode = Status;
                    ClssaveRecord = _NFormApplicationpository.ApproveOrRejectReviewFromACL(ClssaveRecord);

                    string webRootPath = webHostEnvironment.WebRootPath;
                    string uploadsFolder = Path.Combine(webRootPath, "Documents", "Report", "EmailPDF");
                    EmailReportModel EmailReportDetail = new EmailReportModel();
                    EmailReportDetail.ApplicantDetail = ClssaveRecord.EmailReportDetail[0].ApplicantDetail;
                    EmailReportDetail.AppID = ClssaveRecord.EmailReportDetail[0].AppID;
                    EmailReportDetail.Establishmentdetail = ClssaveRecord.EmailReportDetail[0].Establishmentdetail;
                    EmailReportDetail.Date = ClssaveRecord.EmailReportDetail[0].Date;
                    EmailReportDetail.ACLDistrict = ClssaveRecord.EmailReportDetail[0].ACLDistrict;
                    EmailReportDetail.DictrictACLOffice = ClssaveRecord.EmailReportDetail[0].DictrictACLOffice;
                    List<Attachments> AttachedFile = new List<Attachments>();
                    if (ClssaveRecord.EmailReportDetail[0].AppID != null && ClssaveRecord.EmailReportDetail[0].AppID != "")
                    {
                        MemoryStream ms1 = new MemoryStream();
                        string[] rr1 = new string[2000000];
                        rr1 = ReviewRejectPDF(EmailReportDetail);
                        byte[] bytes = System.Convert.FromBase64String(rr1[1]);
                        Stream stream = new MemoryStream(bytes);

                        string filePath = Path.Combine("wwwroot", "Documents", "Report", "EmailPDF");
                        var filename = $"ReviewRejectForm_{DateTime.Now:yyyyMMdd_hhmm}_{ClssaveRecord.EmailReportDetail[0].AppID}" + ".pdf";
                        System.IO.File.WriteAllBytes(filePath + "\\" + filename, bytes);
                        AttachedFile.Add(new Attachments() { FileName = uploadsFolder + "\\" + filename });
                    }

                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        //SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                        int i = 0;
                        int v = 0;

                        if (Status != 9)                        // check if rejected or not Reject = 9 Approve = 8 
                        {

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
                        else
                        {
                            if (ClssaveRecord.ApplicantMailDetail.Count > 0)
                            {
                                for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
                                {
                                    //var test = SendMailApplicant(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, ClssaveRecord.ApplicantMailDetail[i].Body);
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.ApplicantMailDetail[i].Email.ToString() }, ClssaveRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClssaveRecord.ApplicantMailDetail[i].Body);
                                }
                            }

                            if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
                            {
                                for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
                                {
                                    //var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                                    var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.EshtablishmentMailDetail[v].Email.ToString() }, ClssaveRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                                }
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "ApproveReviewFromACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public ActionResult UpdateEshtablishmentAddressdetailFromACL(NFormApplicationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.IP_Address = IP;
                    ClssaveRecord = _NFormApplicationpository.UpdateEshtablishmentAddressdetailFromACL(Obj);
                    if (ClssaveRecord.ErrorCode == 0)
                    {
                        var test = SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "UpdateEshtablishmentAddressdetailFromACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public string[] SendToHearingEmailPDF(EmailReportModel EmailReportDetail)
        {
            MemoryStream ms1 = new MemoryStream();
            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder

            DataSet ds = new DataSet();
            DataTable Dt1 = ds.Tables.Add("Table1");

            // create fields
            Dt1.Columns.Add("DictrictACLOffice", typeof(string));
            Dt1.Columns.Add("AppID", typeof(string));
            Dt1.Columns.Add("ApplicantDetail", typeof(string));
            Dt1.Columns.Add("Establishmentdetail", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("HearingDate", typeof(string));
            Dt1.Columns.Add("HearingTime", typeof(string));

            //insert row values
            Dt1.Rows.Add(new Object[]{
                 EmailReportDetail.DictrictACLOffice,
                 EmailReportDetail.AppID,
                 EmailReportDetail.ApplicantDetail,
                 EmailReportDetail.Establishmentdetail,
                 //EmailReportDetail.ACLName,
                 //EmailReportDetail.DictrictACL,
                 EmailReportDetail.Date,
                 EmailReportDetail.HearingDate,
                 EmailReportDetail.HearingTime
            });

            //// insert row values
            //Dt1.Rows.Add(new Object[]{
            //      "સેવા સદન-ર-સી અમરેલી- ૩૬૫૬૦૧",
            //      "REIN000004",
            //      "Dummy Dummy AMRELI Amreli 836983",
            //      "New data Dummy AHMADABAD Daskroi 393996",
            //      "28-10-2022",
            //      "28-10-2022",
            //      "02:00"

            //});

            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/gretipresenthearing.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/gretihearing.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/gretipresenthearing.xml");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/gretihearing.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            if (EmailReportDetail.IsReviewHearing == 1)
            {
                if (EmailReportDetail.IsNotice == 0)
                {
                    report.Report.Load(webRootPath + "/Documents/Report/GratuityReviewHearing.frx");
                }
                else
                {
                    report.Report.Load(webRootPath + "/Documents/Report/gretipresenthearing.frx");
                }
            }
            else
            {
                if (EmailReportDetail.IsNotice == 0)
                {
                    report.Report.Load(webRootPath + "/Documents/Report/gretihearing.frx");
                }
                else
                {
                    report.Report.Load(webRootPath + "/Documents/Report/gretipresenthearing.frx"); 
                }
            }

            
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

        public ActionResult TFormRecoveryCerti(string id, int AppealID)
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

                    NFormApplicationModel ClssaveRecord = new NFormApplicationModel();
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.AppealID= AppealID;
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord = _NFormApplicationpository.TFormRecoveryCerti(ClssaveRecord);
                    EmailReportModel EmailReportDetail = new EmailReportModel();
                    EmailReportDetail.ApplicantDetail = ClssaveRecord.EmailReportDetail[0].ApplicantDetail;
                    EmailReportDetail.AppID = ClssaveRecord.EmailReportDetail[0].AppID;
                    EmailReportDetail.Establishmentdetail = ClssaveRecord.EmailReportDetail[0].Establishmentdetail;
                    EmailReportDetail.Date = ClssaveRecord.EmailReportDetail[0].Date;
                    EmailReportDetail.ChallanAmount = ClssaveRecord.EmailReportDetail[0].ChallanAmount;

                    string webRootPath = webHostEnvironment.WebRootPath;
                    string uploadsFolder = Path.Combine(webRootPath, "Documents", "Report", "EmailPDF");

                    List<Attachments> AttachedFile = new List<Attachments>();
                    if (ClssaveRecord.EmailReportDetail[0].AppID != null && ClssaveRecord.EmailReportDetail[0].AppID != "")
                    {
                        MemoryStream ms1 = new MemoryStream();
                        string[] rr1 = new string[2000000];
                        rr1 = TRecoveryPDF(EmailReportDetail);
                        byte[] bytes = System.Convert.FromBase64String(rr1[1]);
                        Stream stream = new MemoryStream(bytes);

                        string filePath = Path.Combine("wwwroot", "Documents", "Report", "EmailPDF");
                        var filename = $"RecoveryForm_{DateTime.Now:yyyyMMdd_hhmm}_{ClssaveRecord.EmailReportDetail[0].AppID}" + ".pdf";
                        System.IO.File.WriteAllBytes(filePath + "\\" + filename, bytes);
                        AttachedFile.Add(new Attachments() { FileName = uploadsFolder + "\\" + filename });
                    }

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
                                    //var test = SendMailApplicant(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClssaveRecord.ApplicantMailDetail[i].Body);
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
                                    //var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, AttachedFile, ClssaveRecord.EshtablishmentMailDetail[v].Body);
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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SendtoACLFromClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public string[] TRecoveryPDF(EmailReportModel EmailReportDetail)
        {

            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder

            DataSet ds = new DataSet();
            DataTable Dt1 = ds.Tables.Add("Table1");

            // create fields
            Dt1.Columns.Add("AppID", typeof(string));
            Dt1.Columns.Add("ApplicantDetail", typeof(string));
            Dt1.Columns.Add("Establishmentdetail", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("ChallanAmount", typeof(string));

            //insert row values
            Dt1.Rows.Add(new Object[]{
                 EmailReportDetail.AppID,
                 EmailReportDetail.ApplicantDetail,
                 EmailReportDetail.Establishmentdetail,
                 EmailReportDetail.Date,
                 EmailReportDetail.ChallanAmount,
                
                 //"શ્રમ ભવન,રૂસ્‍તમ કામા માર્ગ, ખાનપુર, અમદાવાદ",
                 //"બાબુભાઈ બળદેવભાઈ ઠાકોર",
                 //"ભુપેન્દ્ર એન્ટરપ્રાઈઝ, C/o, એડીયન્ટ ઈન્ડીયા પ્રા.લી.",
                 //"આર.ડી.પટેલ",
                 //"૧૦-૦૧-૨૦૨૨",
                 //"એડીયન્ટ ઈન્ડીયા પ્રા.લી",
                 //"અમદાવાદ"

            });



            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/TRecovery.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/TRecovery.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            report.Report.Load(webRootPath + "/Documents/Report/RecoveryCerti.frx");

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

        public string[] ReviewRejectPDF(EmailReportModel EmailReportDetail)
        {

            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder

            DataSet ds = new DataSet();
            DataTable Dt1 = ds.Tables.Add("Table1");

            // create fields
            Dt1.Columns.Add("AppID", typeof(string));
            Dt1.Columns.Add("ApplicantDetail", typeof(string));
            Dt1.Columns.Add("Establishmentdetail", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("ACLDistrict", typeof(string));
            Dt1.Columns.Add("DistrictACLOffice", typeof(string));

            //insert row values
            Dt1.Rows.Add(new Object[]{
                 EmailReportDetail.AppID,
                 EmailReportDetail.ApplicantDetail,
                 EmailReportDetail.Establishmentdetail,
                 EmailReportDetail.Date,
                 EmailReportDetail.ACLDistrict,
                 EmailReportDetail.DictrictACLOffice,

            });



            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/GratuityReviewReject.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/GratuityReviewReject.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            report.Report.Load(webRootPath + "/Documents/Report/GratuityReviewReject.frx");

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
    }
}
