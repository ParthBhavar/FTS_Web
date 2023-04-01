using FTS.Business.BranchMaster;
using FTS.Business.CommonList;
using FTS.Data.BranchMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Master.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FTS_Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class BranchMasterController : Controller
    {
        public IBranchMasterBl _BranchRepository;
        public ICommonListBI _Commompository;
        public BranchMasterController(IBranchMasterBl branchRepository, ICommonListBI commompository)
        {
            this._BranchRepository = branchRepository;
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
                    var List = _BranchRepository.BranchList();
                    if (List.Count > 0)
                    {
                        foreach (var item in List)
                        {
                            item.EncryptedId = Encrypt_Decrypt.Encrypt(item.BranchID.ToString());
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
                _Commompository.LogErrorintbl(ex, "BranchMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }
        public ActionResult AddBranchMaster(string id)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null && _ID != 0)
                {
                    int BranchID = 0;
                    if (id != null)
                    {
                        BranchID = Convert.ToInt32(Encrypt_Decrypt.Decrypt(id));
                    }

                    BranchMasterModel branchobj = new BranchMasterModel();
                    branchobj = _BranchRepository.GetBranchRecord(BranchID);
                    var BranchList = _Commompository.BranchList(BranchID, branchobj.DistrictID);
                    var Districtlist = _Commompository.DistrictList(BranchID);
                    branchobj.BranchIDEdit = BranchID;
                    branchobj.Districtlist = Districtlist;
                    branchobj.BranchList = BranchList;

                    return View("AddBranchMaster", branchobj);  
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "BranchMasterController", "Index", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return StatusCode(500, ex.Message);
            }
        }


        public JsonResult SaveBranchRecord(BranchMasterModel ObjBranch)
        {
            var _ID = HttpContext.Session.GetInt32("_ID");
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null)
                {
                BranchMasterModel branchobj = new BranchMasterModel();
                branchobj = _BranchRepository.SaveBranchRecord(ObjBranch);
                return Json(new { data = branchobj });
                }
                else
                {
                    RedirectToAction("Index", "Home");
                    return Json(new { data = "" });
                }
            }
            catch (Exception ex)
            {
                _Commompository.LogErrorintbl(ex, "BranchMasterController", "SaveBranchRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        public JsonResult DeleteBranchRecord(int BranchID)
        {
            var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var _ID = HttpContext.Session.GetInt32("_ID");
            var IP = heserver.AddressList[1].ToString();
            try
            {
                if (_ID != null)
                {
                int UserID = 1;
                BranchMasterModel ClsBundleBreak = new BranchMasterModel();
                ClsBundleBreak = _BranchRepository.DeleteBranchRecord(UserID, BranchID);
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
                _Commompository.LogErrorintbl(ex, "BranchMasterController", "DeleteBranchRecord", Convert.ToInt16(_UserMode), Convert.ToInt16(_ID), IP);
                return new JsonResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
    }
}
