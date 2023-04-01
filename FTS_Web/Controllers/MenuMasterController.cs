using FTS.Business.CommonList;
using FTS.Business.MenuMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class MenuMasterController : Controller
    {

        public IMenuMasterBI _MenuMasterRepository;
        public ICommonListBI _Commompository;

        public MenuMasterController(IMenuMasterBI _MenuMasterRepository, ICommonListBI commompository)
        {
            this._MenuMasterRepository = _MenuMasterRepository;
            _Commompository = commompository;
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
                    var List = _MenuMasterRepository.MenuList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.MenuId.ToString());
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
                _Commompository.LogErrorintbl(ex, "MenuMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }

        }
        //[HttpPost]

        //public JsonResult Index(int EmployeeID)
        //{
        //    var List = _EmployeeMasterRepository.EmployeeMasterList(EmployeeID);
        //    return Json(new { data = List });
        //}


        public ActionResult AddMenuMaster(string menuid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int MenuId = 0;
                    if (menuid != null)
                    {
                        MenuId = Convert.ToInt32(Encrypt_Decrypt.Decrypt(menuid));
                    }
                    MenuMasterModel ClsMenuMaster = new MenuMasterModel();
                    ClsMenuMaster = _MenuMasterRepository.MenuRecord(MenuId);
                    var MenuList = _Commompository.MenuList(MenuId);
                    ClsMenuMaster.MenuList = MenuList;
                    ClsMenuMaster.MenuIdEdit = MenuId;
                    //ClsBundleBreak.EmployeeIDEdit = EmployeeID;
                    return View("AddMenuMaster", ClsMenuMaster);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "MenuMasterController", "AddMenuMaster", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public JsonResult SaveMenuRecord(MenuMasterModel ObjMenu)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null)
                {
                    MenuMasterModel ClsBundleBreak = new MenuMasterModel();
                    ClsBundleBreak.UserID = Convert.ToInt32(_ID);

                    ClsBundleBreak = _MenuMasterRepository.SaveMenuRecord(ObjMenu);
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
                _Commompository.LogErrorintbl(ex, "MenuMasterController", "SaveMenuRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public JsonResult DeleteMenuRecord(int MenuId)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null)
                {
                    int UserID = 1;
                    MenuMasterModel ClsBundleBreak = new MenuMasterModel();
                    ClsBundleBreak = _MenuMasterRepository.DeleteMenuRecord(UserID, MenuId);
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
                _Commompository.LogErrorintbl(ex, "MenuMasterController", "DeleteMenuRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
