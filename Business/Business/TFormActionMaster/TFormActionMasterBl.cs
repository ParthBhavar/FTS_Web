using FTS.Data.TFormActionMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.TFormActionMaster
{
    public class TFormActionMasterBl : ITFormActionMasterBl
    {
        private readonly ITFormActionMasterRepository _TFormActionMasterRepository;

        public TFormActionMasterBl(ITFormActionMasterRepository TFormActionMasterRepositor)
        {
            this._TFormActionMasterRepository = TFormActionMasterRepositor;
        }

        public List<TFormActionMasterModel> TFormActionList()
        {
            return _TFormActionMasterRepository.TFormActionList();
        }

        public TFormActionMasterModel TFormActionRecord(int ActionID)
        {
            var keyValuePairs = _TFormActionMasterRepository.TFormActionRecord(ActionID);
            return keyValuePairs;
        }

        public TFormActionMasterModel SaveTFormActionRecord(TFormActionMasterModel ObjTFormAction)
        {
            var keyValuePairs = _TFormActionMasterRepository.SaveTFormActionRecord(ObjTFormAction);
            return keyValuePairs;
        }

        public TFormActionMasterModel DeleteTFormActionRecord(int UserID, int ActionID)
        {
            var keyValuePairs = _TFormActionMasterRepository.DeleteTFormActionRecord(UserID, ActionID);
            return keyValuePairs;
        }
    }
}
