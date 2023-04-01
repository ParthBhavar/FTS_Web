using FTS.Data.Common;
using FTS.Model.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.EmployeeLoginMaster
{
    public class EmployeeLoginMasterRepository : IEmployeeLoginMasterRepository
    {
      
            #region Private Variables
            public readonly IRepository<EmployeeMasterModel> _employeeloginRepository;
        #endregion

        #region Constructor
        public EmployeeLoginMasterRepository(IRepository<EmployeeMasterModel> employeeloginRepository)
            {
            _employeeloginRepository = employeeloginRepository;
            }
            #endregion
            public EmployeeMasterModel EmployeeLoginRecord(EmployeeMasterModel ObjReglogin)
            {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_EmailID", ObjReglogin.EmailID.Trim());
                param.Add("@p_IsOnchange", ObjReglogin.IsOnchange);
                param.Add("@p_PositionID", ObjReglogin.PositionID);
                //param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjReglogin.Password));
                param.Add("@p_Password", ObjReglogin.Password);

                var test = Encrypt_Decrypt.Encrypt(ObjReglogin.Password);

                var keyValuePairs = _employeeloginRepository.QueryMultipleByProcedure(SPConstants.EmployeeLogin, param);
                var response = new EmployeeMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new EmployeeMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
                        EmployeeID = (int)x.EmployeeID,
                        DesignationID = (int)x.DesignationID,
                        EmailID = (string)x.EmailID,
                        MobileNo = (string)x.MobileNo,
                        Gender = (int)x.Gender,
                        DOB = Convert.ToDateTime(x.DOB),
                        PositionID = (int)x.PositionID,
                        RegionID = (int)x.RegionID,
                        RoleID = (int)x.RoleID,
                        BranchID = (int)x.BranchID,
                        ZoneID = (int)x.ZoneID,
                        DistrictID = (int)x.DistrictID,
                        TalukaID = (int)x.TalukaID,
                        EmpPosID = (int)x.EmpPosID,
                        IsDashoardList = (int)x.IsDashoardList,
                        UserName = (string)x.UserName,
                        Islocked = (int)x.Islocked,
                        IsActive = Convert.ToBoolean(x.IsActive),

                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public EmployeeMasterModel ChangeEmployeePassword(EmployeeMasterModel ObjReglogin)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_EmployeeID", ObjReglogin.UserID);
                param.Add("@p_Password", ObjReglogin.Password);
                param.Add("@p_CPassword", ObjReglogin.CPassword);
                //param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjReglogin.Password));
                //param.Add("@p_CPassword", Encrypt_Decrypt.Encrypt(ObjReglogin.CPassword));
                var keyValuePairs = _employeeloginRepository.QueryMultipleByProcedure(SPConstants.changeEmployeepassword, param);
                var response = new EmployeeMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new EmployeeMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
                        // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeeMasterModel UpdateIslockedflage(EmployeeMasterModel Obj)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_EmployeeID", Obj.EmployeeID);
                param.Add("@p_Islocked", Obj.Islocked);
                param.Add("@p_LastLoginTime", Obj.LastLoginTime);
                param.Add("@p_IP_Address", Obj.IP_Address);
                //param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjReglogin.Password));
                //param.Add("@p_CPassword", Encrypt_Decrypt.Encrypt(ObjReglogin.CPassword));
                var keyValuePairs = _employeeloginRepository.QueryMultipleByProcedure(SPConstants.UpdateIslockedflage, param);
                var response = new EmployeeMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new EmployeeMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
                        // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MenuListModel> MenuList(EmployeeMasterModel Obj)
        {
            try
            {
                List<MenuListModel> lstMenu = new List<MenuListModel>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_EmployeeID", Obj.EmployeeID);
                param.Add("@p_EmpPosID", Obj.EmpPosID);
                param.Add("@p_PositionID", Obj.PositionID);
                param.Add("@p_UserMode", Obj.UserMode);
                var keyValuePairs = _employeeloginRepository.QueryMultipleByProcedure(SPConstants.GetMenuListing, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstMenu = result1.Select(x => new MenuListModel
                    {
                        MenuID = (int)x.MenuID,
                        MenuName = (string)x.MenuName,
                        PageName = (string)x.PageName,
                        PageURL = (string)x.PageURL,
                        Icon = (string)x.Icon,
                        ParentMenuId = (int)x.ParentMenuId,
                        EmployeeID = (int)x.EmployeeID,
                        EmpPosID = (int)x.EmpPosID,
                        ParentMenuName = (string)x.ParentMenuName,
                        IsAssign = (int)x.IsAssign,
                        //ID = (int)x.ID,

                    }).ToList();
                };
                return lstMenu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<UserPositionListModel> UserPositionList(EmployeeMasterModel Obj)
        {
            try
            {
                List<UserPositionListModel> lstUserPositionList = new List<UserPositionListModel>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_EmployeeID", Obj.EmployeeID);

                var keyValuePairs = _employeeloginRepository.QueryMultipleByProcedure(SPConstants.GetUserPositionList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstUserPositionList = result1.Select(x => new UserPositionListModel
                    {
                        EmployeeID = (int)x.EmployeeID,
                        EmpPosID = (int)x.EmpPosID,
                        PositionID = (int)x.PositionID,
                        PositionName = (string)x.PositionName,
                        v_IsDefaultPo = (int)x.v_IsDefaultPo,
                    }).ToList();
                };
                return lstUserPositionList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
