using FTS.Business.CommonList;
using FTS.Business.Inspection;
using FTS.Business.RoleMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Email;
using FileManager;
using Logger;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Net;

namespace FTS_Web.Controllers
{

    public class inspectionController : Controller
    {
        public ICommonListBI _Commompository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IInspectionBI _InspectionBI;
        private ILogging _logger;
        public IFileUploadService _FileUpload;

        public inspectionController(IInspectionBI InspectionBI, IFileUploadService FileUpload, ICommonListBI commompository, IWebHostEnvironment webHostEnvironment, ILogging logger)
        {

            this._InspectionBI = InspectionBI;
            _Commompository = commompository;
            this.webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _FileUpload = FileUpload;
        }
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());


        public ActionResult Index()
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
                inspectionModel ClsAppliactionlst = new inspectionModel();
                ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                //ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                //ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                //ClsAppliactionlst.TalukaCode = Convert.ToInt32(_TalukaID);
                var List = _InspectionBI.InspectionList(ClsAppliactionlst);
                if (List.Count > 0)
                {
                    foreach (var item in List)
                    {
                        item.EncryptedId = Encrypt_Decrypt.Encrypt(item.InspectionID.ToString());
                    }
                }

                return View(List);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult _InspectionView(inspectionModel obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            //try
            //{
            if (_ID != null && _ID != 0)
            {
                int InspectionID = 0;
                if (obj.EncryptedId != null)
                {
                    InspectionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(obj.EncryptedId));
                }


                inspectionModel ClsAppliaction = new inspectionModel();
                ClsAppliaction.UserID = Convert.ToInt32(_ID);
                ClsAppliaction.UserMode = Convert.ToInt32(_UserMode);
                ClsAppliaction.InspectionID = InspectionID;

                ClsAppliaction = _InspectionBI.InspectionApplicationRecord(ClsAppliaction);

                ClsAppliaction._inspectionList[0].Districtlist = _Commompository.DistrictList(ClsAppliaction._inspectionList[0].DistrictID);
                var TypeOfBusinessTradeList = _Commompository.TypeOfBusinessTradeList();
                ClsAppliaction._inspectionList[0].Talukalist = _Commompository.TalukaList(ClsAppliaction._inspectionList[0].InspectionID, ClsAppliaction._inspectionList[0].DistrictID);
                ClsAppliaction._inspectionList[0].AreaList = _Commompository.AreaList(ClsAppliaction._inspectionList[0].InspectionID, ClsAppliaction._inspectionList[0].DistrictID);
                ClsAppliaction.InspectionIDEdit = ClsAppliaction.InspectionID;
                ClsAppliaction.IP_Address = IP;
                ClsAppliaction.UserID = Convert.ToInt32(_ID);
                ClsAppliaction.UserMode = Convert.ToInt32(_UserMode);
                ClsAppliaction.stpid = obj.stpid;
                ClsAppliaction._inspectionList[0].EstablishmenttypeList = ClsAppliaction.EstablishmenttypeList;
                ClsAppliaction._inspectionList[0].YesNoList = ClsAppliaction.YesNoList;
                //ClsAppliaction._inspectionList[0].DocumentList = ClsAppliaction.DocumentList;

                var i = 0;
                if (ClsAppliaction._inspectionEstablishmentsDetailsList.Count > 0)
                {
                    for (i = 0; i < ClsAppliaction._inspectionEstablishmentsDetailsList.Count; ++i)
                    {
                        ClsAppliaction._inspectionEstablishmentsDetailsList[i].Districtlist = _Commompository.DistrictList(ClsAppliaction._inspectionEstablishmentsDetailsList[i].EID);
                        ClsAppliaction._inspectionEstablishmentsDetailsList[i].Talukalist = _Commompository.TalukaList(ClsAppliaction._inspectionEstablishmentsDetailsList[i].EID, ClsAppliaction._inspectionEstablishmentsDetailsList[i].DistrictID);
                        ClsAppliaction._inspectionEstablishmentsDetailsList[i].AreaList = _Commompository.AreaList(ClsAppliaction._inspectionEstablishmentsDetailsList[i].EID, ClsAppliaction._inspectionEstablishmentsDetailsList[i].DistrictID);
                        ClsAppliaction._inspectionEstablishmentsDetailsList[i].TypeOfBusinessTradeList = TypeOfBusinessTradeList;
                        ClsAppliaction._inspectionEstablishmentsDetailsList[i].EstablishmentList = _Commompository.EstablishmentList(ClsAppliaction._inspectionEstablishmentsDetailsList[i].EID);
                        ClsAppliaction._inspectionEstablishmentsDetailsList[i].EstablishmentDetailstypeList = ClsAppliaction.EstablishmentDetailstypeList;

                    }
                }

                var b = 0;
                if (ClsAppliaction._inspectionEstablishmentEmployeeDetailsList.Count > 0)
                {
                    for (b = 0; b < ClsAppliaction._inspectionEstablishmentEmployeeDetailsList.Count; ++b)
                    {
                        ClsAppliaction._inspectionEstablishmentEmployeeDetailsList[b].Districtlist = _Commompository.DistrictList(ClsAppliaction._inspectionEstablishmentEmployeeDetailsList[b].EmployeeID);
                        ClsAppliaction._inspectionEstablishmentEmployeeDetailsList[b].Talukalist = _Commompository.TalukaList(ClsAppliaction._inspectionEstablishmentEmployeeDetailsList[b].EmployeeID, ClsAppliaction._inspectionEstablishmentEmployeeDetailsList[b].DistrictID);
                        ClsAppliaction._inspectionEstablishmentEmployeeDetailsList[b].AreaList = _Commompository.AreaList(ClsAppliaction._inspectionEstablishmentEmployeeDetailsList[b].EmployeeID, ClsAppliaction._inspectionEstablishmentEmployeeDetailsList[b].DistrictID);
                        ClsAppliaction._inspectionEstablishmentEmployeeDetailsList[b].DesignationList = ClsAppliaction.DesignationList;
                    }
                }

                var c = 0;
                if (ClsAppliaction._inspectionEstablishmentContractorDetailsList.Count > 0)
                {
                    for (c = 0; c < ClsAppliaction._inspectionEstablishmentContractorDetailsList.Count; ++c)
                    {
                        ClsAppliaction._inspectionEstablishmentContractorDetailsList[c].YesNoList = ClsAppliaction.YesNoList;
                    }
                }

                return PartialView(ClsAppliaction);
                //return View("AddApplicationRegistration", ClsAppliaction);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            //}
            //catch (Exception ex)
            //{
            //    _Commompository.LogErrorintbl(ex, "inspectionController", "_InspectionView", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
            //    return StatusCode(500, ex.Message);
            //}

        }

        public JsonResult SaveReportInformationRecord(inspectionModel Obj)
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
                    inspectionModel ClssaveRecord = new inspectionModel();
                    ClssaveRecord = _InspectionBI.SaveReportInformationRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.InspectionID.ToString());
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
                _Commompository.LogErrorintbl(ex, "inspectionController", "SaveReportInformationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }


        public JsonResult SaveEstablishmentsRecord(inspectionModel Obj)
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
                    inspectionModel ClssaveRecord = new inspectionModel();
                    ClssaveRecord = _InspectionBI.SaveEstablishmentsRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.InspectionID.ToString());
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
                _Commompository.LogErrorintbl(ex, "inspectionController", "SaveReportInformationRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }


        public JsonResult SaveEstablishmentsDetailRecord(inspectionModel Obj)
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
                    inspectionModel ClssaveRecord = new inspectionModel();
                    ClssaveRecord = _InspectionBI.SaveEstablishmentsDetailRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.InspectionID.ToString());
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
                _Commompository.LogErrorintbl(ex, "inspectionController", "SaveEstablishmentsDetailRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public JsonResult SaveCompanyDetailsRecord(inspectionModel Obj)
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
                    inspectionModel ClssaveRecord = new inspectionModel();
                    ClssaveRecord = _InspectionBI.SaveCompanyDetailsRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.InspectionID.ToString());
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
                _Commompository.LogErrorintbl(ex, "inspectionController", "SaveEstablishmentsDetailRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public JsonResult SaveEmployeeDetailsRecord(inspectionModel Obj)
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
                    inspectionModel ClssaveRecord = new inspectionModel();
                    ClssaveRecord = _InspectionBI.SaveEmployeeDetailsRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.InspectionID.ToString());
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
                _Commompository.LogErrorintbl(ex, "inspectionController", "SaveEstablishmentsDetailRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public JsonResult SaveContractorDetailsRecord(inspectionModel Obj)
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
                    inspectionModel ClssaveRecord = new inspectionModel();
                    ClssaveRecord = _InspectionBI.SaveContractorDetailsRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.InspectionID.ToString());
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
                _Commompository.LogErrorintbl(ex, "inspectionController", "SaveEstablishmentsDetailRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        [HttpPost]
        [RequestSizeLimit(60000000)]
        public IActionResult SaveInspectionOnsitePicture(List<IFormFile> MyUploader, int InspectionID, string URL)
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
                    inspectionModel ClsEstablishment = new inspectionModel();
                    if (MyUploader.Count > 0)
                    {
                        for (i = 0; i < MyUploader.Count; i++)
                        {

                            string type = "";
                            if (MyUploader[i].ContentType == "application/pdf") { type = ".pdf"; }
                            else if (MyUploader[i].ContentType == "application/vnd.ms-word") { type = ".docx"; }
                            else if (MyUploader[i].ContentType == "application/vnd.ms-excel") { type = ".xls"; }
                            else if (MyUploader[i].ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { type = ".xlsx"; }
                            else if (MyUploader[i].ContentType == "image/jpeg") { type = ".jpeg"; }
                            else if (MyUploader[i].ContentType == "image/png") { type = ".png"; };


                            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "Inspection", "OnsitePicture");
                            string filePath = uploadsFolder;
                            string FileName;

                            FileName = $"OnsitePicture_{DateTime.Now:yyyyMMdd_hhmm}_{i}_{InspectionID}" + type;

                            //string FileName = MyUploader[i].FileName;
                            inspectionModel ClssaveRecord = new inspectionModel();
                            ClssaveRecord.InspectionID = InspectionID;
                            ClssaveRecord.fileName = FileName;
                            //ClssaveRecord.DocumentID = i;
                            ClssaveRecord.URL = URL;
                            ClssaveRecord.UserID = Convert.ToInt32(_ID);
                            ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                            ClssaveRecord.IP_Address = IP;

                            var output = _FileUpload.UploadDocument(MyUploader[i], FileName, filePath);
                            var output1 = _InspectionBI.SaveInspectionOnsitePicture(ClssaveRecord);
                            ClsEstablishment = output1;
                        }

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
                _Commompository.LogErrorintbl(ex, "InspectionController", "SaveInspectionOnsitePicture", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }


        }

        public JsonResult SaveOtherDetailsRecord(inspectionModel Obj)
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

                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "Inspection", "Signature");
                    string filePath = uploadsFolder;
                    string InspectorSignFileName = "";
                    string ComplianceSignFileName = "";
                    string AuthorizedSignFileName = "";

                    if (Obj.String64InspectorSignature != null)
                    {
                        if (Obj.String64InspectorSignature.Length > 1)
                        {
                            byte[] bytes = System.Convert.FromBase64String(Obj.String64InspectorSignature);
                            InspectorSignFileName = $"InspectorSign_{DateTime.Now:yyyyMMdd_hhmm}_{1}_{Obj.InspectionID}.png";
                            System.IO.File.WriteAllBytes(filePath + "\\" + InspectorSignFileName, bytes);
                        }
                    }

                    if (Obj.String64ComplianceSignature != null)
                    {
                        if (Obj.String64ComplianceSignature.Length > 1)
                        {
                            byte[] bytes = System.Convert.FromBase64String(Obj.String64ComplianceSignature);
                            ComplianceSignFileName = $"ComplianceSign_{DateTime.Now:yyyyMMdd_hhmm}_{1}_{Obj.InspectionID}.png";
                            System.IO.File.WriteAllBytes(filePath + "\\" + ComplianceSignFileName, bytes);
                        }
                    }

                    if (Obj.String64AuthorizedPersonSignature != null)
                    {
                        if (Obj.String64AuthorizedPersonSignature.Length > 1)
                        {
                            byte[] bytes = System.Convert.FromBase64String(Obj.String64AuthorizedPersonSignature);
                            AuthorizedSignFileName = $"AuthorizedSign_{DateTime.Now:yyyyMMdd_hhmm}_{1}_{Obj.InspectionID}.png";
                            System.IO.File.WriteAllBytes(filePath + "\\" + AuthorizedSignFileName, bytes);
                        }
                    }

                    if (AuthorizedSignFileName != "")
                    {
                        Obj.AuthorizedSignPath = AuthorizedSignFileName;
                    }

                    if (InspectorSignFileName != "")
                    {
                        Obj.InspectorSignPath = InspectorSignFileName;
                    }

                    if (ComplianceSignFileName != "")
                    {
                        Obj.ComplianceSignPath = ComplianceSignFileName;
                    }

                    inspectionModel ClssaveRecord = new inspectionModel();
                    ClssaveRecord = _InspectionBI.SaveOtherDetailsRecord(Obj);
                    ClssaveRecord.TEMPID = Encrypt_Decrypt.Encrypt(ClssaveRecord.InspectionID.ToString());
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
                _Commompository.LogErrorintbl(ex, "inspectionController", "SaveEstablishmentsDetailRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        public IActionResult CC() => View(new inspectionModel());
    }
}
