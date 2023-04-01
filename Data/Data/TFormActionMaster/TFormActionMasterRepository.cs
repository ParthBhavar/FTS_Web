using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.TFormActionMaster
{
    public class TFormActionMasterRepository : ITFormActionMasterRepository
    {

        #region Private Variables
        private readonly IRepository<TFormActionMasterModel> _tformactionRepository;
        #endregion

        #region Constructor
        public TFormActionMasterRepository(IRepository<TFormActionMasterModel> tformactionRepository)
        {
            _tformactionRepository = tformactionRepository;

        }
        #endregion
        public List<TFormActionMasterModel> TFormActionList()
        {
            List<TFormActionMasterModel> lstTFormActionMaster = new List<TFormActionMasterModel>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _tformactionRepository.QueryMultipleByProcedure(SPConstants.GetListTFormActionMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstTFormActionMaster = result1.Select(x => new TFormActionMasterModel
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
            return lstTFormActionMaster;
        }
        public TFormActionMasterModel TFormActionRecord(int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            var keyValuePairs = _tformactionRepository.QueryMultipleByProcedure(SPConstants.GetRecordTFormActionMaster, param);
            var response = new TFormActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormActionMasterModel
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
        public TFormActionMasterModel SaveTFormActionRecord(TFormActionMasterModel ObjTFormAction)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", ObjTFormAction.UserID);
            param.Add("@p_ActionID", ObjTFormAction.ActionID);
            param.Add("@p_ActionCode", ObjTFormAction.ActionCode);
            param.Add("@p_ActionName", ObjTFormAction.ActionName);
            param.Add("@p_Status", ObjTFormAction.Status);
            param.Add("@p_ActionText", ObjTFormAction.ActionText);
            param.Add("@p_PerformActionID", ObjTFormAction.PerformActionID);
            param.Add("@p_ForwardTo", ObjTFormAction.ForwardTo);
            param.Add("@p_ParentActionID", ObjTFormAction.ParentActionID);
            param.Add("@p_IsQuery", ObjTFormAction.IsQuery);
            param.Add("@p_IsActive", ObjTFormAction.IsActive);
            var keyValuePairs = _tformactionRepository.QueryMultipleByProcedure(SPConstants.UpdateTFormActionMaster, param);
            var response = new TFormActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,


                }).FirstOrDefault();
            };
            return response;
        }

        public TFormActionMasterModel DeleteTFormActionRecord(int UserID, int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _tformactionRepository.QueryMultipleByProcedure(SPConstants.DeleteTFormActionMaster, param);
            var response = new TFormActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                }).FirstOrDefault();
            };
            return response;
        }
    }
}
