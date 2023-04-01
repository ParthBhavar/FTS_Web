using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.TradeUnionRegistrationMaster
{
    public interface ITradeUnionRegistrationMasterBl
    {
        List<TradeUnionRegistrationMasterModel> TradeUnionRegistrationList();
        TradeUnionRegistrationMasterModel TradeUnionRegistrationRecord(int TradunionID);
        TradeUnionRegistrationMasterModel SaveTradeUnionRegistrationRecord(TradeUnionRegistrationMasterModel ObjTradeUnionRegistration);
        TradeUnionRegistrationMasterModel DeleteTradeUnionRegistrationRecord(int UserID, int TradunionID);

    }
}
