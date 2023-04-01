using FTS.Data.LicenceActionMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.LicenceActionMaster
{
    public class LicenceActionMasterBl : ILicenceActionMasterBl
    {
        private readonly ILicenceActionMasterRepository _LicenceActionMasterRepository;
        public LicenceActionMasterBl(ILicenceActionMasterRepository LicenceActionMasterRepositor)
        {
            this._LicenceActionMasterRepository = LicenceActionMasterRepositor;
        }
        public List<LicenceActionMasterModel> licenceActionList()
        {
            return _LicenceActionMasterRepository.licenceActionList();
        }
        public LicenceActionMasterModel licenceActionRecord(int ActionID)
        {
            var keyValuePairs = _LicenceActionMasterRepository.licenceActionRecord(ActionID);
            return keyValuePairs;
        }

        public LicenceActionMasterModel SavelicenceActionRecord(LicenceActionMasterModel ObjAction)
        {
            var keyValuePairs = _LicenceActionMasterRepository.SavelicenceActionRecord(ObjAction);
            return keyValuePairs;
        }

        public LicenceActionMasterModel DeletelicenceActionRecord(int UserID, int ActionID)
        {
            var keyValuePairs = _LicenceActionMasterRepository.DeletelicenceActionRecord(UserID, ActionID);
            return keyValuePairs;
        }
    }
}
