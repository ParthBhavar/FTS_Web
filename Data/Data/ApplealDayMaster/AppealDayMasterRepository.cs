using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ApplealDayMaster
{
    public class AppealDayMasterRepository : IAppealDayMasterRepository
    {
        #region Private Variables
        private readonly IRepository<AppealDayMasterModel> _appealdayRepository;
        #endregion

        #region Constructor
        public AppealDayMasterRepository(IRepository<AppealDayMasterModel> appealdayRepository)
        {
            _appealdayRepository = appealdayRepository;

        }
        #endregion

        public List<AppealDayMasterModel> AppealDayMasterList()
        {
            try
            {
                List<AppealDayMasterModel> lstAppealDayMaster = new List<AppealDayMasterModel>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _appealdayRepository.QueryMultipleByProcedure(SPConstants.GetAppealDayMasterList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstAppealDayMaster = result1.Select(x => new AppealDayMasterModel
                    {
                        ID = (int)x.ID,
                        Days = (int)x.Days,
                        Action = (string)x.Action,
                        IsActive = Convert.ToBoolean(x.IsActive),
                    }).ToList();
                };
                return lstAppealDayMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public AppealDayMasterModel GetAppealDayRecord(int ID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_ID", ID);
                var keyValuePairs = _appealdayRepository.QueryMultipleByProcedure(SPConstants.GetRecordAppealDayMaster, param);
                var response = new AppealDayMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new AppealDayMasterModel
                    {
                        ID = (int)x.ID,
                        Days = (int)x.Days,
                        Action = (string)x.Action,
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
        public AppealDayMasterModel SaveAppealDayRecord(AppealDayMasterModel ObjAppeal)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", 1);
                param.Add("@p_ID", ObjAppeal.ID);
                param.Add("@p_Days", ObjAppeal.Days);
                param.Add("@p_Action", ObjAppeal.Action);
                param.Add("@p_IsActive", ObjAppeal.IsActive);
                //param.Add("@p_IsDeleted", ObjBranch.IsDeleted);
                var keyValuePairs = _appealdayRepository.QueryMultipleByProcedure(SPConstants.AppealDayMasterAddEdit, param);
                var response = new AppealDayMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new AppealDayMasterModel
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
        public AppealDayMasterModel DeleteAppealDayRecord(int UserID, int ID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_ID", ID);
                param.Add("@p_UserID", UserID);
                var keyValuePairs = _appealdayRepository.QueryMultipleByProcedure(SPConstants.DeleteAppealDayMaster, param);
                var response = new AppealDayMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new AppealDayMasterModel
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