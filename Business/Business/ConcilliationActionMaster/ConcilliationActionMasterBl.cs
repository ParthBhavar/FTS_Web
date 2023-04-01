using FTS.Data.ConcilliationActionMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ConcilliationActionMaster
{
    public class ConcilliationActionMasterBl : IConcilliationActionMasterBl 
    {
        private readonly IConcilliationActionMasterRepository _ConcilliationActionMasterRepository;

        public ConcilliationActionMasterBl(IConcilliationActionMasterRepository ConcilliationActionMasterRepositor)
        {
            this._ConcilliationActionMasterRepository = ConcilliationActionMasterRepositor;
        }

        public List<ConcilliationActionMasterModel> ConcilliationActionList()
        {
            return _ConcilliationActionMasterRepository.ConcilliationActionList();
        }

        public ConcilliationActionMasterModel ConcilliationActionRecord(int ActionID)
        {
            var keyValuePairs = _ConcilliationActionMasterRepository.ConcilliationActionRecord(ActionID);
            return keyValuePairs;
        }

        public ConcilliationActionMasterModel SaveConcilliationActionRecord(ConcilliationActionMasterModel ObjConcAction)
        {
            var keyValuePairs = _ConcilliationActionMasterRepository.SaveConcilliationActionRecord(ObjConcAction);
            return keyValuePairs;
        }

        public ConcilliationActionMasterModel DeleteConcilliationActionRecord(int UserID, int ActionID)
        {
            var keyValuePairs = _ConcilliationActionMasterRepository.DeleteConcilliationActionRecord(UserID, ActionID);
            return keyValuePairs;
        }

    }
}
