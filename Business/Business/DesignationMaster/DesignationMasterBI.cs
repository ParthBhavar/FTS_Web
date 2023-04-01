using FTS.Data.DesignationMaster;
using FTS.Model.Common;
using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.DesignationMaster
{
    public class DesignationMasterBI : IDesignationMasterBI
    {
        private readonly IDesignationMasterRepository _DesignationMasterRepository;

        public DesignationMasterBI(IDesignationMasterRepository DesignationMasterRepositor)
        {
            this._DesignationMasterRepository = DesignationMasterRepositor;
        }

        //public async Task<IEnumerable<DesignationMasterModel>> DesignationMasterList(int DesignationID)
        //{

        //    return await _DesignationMasterRepository.DesignationMasterList(DesignationID);
        //}

        public List<DesignationMasterModel> DesignationList()
        {
            return _DesignationMasterRepository.DesignationList();
        }
        public DesignationMasterModel DesignationRecord(int DesignationID)
        {
            var keyValuePairs = _DesignationMasterRepository.DesignationRecord(DesignationID);
            return keyValuePairs;
        }

        public DesignationMasterModel SaveDesignationRecord(DesignationMasterModel Objdes)
        {
            var keyValuePairs = _DesignationMasterRepository.SaveDesignationRecord(Objdes);
            return keyValuePairs;
        }

        public DesignationMasterModel DeleteDesignationRecord(int UserID,int DesignationID)
        {
            var keyValuePairs = _DesignationMasterRepository.DeleteDesignationRecord(UserID,DesignationID);
            return keyValuePairs;
        }


    }
}