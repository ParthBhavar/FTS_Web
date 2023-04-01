using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ConcilliationActionMaster
{
    public class ConcilliationActionMasterRepository : IConcilliationActionMasterRepository
    {
        #region Private Variables
        private readonly IRepository<ConcilliationActionMasterModel> _concilliationactionRepository;
        #endregion

        #region Constructor
        public ConcilliationActionMasterRepository(IRepository<ConcilliationActionMasterModel> concilliationactionRepository)
        {
            _concilliationactionRepository = concilliationactionRepository;

        }
        #endregion
        public List<ConcilliationActionMasterModel> ConcilliationActionList()
        {
            List<ConcilliationActionMasterModel> lstConcilliationActionMaster = new List<ConcilliationActionMasterModel>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _concilliationactionRepository.QueryMultipleByProcedure(SPConstants.GetListConcilliationActionMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstConcilliationActionMaster = result1.Select(x => new ConcilliationActionMasterModel
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
            return lstConcilliationActionMaster;
        }
        public ConcilliationActionMasterModel ConcilliationActionRecord(int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            var keyValuePairs = _concilliationactionRepository.QueryMultipleByProcedure(SPConstants.GetRecordConcilliationActionMaster, param);
            var response = new ConcilliationActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConcilliationActionMasterModel
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

        public ConcilliationActionMasterModel SaveConcilliationActionRecord(ConcilliationActionMasterModel ObjConcAction)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", ObjConcAction.UserID);
            param.Add("@p_ActionID", ObjConcAction.ActionID);
            param.Add("@p_ActionCode", ObjConcAction.ActionCode);
            param.Add("@p_ActionName", ObjConcAction.ActionName);
            param.Add("@p_Status", ObjConcAction.Status);
            param.Add("@p_ActionText", ObjConcAction.ActionText);
            param.Add("@p_PerformActionID", ObjConcAction.PerformActionID);
            param.Add("@p_ForwardTo", ObjConcAction.ForwardTo);
            param.Add("@p_ParentActionID", ObjConcAction.ParentActionID);
            param.Add("@p_IsQuery", ObjConcAction.IsQuery);
            param.Add("@p_IsActive", ObjConcAction.IsActive);
            var keyValuePairs = _concilliationactionRepository.QueryMultipleByProcedure(SPConstants.UpdateConcilliationActionMaster, param);
            var response = new ConcilliationActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConcilliationActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,


                }).FirstOrDefault();
            };
            return response;
        }

        public ConcilliationActionMasterModel DeleteConcilliationActionRecord(int UserID, int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _concilliationactionRepository.QueryMultipleByProcedure(SPConstants.DeleteConcilliationActionMaster, param);
            var response = new ConcilliationActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConcilliationActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                }).FirstOrDefault();
            };
            return response;
        }

    }
}
