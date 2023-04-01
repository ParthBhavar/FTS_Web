using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ReinstatementActionMaster
{
    public class ReinstatementActionMasterRepository : IReinstatementActionMasterRepository
    {
        #region Private Variables
        private readonly IRepository<ReinstatementActionMasterModel> _reinstactionRepository;
        #endregion

        #region Constructor
        public ReinstatementActionMasterRepository(IRepository<ReinstatementActionMasterModel> reinstactionRepository)
        {
            _reinstactionRepository = reinstactionRepository;

        }
        #endregion

        public List<ReinstatementActionMasterModel> ReinstatementActionList()
        {
            List<ReinstatementActionMasterModel> lstReinstatementActionMaster = new List<ReinstatementActionMasterModel>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _reinstactionRepository.QueryMultipleByProcedure(SPConstants.GetListReinstatementActionMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstReinstatementActionMaster = result1.Select(x => new ReinstatementActionMasterModel
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
            return lstReinstatementActionMaster;
        }
        public ReinstatementActionMasterModel ReinstatementActionRecord(int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            var keyValuePairs = _reinstactionRepository.QueryMultipleByProcedure(SPConstants.GetRecordReinstatementActionMaster, param);
            var response = new ReinstatementActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementActionMasterModel
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

        public ReinstatementActionMasterModel SaveReinstatementActionRecord(ReinstatementActionMasterModel ObjAction)
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
            var keyValuePairs = _reinstactionRepository.QueryMultipleByProcedure(SPConstants.UpdateReinstatementActionMaster, param);
            var response = new ReinstatementActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,


                }).FirstOrDefault();
            };
            return response;
        }

        public ReinstatementActionMasterModel DeleteReinstatementActionRecord(int UserID, int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _reinstactionRepository.QueryMultipleByProcedure(SPConstants.DeleteReinstatementActionMaster, param);
            var response = new ReinstatementActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                }).FirstOrDefault();
            };
            return response;
        }
    }
}
