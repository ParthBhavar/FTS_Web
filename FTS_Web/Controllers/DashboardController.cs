using FTS.Business.CommonList;
using FTS.Business.DashBoard;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class DashboardController : Controller
    {
        public ICommonListBI _Commompository;
        public IDashBoardBI _DashBoardDetailRepository;

        public DashboardController(IDashBoardBI DashBoardDetailRepository, ICommonListBI commompository)
        {
            this._DashBoardDetailRepository = DashBoardDetailRepository;
            _Commompository = commompository;
        }
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());

        public IActionResult Index(DashBoardModel Objdash)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _DesID = HttpContext.Session.GetInt32("_DesID");
            var _PositionID = HttpContext.Session.GetInt32("_PositionID");
            var _RoleID = HttpContext.Session.GetInt32("_RoleID");
            var _RegionID = HttpContext.Session.GetInt32("_RegionID");
            var _BranchID = HttpContext.Session.GetInt32("_BranchID");
            var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
            var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
            var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
            var _EmpPosID = HttpContext.Session.GetInt32("_EmpPosID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _IsDashoardList = HttpContext.Session.GetInt32("_IsDashoardList");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    Objdash.UserID = Convert.ToInt32(_ID);
                    Objdash.DesignationID = Convert.ToInt32(_DesID);
                    Objdash.PositionID = Convert.ToInt32(_PositionID);
                    Objdash.RoleID = Convert.ToInt32(_RoleID);
                    Objdash.RegionID = Convert.ToInt32(_RegionID);
                    Objdash.BranchID = Convert.ToInt32(_BranchID);
                    Objdash.ZoneID = Convert.ToInt32(_ZoneID);
                    Objdash.DistrictID = Convert.ToInt32(_DistrictID);
                    Objdash.TalukaID = Convert.ToInt32(_TalukaID);
                    Objdash.EmpPosID = Convert.ToInt32(_EmpPosID);
                    Objdash.UserMode = Convert.ToInt32(_UserMode);
                    Objdash.IP_Address = IP;
                    Objdash.IsDashoardList = Convert.ToInt32(_IsDashoardList);

                    DashBoardModel ClssaveRecord = new DashBoardModel();
                    ClssaveRecord = _DashBoardDetailRepository.DashBoardDetail(Objdash);
                    return View(ClssaveRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                    
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "DashboardController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        public IActionResult CaseClimeDetail(DashBoardModel Objdash)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _DesID = HttpContext.Session.GetInt32("_DesID");
            var _PositionID = HttpContext.Session.GetInt32("_PositionID");
            var _RoleID = HttpContext.Session.GetInt32("_RoleID");
            var _RegionID = HttpContext.Session.GetInt32("_RegionID");
            var _BranchID = HttpContext.Session.GetInt32("_BranchID");
            var _ZoneID = HttpContext.Session.GetInt32("_ZoneID");
            var _DistrictID = HttpContext.Session.GetInt32("_DistrictID");
            var _TalukaID = HttpContext.Session.GetInt32("_TalukaID");
            var _EmpPosID = HttpContext.Session.GetInt32("_EmpPosID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _IsDashoardList = HttpContext.Session.GetInt32("_IsDashoardList");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    Objdash.UserID = Convert.ToInt32(_ID);
                    Objdash.DesignationID = Convert.ToInt32(_DesID);
                    Objdash.PositionID = Convert.ToInt32(_PositionID);
                    Objdash.RoleID = Convert.ToInt32(_RoleID);
                    Objdash.RegionID = Convert.ToInt32(_RegionID);
                    Objdash.BranchID = Convert.ToInt32(_BranchID);
                    Objdash.ZoneID = Convert.ToInt32(_ZoneID);
                    //Objdash.DistrictID = Convert.ToInt32(_DistrictID);
                    Objdash.TalukaID = Convert.ToInt32(_TalukaID);
                    Objdash.EmpPosID = Convert.ToInt32(_EmpPosID);
                    Objdash.UserMode = Convert.ToInt32(_UserMode);
                    Objdash.PositionDistrictID = Convert.ToInt32(_DistrictID);
                    Objdash.IP_Address = IP;
                    Objdash.IsDashoardList = Convert.ToInt32(_IsDashoardList);

                    DashBoardModel ClssaveRecord = new DashBoardModel();
                    ClssaveRecord = _DashBoardDetailRepository.CaseClimeDetail(Objdash);
                    ClssaveRecord.Districtlist = _Commompository.DistrictList(0);
                    return View("CaseClimeDetailForm", ClssaveRecord);
                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "DashboardController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

    }
}
