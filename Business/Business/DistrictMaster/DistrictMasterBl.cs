using FTS.Data.DistrictMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.DistrictMaster
{
    public class DistrictMasterBl : IDistrictMasterBl
    {
        public readonly IDistrictMasterRepository _DistrictMasterRepository;

        public DistrictMasterBl(IDistrictMasterRepository DistrictMasterRepositor)
        {
            this._DistrictMasterRepository = DistrictMasterRepositor;
        }

        public List<DistrictMasterModel> DistrictList()
        {
            return _DistrictMasterRepository.DistrictList();
        }

        public DistrictMasterModel DistrictRecord(int DistrictId)
        {
            var keyValuePairs = _DistrictMasterRepository.DistrictRecord(DistrictId);
            return keyValuePairs;
        }

        public DistrictMasterModel SaveDistrictRecord(DistrictMasterModel ObjDistrict)
        {
            var keyValuePairs = _DistrictMasterRepository.SaveDistrictRecord(ObjDistrict);
            return keyValuePairs;
        }

        public DistrictMasterModel DeleteDistrictRecord(int DistrictId, int UserID)
        {
            var keyValuePairs = _DistrictMasterRepository.DeleteDistrictRecord(DistrictId, UserID);
            return keyValuePairs;
        }
    }
}
