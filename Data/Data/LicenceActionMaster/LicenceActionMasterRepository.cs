using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTS.Model.Entities;
using Dapper;
using FTS.Data.Common;

namespace FTS.Data.LicenceActionMaster  
{
    public class LicenceActionMasterRepository : ILicenceActionMasterRepository
    {
        #region Private Variables
        private readonly IRepository<LicenceActionMasterModel> _licenceRepository;
        #endregion

        #region Constructor
        public LicenceActionMasterRepository(IRepository<LicenceActionMasterModel> licenceRepository)
        {
            _licenceRepository = licenceRepository;
        }
        #endregion

        public List<LicenceActionMasterModel> licenceActionList()
        {
            List<LicenceActionMasterModel> lstlicenceActionMaster = new List<LicenceActionMasterModel>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _licenceRepository.QueryMultipleByProcedure(SPConstants.GetListLicenceActionMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstlicenceActionMaster = result1.Select(x => new LicenceActionMasterModel
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
            return lstlicenceActionMaster;
        }

        public LicenceActionMasterModel licenceActionRecord(int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            var keyValuePairs = _licenceRepository.QueryMultipleByProcedure(SPConstants.GetRecordLicenceActionMaster, param);
            var response = new LicenceActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceActionMasterModel
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
        public LicenceActionMasterModel SavelicenceActionRecord(LicenceActionMasterModel ObjAction)
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
            var keyValuePairs = _licenceRepository.QueryMultipleByProcedure(SPConstants.UpdateLicenceActionMaster, param);
            var response = new LicenceActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,


                }).FirstOrDefault();
            };
            return response;
        }

        public LicenceActionMasterModel DeletelicenceActionRecord(int UserID, int ActionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ActionID", ActionID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _licenceRepository.QueryMultipleByProcedure(SPConstants.DeleteLicenceActionMaster, param);
            var response = new LicenceActionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceActionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                }).FirstOrDefault();
            };
            return response;
        }
    }
}
