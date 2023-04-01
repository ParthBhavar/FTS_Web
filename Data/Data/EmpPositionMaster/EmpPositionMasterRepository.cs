using FTS.Data.Common;
using FTS.Model.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.EmpPositionMaster
{
    public class EmpPositionMasterRepository : IEmpPositionMasterRepository
    {
        #region Private Variables
        private readonly IRepository<EmpPositionMasterModel> _emppositionRepository;
        #endregion

        #region Constructor
        public EmpPositionMasterRepository(IRepository<EmpPositionMasterModel> emppositionRepository)
        {
            _emppositionRepository = emppositionRepository;

        }
        #endregion

        public List<EmpPositionMasterModel> EmpPositionMasterList()
        {
            List<EmpPositionMasterModel> lstemppositionMaster = new List<EmpPositionMasterModel>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _emppositionRepository.QueryMultipleByProcedure(SPConstants.GetListEmpPositionMasterMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstemppositionMaster = result1.Select(x => new EmpPositionMasterModel
                {
                    EmpPosID = (int)x.EmpPosID,
                    PositionName = (string)x.PositionName,
                    EmployeeName = (string)x.EmployeeName,
                    StartDate = Convert.ToDateTime(x.StartDate),
                    EndDate = Convert.ToDateTime(x.EndDate),
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstemppositionMaster;
        }

        public EmpPositionMasterModel EmpPositionMasterRecord(int EmpPosID)
        {
            DynamicParameters param = new DynamicParameters();
             List<ReinstatementAppliactionModel> lstReinstatementApplicationdetail = new List<ReinstatementAppliactionModel>();
            List<EmpPositionMasterModel> lstMenu = new List<EmpPositionMasterModel>();
            param.Add("@p_EmpPosID", EmpPosID);
            var keyValuePairs = _emppositionRepository.QueryMultipleByProcedure(SPConstants.GetRecordEmpPositionMaster, param);
            var response = new EmpPositionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new EmpPositionMasterModel
                {
                    EmpPosID = (int)x.EmpPosID,
                    PositionID = (int)x.PositionID,
                    EmployeeID = (int)x.EmployeeID,
                    IsDashoardList = (int)x.IsDashoardList,
                    StartDate = Convert.ToDateTime(x.StartDate),
                    EndDate = Convert.ToDateTime(x.EndDate),
                    IsActive = Convert.ToBoolean(x.IsActive),
                    SetAsDefault = Convert.ToBoolean(x.SetAsDefault),
                    MobileNo = (string)x.MobileNo,
                    Email = (string)x.Email,


                }).FirstOrDefault();
            };

            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                lstMenu = result2.Select(x => new EmpPositionMasterModel
                {
                    DataValue = (int)x.MenuID,
                    ParentId = (int)x.ParentMenuId,
                    IsCheck = (int)x.IsCheck,
                    DisplayValue = (string)x.MenuName,


                }).ToList();
            };
            response.MenuList = lstMenu;
            return response;
        }
        public EmpPositionMasterModel SaveEmpPositionMasterRecord(EmpPositionMasterModel ObjEmpPosition)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", ObjEmpPosition.UserID);
            param.Add("@p_EmpPosID", ObjEmpPosition.EmpPosID);
            param.Add("@p_PositionID", ObjEmpPosition.PositionID);
            param.Add("@p_EmployeeID", ObjEmpPosition.EmployeeID);
            param.Add("@p_StartDate", ObjEmpPosition.UPStartDate);
            param.Add("@p_EndDate", ObjEmpPosition.UPEndDate);
            param.Add("@p_IsActive", ObjEmpPosition.IsActive);
            param.Add("@p_SetAsDefault", ObjEmpPosition.SetAsDefault);
            param.Add("@p_IsDashoardList", ObjEmpPosition.IsDashoardList);
            param.Add("@p_MobileNo", ObjEmpPosition.MobileNo);
            param.Add("@p_Email", ObjEmpPosition.Email);
            var keyValuePairs = _emppositionRepository.QueryMultipleByProcedure(SPConstants.UpdateEmpPositionMaster, param);
            var response = new EmpPositionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new EmpPositionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    EmpPosID = (int)x.EmpPosID,  
                    
                }).FirstOrDefault();
            };
            return response;
        }

        public EmpPositionMasterModel DeleteEmpPositionMasterRecord(int UserID, int EmpPosID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_EmpPosID", EmpPosID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _emppositionRepository.QueryMultipleByProcedure(SPConstants.DeleteEmpPositionMaster, param);
            var response = new EmpPositionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new EmpPositionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,                   

                }).FirstOrDefault();
            };
            return response;
        }



        public EmpPositionMasterModel SaveAssignPositionRecord(EmpPositionMasterModel ObjEmpPosition)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", ObjEmpPosition.UserID);
            param.Add("@p_XMLString", ObjEmpPosition.XMLString);
            param.Add("@p_EmployeeID", ObjEmpPosition.EmployeeID);
            param.Add("@P_EmpPosID", ObjEmpPosition.EmpPosID);
            param.Add("@P_PositionID", ObjEmpPosition.PositionID);
            var keyValuePairs = _emppositionRepository.QueryMultipleByProcedure(SPConstants.UpdateAssignMenuDetail, param);
            var response = new EmpPositionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new EmpPositionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    //ApplicationID = (int)x.ApplicationID,
                    //ISNew = (int)x.ISNew,
                    //isReqiredTradDetail = Convert.ToBoolean(x.isReqiredTradDetail),

                }).FirstOrDefault();
            };
            return response;
        }





    }
}
