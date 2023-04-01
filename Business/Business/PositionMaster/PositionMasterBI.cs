using FTS.Data.PositionMaster;
using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.PositionMaster
{
    public class PositionMasterBI : IPositionMasterBI
    {
        private readonly IPositionMasterRepository _PositionMasterRepository;

        public PositionMasterBI(IPositionMasterRepository PositionMasterRepository)
        {
            this._PositionMasterRepository = PositionMasterRepository;
        }

        //public List<MRoleMaster> RoleList(PaginationRequest model)
        //{
        //    return  _RoleMasterRepository.RoleList(model);
        //}

        public List<PositionMasterModel> PositionList()
        {
            return _PositionMasterRepository.PositionList();
        }

        public PositionMasterModel PositionRecord(int PositionID)
        {
            var keyValuePairs = _PositionMasterRepository.PositionRecord(PositionID);
            return keyValuePairs;
        }

        public PositionMasterModel SavePositionRecord(PositionMasterModel Obj)
        {
            var keyValuePairs = _PositionMasterRepository.SavePositionRecord(Obj);
            return keyValuePairs;
        }

        public PositionMasterModel DeletePositionRecord(int UserID, int PositionID)
        {
            var keyValuePairs = _PositionMasterRepository.DeletePositionRecord(UserID, PositionID);
            return keyValuePairs;
        }

    }
}
