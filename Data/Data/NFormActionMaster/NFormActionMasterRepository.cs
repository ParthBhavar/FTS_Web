using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.NFormActionMaster
{
    public class NFormActionMasterRepository : INFormActionMasterRepository
    {
        #region Private Variables
        private readonly IRepository<NFormActionMasterModel> _nformactionRepository;
        #endregion

        #region Constructor
        public NFormActionMasterRepository(IRepository<NFormActionMasterModel> nformactionRepository)
        {
            _nformactionRepository = nformactionRepository;

        }
        #endregion

        public List<NFormActionMasterModel> NFormActionList()
        {
            List<NFormActionMasterModel> lstNFormActionMaster = new List<NFormActionMasterModel>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _nformactionRepository.QueryMultipleByProcedure(SPConstants.GetListNFormActionMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstNFormActionMaster = result1.Select(x => new NFormActionMasterModel
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
            return lstNFormActionMaster;
        }
        public NFormActionMasterModel NFormActionRecord(int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            var keyValuePairs = _nformactionRepository.QueryMultipleByProcedure(SPConstants.GetRecordNFormActionMaster, param);
            var response = new NFormActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormActionMasterModel
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
        public NFormActionMasterModel SaveNFormActionRecord(NFormActionMasterModel ObjAction)
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
            var keyValuePairs = _nformactionRepository.QueryMultipleByProcedure(SPConstants.UpdateNFormActionMaster, param);
            var response = new NFormActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,


                }).FirstOrDefault();
            };
            return response;
        }
        public NFormActionMasterModel DeleteNFormActionRecord(int UserID, int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _nformactionRepository.QueryMultipleByProcedure(SPConstants.DeleteNFormActionMaster, param);
            var response = new NFormActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                }).FirstOrDefault();
            };
            return response;
        }
    }
}
