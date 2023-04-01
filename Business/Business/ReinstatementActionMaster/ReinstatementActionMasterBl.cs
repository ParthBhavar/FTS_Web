using FTS.Data.ReinstatementActionMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ReinstatementActionMaster
{
    public class ReinstatementActionMasterBl : IReinstatementActionMasterBl
    {
        private readonly IReinstatementActionMasterRepository _ReinstatementActionMasterRepository;

        public ReinstatementActionMasterBl(IReinstatementActionMasterRepository ReinstatementActionMasterRepositor)
        {
            this._ReinstatementActionMasterRepository = ReinstatementActionMasterRepositor;
        }

        public List<ReinstatementActionMasterModel>ReinstatementActionList()
        {
            return _ReinstatementActionMasterRepository.ReinstatementActionList();
        }

        public ReinstatementActionMasterModel ReinstatementActionRecord(int ActionID)
        {
            var keyValuePairs = _ReinstatementActionMasterRepository.ReinstatementActionRecord(ActionID);
            return keyValuePairs;
        }

        public ReinstatementActionMasterModel SaveReinstatementActionRecord(ReinstatementActionMasterModel ObjAction)
        {
            var keyValuePairs = _ReinstatementActionMasterRepository.SaveReinstatementActionRecord(ObjAction);
            return keyValuePairs;
        }

        public ReinstatementActionMasterModel DeleteReinstatementActionRecord(int UserID, int ActionID)
        {
            var keyValuePairs = _ReinstatementActionMasterRepository.DeleteReinstatementActionRecord(UserID, ActionID);
            return keyValuePairs;
        }
    }
}
