using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.DistrictMaster
{
    public class DistrictMasterRepository : IDistrictMasterRepository
    {
        #region Private Variables
        private readonly IRepository<DistrictMasterModel> _districtRepository;
        #endregion

        #region Constructor
        public DistrictMasterRepository(IRepository<DistrictMasterModel> districtRepository)
        {
            _districtRepository = districtRepository;

        }
        #endregion

        public List<DistrictMasterModel> DistrictList()
        {
            try
            {
                List<DistrictMasterModel> lstDistrictMaster = new List<DistrictMasterModel>();
                DynamicParameters param = new DynamicParameters();

                var keyValuePairs = _districtRepository.QueryMultipleByProcedure(SPConstants.GetListDistrictMaster, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstDistrictMaster = result1.Select(x => new DistrictMasterModel
                    {
                        DistrictID = (int)x.DistrictId,
                        DistrictName = (string)x.DistrictName,
                        IsActive = Convert.ToBoolean(x.IsActive),
                    }).ToList();
                };
                return lstDistrictMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DistrictMasterModel DistrictRecord(int DistrictId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_DistrictId", DistrictId);
                var keyValuePairs = _districtRepository.QueryMultipleByProcedure(SPConstants.GetRecordDistrictmaster, param);
                var response = new DistrictMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new DistrictMasterModel
                    {
                        DistrictID = (int)x.DistrictId,
                        DistrictName = (string)x.DistrictName,
                        IsActive = Convert.ToBoolean(x.IsActive),
                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DistrictMasterModel SaveDistrictRecord(DistrictMasterModel ObjDistrict)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", 1);
                param.Add("@p_DistrictId", ObjDistrict.DistrictID);
                param.Add("@p_DistrictName", ObjDistrict.DistrictName);
                param.Add("@p_IsActive", ObjDistrict.IsActive);
                param.Add("@p_IsDeleted", ObjDistrict.IsDeleted);
                var keyValuePairs = _districtRepository.QueryMultipleByProcedure(SPConstants.UpdateDistrictmaster, param);
                var response = new DistrictMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new DistrictMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
                        // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DistrictMasterModel DeleteDistrictRecord(int DistrictId,int UserID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_DistrictId", DistrictId);
                param.Add("@p_UserID", UserID);
                var keyValuePairs = _districtRepository.QueryMultipleByProcedure(SPConstants.DeleteDistrictMaster, param);
                var response = new DistrictMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new DistrictMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
                        // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
 

