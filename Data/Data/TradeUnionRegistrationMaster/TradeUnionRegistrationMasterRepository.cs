using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.TradeUnionRegistrationMaster
{
    public class TradeUnionRegistrationMasterRepository : ITradeUnionRegistrationMasterRepository
    {

        #region Private Variables
        private readonly IRepository<TradeUnionRegistrationMasterModel> _tradeunionregistrationRepository;
        #endregion

        #region Constructor
        public TradeUnionRegistrationMasterRepository(IRepository<TradeUnionRegistrationMasterModel> tradeunionregistrationRepository)
        {
            _tradeunionregistrationRepository = tradeunionregistrationRepository;

        }
        #endregion

        public List<TradeUnionRegistrationMasterModel> TradeUnionRegistrationList()
        {
            List<TradeUnionRegistrationMasterModel> lstTradeUnionRegistrationMaster = new List<TradeUnionRegistrationMasterModel>();
            DynamicParameters param = new DynamicParameters();


            var keyValuePairs = _tradeunionregistrationRepository.QueryMultipleByProcedure(SPConstants.GetListTradeUnionRegistrationMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstTradeUnionRegistrationMaster = result1.Select(x => new TradeUnionRegistrationMasterModel
                {
                    TradunionID = (int)x.TradunionID,
                    RegistrationNo = (string)x.RegistrationNo,
                    RegistrationName = (string)x.RegistrationName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,                   
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    Pincode = (int)x.Pincode,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstTradeUnionRegistrationMaster;
        }

        public TradeUnionRegistrationMasterModel TradeUnionRegistrationRecord(int TradunionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_TradunionID", TradunionID);
            var keyValuePairs = _tradeunionregistrationRepository.QueryMultipleByProcedure(SPConstants.GetRecordTradenionRegistrationmaster, param);
            var response = new TradeUnionRegistrationMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TradeUnionRegistrationMasterModel
                {
                    TradunionID = (int)x.TradunionID,
                    RegistrationNo = (string)x.RegistrationNo,
                    RegistrationName = (string)x.RegistrationName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    TalukaID = (int)x.TalukaID,
                    DistrictID = (int)x.DistrictID,
                    Pincode = (int)x.Pincode,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }

        public TradeUnionRegistrationMasterModel SaveTradeUnionRegistrationRecord(TradeUnionRegistrationMasterModel ObjTradeUnionRegistration)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_TradunionID", ObjTradeUnionRegistration.TradunionID);
            param.Add("@p_RegistrationNo", ObjTradeUnionRegistration.RegistrationNo);
            param.Add("@p_RegistrationName", ObjTradeUnionRegistration.RegistrationName);
            param.Add("@p_PAddress", ObjTradeUnionRegistration.PAddress);
            param.Add("@p_SAddress", ObjTradeUnionRegistration.SAddress);
            param.Add("@p_TalukaID", ObjTradeUnionRegistration.TalukaID);
            param.Add("@p_DistrictID", ObjTradeUnionRegistration.DistrictID);            
            param.Add("@p_Pincode", ObjTradeUnionRegistration.Pincode);
            param.Add("@p_IsActive", ObjTradeUnionRegistration.IsActive);
            param.Add("@p_IsDeleted", ObjTradeUnionRegistration.IsDeleted);
            var keyValuePairs = _tradeunionregistrationRepository.QueryMultipleByProcedure(SPConstants.UpdateTradeUnionRegistrationmaster, param);
            var response = new TradeUnionRegistrationMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TradeUnionRegistrationMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public TradeUnionRegistrationMasterModel DeleteTradeUnionRegistrationRecord(int UserID, int TradunionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_TradunionID", TradunionID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _tradeunionregistrationRepository.QueryMultipleByProcedure(SPConstants.DeleteTradeUnionRegistrationMaster, param);
            var response = new TradeUnionRegistrationMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TradeUnionRegistrationMasterModel
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
