using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.PositionMaster
{
    public class PositionMasterRepository : IPositionMasterRepository
    {
        #region Private Variables
        private readonly IRepository<PositionMasterModel> _PositionRepository;
        #endregion

        #region Constructor
        public PositionMasterRepository(IRepository<PositionMasterModel> PositionRepository)
        {
            _PositionRepository = PositionRepository;

        }
        #endregion

        public List<PositionMasterModel> PositionList()
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<PositionMasterModel> lstPositionMaster = new List<PositionMasterModel>();
            DynamicParameters param = new DynamicParameters();

            //param.Add("@P_PageNumber", model.PageNumber);
            //param.Add("@P_PageSize", 100);
            //param.Add("@P_SearchText", model.SearchText);
            var keyValuePairs = _PositionRepository.QueryMultipleByProcedure(SPConstants.GetListPositionMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstPositionMaster = result1.Select(x => new PositionMasterModel
                {
                    PositionID = (int)x.PositionID,
                    PositionName = (string)x.PositionName,
                    //ParentPositionID = (int)x.ParentPositionID,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstPositionMaster;
        }

        public PositionMasterModel PositionRecord(int PositionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_PositionID", PositionID);
            var keyValuePairs = _PositionRepository.QueryMultipleByProcedure(SPConstants.GetRecoredPositionMaster, param);
            var response = new PositionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new PositionMasterModel
                {
                    PositionID = (int)x.PositionID,
                    RegionID = (int)x.RegionID,
                    RoleID = (int)x.RoleID,
                    BranchID = (int)x.BranchID,
                    ZoneID = (int)x.ZoneID,
                    PositionName = (string)x.PositionName,
                    TalukaID = (int)x.TalukaID,
                    DistrictID = (int)x.DistrictID,
                    ParentPositionID = (int)x.ParentPositionID,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }

        public PositionMasterModel SavePositionRecord(PositionMasterModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_PositionID", Obj.PositionID);
            param.Add("@p_RegionID", Obj.RegionID);
            param.Add("@p_RoleID", Obj.RoleID);
            param.Add("@p_BranchId", Obj.BranchID);
            param.Add("@p_ZoneId", Obj.ZoneID);
            param.Add("@p_DistrictID", Obj.DistrictID);
            param.Add("@p_TalukaID", Obj.TalukaID);
            param.Add("@p_PositionName", Obj.PositionName);
            param.Add("@p_ParentPositionID", Obj.ParentPositionID);
            param.Add("@p_IsActive", Obj.IsActive);
            var keyValuePairs = _PositionRepository.QueryMultipleByProcedure(SPConstants.UpdatePositionMaster, param);
            var response = new PositionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new PositionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public PositionMasterModel DeletePositionRecord(int UserID, int PositionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_PositionID", PositionID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _PositionRepository.QueryMultipleByProcedure(SPConstants.DeletePositionMaster, param);
            var response = new PositionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new PositionMasterModel  
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
    }
}
