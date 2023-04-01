using FTS.Business.ApplicationRegistration;
using FTS.Business.CommonList;
using FTS.Model.Common;
using FTS.Model.Entities;
using FileManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    public class ApplicationRegistrationController : Controller
    {
        public IApplicationRegistrationBI _ApplicationRegistration;
        public ICommonListBI _Commompository;
        public IFileUploadService _FileUpload;
        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly IHttpContextAccessor httpContextAccessor;

        public ApplicationRegistrationController(IApplicationRegistrationBI ApplicationRegistration, ICommonListBI commompository, IFileUploadService FileUpload, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {

            this._ApplicationRegistration = ApplicationRegistration;
            _Commompository = commompository;
            _FileUpload = FileUpload;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }


        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
        //ApplicationRegistration
        public ActionResult Index()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            var _PositionID = HttpContext.Session.GetInt32("_PositionID");
            var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");

            try
            {
                //var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                //var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                //var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");

                if (_ID != null && _ID != 0)
                {
                    ApplicationRegistrationModel ClsAppliactionlst = new ApplicationRegistrationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.PositionID = Convert.ToInt32(_PositionID);
                    ClsAppliactionlst.PositionDistrictID = Convert.ToInt32(_DistrictID);
                    //ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    //ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    //ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    var List = _ApplicationRegistration.RegistrationList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.RegistrationID.ToString());
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


        //public ActionResult AddApplicationRegistration(string id)
        //{
        //    var _ID = HttpContext.Session.GetInt32("_ID");
        //    var _UserMode = HttpContext.Session.GetInt32("_UserMode");
        //    var IP = heserver.AddressList[1].ToString();

        //    //try
        //    //{
        //        if (_ID != null && _ID != 0)
        //        {
        //            int RegistrationID = 0;
        //            if (id != null)
        //            {
        //                RegistrationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
        //            }


        //            ApplicationRegistrationModel ClsAppliaction = new ApplicationRegistrationModel();
        //            ClsAppliaction = _ApplicationRegistration.ApplicationRegistrationRecord(RegistrationID);
        //            var DistrictList = _Commompository.DistrictList(ClsAppliaction.Establishmentregistrationdetaillst[0].DistrictID);
        //            var TypeOfBusinessTradeList = _Commompository.TypeOfBusinessTradeList();
        //            var TalukaList = _Commompository.TalukaList(ClsAppliaction.Establishmentregistrationdetaillst[0].RegistrationID, ClsAppliaction.Establishmentregistrationdetaillst[0].DistrictID);
        //            var AreaList = _Commompository.AreaList(ClsAppliaction.Establishmentregistrationdetaillst[0].RegistrationID, ClsAppliaction.Establishmentregistrationdetaillst[0].DistrictID);
        //            ClsAppliaction.Establishmentregistrationdetaillst[0].Talukalist = TalukaList;
        //            ClsAppliaction.Establishmentregistrationdetaillst[0].Districtlist = DistrictList;
        //            ClsAppliaction.Establishmentregistrationdetaillst[0].AreaList = AreaList;
        //            ClsAppliaction.Establishmentregistrationdetaillst[0].TypeOfBusinessTradeList = TypeOfBusinessTradeList;
        //            ClsAppliaction.RegistrationIDEdit = RegistrationID;
        //            ClsAppliaction.IP_Address = IP;
        //            ClsAppliaction.UserID = Convert.ToInt32(_ID);

        //            var i = 0;
        //            if (ClsAppliaction.PrincipalEmployerInformationdetaillst.Count > 0)
        //            {
        //                for (i = 0; i < ClsAppliaction.PrincipalEmployerInformationdetaillst.Count; ++i)
        //                {
        //                    ClsAppliaction.PrincipalEmployerInformationdetaillst[i].Districtlist = _Commompository.DistrictList(ClsAppliaction.PrincipalEmployerInformationdetaillst[i].PrincipalID);
        //                    ClsAppliaction.PrincipalEmployerInformationdetaillst[i].Talukalist = _Commompository.TalukaList(ClsAppliaction.PrincipalEmployerInformationdetaillst[i].PrincipalID, ClsAppliaction.PrincipalEmployerInformationdetaillst[i].DistrictID);
        //                    ClsAppliaction.PrincipalEmployerInformationdetaillst[i].AreaList = _Commompository.AreaList(ClsAppliaction.PrincipalEmployerInformationdetaillst[i].PrincipalID, ClsAppliaction.PrincipalEmployerInformationdetaillst[i].DistrictID);
        //                }
        //            }

        //            var v = 0;
        //            if (ClsAppliaction.Contractordetaillst.Count > 0)
        //            {
        //                for (v = 0; v < ClsAppliaction.Contractordetaillst.Count; ++v)
        //                {
        //                    ClsAppliaction.Contractordetaillst[v].Districtlist = _Commompository.DistrictList(ClsAppliaction.Contractordetaillst[v].PrincipalID);
        //                    ClsAppliaction.Contractordetaillst[v].Talukalist = _Commompository.TalukaList(ClsAppliaction.Contractordetaillst[v].PrincipalID, ClsAppliaction.Contractordetaillst[v].DistrictID);
        //                    ClsAppliaction.Contractordetaillst[v].AreaList = _Commompository.AreaList(ClsAppliaction.Contractordetaillst[v].PrincipalID, ClsAppliaction.Contractordetaillst[v].DistrictID);
        //                }
        //            }

        //            return View("AddApplicationRegistration", ClsAppliaction);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "AddReinstatementAppliaction", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
        //    //    return StatusCode(500, ex.Message);
        //    //}


        //}

        public ActionResult _ApplicationRegistrationDetail(string id,int ISAL , int ISDL, int ISView,int ISHO, int ISAmendment, int IsEdit, int stpid = 1)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            try
            {
                if (_ID != null && _ID != 0)
            {
                int RegistrationID = 0;
                if (id != null)
                {
                    RegistrationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                }
                  

                    ApplicationRegistrationModel ClsAppliaction = new ApplicationRegistrationModel();
                    ClsAppliaction.UserID = Convert.ToInt32(_ID);
                    ClsAppliaction.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliaction.RegistrationID = RegistrationID;
                    ClsAppliaction.ISAmendment = ISAmendment;
                    ClsAppliaction.IsEdit = IsEdit;
                   
                     ClsAppliaction = _ApplicationRegistration.ApplicationRegistrationRecord(ClsAppliaction);
                    
                var DistrictList = _Commompository.DistrictList(ClsAppliaction.Establishmentregistrationdetaillst[0].DistrictID);
                var TypeOfBusinessTradeList = _Commompository.TypeOfBusinessTradeList();
                var TalukaList = _Commompository.TalukaList(ClsAppliaction.Establishmentregistrationdetaillst[0].RegistrationID, ClsAppliaction.Establishmentregistrationdetaillst[0].DistrictID);
                var AreaList = _Commompository.AreaList(ClsAppliaction.Establishmentregistrationdetaillst[0].RegistrationID, ClsAppliaction.Establishmentregistrationdetaillst[0].DistrictID);
                ClsAppliaction.Establishmentregistrationdetaillst[0].Talukalist = TalukaList;
                ClsAppliaction.Establishmentregistrationdetaillst[0].Districtlist = DistrictList;
                ClsAppliaction.Establishmentregistrationdetaillst[0].AreaList = AreaList;
                ClsAppliaction.Establishmentregistrationdetaillst[0].TypeOfBusinessTradeList = TypeOfBusinessTradeList;
                ClsAppliaction.RegistrationIDEdit = ClsAppliaction.RegistrationID;
                ClsAppliaction.IP_Address = IP;
                ClsAppliaction.UserID = Convert.ToInt32(_ID);
                ClsAppliaction.ISAL = ISAL;
                ClsAppliaction.ISDL = ISDL;
                ClsAppliaction.ISView = ISView;
                ClsAppliaction.ISHO = ISHO;
                ClsAppliaction.ISAmendment = ISAmendment;
                ClsAppliaction.IsEdit = IsEdit;
                ClsAppliaction.stpid = stpid;
                ClsAppliaction.UserMode = Convert.ToInt32(_UserMode);

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

                var v = 0;
                if (ClsAppliaction.Contractordetaillst.Count > 0)
                {
                    for (v = 0; v < ClsAppliaction.Contractordetaillst.Count; ++v)
                    {
                        ClsAppliaction.Contractordetaillst[v].Districtlist = _Commompository.DistrictList(ClsAppliaction.Contractordetaillst[v].PrincipalID);
                        ClsAppliaction.Contractordetaillst[v].Talukalist = _Commompository.TalukaList(ClsAppliaction.Contractordetaillst[v].PrincipalID, ClsAppliaction.Contractordetaillst[v].DistrictID);
                        ClsAppliaction.Contractordetaillst[v].AreaList = _Commompository.AreaList(ClsAppliaction.Contractordetaillst[v].PrincipalID, ClsAppliaction.Contractordetaillst[v].DistrictID);
                    }
                }

                return PartialView(ClsAppliaction);
                //return View("AddApplicationRegistration", ClsAppliaction);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "AddReinstatementAppliaction", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
    }

        }
 
        public JsonResult SaveEstablishmentRegistrationRecord(ApplicationRegistrationModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            var _PositionID = HttpContext.Session.GetInt32("_PositionID");
            var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");

            try
            {

                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    Obj.UserMode = Convert.ToInt32(_UserMode);
                    Obj.PositionID = Convert.ToInt32(_PositionID);
                    Obj.PositionDistrictID = Convert.ToInt32(_DistrictID);
                    Obj.IP_Address = IP;
                    ApplicationRegistrationModel ClssaveRecord = new ApplicationRegistrationModel();
                    ClssaveRecord = _ApplicationRegistration.SaveEstablishmentRegistrationRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.RegistrationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "SaveApplicationRegistrationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public JsonResult SavePrincipalEmployerRegistrationRecord(ApplicationRegistrationModel Obj)
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
                    ApplicationRegistrationModel ClssaveRecord = new ApplicationRegistrationModel();
                    ClssaveRecord = _ApplicationRegistration.SavePrincipalEmployerRegistrationRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.RegistrationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "SavePrincipalEmployerRegistrationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public JsonResult SaveContractorRegistrationRecord(ApplicationRegistrationModel Obj)
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
                    ApplicationRegistrationModel ClssaveRecord = new ApplicationRegistrationModel();
                    ClssaveRecord = _ApplicationRegistration.SaveContractorRegistrationRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.RegistrationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "SaveContractorRegistrationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        [HttpPost]
        [RequestSizeLimit(60000000)]
        public IActionResult DocumentUploader(List<IFormFile> MyUploader, int RegistrationID, int IsSubmit, string URL, string AppID)
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
                    ApplicationRegistrationModel ClsEstablishment = new ApplicationRegistrationModel();
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


                            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "ApplicationRegistration");
                            string filePath = uploadsFolder;
                            string FileName;
                            if (MyUploader[i].FileName == "0")
                            {
                                FileName = $"FactoryLicence_{DateTime.Now:yyyyMMdd_hhmm}_{RegistrationID}" + type;
                            }
                            else if (MyUploader[i].FileName == "1")
                            {
                                FileName = $"GumastaCertificate_{DateTime.Now:yyyyMMdd_hhmm}_{RegistrationID}" + type;
                            }
                            else if (MyUploader[i].FileName == "2")
                            {
                                FileName = $"EPFCodeCertificate_{DateTime.Now:yyyyMMdd_hhmm}_{RegistrationID}" + type;
                            }
                            else if (MyUploader[i].FileName == "3")
                            {
                                FileName = $"ESICodeCertificate_{DateTime.Now:yyyyMMdd_hhmm}_{RegistrationID}" + type;
                            }
                            else if (MyUploader[i].FileName == "4")
                            {
                                FileName = $"Pancard_{DateTime.Now:yyyyMMdd_hhmm}_{RegistrationID}" + type;
                            }
                            else 
                            {
                                FileName = $"Others_{DateTime.Now:yyyyMMdd_hhmm}_{RegistrationID}" + type;
                            }

                            //string FileName = MyUploader[i].FileName;
                            ApplicationRegistrationModel ClssaveRecord = new ApplicationRegistrationModel();
                            ClssaveRecord.RegistrationID = RegistrationID;
                            ClssaveRecord.fileName = FileName;
                            ClssaveRecord.DocumentID = Convert.ToInt32(MyUploader[i].FileName);
                            ClssaveRecord.URL = URL;
                            ClssaveRecord.UserID = Convert.ToInt32(_ID);
                            ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                            ClssaveRecord.IP_Address = IP;
                            ClssaveRecord.AppID = AppID;

                            if (IsSubmit == 1) { ClssaveRecord.IsSubmit = true; } else { ClssaveRecord.IsSubmit = false; }

                            var output = _FileUpload.UploadDocument(MyUploader[i], FileName, filePath);
                            var output1 = _ApplicationRegistration.SaveDocumnetandapplication(ClssaveRecord);
                            ClsEstablishment = output1;
                            ClsEstablishment.stpid = 4;
                            ClsEstablishment.TEMPID = Encrypt_Decrypt.Encrypt(output1.RegistrationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "DocumentUploader", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
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
                    var path = Path.Combine("wwwroot", "Documents", "ApplicationRegistration", FileName);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "DownloadFile", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public JsonResult ApplicationRegistrationUpdateSubmit(ApplicationRegistrationModel Obj)
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
                    ApplicationRegistrationModel ClssaveRecord = new ApplicationRegistrationModel();
                    ClssaveRecord = _ApplicationRegistration.ApplicationRegistrationUpdateSubmit(Obj);
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
                _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "ApplicationRegistrationUpdateSubmit", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


        public ActionResult ApplicationRegistrationAClList()
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
                    ApplicationRegistrationModel ClsAppliactionlst = new ApplicationRegistrationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _ApplicationRegistration.ApplicationRegistrationAClList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.RegistrationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "ApplicationRegistrationAClList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public ActionResult ApplicationRegistrationHODCLList()
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
                ApplicationRegistrationModel ClsAppliactionlst = new ApplicationRegistrationModel();
                ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                var List = _ApplicationRegistration.ApplicationRegistrationHODCLList(ClsAppliactionlst);
                if (List.Count > 0)
                {
                    foreach (var item in List)
                    {
                        item.EncryptedId = Encrypt_Decrypt.Encrypt(item.RegistrationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "ApplicationRegistrationHODCLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public ActionResult SendtoCommentFromACL(string Comments, string id, string URL,int IsDCL)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int RegistrationID = 0;
                    if (id != null)
                    {
                        RegistrationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }


                    ApplicationRegistrationModel ClssaveRecord = new ApplicationRegistrationModel();
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.RegistrationID = RegistrationID;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.Comments = Comments;
                    ClssaveRecord.ISDL = IsDCL;
                    ClssaveRecord = _ApplicationRegistration.SendtoCommentFromACL(ClssaveRecord);
                    //if (ClssaveRecord.ErrorCode == 0)
                    //{
                    //    var test = SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                    //}

                    ////if (ClssaveRecord.ErrorCode == 0)
                    ////{
                    ////    int i = 0;
                    ////    int v = 0;

                    ////    if (ClssaveRecord.ApplicantMailDetail.Count > 0)
                    ////    {
                    ////        for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
                    ////        {
                    ////            if (ClssaveRecord.ApplicantMailDetail[i].Email != null)
                    ////            {
                    ////                var test = SendMailApplicant(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, ClssaveRecord.ApplicantMailDetail[i].Body);
                    ////            }
                    ////        }
                    ////    }

                    ////    if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
                    ////    {
                    ////        for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
                    ////        {
                    ////            if (ClssaveRecord.EshtablishmentMailDetail[v].Email != null)
                    ////            {
                    ////                var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                    ////            }
                    ////        }
                    ////    }
                    ////}
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
                _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "SendtoCommentFromACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        //public ActionResult ApplicationApproveRejectFromACL(ApplicationRegistrationModel obj)
        //{
        //    var _ID = HttpContext.Session.GetInt32("_ID");
        //    var _UserMode = HttpContext.Session.GetInt32("_UserMode");
        //    var IP = heserver.AddressList[1].ToString();
        //    try
        //    {
        //        if (_ID != null && _ID != 0)
        //        {
        //            //int RegistrationID = 0;
        //            //if (id != null)
        //            //{
        //            //    RegistrationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
        //            //}


        //            ApplicationRegistrationModel ClssaveRecord = new ApplicationRegistrationModel();
        //            //ClssaveRecord.UserID = Convert.ToInt32(_ID);
        //            //ClssaveRecord.RegistrationID = RegistrationID;
        //             obj.UserID = Convert.ToInt32(_ID);
        //             obj.UserMode = Convert.ToInt32(_UserMode);
        //            obj.IP_Address = IP;
        //            ClssaveRecord = _ApplicationRegistration.ApplicationApproveRejectFromACL(obj);
        //            //if (ClssaveRecord.ErrorCode == 0)
        //            //{
        //            //    var test = SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
        //            //}

        //            ////if (ClssaveRecord.ErrorCode == 0)
        //            ////{
        //            ////    int i = 0;
        //            ////    int v = 0;

        //            ////    if (ClssaveRecord.ApplicantMailDetail.Count > 0)
        //            ////    {
        //            ////        for (i = 0; i < ClssaveRecord.ApplicantMailDetail.Count; i++)
        //            ////        {
        //            ////            if (ClssaveRecord.ApplicantMailDetail[i].Email != null)
        //            ////            {
        //            ////                var test = SendMailApplicant(ClssaveRecord.ApplicantMailDetail[i].Email, ClssaveRecord.ApplicantMailDetail[i].Subject, ClssaveRecord.ApplicantMailDetail[i].Body);
        //            ////            }
        //            ////        }
        //            ////    }

        //            ////    if (ClssaveRecord.EshtablishmentMailDetail.Count > 0)
        //            ////    {
        //            ////        for (v = 0; v < ClssaveRecord.EshtablishmentMailDetail.Count; v++)
        //            ////        {
        //            ////            if (ClssaveRecord.EshtablishmentMailDetail[v].Email != null)
        //            ////            {
        //            ////                var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, ClssaveRecord.EshtablishmentMailDetail[v].Body);
        //            ////            }
        //            ////        }
        //            ////    }
        //            ////}
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
        //        _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "ApplicationApproveRejectFromACL", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
        //        return new JsonResult(ex.Message)
        //        {
        //            StatusCode = (int)HttpStatusCode.InternalServerError
        //        };
        //    }

        //}

        public ActionResult ApplicationRegistrationHOAClList()
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
                    ApplicationRegistrationModel ClsAppliactionlst = new ApplicationRegistrationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _ApplicationRegistration.ApplicationRegistrationHOAClList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.RegistrationID.ToString());
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
                _Commompository.LogErrorintbl(ex, "ApplicationRegistrationController", "ApplicationRegistrationAClList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public ActionResult AppRegistrationSendtoHODCLFromACL(string id, string URL,string Comments)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int RegistrationID = 0;
                    if (id != null)
                    {
                        RegistrationID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }

                    ApplicationRegistrationModel ClssaveRecord = new ApplicationRegistrationModel();
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.RegistrationID = RegistrationID;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.Comments = Comments;
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord = _ApplicationRegistration.AppRegistrationSendtoHODCLFromACL(ClssaveRecord);

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
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SendtoACLFromClerk", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public ActionResult ApplicationRegistrationAmendmentList()
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
                    ApplicationRegistrationModel ClsAppliactionlst = new ApplicationRegistrationModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.mobile = _MoNo;

                    //ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    //ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    //ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    var List = _ApplicationRegistration.ApplicationRegistrationAmendmentList(ClsAppliactionlst);
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.RegistrationID.ToString());
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


        [HttpPost]
        [RequestSizeLimit(20000000)]
        public IActionResult ApplicationApproveRejectFromACL(IFormFile MyUploader, ApplicationRegistrationModel obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    ApplicationRegistrationModel ClssaveRecord = new ApplicationRegistrationModel();
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "ApplicationRegistration");
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
                    ClssaveRecord.RegistrationID = obj.RegistrationID;
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

                        FileName = $"ApproveRejectDoc_{DateTime.Now:yyyyMMdd_hhmm}_{obj.RegistrationID}" + type;
                        ClssaveRecord.fileName = FileName;
                        
                        output = _FileUpload.UploadDocument(MyUploader, FileName, filePath);
                        ClssaveRecord = _ApplicationRegistration.ApplicationApproveRejectFromACL(ClssaveRecord);
                    }
                    else
                    {
                        ClssaveRecord = _ApplicationRegistration.ApplicationApproveRejectFromACL(ClssaveRecord);
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
    }
    
}



