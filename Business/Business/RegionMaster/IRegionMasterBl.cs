using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.RegionMaster
{
    public interface IRegionMasterBl
    {
        List<RegionMasterModel> RegionList();
        RegionMasterModel RegionRecord(int RegionID);
        RegionMasterModel SaveRegionRecord(RegionMasterModel ObjRegion);
        RegionMasterModel DeleteRegionRecord(int UserID, int RegionID);
    }
}
