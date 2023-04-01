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

namespace FTS.Data.RegionMaster
{
    public class RegionMasterRepository :IRegionMasterRepository
    {
        #region Private Variables
        private readonly IRepository<RegionMasterModel> _regionRepository;
        #endregion

        #region Constructor
        public RegionMasterRepository(IRepository<RegionMasterModel> regionRepository)
        {
            _regionRepository = regionRepository;

        }
        #endregion

        public List<RegionMasterModel> RegionList()
        {           
            List<RegionMasterModel> lstRegionMaster = new List<RegionMasterModel>();
            DynamicParameters param = new DynamicParameters();           
            var keyValuePairs = _regionRepository.QueryMultipleByProcedure(SPConstants.GetListRegionMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstRegionMaster = result1.Select(x => new RegionMasterModel
                {
                    RegionID = (int)x.RegionID,
                    RegionName = (string)x.RegionName,                   
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstRegionMaster;
        }
        public RegionMasterModel RegionRecord(int RegionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_RegionID", RegionID);
            var keyValuePairs = _regionRepository.QueryMultipleByProcedure(SPConstants.GetRecordRegionmaster, param);
            var response = new RegionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new RegionMasterModel
                {
                    RegionID = (int)x.RegionID,
                    RegionName = (string)x.RegionName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }

        public RegionMasterModel SaveRegionRecord(RegionMasterModel ObjRegion)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_RegionID", ObjRegion.RegionID);
            param.Add("@p_RegionName", ObjRegion.RegionName);
            param.Add("@p_IsActive", ObjRegion.IsActive);
            param.Add("@p_IsDeleted", ObjRegion.IsDeleted);
            var keyValuePairs = _regionRepository.QueryMultipleByProcedure(SPConstants.UpdateRegionmaster, param);
            var response = new RegionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new RegionMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    //IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public RegionMasterModel DeleteRegionRecord(int UserID, int RegionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_RegionID", RegionID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _regionRepository.QueryMultipleByProcedure(SPConstants.DeleteRegionMaster, param);
            var response = new RegionMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new RegionMasterModel
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
