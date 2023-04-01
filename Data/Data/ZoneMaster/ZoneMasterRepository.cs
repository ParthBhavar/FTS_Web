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

namespace FTS.Data.ZoneMaster
{
    public class ZoneMasterRepository : IZoneMasterRepository
    {

        #region Private Variables
        private readonly IRepository<ZoneMasterModel> _zoneRepository;
        #endregion

        #region Constructor
        public ZoneMasterRepository(IRepository<ZoneMasterModel> zoneRepository)
        {
            _zoneRepository = zoneRepository;

        }
        #endregion

        public List<ZoneMasterModel> ZoneList()
        {
            try
            {
                List<ZoneMasterModel> lstZoneMaster = new List<ZoneMasterModel>();
                DynamicParameters param = new DynamicParameters();

                var keyValuePairs = _zoneRepository.QueryMultipleByProcedure(SPConstants.GetListZoneMaster, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstZoneMaster = result1.Select(x => new ZoneMasterModel
                    {
                        ZoneID = (int)x.ZoneID,
                        ZoneName = (string)x.ZoneName,
                        IsActive = Convert.ToBoolean(x.IsActive),
                    }).ToList();
                };
                return lstZoneMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ZoneMasterModel ZoneRecord(int ZoneID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_ZoneID", ZoneID);
                var keyValuePairs = _zoneRepository.QueryMultipleByProcedure(SPConstants.GetRecordZonemaster, param);
                var response = new ZoneMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new ZoneMasterModel
                    {
                        ZoneID = (int)x.ZoneID,
                        ZoneName = (string)x.ZoneName,
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


        public ZoneMasterModel SaveZoneRecord(ZoneMasterModel ObjZone)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", 1);
                param.Add("@p_ZoneID", ObjZone.ZoneID);
                param.Add("@p_ZoneName", ObjZone.ZoneName);
                param.Add("@p_IsActive", ObjZone.IsActive);
                param.Add("@p_IsDeleted", ObjZone.IsDeleted);
                var keyValuePairs = _zoneRepository.QueryMultipleByProcedure(SPConstants.UpdateZonemaster, param);
                var response = new ZoneMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new ZoneMasterModel
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
        public ZoneMasterModel DeleteZoneRecord(int UserID, int ZoneID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_ZoneID", ZoneID);
                param.Add("@p_UserID", UserID);
                var keyValuePairs = _zoneRepository.QueryMultipleByProcedure(SPConstants.DeleteZoneMaster, param);
                var response = new ZoneMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new ZoneMasterModel
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
