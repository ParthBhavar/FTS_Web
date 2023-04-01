using FTS.Business.CommonList;
using FTS.Business.ReinstatementAppliaction;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FileManager;
using AspNetCore.Reporting;
using Email;
using MySql.Data.MySqlClient;
using System.Data;
using Logger;
//using Rotativa.AspNetCore;
using RotativaHQ.AspNetCore;
//using FastReport.Export.Html;
//using System.Text;
//using FastReport;
//using FastReport.Export.PdfSimple;
using System.Net.Mail;
using FastReport.Export.PdfSimple;
using FastReport;
using System.Text;
using FastReport.Export.Html;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ReinstatementAppliactionController : Controller
    {
        public IReinstatementAppliactionBI _Reinstatementpository;
        public ICommonListBI _Commompository;
        public IFileUploadService _FileUpload;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string _connectionString;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ILogging _logger;
        public ReinstatementAppliactionController(IReinstatementAppliactionBI Reinstatementpository, ICommonListBI commompository, IFileUploadService FileUpload, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, IConfiguration configuration, ILogging logger)
        {

            this._Reinstatementpository = Reinstatementpository;
            _Commompository = commompository;
            _FileUpload = FileUpload;
            this._emailSender = emailSender;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
            _connectionString = configuration.GetConnectionString("DbConnection");
            _logger = logger;
        }


        public IDbConnection Connection => new MySqlConnection(_connectionString);
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
        //public int _ID = httpContextAccessor.HttpContext.Request.QueryString["ID"];

        //var _ID = HttpContext.Session.GetInt32("_ID");
        //var _DesID = HttpContext.Session.GetInt32("_DesID");
        //var _EmailID = HttpContext.Session.GetInt32("_EmailID");
        //var _MoNo = HttpContext.Session.GetInt32("_MoNo");
        //var _Gender = HttpContext.Session.GetInt32("_Gender");
        //var _PositionID = HttpContext.Session.GetInt32("_PositionID");
        //var _RegionID = HttpContext.Session.GetInt32("_RegionID");
        //var _BranchID = HttpContext.Session.GetInt32("_BranchID");
        //var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
        //var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
        //var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
        //var _UserMode = HttpContext.Session.GetInt32("_UserMode");

        public ActionResult Index()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
                var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
                var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
                var _PositionID = HttpContext.Session.GetInt32("_PositionID");

                if (_ID != null && _ID != 0)
                {
                    ReinstatementAppliactionModel ClsAppliactionlst = new ReinstatementAppliactionModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.PositionID = Convert.ToInt32(_PositionID);
                    ClsAppliactionlst.PositionDistrictID = Convert.ToInt32(_DistrictID);
                    //ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    //ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    //ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    var List = _Reinstatementpository.ReinstatementList(ClsAppliactionlst);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public IActionResult DemoPageSizePDF()
        {

            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
            var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
            var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
            var _PositionID = HttpContext.Session.GetInt32("_PositionID");

            ReinstatementAppliactionModel ClsAppliactionlst = new ReinstatementAppliactionModel();
            ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
            ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
            ClsAppliactionlst.PositionID = Convert.ToInt32(_PositionID);
            ClsAppliactionlst.PositionDistrictID = Convert.ToInt32(_DistrictID);
            //ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
            //ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
            //ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
            ReinstatementAppliactionModel test = new ReinstatementAppliactionModel();
            test.List = _Reinstatementpository.ReinstatementList(ClsAppliactionlst);
            //totalrecord = List[0].TotalRecord;
            //return View(List);

            //return new Rotativa.AspNetCore.ViewAsPdf("test");
            return new ViewAsPdf("_DetailView", test);

        }

        public ActionResult AddReinstatementAppliaction(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            try
            {
                if (_ID != null && _ID != 0)
                {
                    int AppliactionID = 0;
                    if (id != null)
                    {
                        AppliactionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }


                    ReinstatementAppliactionModel ClsAppliaction = new ReinstatementAppliactionModel();
                    ClsAppliaction = _Reinstatementpository.AppliactionRecord(AppliactionID);
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

                    return View("AddReinstatementAppliaction", ClsAppliaction);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "TalukaList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public IActionResult AllList(int mode, int DistrictID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            try
            {
                if (_ID != null && _ID != 0)
                {
                    ReinstatementAppliactionModel List = new ReinstatementAppliactionModel();
                    List.Talukalist = _Commompository.TalukaList(mode, DistrictID);
                    List.AreaList = _Commompository.AreaList(mode, DistrictID);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "AllList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public JsonResult SaveAppliactionRecord(ReinstatementAppliactionModel Obj)
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
                    Obj.IP_Address = IP;
                    ReinstatementAppliactionModel ClssaveRecord = new ReinstatementAppliactionModel();
                    ClssaveRecord = _Reinstatementpository.SaveAppliactionRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "SaveAppliactionRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public ActionResult AddReinstatementTradunion(int ApplicationID, int ISNew)
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
                    ReinstatementAppliactionModel ClsTradunion = new ReinstatementAppliactionModel();
                    ClsTradunion = _Reinstatementpository.ReinstatementTradunionRecord(ApplicationID, ISNew);
                    var DistrictList = _Commompository.DistrictList(ClsTradunion.TradunionID);
                    var TradunionList = _Commompository.TradunionList(ClsTradunion.TradunionID);
                    var TalukaList = _Commompository.TalukaList(ClsTradunion.TradunionID, ClsTradunion.DistrictID);
                    var AreaList = _Commompository.AreaList(ClsTradunion.TradunionID, ClsTradunion.DistrictID);
                    var DepartmentList = _Commompository.DepartmentList(ClsTradunion.TradunionID);
                    var WorkTypeList = _Commompository.WorkTypeList(ClsTradunion.TradunionID);
                    ClsTradunion.Talukalist = TalukaList;
                    ClsTradunion.Districtlist = DistrictList;
                    ClsTradunion.TradunionList = TradunionList;
                    ClsTradunion.DepartmentList = DepartmentList;
                    ClsTradunion.WorkTypeList = WorkTypeList;
                    ClsTradunion.AreaList = AreaList;
                    //ClsTradunion.EditId = AppliactionID;
                    return View("AddReinstatementTradunion", ClsTradunion);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "AddReinstatementTradunion", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public JsonResult SaveReinstatementTradunionRecord(ReinstatementAppliactionModel Obj)
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
                    ReinstatementAppliactionModel ClssaveRecord = new ReinstatementAppliactionModel();
                    ClssaveRecord = _Reinstatementpository.SaveReinstatementTradunionRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "SaveReinstatementTradunionRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        //public ActionResult AddReinstatementEstablishment(int ApplicationID, int ISNew)
        //{
        //    //int AppliactionID = 0;

        //    //if (id != null)
        //    //{
        //    //    AppliactionID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
        //    //}
        //    ReinstatementAppliactionModel ClsEstablishment = new ReinstatementAppliactionModel();
        //    ClsEstablishment.ClsEstablish = _Reinstatementpository.ReiniEstablishmentRecord(ApplicationID, ISNew);
        //    var DistrictList = _Commompository.DistrictList(ClsEstablishment.EstablisDetailID);
        //    var TalukaList = _Commompository.TalukaList(ClsEstablishment.EstablisDetailID, ClsEstablishment.DistrictID);
        //    var AreaList = _Commompository.AreaList(ClsEstablishment.EstablisDetailID, ClsEstablishment.DistrictID);
        //    var EstablishmentList = _Commompository.EstablishmentList(ClsEstablishment.EstablisDetailID);
        //    ClsEstablishment.Talukalist = TalukaList;
        //    ClsEstablishment.Districtlist = DistrictList;
        //    ClsEstablishment.AreaList = AreaList;
        //    ClsEstablishment.EstablishmentList = EstablishmentList;

        //    var i = 0;
        //    if (ClsEstablishment.ClsEstablish.Count > 0)
        //    {
        //        for (i = 0; i < ClsEstablishment.ClsEstablish.Count; ++i)
        //        {
        //            ClsEstablishment.ClsEstablish[i].Districtlist = _Commompository.DistrictList(ClsEstablishment.ClsEstablish[i].EstablisDetailID);
        //            ClsEstablishment.ClsEstablish[i].Talukalist = _Commompository.TalukaList(ClsEstablishment.ClsEstablish[i].EstablisDetailID, ClsEstablishment.ClsEstablish[i].DistrictID);
        //            ClsEstablishment.ClsEstablish[i].AreaList = _Commompository.AreaList(ClsEstablishment.ClsEstablish[i].EstablisDetailID, ClsEstablishment.ClsEstablish[i].DistrictID);
        //            ClsEstablishment.ClsEstablish[i].EstablishmentList = _Commompository.EstablishmentList(ClsEstablishment.ClsEstablish[i].EstablisDetailID);
        //        }
        //    }

        //    //ClsTradunion.EditId = AppliactionID;
        //    return View("AddReinstatementEstablishment", ClsEstablishment);
        //}

        public ActionResult AddReinstatementEstablishment(int ApplicationID, int ISNew)
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
                    ReinstatementAppliactionModel ClsEstablishment = new ReinstatementAppliactionModel();
                    ClsEstablishment.ClsEstablish = _Reinstatementpository.ReiniEstablishmentRecord(ApplicationID, ISNew);
                    var DistrictList = _Commompository.DistrictList(ClsEstablishment.ClsEstablish[0].EstablisDetailID);
                    //var TalukaList = _Commompository.TalukaList(ClsEstablishment.EstablisDetailID, ClsEstablishment.DistrictID);
                    //var AreaList = _Commompository.AreaList(ClsEstablishment.EstablisDetailID, ClsEstablishment.DistrictID);
                    var EstablishmentList = _Commompository.EstablishmentList(ClsEstablishment.ClsEstablish[0].EstablisDetailID);
                    //ClsEstablishment.Talukalist = TalukaList;
                    ClsEstablishment.Districtlist = DistrictList;
                    //ClsEstablishment.AreaList = AreaList;
                    ClsEstablishment.EstablishmentList = EstablishmentList;
                    //ClsTradunion.EditId = AppliactionID;
                    ClsEstablishment.ApplicationID = ApplicationID;
                    ClsEstablishment.ISNew = ISNew;
                    ClsEstablishment.IP_Address = heserver.AddressList[1].ToString();
                    ClsEstablishment.isReqiredTradDetail = ClsEstablishment.ClsEstablish[0].isReqiredTradDetail;
                    ClsEstablishment.IsSubmit = ClsEstablishment.ClsEstablish[0].IsSubmit;
                    ClsEstablishment.EncryptedId = Encrypt_Decrypt.Encrypt(ApplicationID.ToString());
                    return View("AddReinstatementEstablishment", ClsEstablishment);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "AddReinstatementEstablishment", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPost]
        public JsonResult ReinstatementEstablishment(int ApplicationID, int ISNew)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    ReinstatementAppliactionModel ClsEstablishment = new ReinstatementAppliactionModel();

                    //  ClsEstablishment.ClsEstablish = _Reinstatementpository.ReiniEstablishmentRecord(ApplicationID, ISNew);
                    //ClsEstablishment.TestList = _Reinstatementpository.ReiniEstablishmentRecord(ApplicationID, ISNew);
                    var ClsEstablish = _Reinstatementpository.ReiniEstablishmentRecord(ApplicationID, ISNew);
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
                            //ClsEstablish[i].AreaList = GetArea(ClsEstablish[i].EstablisDetailID, ClsEstablish[i].DistrictID);
                            ClsEstablish[i].EstablishmentList = _Commompository.EstablishmentList(ClsEstablish[i].EstablisDetailID);
                        }
                    }

                    //var B = 0;
                    //if (ClsEstablish.Count > 0)
                    //{
                    //    for (B = 0; B < ClsEstablish.Count; ++i)
                    //    {
                    //        ClsEstablish[B].AreaList = _Commompository.AreaList(ClsEstablish[B].EstablisDetailID, ClsEstablish[B].DistrictID);

                    //    }
                    //}


                    //var i = 0;
                    //if (ClsEstablishment.TestList.Count > 0)
                    //{
                    //    for (i = 0; i < ClsEstablishment.TestList.Count; ++i)
                    //    {
                    //        ClsEstablishment.TestList[i].Districtlist = _Commompository.DistrictList(ClsEstablishment.TestList[i].EstablisDetailID);
                    //        ClsEstablishment.TestList[i].Talukalist = _Commompository.TalukaList(ClsEstablishment.TestList[i].EstablisDetailID, ClsEstablishment.TestList[i].DistrictID);
                    //        ClsEstablishment.TestList[i].AreaList = _Commompository.AreaList(ClsEstablishment.TestList[i].EstablisDetailID, ClsEstablishment.TestList[i].DistrictID);
                    //        ClsEstablishment.TestList[i].EstablishmentList = _Commompository.EstablishmentList(ClsEstablishment.TestList[i].EstablisDetailID);
                    //    }
                    //}


                    //string[] ng = new string[5];
                    //ng[0] = "TRRR";
                    //ng[1] = "TRsR";
                    //ng[2] = "TRddR";
                    //ng[3] = "TfR";
                    //ng[4] = "TgggRR";
                    ////ClsEstablishment.TestList = ng;
                    //ClsEstablishment.TestList1 = ng;
                    //var settings = new JsonSerializerSettings()
                    //{
                    //    Converters =
                    //    {
                    //        new StringEnumConverter()
                    //    }
                    //};

                    //return new JsonResult(ClsEstablish, settings)
                    //{
                    //    StatusCode = (int)HttpStatusCode.OK
                    //};

                    //return new ContentResult()
                    //{
                    //    StatusCode = (int)HttpStatusCode.OK,
                    //    ContentType = "application/json",
                    //    Content = JsonConvert.SerializeObject(stock, settings)
                    //};
                    //return Json(new { data = JsonConvert.SerializeObject(ClsEstablish), MaxJsonLength = Int32.MaxValue });
                    return Json(new { data = ClsEstablish });


                    //return Json(new { data = ClsEstablishment });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _logger.Error("", ex);
                _logger.Log(ex.Message);
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "ReinstatementEstablishment", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        private List<MArea> GetArea(int EstablisDetailID, int DistrictID)
        {
            List<MArea> test = new List<MArea>();
            test = _Commompository.AreaList(EstablisDetailID, DistrictID);

            return test;

        }
        private List<MArea> GetCustomers()
        {
            return new List<MArea>()
            {
                new MArea(){ DataValue=1, DisplayValue="Ahmedabad", ZoneID=1},
                new MArea(){ DataValue=1, DisplayValue="Ahmedabad", ZoneID=1},
                new MArea(){ DataValue=1, DisplayValue="Ahmedabad", ZoneID=1},
                new MArea(){ DataValue=1, DisplayValue="Ahmedabad", ZoneID=1},
                new MArea(){ DataValue=1, DisplayValue="Ahmedabad", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
                new MArea(){ DataValue=3, DisplayValue="Parth", ZoneID=1},
            };
        }

        public JsonResult SaveReiniEstablishmentRecord(ReinstatementAppliactionModel Obj)
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
                    ReinstatementAppliactionModel ClssaveRecord = new ReinstatementAppliactionModel();
                    ClssaveRecord = _Reinstatementpository.SaveReiniEstablishmentRecord(Obj);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "SaveReiniEstablishmentRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
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
                    int F = 0;
                    ReinstatementAppliactionModel ClsEstablishment = new ReinstatementAppliactionModel();
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


                            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "Reinstatement");
                            string filePath = uploadsFolder;
                            string FileName;
                            if (i == 0)
                            {
                                FileName = $"Adhare_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (i == 1)
                            {
                                FileName = $"Pan_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else if (i == 2)
                            {
                                FileName = $"Doc_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            else 
                            {
                                FileName = $"AuthorityLetter_{DateTime.Now:yyyyMMdd_hhmm}_{ApplicationID}" + type;
                            }
                            //string FileName = MyUploader[i].FileName;
                            ReinstatementAppliactionModel ClssaveRecord = new ReinstatementAppliactionModel();
                            ClssaveRecord.ApplicationID = ApplicationID;
                            ClssaveRecord.fileName = FileName;
                            ClssaveRecord.DocumentID = i;
                            ClssaveRecord.URL = URL;
                            ClssaveRecord.UserID = Convert.ToInt32(_ID);
                            ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                            ClssaveRecord.IP_Address = IP;
                            ClssaveRecord.AppID = AppID;

                            if (IsSubmit == 1) { ClssaveRecord.IsSubmit = true; } else { ClssaveRecord.IsSubmit = false; }

                            var output = _FileUpload.UploadDocument(MyUploader[i], FileName, filePath);
                            var output1 = _Reinstatementpository.SaveDocumnetandapplication(ClssaveRecord);
                            ClsEstablishment = output1;

                            //if (output1.ErrorCode == 0 && output1.CID == 1)
                            //{
                            //   var  test = SendMailApplicant(output1.EMailIDList, output1.EmailSubject, output1.EmailBody);
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


                            if (output == true && output1.ErrorCode == 0)
                            {
                                F = i;
                            }

                        }

                    }
                    if (MyUploader.Count == 0)
                    {
                        if (F != 2)
                        {
                            ClsEstablishment.ErrorCode = 1;
                            ClsEstablishment.ErrorMassage = "You are already upoaded Document if you need update So please select Document ";
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "OnPostMyUploader", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }


        }

        public ActionResult AddUploadDocment(int ApplicationID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    ReinstatementAppliactionModel ClsEstablishment = new ReinstatementAppliactionModel();
                    ClsEstablishment = _Reinstatementpository.ReinAppliactionDocumentURLRecord(ApplicationID);
                    return View("AddUploadDocment", ClsEstablishment);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "AddUploadDocment", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
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
                    var path = Path.Combine("wwwroot", "Documents", "Reinstatement", FileName);
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
                _logger.Error("", ex);
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SendMailApplicant", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                throw;
            }
        }

        public async Task testMail()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            string EmailID = "rajgorchirag823@gmail.com";
            string Title = "test";
            string Body = "eweweerajgorchirag823@gmail.com";
            try
            {
                await _emailSender.SendEmailAsync(EmailID, Title, Body);
            }
            catch (Exception ex)
            {
                _logger.Error("", ex);
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SendMailApplicant", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                throw;
            }
        }

        public async Task testMail1()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            string EmailID = "rajgorchirag823@gmail.com";
            string Title = "test";
            string Body = "eweweerajgorchirag823@gmail.com";
            try
            {
                //var email = new MimeMessage();
                //email.From.Add(MailboxAddress.Parse("from_address@example.com"));
                //email.To.Add(MailboxAddress.Parse("to_address@example.com"));
                //email.Subject = "Test Email Subject";
                //email.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };

                MailMessage Msg = new MailMessage();
                // Sender e-mail address.
                Msg.From = new MailAddress("hogidc@gmail.com");
                // Recipient e-mail address.
                Msg.To.Add("rajgorchirag823@gmail.com");
                Msg.CC.Add("rajgorchirag823@gmail.com");
                Msg.Subject = "Timesheet Payment Instruction updated";
                Msg.IsBodyHtml = true;
                Msg.Body = "Test";

                var userName = "chirag960127@gmail.com";
                var password = "kcxmyggwcbpsigyq";
                var Credentials = new NetworkCredential(userName, password);

                // send email
                using var smtp = new SmtpClient();
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = Credentials;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Send(Msg);



            }
            catch (Exception ex)
            {
                _logger.Error("", ex);
                _Commompository.LogErrorintbl(ex, "NFormApplicationController", "SendMailApplicant", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                throw;
            }
        }


        //public  bool  SendMailApplicant(List<string> emails, string subject, List<Attachments> attachments, string body)
        //{
        //    var _ID = HttpContext.Session.GetInt32("_ID");
        //    var _UserMode = HttpContext.Session.GetInt32("_UserMode");
        //    var IP = heserver.AddressList[1].ToString();

        //    ReinstatementAppliactionModel Reappobj = new ReinstatementAppliactionModel();
        //    var test = _emailSender.SendEmailAsync(emails, subject, attachments, body);

        //    return true;
        //}
        public JsonResult ReinstatementAppliactionUpdateSubmit(ReinstatementAppliactionModel Obj)
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
                    ReinstatementAppliactionModel ClssaveRecord = new ReinstatementAppliactionModel();
                    ClssaveRecord = _Reinstatementpository.ReinstatementAppliactionUpdateSubmit(Obj);
                    //if(ClssaveRecord.ErrorCode == 0)
                    //{
                    //    var test = SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);

                    //    //_emailSender.SendEmailAsync(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "ReinstatementAppliactionUpdateSubmit", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public ActionResult ReinstatementACOLList()
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
                    ReinstatementAppliactionModel ClsAppliactionlst = new ReinstatementAppliactionModel();
                    ClsAppliactionlst.UserID = Convert.ToInt32(_ID);
                    ClsAppliactionlst.UserMode = Convert.ToInt32(_UserMode);
                    ClsAppliactionlst.ZoneID = Convert.ToInt32(_ZoneID);
                    ClsAppliactionlst.DistrictID = Convert.ToInt32(_DistrictID);
                    ClsAppliactionlst.TalukaID = Convert.ToInt32(_TalukaID);
                    ClsAppliactionlst.RoleID = Convert.ToInt32(_RoleID);
                    var List = _Reinstatementpository.ReinstatementACOLList(ClsAppliactionlst);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "ReinstatementACOLList", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public ActionResult AddReinstatementHearingALC(string id, int ISNew, int ISView)
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
                    ReinstatementAppliactionModel ClsRecord = new ReinstatementAppliactionModel();
                    if (ISNew == 0)
                    {
                        ClsRecord = _Reinstatementpository.ReinstatementACOLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID, IsACL);

                    }
                    else if (ISNew == 1)  
                    {
                        ClsRecord = _Reinstatementpository.ReinstatementACOLRecord(ApplicationID, ISNew);
                        ClsRecord.HearingReasonList = _Commompository.HearingReasonList(ClsRecord.ApplicationID, IsACL);
                    }
                    else if (ISNew == 2)
                    {
                        ClsRecord = _Reinstatementpository.ReinstatementACOLRecord(ApplicationID, ISNew);
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
                    return View("AddReinstatementHearingALC", ClsRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "AddReinstatementHearingALC", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }



        }

        public JsonResult SaveReinstatementHearingACOLRecord(ReinstatementAppliactionModel Obj)
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
                    ReinstatementAppliactionModel ClssaveRecord = new ReinstatementAppliactionModel();
                    ClssaveRecord = _Reinstatementpository.SaveReinstatementHearingACOLRecord(Obj);

                    EmailReportModel EmailReportDetail = new EmailReportModel();
                    EmailReportDetail.DictrictACLOffice = ClssaveRecord.EmailReportDetail[0].DictrictACLOffice;
                    EmailReportDetail.AppID = ClssaveRecord.EmailReportDetail[0].AppID;
                    EmailReportDetail.ApplicantDetail = ClssaveRecord.EmailReportDetail[0].ApplicantDetail;
                    EmailReportDetail.Establishmentdetail = ClssaveRecord.EmailReportDetail[0].Establishmentdetail;
                    EmailReportDetail.ACLName = ClssaveRecord.EmailReportDetail[0].ACLName;
                    EmailReportDetail.DictrictACL = ClssaveRecord.EmailReportDetail[0].DictrictACL;
                    EmailReportDetail.Date = ClssaveRecord.EmailReportDetail[0].Date;
                    EmailReportDetail.HearingDate = ClssaveRecord.EmailReportDetail[0].HearingDate;
                    EmailReportDetail.HearingTime = ClssaveRecord.EmailReportDetail[0].HearingTime;

                    string webRootPath = webHostEnvironment.WebRootPath;
                    string uploadsFolder = Path.Combine(webRootPath, "Documents", "Report", "EmailPDF");
                    var filename = $"Hearing_{DateTime.Now:yyyyMMdd_hhmm}_{ClssaveRecord.EmailReportDetail[0].AppID}" + ".pdf";
                    // List<MemoryStream> _FileStrem = new List<MemoryStream> { GenerateStreamFromString(SendToHearingEmailPDF(EmailReportDetail)) };
                    //if (ClssaveRecord.ErrorCode == 0)
                    //{
                    //    var test = SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                    //}
                    List<Attachments> AttachedFile = new List<Attachments>();
                    if (ClssaveRecord.EmailReportDetail[0].AppID != null && ClssaveRecord.EmailReportDetail[0].AppID != "")
                    {
                        MemoryStream ms1 = new MemoryStream();
                    string[] rr1 = new string[2000000];
                    rr1 = SendToHearingEmailPDF(EmailReportDetail);
                    byte[] bytes = System.Convert.FromBase64String(rr1[1]);
                    //Stream stream = new MemoryStream(bytes);

                    string filePath = Path.Combine("wwwroot", "Documents", "Report", "EmailPDF");
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
                                    //var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.ApplicantMailDetail[i].Email.ToString() }, ClssaveRecord.ApplicantMailDetail[i].Subject, stream, ClssaveRecord.ApplicantMailDetail[i].Body);
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
                                    //var test = _emailSender.SendEmailAsync1(new List<string> { ClssaveRecord.EshtablishmentMailDetail[v].Email.ToString() }, ClssaveRecord.EshtablishmentMailDetail[v].Subject, stream, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                                    //var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[v].Email, ClssaveRecord.EshtablishmentMailDetail[v].Subject, ClssaveRecord.EshtablishmentMailDetail[v].Body);
                                }
                            }
                        }
                    }

                    //var fullPath = filePath + "//" + filename;

                    //if (System.IO.File.Exists(fullPath))
                    //{
                    //    System.IO.File.Delete(@"D:\Project\COL_Code\FTS_Web\wwwroot\Documents\Reinstatement\"+ filename);

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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "SaveReinstatementHearingACOLRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(20000000)]
        public IActionResult SaveReinstatementACOLSettlementrecord(IFormFile MyUploader, int ApplicationID, string SettlementDate, string SettlementNote,
            string URL, int HearingReasonID, string OrderOutwardDate, int OrderOutwardNo)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            try
            {
                if (_ID != null && _ID != 0)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Documents", "Reinstatement", "Settlement");
                    string filePath = uploadsFolder;
                    string FileName = "";
                    bool output = false;

                    ReinstatementAppliactionModel ClssaveRecord = new ReinstatementAppliactionModel();
                    ClssaveRecord.ApplicationID = ApplicationID;
                    ClssaveRecord.fileName = FileName;
                    ClssaveRecord.URL = URL;
                    ClssaveRecord.UserID = Convert.ToInt32(_ID);
                    ClssaveRecord.UserMode = Convert.ToInt32(_UserMode);
                    ClssaveRecord.IP_Address = IP;
                    ClssaveRecord.UPSettlementDate = SettlementDate;
                    ClssaveRecord.SettlementNote = SettlementNote;
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
                        //string FileName = MyUploader[i].FileName;
                        ClssaveRecord.fileName = FileName;

                         output = _FileUpload.UploadDocument(MyUploader, FileName, filePath);
                        ClssaveRecord = _Reinstatementpository.SaveReinstatementACOLSettlementrecord(ClssaveRecord);
                    }
                    else
                    {
                        ClssaveRecord = _Reinstatementpository.SaveReinstatementACOLSettlementrecord(ClssaveRecord);
                    }
                    //if (ClssaveRecord.ErrorCode == 0)
                    //{

                    //    var test = SendMailApplicant(ClssaveRecord.EMailIDList, ClssaveRecord.EmailSubject, ClssaveRecord.EmailBody);
                    //}

                    if (ClssaveRecord.ErrorCode == 0)
                    {

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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "SaveReinstatementACOLSettlementrecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
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
                if (_ID != null && _ID != 0)
                {

                    if (FileName == null)
                        return Content("filename not present");

                    //var path = "https://localhost:44314/images/logo.png";
                    var path = Path.Combine("wwwroot", "Documents", "Reinstatement", "Settlement", FileName);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "DownloadSettlementFile", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public JsonResult SaveReinstatementSendtolabourACOLRecord(ReinstatementAppliactionModel Obj)
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
                    ReinstatementAppliactionModel ClssaveRecord = new ReinstatementAppliactionModel();
                    ClssaveRecord = _Reinstatementpository.SaveReinstatementSendtolabourACOLRecord(Obj);
                    EmailReportModel EmailReportDetail = new EmailReportModel();
                    EmailReportDetail.DictrictACLOffice = ClssaveRecord.EmailReportDetail[0].DictrictACLOffice;
                    EmailReportDetail.AppID = ClssaveRecord.EmailReportDetail[0].AppID;
                    EmailReportDetail.ApplicantDetail = ClssaveRecord.EmailReportDetail[0].ApplicantDetail;
                    EmailReportDetail.Establishmentdetail = ClssaveRecord.EmailReportDetail[0].Establishmentdetail;
                    EmailReportDetail.ACLName = ClssaveRecord.EmailReportDetail[0].ACLName;
                    EmailReportDetail.LCAddress = ClssaveRecord.EmailReportDetail[0].LCAddress;
                    EmailReportDetail.Date = ClssaveRecord.EmailReportDetail[0].Date;
                    EmailReportDetail.SendLCDate = ClssaveRecord.EmailReportDetail[0].SendLCDate;

                    string webRootPath = webHostEnvironment.WebRootPath;
                    string uploadsFolder = Path.Combine(webRootPath, "Documents", "Report", "EmailPDF");

                    MemoryStream ms1 = new MemoryStream();
                    string[] rr1 = new string[2000000];
                    rr1 = SendToLabourEmailPDF(EmailReportDetail);
                    byte[] bytes = System.Convert.FromBase64String(rr1[1]);
                   
                    Stream stream = new MemoryStream(bytes);

                    string filePath = Path.Combine("wwwroot", "Documents", "Report", "EmailPDF");
                    var filename = $"labour_{DateTime.Now:yyyyMMdd_hhmm}_{ClssaveRecord.EmailReportDetail[0].AppID}" + ".pdf";
                    System.IO.File.WriteAllBytes(filePath + "\\" + filename, bytes);

                    List<Attachments> AttachedFile = new List<Attachments>();
                    AttachedFile.Add(new Attachments() { FileName = uploadsFolder + "\\" + filename });
                    ClssaveRecord.fileName = filename;

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

                                    var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.ApplicantMailDetail[i].Email.ToString() }, ClssaveRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClssaveRecord.ApplicantMailDetail[i].Body);
                                    // var test = _emailSender.SendEmailAsync(new List<string> { ClssaveRecord.ApplicantMailDetail[i].Email.ToString() }, ClssaveRecord.ApplicantMailDetail[i].Subject, AttachedFile, ClssaveRecord.ApplicantMailDetail[i].Body);
                                    //var test = SendMailApplicant(ClssaveRecord.EshtablishmentMailDetail[i].Email, ClssaveRecord.EshtablishmentMailDetail[i].Subject, ClssaveRecord.EshtablishmentMailDetail[i].Body);

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
                   
                    //FileInfo file = new FileInfo(deletefilepath1);
                    //if (file.Exists)//check file exsit or not  
                    //{
                    //    //file.Delete();
                    //}
                    return Json(new { data = ClssaveRecord});
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "SaveReinstatementSendtolabourACOLRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public IActionResult ReinstatementHistory(ReinstatementAppliactionModel Obj)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    Obj.UserID = Convert.ToInt32(_ID);
                    ReinstatementAppliactionModel ClssaveRecord = new ReinstatementAppliactionModel();
                    ClssaveRecord = _Reinstatementpository.ReinstatementHistory(Obj);
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
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "ReinstatementHistory", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public async Task<IActionResult> GetReport(int id)
        {
            try
            {
                string mimtype = "";
                int extension = 1;
                string webRootPath = webHostEnvironment.WebRootPath;
                var path = Path.Combine("wwwroot", "Report", "Reintatment.rdlc");
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                ReinstatementAppliactionModel ClssaveRecord = new ReinstatementAppliactionModel();
                ReinstatementAppliactionModel test = new ReinstatementAppliactionModel();
                test.ApplicationID = id;
                ClssaveRecord = _Reinstatementpository.ReinstatementHistory(test);
                LocalReport localreport = new LocalReport(path);
                localreport.AddDataSource("DataSet1", ClssaveRecord.basicdetailtlst);
                localreport.AddDataSource("DataSet2", ClssaveRecord.establishdetaillst);
                var result = localreport.Execute(RenderType.Pdf, extension, parameters, mimtype);
                return File(result.MainStream, "application/pdf");
            }
            catch (Exception ex)
            {
                _logger.Error("", ex);
                _logger.Log(ex.Message);
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "ReinstatementHistory", 1, 1, "");
                return StatusCode(500, ex.Message);
            }
        }

        public Report report = new Report();

        public string[] SendToLabourEmailPDF(EmailReportModel EmailReportDetail)
        {

            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder

            DataSet ds = new DataSet();
            DataTable Dt1 = ds.Tables.Add("Table1");

            // create fields
            Dt1.Columns.Add("DictrictACLOffice", typeof(string));
            Dt1.Columns.Add("AppID", typeof(string));
            Dt1.Columns.Add("ApplicantDetail", typeof(string));
            Dt1.Columns.Add("Establishmentdetail", typeof(string));
            Dt1.Columns.Add("ACLName", typeof(string));
            Dt1.Columns.Add("LCAddress", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("SendLCDate", typeof(string));

            //insert row values
            Dt1.Rows.Add(new Object[]{
                 EmailReportDetail.DictrictACLOffice,
                 EmailReportDetail.AppID,
                 EmailReportDetail.ApplicantDetail,
                 EmailReportDetail.Establishmentdetail,
                 EmailReportDetail.ACLName,
                 EmailReportDetail.LCAddress,
                 EmailReportDetail.Date,
                 EmailReportDetail.SendLCDate
            });

            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/reinsendtolabour.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/reinsendtolabour.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            report.Report.Load(webRootPath + "/Documents/Report/reinsendtolabour1.frx");

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

        public IActionResult SendToLabourEmailPDF1()
        {
            //FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/shruti.ttf");
            //FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/Lohit_Gujarati.ttf");
            //FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/Darks_Calibri_Remix.ttf");
            //FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/Nirmala.ttf");
            //FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/arial-unicode-ms.ttf");
            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder

            DataSet ds = new DataSet();
            DataTable Dt1 = ds.Tables.Add("Table1");

            // create fields
            Dt1.Columns.Add("DictrictACLOffice", typeof(string));
            Dt1.Columns.Add("AppID", typeof(string));
            Dt1.Columns.Add("ApplicantDetail", typeof(string));
            Dt1.Columns.Add("Establishmentdetail", typeof(string));
            Dt1.Columns.Add("ACLName", typeof(string));
            Dt1.Columns.Add("LCAddress", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("SendLCDate", typeof(string));

            // insert row values
            // Dt1.Rows.Add(new Object[]{
            //     EmailReportDetail.DictrictACLOffice,
            //     EmailReportDetail.AppID,
            //     EmailReportDetail.ApplicantDetail,
            //     EmailReportDetail.Establishmentdetail,
            //     EmailReportDetail.ACLName,
            //     EmailReportDetail.LCAddress,
            //     EmailReportDetail.Date,
            //     EmailReportDetail.SendLCDate
            //});

            // insert row values
            Dt1.Rows.Add(new Object[]{
                 "હુકમની નકલ વિવાદ ના પક્ષકારોને સીધી મોકલવામાં આવી છે,",
                 "આ કચેરી પાસેના વિવાદને લગતા કાગળોની અસલ ફાઈલ આ સાથે બીડેલ છે.",
                 "આ કચેરી પાસેના વિવાદને લગતા કાગળોની અસલ ફાઈલ આ સાથે બીડેલ છે.",
                 "જે કામ પુરૂ થયે આ કચેરીને પરત મોકલવા વિનંતી છે.",
                 "જે કામ પુરૂ થયે આ કચેરીને પરત મોકલવા વિનંતી છે.",
                 "Test Chirag",
                 "Test Chirag",
                 "Test Chirag"

            });

            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/reinsendtolabour.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/reinsendtolabour.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            report.Report.Load(webRootPath + "/Documents/Report/reinsendtolabour1.frx");
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

        public IActionResult test()
        {
            try
            {
                FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/shruti.ttf");
                FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/Lohit_Gujarati.ttf");
                FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/Darks_Calibri_Remix.ttf");
                FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/Nirmala.ttf");
                FastReport.Utils.Config.WebMode = true;
                string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder

                DataSet ds = new DataSet();
                DataTable Dt1 = ds.Tables.Add("Table1");

                // create fields
                Dt1.Columns.Add("DictrictACLOffice", typeof(string));
                Dt1.Columns.Add("AppID", typeof(string));
                Dt1.Columns.Add("ApplicantDetail", typeof(string));
                Dt1.Columns.Add("Establishmentdetail", typeof(string));
                Dt1.Columns.Add("ACLName", typeof(string));
                Dt1.Columns.Add("LCAddress", typeof(string));
                Dt1.Columns.Add("Date", typeof(string));
                Dt1.Columns.Add("SendLCDate", typeof(string));

                // insert row values
                // Dt1.Rows.Add(new Object[]{
                //     EmailReportDetail.DictrictACLOffice,
                //     EmailReportDetail.AppID,
                //     EmailReportDetail.ApplicantDetail,
                //     EmailReportDetail.Establishmentdetail,
                //     EmailReportDetail.ACLName,
                //     EmailReportDetail.LCAddress,
                //     EmailReportDetail.Date,
                //     EmailReportDetail.SendLCDate
                //});

                // insert row values
                Dt1.Rows.Add(new Object[]{
                 "હુકમની નકલ વિવાદ ના પક્ષકારોને સીધી મોકલવામાં આવી છે,",
                 "આ કચેરી પાસેના વિવાદને લગતા કાગળોની અસલ ફાઈલ આ સાથે બીડેલ છે.",
                 "આ કચેરી પાસેના વિવાદને લગતા કાગળોની અસલ ફાઈલ આ સાથે બીડેલ છે.",
                 "જે કામ પુરૂ થયે આ કચેરીને પરત મોકલવા વિનંતી છે.",
                 "જે કામ પુરૂ થયે આ કચેરીને પરત મોકલવા વિનંતી છે.",
                 "Test Chirag",
                 "Test Chirag",
                 "Test Chirag"
            });

                //ds.WriteXmlSchema(webRootPath + "/Documents/Report/reinsendtolabour.xsd");
                //ds.WriteXmlSchema(webRootPath + "/Documents/Report/reinsendtolabour.xml");
                report.Report.RegisterData(ds.Tables[0], "Table1");
                report.Report.Load(webRootPath + "/Documents/Report/reinsendtolabour1.frx");
                //report.Report.Load(path);
                report.Prepare();

                HTMLExport export = new HTMLExport();
                export.Layers = true;
                using (MemoryStream ms = new MemoryStream())
                {
                    export.EmbedPictures = true;
                    export.Export(report, ms);
                    ms.Flush();
                    ViewData["Report"] = Encoding.UTF8.GetString(ms.ToArray());
                    ViewData["ReportName"] = "reinsendtolabour1.frx";
                }
                return View("test");
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "test", 1, 1, "");
                return StatusCode(500, ex.Message);
            }
        }

        //public IActionResult SendToHearingEmailPDF()
        public string[] SendToHearingEmailPDF(EmailReportModel EmailReportDetail)
        {
            FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/shruti.ttf");
            FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/Lohit_Gujarati.ttf");
            FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/Darks_Calibri_Remix.ttf");
            FastReport.Utils.Config.PrivateFontCollection.AddFontFile("wwwroot/fonts/Nirmala.ttf");
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
            Dt1.Columns.Add("ACLName", typeof(string));
            Dt1.Columns.Add("DictrictACL", typeof(string));
            Dt1.Columns.Add("Date", typeof(string));
            Dt1.Columns.Add("HearingDate", typeof(string));
            Dt1.Columns.Add("HearingTime", typeof(string));

            //insert row values
            Dt1.Rows.Add(new Object[]{
                 EmailReportDetail.DictrictACLOffice,
                 EmailReportDetail.AppID,
                 EmailReportDetail.ApplicantDetail,
                 EmailReportDetail.Establishmentdetail,
                 EmailReportDetail.ACLName,
                 EmailReportDetail.DictrictACL,
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
            //      "AHMZ1 AHMZ1",
            //     "અમરેલી",
            //     "28-10-2022",
            //     "28-10-2022",
            //     "02:00"

            //});

            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/reinhearing.xsd");
            //ds.WriteXmlSchema(webRootPath + "/Documents/Report/reinhearing.xml");
            report.Report.RegisterData(ds.Tables[0], "Table1");
            report.Report.Load(webRootPath + "/Documents/Report/reinhearing.frx");
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
                //ms.Flush();
                //return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension("Simple List") + ".pdf");
            }


            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        export.Export(report, ms);
            //        ms.Flush();
            //        return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension("Simple List") + ".pdf");
            //    }

        }
    }

}
