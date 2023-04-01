using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTS.Model.Entities;

namespace FTS.Data.RegionMaster
{
    public interface IRegionMasterRepository
    {
        List<RegionMasterModel> RegionList();
        RegionMasterModel RegionRecord(int RegionID);
        RegionMasterModel SaveRegionRecord(RegionMasterModel ObjRegion);
        RegionMasterModel DeleteRegionRecord(int UserID,int RegionID);

    }
}
