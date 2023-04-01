using FTS.Business.CommonList;
using FTS.Business.TypeOfBody;
using FTS.Model.Common;
using FTS.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class TypeOfBodyController : Controller
    {

        public ITypeOfBodyBl _TypeOfBodypository;
        public ICommonListBI _Commompository;

        public TypeOfBodyController(ITypeOfBodyBl _TypeOfBodypository, ICommonListBI commompository)
        {
            this._TypeOfBodypository = _TypeOfBodypository;
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
                    //int totalrecord = 0;
                    var List = _TypeOfBodypository.TypeOfBodyList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.TypeOfBodyID.ToString());
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
                _Commompository.LogErrorintbl(ex, "TypeOfBodyController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public ActionResult AddTypeOfBody(string typeofbodyid)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int TypeOfBodyID = 0;
                    if (typeofbodyid != null)
                    {
                        TypeOfBodyID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(typeofbodyid));
                    }
                    TypeOfBodyModel ClsBundleBreak = new TypeOfBodyModel();
                    ClsBundleBreak = _TypeOfBodypository.TypeOfBodyRecord(TypeOfBodyID);
                    ClsBundleBreak.TypeOfBodyIDEdit = TypeOfBodyID;
                    return View("AddTypeOfBody", ClsBundleBreak);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "TypeOfBodyController", "AddTypeOfBody", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }

        public JsonResult SaveTypeOfBodyRecord(TypeOfBodyModel Objtypeofbody)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    TypeOfBodyModel ClsBundleBreak = new TypeOfBodyModel();
                    ClsBundleBreak = _TypeOfBodypository.SaveTypeOfBodyRecord(Objtypeofbody);
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
                _Commompository.LogErrorintbl(ex, "TypeOfBodyController", "SaveTypeOfBodyRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }


        public JsonResult DeleteTypeOfBody(int TypeOfBodyID)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int UserID = 1;
                    TypeOfBodyModel ClsBundleBreak = new TypeOfBodyModel();
                    ClsBundleBreak = _TypeOfBodypository.DeleteTypeOfBody(TypeOfBodyID, UserID);
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
                _Commompository.LogErrorintbl(ex, "TypeOfBodyController", "DeleteRoleRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
