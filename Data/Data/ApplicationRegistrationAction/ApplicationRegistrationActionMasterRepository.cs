using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ApplicationRegistrationAction
{
    public class ApplicationRegistrationActionMasterRepository : IApplicationRegistrationActionMasterRepository
    {
        #region Private Variables
        private readonly IRepository<ApplicationRegistrationActionMasterModel> _regappactionRepository;
        #endregion

        #region Constructor
        public ApplicationRegistrationActionMasterRepository(IRepository<ApplicationRegistrationActionMasterModel> regappactionRepository)
        {
            _regappactionRepository = regappactionRepository;
        }
        #endregion
        public List<ApplicationRegistrationActionMasterModel> ApplicationRegistratationActionList()
        {
            List<ApplicationRegistrationActionMasterModel> lstAppRegistrationActionMaster = new List<ApplicationRegistrationActionMasterModel>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _regappactionRepository.QueryMultipleByProcedure(SPConstants.GetListAppRegActionMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstAppRegistrationActionMaster = result1.Select(x => new ApplicationRegistrationActionMasterModel
                {
                    ActionID = (int)x.ActionID,
                    ActionCode = (int)x.ActionCode,
                    ActionName = (string)x.ActionName,
                    Status = (string)x.Status,
                    ActionText = (string)x.ActionText,
                    PerformActionID = (int)x.PerformActionID,
                    ForwardTo = (int)x.ForwardTo,
                    ParentActionID = (int)x.ParentActionID,
                    IsQuery = Convert.ToBoolean(x.IsQuery),
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstAppRegistrationActionMaster;
        }

        public ApplicationRegistrationActionMasterModel ApplicationRegistratationActionRecord(int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            var keyValuePairs = _regappactionRepository.QueryMultipleByProcedure(SPConstants.GetRecordAppRegActionMaster, param);
            var response = new ApplicationRegistrationActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationActionMasterModel
                {
                    ActionID = (int)x.ActionID,
                    ActionCode = (int)x.ActionCode,
                    ActionName = (string)x.ActionName,
                    Status = (string)x.Status,
                    ActionText = (string)x.ActionText,
                    PerformActionID = (int)x.PerformActionID,
                    ForwardTo = (int)x.ForwardTo,
                    ParentActionID = (int)x.ParentActionID,
                    IsQuery = Convert.ToBoolean(x.IsQuery),
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }

        public ApplicationRegistrationActionMasterModel SaveAppRegActionRecord(ApplicationRegistrationActionMasterModel ObjAction)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", ObjAction.UserID);
            param.Add("@p_ActionID", ObjAction.ActionID);
            param.Add("@p_ActionCode", ObjAction.ActionCode);
            param.Add("@p_ActionName", ObjAction.ActionName);
            param.Add("@p_Status", ObjAction.Status);
            param.Add("@p_ActionText", ObjAction.ActionText);
            param.Add("@p_PerformActionID", ObjAction.PerformActionID);
            param.Add("@p_ForwardTo", ObjAction.ForwardTo);
            param.Add("@p_ParentActionID", ObjAction.ParentActionID);
            param.Add("@p_IsQuery", ObjAction.IsQuery);
            param.Add("@p_IsActive", ObjAction.IsActive);
            var keyValuePairs = _regappactionRepository.QueryMultipleByProcedure(SPConstants.UpdateAppRegActionMaster, param);
            var response = new ApplicationRegistrationActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,


                }).FirstOrDefault();
            };
            return response;
        }
        public ApplicationRegistrationActionMasterModel DeleteAppREgActionRecord(int UserID, int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _regappactionRepository.QueryMultipleByProcedure(SPConstants.DeleteAppRegActionMaster, param);
            var response = new ApplicationRegistrationActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                }).FirstOrDefault();
            };
            return response;
        }
    }
}
