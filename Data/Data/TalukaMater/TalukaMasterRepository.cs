using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.TalukaMater
{
    public class TalukaMasterRepository : ITalukaMasterRepository
    {
        #region Private Variables
        private readonly IRepository<TalukaMasterModel> _talukaRepository;
        #endregion

        #region Constructor
        public TalukaMasterRepository(IRepository<TalukaMasterModel> talukaRepository)
        {
            _talukaRepository = talukaRepository;

        }
        #endregion

        public List<TalukaMasterModel> TalukaList()
        {
          try
            {
                List<TalukaMasterModel> lstTalukaMaster = new List<TalukaMasterModel>();
                DynamicParameters param = new DynamicParameters();


                var keyValuePairs = _talukaRepository.QueryMultipleByProcedure(SPConstants.GetListTalukaMaster, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstTalukaMaster = result1.Select(x => new TalukaMasterModel
                    {
                        TalukaID = (int)x.TalukaId,
                        TalukaCode = (string)x.TalukaCode,
                        TalukaName = (string)x.TalukaName,
                        DistrictID = (int)x.DistrictId,
                        IsActive = Convert.ToBoolean(x.IsActive),
                    }).ToList();
                };
                return lstTalukaMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TalukaMasterModel TalukaRecord(int TalukaId)
        {
          try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_TalukaId", TalukaId);
                var keyValuePairs = _talukaRepository.QueryMultipleByProcedure(SPConstants.GetRecordTalukamaster, param);
                var response = new TalukaMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new TalukaMasterModel
                    {
                        TalukaID = (int)x.TalukaId,
                        TalukaCode = (string)x.TalukaCode,
                        TalukaName = (string)x.TalukaName,
                        DistrictID = (int)x.DistrictId,
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

        public TalukaMasterModel SaveTalukaRecord(TalukaMasterModel ObjTaluka)
        {
          try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", ObjTaluka.UserID);
                param.Add("@p_TalukaId", ObjTaluka.TalukaID);
                param.Add("@p_TalukaCode", ObjTaluka.TalukaCode);
                param.Add("@p_TalukaName", ObjTaluka.TalukaName);
                param.Add("@p_DistrictId", ObjTaluka.DistrictID);
                param.Add("@p_IsActive", ObjTaluka.IsActive);
                var keyValuePairs = _talukaRepository.QueryMultipleByProcedure(SPConstants.UpdateTalukamaster, param);
                var response = new TalukaMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new TalukaMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,


                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TalukaMasterModel DeleteTalukaRecord(int UserID, int TalukaId)
        {
          try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_TalukaId", TalukaId);
                param.Add("@p_UserID", UserID);
                var keyValuePairs = _talukaRepository.QueryMultipleByProcedure(SPConstants.DeleteTalukaMaster, param);
                var response = new TalukaMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new TalukaMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
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
