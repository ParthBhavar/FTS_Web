using FTS.Data.RegionMaster;
using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.RegionMaster
{
    public class RegionMasterBl :IRegionMasterBl
    {
        private readonly IRegionMasterRepository _RegionMasterRepository;

        public RegionMasterBl(IRegionMasterRepository RegionMasterRepositor)
        {
            this._RegionMasterRepository = RegionMasterRepositor;
        }

        public List<RegionMasterModel> RegionList()
        {
            return _RegionMasterRepository.RegionList();
        }

        public RegionMasterModel RegionRecord(int RegionID)
        {
            var keyValuePairs = _RegionMasterRepository.RegionRecord(RegionID);
            return keyValuePairs;
        }

        public RegionMasterModel SaveRegionRecord(RegionMasterModel ObjRegion)
        {
            var keyValuePairs = _RegionMasterRepository.SaveRegionRecord(ObjRegion);
            return keyValuePairs;
        }

        public RegionMasterModel DeleteRegionRecord(int UserID,int RegionID)
        {
            var keyValuePairs = _RegionMasterRepository.DeleteRegionRecord(UserID,RegionID);
            return keyValuePairs;
            }

    }
}
