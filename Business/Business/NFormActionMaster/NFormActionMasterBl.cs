using FTS.Data.NFormActionMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.NFormActionMaster
{
    public class NFormActionMasterBl : INFormActionMasterBl
    {              
            private readonly INFormActionMasterRepository _NFormActionMasterRepository;

            public NFormActionMasterBl(INFormActionMasterRepository NFormActionMasterRepositor)
            {
                this._NFormActionMasterRepository = NFormActionMasterRepositor;
            }

            public List<NFormActionMasterModel> NFormActionList()
            {
                return _NFormActionMasterRepository.NFormActionList();
            }

            public NFormActionMasterModel NFormActionRecord(int ActionID)
            {
                var keyValuePairs = _NFormActionMasterRepository.NFormActionRecord(ActionID);
                return keyValuePairs;
            }

            public NFormActionMasterModel SaveNFormActionRecord(NFormActionMasterModel ObjAction)
            {
                var keyValuePairs = _NFormActionMasterRepository.SaveNFormActionRecord(ObjAction);
                return keyValuePairs;
            }

            public NFormActionMasterModel DeleteNFormActionRecord(int UserID, int ActionID)
            {
                var keyValuePairs = _NFormActionMasterRepository.DeleteNFormActionRecord(UserID, ActionID);
                return keyValuePairs;
            }
    }
}
