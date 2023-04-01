using FTS.Business.CessProjectDetails;
using FTS.Business.CommonList;
using FTS.Model.Common;
using FTS.Model.Entities;
using FileManager;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CessProjectDetailsController : Controller
    {
        public IProjectDetailsBl _ProjectDetailspository;
        public ICommonListBI _Commompository;
        public IFileUploadService _FileUpload;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CessProjectDetailsController(IProjectDetailsBl _ProjectDetailspository, IFileUploadService FileUpload, IWebHostEnvironment webHostEnvironment, ICommonListBI commompository)
        {
            this._ProjectDetailspository = _ProjectDetailspository;
            _Commompository = commompository;
            this.webHostEnvironment = webHostEnvironment;
            _FileUpload = FileUpload;
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
                    var List = _ProjectDetailspository.ProjectDetailsList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.ProjectID.ToString());
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
                _Commompository.LogErrorintbl(ex, "CessProjectDetailsController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }


        }

        public ActionResult ProjectDetails(string projectid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int ProjectID = 0;
                    if (projectid != null)
                    {
                        ProjectID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(projectid));
                    }
                    CessProjectDetailsModel ClsProjectDetailsRecord = new CessProjectDetailsModel();
                    ClsProjectDetailsRecord = _ProjectDetailspository.ProjectDetailsRecord(ProjectID);
                    var TalukaList = _Commompository.TalukaList(ProjectID, ClsProjectDetailsRecord.DistrictID);
                    var DistrictList = _Commompository.DistrictList(ProjectID);
                    var DepartmentList = _Commompository.DepartmentList(ProjectID);
                    ClsProjectDetailsRecord.Talukalist = TalukaList;
                    ClsProjectDetailsRecord.Districtlist = DistrictList;
                    ClsProjectDetailsRecord.DepartmentList = DepartmentList;
                    ClsProjectDetailsRecord.ProjectIDEdit = ProjectID;
                    ClsProjectDetailsRecord.UserID = Convert.ToInt32(_ID);
                    return View("ProjectDetails", ClsProjectDetailsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "CessProjectDetailsController", "ProjectDetails", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
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
                _Commompository.LogErrorintbl(ex, "CessProjectDetailsController", "TalukaList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
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
                if (_ID != null && _ID != 0)
                {
                    ConciliationApplicationModel List = new ConciliationApplicationModel();
                    List.Talukalist = _Commompository.TalukaList(mode, DistrictID);
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
                _Commompository.LogErrorintbl(ex, "CessProjectDetailsController", "AllList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult SaveProjectDetails(CessProjectDetailsModel ObjProjectdtl)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
         
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    ObjProjectdtl.UserID = Convert.ToInt32(_ID);
                    CessProjectDetailsModel ClsBundleBreak = new CessProjectDetailsModel();
                    ClsBundleBreak = _ProjectDetailspository.SaveProjectDetails(ObjProjectdtl);
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
                _Commompository.LogErrorintbl(ex, "CessProjectDetailsController", "SaveProjectDetails", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult DeleteProjectDetails(int ProjectID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int UserID = 1;
                    CessProjectDetailsModel ClsBundleBreak = new CessProjectDetailsModel();
                    ClsBundleBreak = _ProjectDetailspository.DeleteProjectDetails(UserID, ProjectID);
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
                _Commompository.LogErrorintbl(ex, "CessProjectDetailsController", "DeleteProjectDetails", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        public ActionResult CessCollectionDetails( string establishmentid )
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
                    CessProjectDetailsModel ClsCessCollectionDetailsRecord = new CessProjectDetailsModel();
                    ClsCessCollectionDetailsRecord = _ProjectDetailspository.CessCollectionDetailsRecord(EstablishmentID);

                    var TypeOfBodyList = _Commompository.TypeOfBodyList(EstablishmentID);
                    ClsCessCollectionDetailsRecord.TypeOfBodylist = TypeOfBodyList;

                    var CessCollectionTypeList = _Commompository.CessPaymentTypeList(EstablishmentID);
                    ClsCessCollectionDetailsRecord.CessPaymentTypelist = CessCollectionTypeList;

                    var ProjectList = _Commompository.ProjectList(EstablishmentID);
                    ClsCessCollectionDetailsRecord.Projectlist = ProjectList;

                    ClsCessCollectionDetailsRecord.EstablishmentIDEdit = EstablishmentID;
                    return View("CessCollectionDetails", ClsCessCollectionDetailsRecord);

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "CessProjectDetailsController", "CessCollectionDetails", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }


        public IActionResult SaveCessCollectionDetails(List<IFormFile> MyUploader, int EstablishmentID, string EstablishmentName, int ProjectID,int TypeOfBodyID, int CessPaymentID, decimal Amount, string ChallanNumber, DateTime Date , bool IsActive)

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
                    int i = 0;
                    int F = 0;
                    CessProjectDetailsModel ClssaveEstablishment = new CessProjectDetailsModel();
                    CessProjectDetailsModel ClsBundleBreak = new CessProjectDetailsModel();
                    var Doc1 = "";
                    //var Doc2 = "";
                    //var Doc3 = "";
                    //var Doc4 = "";
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


                            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "CessProjectCollection");
                            string filePath = uploadsFolder; 
                            string FileName;


                            // string output = "";
                            var output = false;
                            if (i == 0)
                            {
                                Doc1 = $"Doc1{DateTime.Now:yyyyMMdd_hhmm}_{EstablishmentID}" + type;
                                output = _FileUpload.UploadDocument(MyUploader[i], Doc1, filePath);
                            }
                            else if (i == 1)
                            {
                                //Doc2 = $"Doc2{DateTime.Now:yyyyMMdd_hhmm}_{EstablishmentID}" + type;
                                 //output = _FileUpload.UploadDocument(MyUploader[i], Doc2, filePath);
                            }
                            else if (i == 2)
                            {
                                 //Doc3 = $"Doc3{DateTime.Now:yyyyMMdd_hhmm}_{EstablishmentID}" + type;
                                 //output = _FileUpload.UploadDocument(MyUploader[i], Doc3, filePath);
                            }
                            else
                            {
                                //Doc4 = $"Doc4{DateTime.Now:yyyyMMdd_hhmm}_{EstablishmentID}" + type;
                                 //output = _FileUpload.UploadDocument(MyUploader[i], Doc4, filePath);
                            }

                            //string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "CessProjectCollection");
                            //string filePath = uploadsFolder;

                            //string FileName = $"Doc1{DateTime.Now:yyyyMMdd_hhmm}_{ClsBundleBreak.EstablishmentID}" + type;

                           
                     
                            ClsBundleBreak.EstablishmentID = EstablishmentID;
                            ClsBundleBreak.EstablishmentName = EstablishmentName;
                            ClsBundleBreak.ProjectID = ProjectID;
                            ClsBundleBreak.TypeOfBodyID = TypeOfBodyID;
                            ClsBundleBreak.CessPaymentID = CessPaymentID;
                            ClsBundleBreak.Amount = Amount;
                            ClsBundleBreak.ChallanNumber = ChallanNumber;
                            ClsBundleBreak.Date = Date;
                            ClsBundleBreak.IsActive = IsActive;
                            ClsBundleBreak.PositionID = Convert.ToInt32(_PositionID);
                            ClsBundleBreak.PositionDistrictID = Convert.ToInt32(_DistrictID);
                            //  ClsBundleBreak.fileName = FileName;


                            // ClsBundleBreak.Doc2 = FileName;
                            //if (IsSubmit == 1) { ClssaveRecord.IsSubmit = true; } else { ClssaveRecord.IsSubmit = false; }


                            //if (output == true && output1.ErrorCode == 0)
                            //{
                            //    F = i;
                            //}

                        }
                        ClsBundleBreak.Doc1 = Doc1;
                        //ClsBundleBreak.Doc2 = Doc2;
                        //ClsBundleBreak.Doc3 = Doc3;
                        //ClsBundleBreak.Doc4 = Doc4;

                        var output1 = _ProjectDetailspository.SaveCessCollectionDetails(ClsBundleBreak);
                        //ClsBundleBreak = _ProjectDetailspository.SaveCessCollectionDetails(ClsBundleBreak);
                        ClssaveEstablishment = output1;
                        if ( output1.ErrorCode == 0)
                            {
                                F = i;
                            }

                    }
                    if (F != 1)
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
                _Commompository.LogErrorintbl(ex, "CessProjectDetails", "SaveCessCollectionDetails", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
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
                    var path = Path.Combine("wwwroot", "Documents", "CessProjectCollection", FileName);
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
                _Commompository.LogErrorintbl(ex, "CessProjectDetails", "DownloadFile", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }


        public IActionResult Index1()
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
                   
                    CessProjectDetailsModel model = new CessProjectDetailsModel();
                    //model.PageNumber = 1;
                    //model.PageSize = 100;
                    //model.SearchText = "";
                    model.PositionID = Convert.ToInt32(_PositionID);
                    model.PositionDistrictID = Convert.ToInt32(_DistrictID);
                    int totalrecord = 0;
                    var List = _ProjectDetailspository.CessCollectionList(model);
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
                _Commompository.LogErrorintbl(ex, "CessProjectDetailsController", "Index1", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }



        }
    }
}
