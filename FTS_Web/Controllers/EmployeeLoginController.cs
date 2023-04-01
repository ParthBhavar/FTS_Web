using FTS.Business.CommonList;
using FTS.Business.EmployeeLoginMaster;
using FTS.Model.Entities;
using DNTCaptcha.Core;
using Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class EmployeeLoginController : Controller
    {
        public IEmployeeLoginMasterBI _employeeloginRepository;
        const string SeID = "_ID";
        const string SeDesignationID = "_DesID";
        const string SeEmailID = "_EmailID";
        const string SeMobileNo = "_MoNo";
        const string SeGender = "_Gender";
        const string SePositionID = "_PositionID";
        const string SeRoleID = "_RoleID";
        const string SeRegionID = "_RegionID";
        const string SeBranchID = "_BranchID";
        const string SeZoneID = "_ZoneID";
        const string SeDistrictID = "_DistrictID";
        const string SeTalukaID = "_TalukaID";
        const string SeUserMode = "_UserMode";
        const string SeEmpPosID = "_EmpPosID";
        const string SeIP = "_IP";
        const string SeIsDashoardList = "_IsDashoardList";
        const string SeUserName = "_UserName";



        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _catchaOptions;
        public ICommonListBI _Commompository;

        public EmployeeLoginController(IEmployeeLoginMasterBI EmployeeLoginMasterRepository , IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> catchaOptions, IEmailSender emailSender, ICommonListBI commompository)
        {
            this._employeeloginRepository = EmployeeLoginMasterRepository;
            _validatorService = validatorService;
            _catchaOptions = catchaOptions == null ? throw new ArgumentNullException(nameof(catchaOptions)) : catchaOptions.Value;
            _Commompository = commompository;
        }
        public IActionResult Index()

        {

            return View();
        }

        public IActionResult ChangeEmployeePassword()
        {
            EmployeeMasterModel Employeeobj = new EmployeeMasterModel();
            return View("ChangeEmployeePassword", Employeeobj);
        }



        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Index(EmployeeMasterModel  ObjReglogin)
        {
            var IP = heserver.AddressList[1].ToString();


            try
            {
                ViewBag.wrongcaptcha = 0;
                        if (!ModelState.IsValid)
                    {
                        if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                        {
                        ViewBag.wrongcaptcha = 1;
                        this.ModelState.AddModelError(_catchaOptions.CaptchaComponent.CaptchaInputName, "Plese enter the security code as number");
                       
                            return View(ObjReglogin);
                        }
                    }
                    EmployeeMasterModel Employeeobj = new EmployeeMasterModel();
                    Employeeobj = _employeeloginRepository.EmployeeLoginRecord(ObjReglogin);

                if (Employeeobj.ErrorCode == 2 && Employeeobj.Islocked < 3)
                {

                    EmployeeMasterModel Empbj = new EmployeeMasterModel();
                    string LastLoginTime = $"{DateTime.Now}";
                    Empbj.IP_Address = IP;
                    Empbj.Islocked = Employeeobj.Islocked + 1;
                    Empbj.EmployeeID = Employeeobj.EmployeeID;
                    Empbj.LastLoginTime = Convert.ToDateTime(LastLoginTime);
                    _employeeloginRepository.UpdateIslockedflage(Empbj);
                }

                    if (Employeeobj.ErrorCode == 0)
                    {
                        HttpContext.Session.SetInt32("_ID", Employeeobj.EmployeeID);
                        HttpContext.Session.SetInt32("_DesID", Employeeobj.DesignationID);
                        HttpContext.Session.SetString("_EmailID", Employeeobj.EmailID);
                        HttpContext.Session.SetString("_MoNo", Employeeobj.MobileNo);
                        HttpContext.Session.SetInt32("_Gender", Employeeobj.Gender);
                        HttpContext.Session.SetInt32("_PositionID", Employeeobj.PositionID);
                        HttpContext.Session.SetInt32("_RoleID", Employeeobj.RoleID);
                        HttpContext.Session.SetInt32("_RegionID", Employeeobj.RegionID);
                        HttpContext.Session.SetInt32("_BranchID", Employeeobj.BranchID);
                        HttpContext.Session.SetInt32("_ZoneID", Employeeobj.ZoneID);
                        HttpContext.Session.SetInt32("_DistrictID", Employeeobj.DistrictID);
                        HttpContext.Session.SetInt32("_TalukaID", Employeeobj.TalukaID);
                        HttpContext.Session.SetInt32("_EmpPosID", Employeeobj.EmpPosID);
                        HttpContext.Session.SetInt32("_UserMode", 1);
                        HttpContext.Session.SetString("_IP", IP);
                        HttpContext.Session.SetInt32("_IsDashoardList", Employeeobj.IsDashoardList);
                        HttpContext.Session.SetString("_UserName", Employeeobj.UserName);
                        _Commompository.Officeloginlogout(Employeeobj.EmployeeID, "Login", IP);
                        _MenuListing();

                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ViewBag.errormessage = Employeeobj.ErrorMassage;
                        return View("Index");
                    }
            }
            catch (Exception ex)
            {

                _Commompository.LogErrorintbl(ex, "EmployeeLoginController", "Index", 0, 0, IP);
                return StatusCode(500, ex.Message);
            }

        }


        public JsonResult PostionOnChange(EmployeeMasterModel ObjReglogin)
        {
            var IP = heserver.AddressList[1].ToString();
            try
            {
                EmployeeMasterModel Employeeobj = new EmployeeMasterModel();
                Employeeobj = _employeeloginRepository.EmployeeLoginRecord(ObjReglogin);

                if (Employeeobj.ErrorCode == 0)
                {
                    HttpContext.Session.SetInt32("_ID", Employeeobj.EmployeeID);
                    HttpContext.Session.SetInt32("_DesID", Employeeobj.DesignationID);
                    HttpContext.Session.SetString("_EmailID", Employeeobj.EmailID);
                    HttpContext.Session.SetString("_MoNo", Employeeobj.MobileNo);
                    HttpContext.Session.SetInt32("_Gender", Employeeobj.Gender);
                    HttpContext.Session.SetInt32("_PositionID", Employeeobj.PositionID);
                    HttpContext.Session.SetInt32("_RoleID", Employeeobj.RoleID);
                    HttpContext.Session.SetInt32("_RegionID", Employeeobj.RegionID);
                    HttpContext.Session.SetInt32("_BranchID", Employeeobj.BranchID);
                    HttpContext.Session.SetInt32("_ZoneID", Employeeobj.ZoneID);
                    HttpContext.Session.SetInt32("_DistrictID", Employeeobj.DistrictID);
                    HttpContext.Session.SetInt32("_TalukaID", Employeeobj.TalukaID);
                    HttpContext.Session.SetInt32("_EmpPosID", Employeeobj.EmpPosID);
                    HttpContext.Session.SetInt32("_UserMode", 1);
                    HttpContext.Session.SetString("_IP", IP);
                    HttpContext.Session.SetInt32("_IsDashoardList", Employeeobj.IsDashoardList);
                    _MenuListing();
                    return Json(new { data = Employeeobj });
                }
                else
                {
                    ViewBag.errormessage = Employeeobj.ErrorMassage;
                    return Json(new { data = Employeeobj });
                }
            }
            catch (Exception ex)
            {

                _Commompository.LogErrorintbl(ex, "EmployeeLoginController", "Index", 0, 0, IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
            //ViewBag.errormessage = Employeeobj.ErrorMassage;
            //return Json(new { data = Employeeobj });


        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult ChangeEmployeePassword(EmployeeMasterModel ObjReglogin)
        //{
        //    var _ID = HttpContext.Session.GetInt32("_ID");
        //    var _UserMode = HttpContext.Session.GetInt32("_UserMode");
        //    var IP = heserver.AddressList[1].ToString();
        //    try
        //    {
        //        if (_ID != null && _ID != 0)
        //        {
        //            EmployeeMasterModel Employeeobj = new EmployeeMasterModel();
        //            ObjReglogin.UserID = Convert.ToInt32(_ID);
        //            Employeeobj = _employeeloginRepository.ChangeEmployeePassword(ObjReglogin);
        //            ViewBag.errormessage = Employeeobj.ErrorMassage;
        //            return View("Index");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _Commompository.LogErrorintbl(ex, "EmployeeLoginController", "ChangeEmployeePassword", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        public JsonResult ChangeEmployeePassword(EmployeeMasterModel ObjReglogin)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");

            EmployeeMasterModel Employeeobj = new EmployeeMasterModel();
            ObjReglogin.UserID = Convert.ToInt32(_ID);
            Employeeobj = _employeeloginRepository.ChangeEmployeePassword(ObjReglogin);
            ViewBag.errormessage = Employeeobj.ErrorMassage;
            return Json(new { data = Employeeobj });

        }


        public IActionResult _MenuListing()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();

            try
            {
                if (_ID != null && _ID != 0)
                {
                    var _EmpPosID = HttpContext.Session.GetInt32("_EmpPosID");
                var _PositionID = HttpContext.Session.GetInt32("_PositionID");
                //if (Session["Menus"] != null)
                //{
                //    List<win_mst> lsting = Session["Menus"] as List<win_mst>;
                //    return PartialView(lsting);
                //}

                EmployeeMasterModel Classget = new EmployeeMasterModel();
                Classget.EmployeeID = Convert.ToInt32(_ID);
                Classget.EmpPosID = Convert.ToInt32(_EmpPosID);
                Classget.UserMode = Convert.ToInt32(_UserMode);
                Classget.PositionID = Convert.ToInt32(_PositionID);
                List<MenuListModel> lst = _employeeloginRepository.MenuList(Classget);
                //return PartialView(lst);
                return PartialView(lst);
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return PartialView("");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EmployeeLoginController", "_MenuListing", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult _UserPositionListListing()
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            //var _EmpPosID = HttpContext.Session.GetInt32("_EmpPosID");
            //var _PositionID = HttpContext.Session.GetInt32("_PositionID");
            //var _PositionName = HttpContext.Session.GetString("_PositionName");

            //if (Session["Menus"] != null)
            //{
            //    List<win_mst> lsting = Session["Menus"] as List<win_mst>;
            //    return PartialView(lsting);
            //}

            //RefineSearch empmodel = new RefineSearch();
            //int empno = Convert.ToInt32(Session["Emp_No"]);
            //int deptid = Convert.ToInt32(Session["Dept_Id"]);
            ////empmodel.SearchText = Request.Form.GetValues("search[value]")[0]; 
            //List<win_mst> lst = _emplogrepository.GetMenus(empno, deptid);

            //Session["Menus"] = lst;


            try
            {

                if (_ID != null && _ID != 0)
                {
                    EmployeeMasterModel Classget = new EmployeeMasterModel();
                    Classget.EmployeeID = Convert.ToInt32(_ID);
                    //Classget.EmpPosID = Convert.ToInt32(_EmpPosID);
                    //Classget.PositionID = Convert.ToInt32(_PositionID);
                    //Classget.PositionName = Convert.ToString(_PositionName);
                    List<UserPositionListModel> lst = _employeeloginRepository.UserPositionList(Classget);

                    //return PartialView(lst);
                    return Json(new { data = lst });

                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "EmployeeLogin", "_UserPositionListListing", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

       

      
    }
}
