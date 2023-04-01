using FTS.Data.TradeUnionRegistrationMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.TradeUnionRegistrationMaster
{
    public class TradeUnionRegistrationMasterBl: ITradeUnionRegistrationMasterBl
    {
        private readonly ITradeUnionRegistrationMasterRepository _TradeUnionRegistrationMasterRepository;

        public TradeUnionRegistrationMasterBl(ITradeUnionRegistrationMasterRepository TradeUnionRegistrationMasterRepositor)
        {
            this._TradeUnionRegistrationMasterRepository = TradeUnionRegistrationMasterRepositor;
        }              

        public List<TradeUnionRegistrationMasterModel> TradeUnionRegistrationList()
        {
            return _TradeUnionRegistrationMasterRepository.TradeUnionRegistrationList();
        }

        public TradeUnionRegistrationMasterModel TradeUnionRegistrationRecord(int TradunionID)
        {
            var keyValuePairs = _TradeUnionRegistrationMasterRepository.TradeUnionRegistrationRecord(TradunionID);
            return keyValuePairs;
        }

        public TradeUnionRegistrationMasterModel SaveTradeUnionRegistrationRecord(TradeUnionRegistrationMasterModel ObjTradeUnionRegistration)
        {
            var keyValuePairs = _TradeUnionRegistrationMasterRepository.SaveTradeUnionRegistrationRecord(ObjTradeUnionRegistration);
            return keyValuePairs;
        }

        public TradeUnionRegistrationMasterModel DeleteTradeUnionRegistrationRecord(int UserID, int TradunionID)
        {
            var keyValuePairs = _TradeUnionRegistrationMasterRepository.DeleteTradeUnionRegistrationRecord(UserID, TradunionID);
            return keyValuePairs;
        }
    }

}


