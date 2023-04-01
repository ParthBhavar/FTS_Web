using FTS.Data.EmpPositionMaster;
using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.EmpPositionMaster
{
    public class EmpPositionMasterBl : IEmpPositionMasterBl
    {
        private readonly IEmpPositionMasterRepository _EmpPositionMasterRepository;

        public EmpPositionMasterBl(IEmpPositionMasterRepository EmpPositionMasterRepositor)
        {
            this._EmpPositionMasterRepository = EmpPositionMasterRepositor;
        }

        public List<EmpPositionMasterModel> EmpPositionMasterList()
        {
            return _EmpPositionMasterRepository.EmpPositionMasterList();
        }

        public EmpPositionMasterModel EmpPositionMasterRecord(int EmpPosID)
        {
            var keyValuePairs = _EmpPositionMasterRepository.EmpPositionMasterRecord(EmpPosID);
            return keyValuePairs;
        }

        public EmpPositionMasterModel SaveEmpPositionMasterRecord(EmpPositionMasterModel ObjEmpPosition)
        {
            var keyValuePairs = _EmpPositionMasterRepository.SaveEmpPositionMasterRecord(ObjEmpPosition);
            return keyValuePairs;
        }

        public EmpPositionMasterModel DeleteEmpPositionMasterRecord(int UserID, int EmpPosID)
        {
            var keyValuePairs = _EmpPositionMasterRepository.DeleteEmpPositionMasterRecord(UserID, EmpPosID);
            return keyValuePairs;
        }

        public EmpPositionMasterModel SaveAssignPositionRecord(EmpPositionMasterModel ObjEmpPosition)
        {
            var keyValuePairs = _EmpPositionMasterRepository.SaveAssignPositionRecord(ObjEmpPosition);
            return keyValuePairs;
        }
    }
}
