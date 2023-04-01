using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.AreaMaster
{
    public class AreaMasterRepository : IAreaMasterRepository
    {
        #region Private Variables
        private readonly IRepository<AreaMasterModel> _areaRepository;
        #endregion

        #region Constructor
        public AreaMasterRepository(IRepository<AreaMasterModel> areaRepository)
        {
            _areaRepository = areaRepository;

        }
        #endregion

        public List<AreaMasterModel> AreaList()
        {

            List<AreaMasterModel> lstAreaMaster = new List<AreaMasterModel>();
            DynamicParameters param = new DynamicParameters();


            var keyValuePairs = _areaRepository.QueryMultipleByProcedure(SPConstants.GetListAreaMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstAreaMaster = result1.Select(x => new AreaMasterModel
                {
                    AreaID = (int)x.AreaID,
                    AreaName = (string)x.AreaName,
                    ZoneID = (int)x.ZoneID,
                    DistrictId = (int)x.DistrictId,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstAreaMaster;
        }
        public AreaMasterModel AreaRecord(int AreaID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_AreaID", AreaID);
            var keyValuePairs = _areaRepository.QueryMultipleByProcedure(SPConstants.GetRecordAreaMaster, param);
            var response = new AreaMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new AreaMasterModel
                {
                    AreaID = (int)x.AreaID,
                    AreaName = (string)x.AreaName,
                    ZoneID = (int)x.ZoneID,
                    DistrictId = (int)x.DistrictId,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }
        public AreaMasterModel SaveAreaRecord(AreaMasterModel ObjArea)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", ObjArea.UserID);
            param.Add("@p_AreaID", ObjArea.AreaID);
            param.Add("@p_AreaName", ObjArea.AreaName);
            param.Add("@p_ZoneID", ObjArea.ZoneID);
            param.Add("@p_DistrictId", ObjArea.DistrictId);
            param.Add("@p_IsActive", ObjArea.IsActive);
            var keyValuePairs = _areaRepository.QueryMultipleByProcedure(SPConstants.UpdateAreaMaster, param);
            var response = new AreaMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new AreaMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,


                }).FirstOrDefault();
            };
            return response;
        }

        public AreaMasterModel DeleteAreaRecord(int UserID, int AreaID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_AreaID", AreaID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _areaRepository.QueryMultipleByProcedure(SPConstants.DeleteAreaMaster, param);
            var response = new AreaMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new AreaMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                }).FirstOrDefault();
            };
            return response;
        }

    }
}
