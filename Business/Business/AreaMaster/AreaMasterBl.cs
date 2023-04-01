using FTS.Data.AreaMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.AreaMaster
{
    public class AreaMasterBl : IAreaMasterBl
    {
        private readonly IAreaMasterRepository _AreaMasterRepository;

        public AreaMasterBl(IAreaMasterRepository AreaMasterRepositor)
        {
            this._AreaMasterRepository = AreaMasterRepositor;
        }

        public List<AreaMasterModel> AreaList()
        {
            return _AreaMasterRepository.AreaList();
        }

        public AreaMasterModel AreaRecord(int AreaID)
        {
            var keyValuePairs = _AreaMasterRepository.AreaRecord(AreaID);
            return keyValuePairs;
        }

        public AreaMasterModel SaveAreaRecord(AreaMasterModel ObjArea)
        {
            var keyValuePairs = _AreaMasterRepository.SaveAreaRecord(ObjArea);
            return keyValuePairs;
        }

        public AreaMasterModel DeleteAreaRecord(int UserID, int AreaID)
        {
            var keyValuePairs = _AreaMasterRepository.DeleteAreaRecord(UserID, AreaID);
            return keyValuePairs;
        }

    }
}
