using FTS.Business.CommonList;
using FTS.Business.RoleMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data;
using MySql.Data.MySqlClient;
//using AspNetCore.Reporting;
//using FastReport;
//using FastReport.Export.Html;
//using FastReport.Export.PdfSimple;
//using System.Text;
//using Microsoft.Extensions.Logging;
using Logger;
using FastReport;
using FastReport.Export.PdfSimple;
using FTS.Data.Common;
using FastReport.Export.Html;
using System.Text;
//using FastReport.Web;
//using NPOI.SS.Formula.Functions;
//using FTS.Data.Common;
//using FastReport.Web;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class RoleMasterController : Controller
    {

        public IRoleMasterBl _Rolepository;
        public ICommonListBI _Commompository;
        private readonly string _connectionString;
        private readonly IWebHostEnvironment webHostEnvironment;
        private ILogging _logger;

        public RoleMasterController(IRoleMasterBl _Rolepository, ICommonListBI commompository, IWebHostEnvironment webHostEnvironment , IConfiguration configuration, ILogging logger)
        {

            this._Rolepository = _Rolepository;
            _Commompository = commompository;
            this.webHostEnvironment = webHostEnvironment;
            _connectionString = configuration.GetConnectionString("DbConnection");
            _logger = logger;
        }
        public IDbConnection Connection => new MySqlConnection(_connectionString);
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());

       
        public IActionResult Index()
        {


            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {

                //if (_ID != null && _ID != 0)
                //{
                    PaginationRequest model = new PaginationRequest();
                    model.PageNumber = 1;
                    model.PageSize = 100;
                    model.SearchText = "";
                    int totalrecord = 0;
                    var List = _Rolepository.RoleList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.RoleID.ToString());
                        }
                    }
                //totalrecord = List[0].TotalRecord;
                _logger.Log("The main page has been accessed");
                return View(List);
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }
            catch (Exception ex)
            {
                _logger.Error("",ex);
                _logger.Log(ex.Message);
                _Commompository.LogErrorintbl(ex, "RoleMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost]
        //public JsonResult Index([DataSourceRequest] DataSourceRequest request, string Search)
        //{
        //    PaginationRequest model = new PaginationRequest();
        //    model.PageNumber = request.Page;
        //    model.PageSize = request.PageSize;
        //    model.SearchText = Search;
        //    int totalrecord = 0;
        //    var List = _Rolepository.RoleList(model);
        //    totalrecord = List[0].TotalRecord;
        //    return Json(new { data = List, TotalRecord = totalrecord });
        //}


        //public ActionResult AddRoleMaster()
        //{
        //    PaginationRequest model = new PaginationRequest();
        //    model.PageNumber = 1;
        //    model.PageSize = 100;
        //    model.SearchText = "";
        //    int totalrecord = 0;
        //    var List = _Rolepository.RoleList(model);
        //    totalrecord = List[0].TotalRecord;
        //    return Json(new { data = List, TotalRecord = totalrecord });
        //}


        public ActionResult AddRoleMaster(string roleid)
        {

          var _EmpID =   HttpContext.Session.GetInt32("_EmpID");
          var _DesID =   HttpContext.Session.GetInt32("_DesID");
          var _EmailID =   HttpContext.Session.GetInt32("_EmailID");
          var _MoNo =   HttpContext.Session.GetInt32("_MoNo");
          var _Gender =   HttpContext.Session.GetInt32("_Gender");
          var _PositionID =   HttpContext.Session.GetInt32("_PositionID");
          var _RegionID =   HttpContext.Session.GetInt32("_RegionID");
          var _BranchID =   HttpContext.Session.GetInt32("_BranchID");
          var _ZoneID =   HttpContext.Session.GetInt32("_ZoneID");
          var _DistrictID =   HttpContext.Session.GetInt32("_DistrictID");
          var _TalukaID =   HttpContext.Session.GetInt32("_TalukaID");
          var _UserMode =   HttpContext.Session.GetInt32("_UserMode");

            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();

            try
                {
                if (_ID != null && _ID != 0)
                {
                    int RoleID = 0;
                    if (roleid != null)
                    {
                        RoleID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(roleid));
                    }
                    MRoleMaster ClsRoleRecord = new MRoleMaster();
                    ClsRoleRecord = _Rolepository.RoleRecord(RoleID);
                    var PositionList = _Commompository.PositionList(RoleID);
                    ClsRoleRecord.Positionlist = PositionList;
                    ClsRoleRecord.RoleIDEdit = RoleID;
                    return View("AddRoleMaster", ClsRoleRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
               catch (Exception ex)
                {
                    _Commompository.LogErrorintbl(ex, "RoleMasterController", "AddRoleMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                    return StatusCode(500, ex.Message);
                }


        }

        public JsonResult SaveRoleRecord(MRoleMaster ObjROle)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    MRoleMaster ClssaveRecord = new MRoleMaster();
                    ClssaveRecord = _Rolepository.SaveRoleRecord(ObjROle);
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

        public JsonResult DeleteRoleRecord(string roleid)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int RoleID = 0;
                    if (roleid != null)
                    {
                        RoleID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(roleid));
                    }

                    int UserID = 1;
                    MRoleMaster Clsdeleterecord = new MRoleMaster();
                    Clsdeleterecord = _Rolepository.DeleteRoleRecord(UserID, RoleID);
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
                _Commompository.LogErrorintbl(ex, "RoleMasterController", "DeleteRoleRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


        //public WebReport GetReport()
        //{
        //    string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder
        //    WebReport webReport = new WebReport();
        //    //string connetion = "Server=10.10.0.42;User ID=developer;Password=D#v#l0p#r;Database=col;port=3306;Pooling=false;";
        //    MySqlConnection con = new MySqlConnection(Connection.ConnectionString);
        //    con.Open();
        //    MySqlCommand cmd = new MySqlCommand("SP_GetRecordConciliationApplicationHistory", con);
        //    cmd.Parameters.AddWithValue("p_ApplicationId", 1);
        //    cmd.Parameters.AddWithValue("p_UserID", 1);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    sda.Fill(ds);

        //    //cmd.ExecuteNonQuery();
        //    //con.Close();
        //    //FastReport.Utils.RegisteredObjects.AddConnection(typeof(MySqlDataConnection));
        //    //webReport.Report.Load(webRootPath + "/Report/Conciliation.frx");

        //    webReport.Report.Load(webRootPath + "/Report/test.frx");

        //    ds.WriteXmlSchema(webRootPath + "/Report/ApplicationHistory.xsd");
        //    webReport.Report.RegisterData(ds, Connection.ConnectionString); // Register the data source in the report
        //    /*webReport.Report.RegisterData(ds, Connection.ConnectionString); */// Register the data source in the report
        //                                                                        //ds.Tables[1].TableName = "Table";
        //                                                                        //ds.Tables[2].TableName = "Temp1";


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


        public IActionResult Pdf(int id)
        {
            FastReport.Utils.Config.WebMode = true;
            string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder
            MySqlConnection con = new MySqlConnection(Connection.ConnectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(SPConstants.GetRecordReinstatementApplicationHistory, con);
            cmd.Parameters.AddWithValue("p_ApplicationId", id);
            cmd.Parameters.AddWithValue("p_UserID", 1);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            report.Report.RegisterData(ds.Tables[2], "Table2");
            report.Report.Load(webRootPath + "/Documents/Report/Role.frx");
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


        public IActionResult SendToLabourEmailPDF1()
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


        public Report report = new Report();
        public IActionResult test(int id)
        {
            try
            {
                FastReport.Utils.Config.WebMode = true;
                string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder
                MySqlConnection con = new MySqlConnection(Connection.ConnectionString);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(SPConstants.GetRecordReinstatementApplicationHistory, con);
                cmd.Parameters.AddWithValue("p_ApplicationId", id);
                cmd.Parameters.AddWithValue("p_UserID", 1);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                //ds.WriteXmlSchema(webRootPath + "/Documents/Report/Role.xsd");
                //ds.WriteXmlSchema(webRootPath + "/Documents/Report/Role.xml");
                report.Report.RegisterData(ds.Tables[2], "Table2");
                report.Report.RegisterData(ds.Tables[1], "Table1");
                report.Report.Load(webRootPath + "/Documents/Report/Role.frx");
                report.Prepare();

                HTMLExport export = new HTMLExport();
                export.Layers = true;
                using (MemoryStream ms = new MemoryStream())
                {
                    export.EmbedPictures = true;
                    export.Export(report, ms);
                    ms.Flush();
                    ViewData["Report"] = Encoding.UTF8.GetString(ms.ToArray());
                    ViewData["ReportName"] = "Role.frx";
                }
                return View("test");
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "ReinstatementAppliactionController", "test", 1, 1, "");
                return StatusCode(500, ex.Message);
            }
        }

        //WebReport Report1 = new WebReport();
        //public IActionResult Index1()
        //{
        //    string webRootPath = webHostEnvironment.WebRootPath; // Get the path to the wwwroot folder
        //    Report1.Report.Load(webRootPath + "/Report/Conciliation.frx");

        //    var dataSet = new DataSet();

        //    Report1.Report.RegisterData(dataSet, "Table2");
        //    ViewBag.WebReport = Report1;
        //    return View("test");
        //}

    }
}



