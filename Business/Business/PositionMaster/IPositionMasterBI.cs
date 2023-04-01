using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.PositionMaster
{
    public interface IPositionMasterBI
    {
        public List<PositionMasterModel> PositionList();

        PositionMasterModel PositionRecord(int PositionID);

        PositionMasterModel SavePositionRecord(PositionMasterModel Obj);

        PositionMasterModel DeletePositionRecord(int UserID, int PositionID);
    }
}
