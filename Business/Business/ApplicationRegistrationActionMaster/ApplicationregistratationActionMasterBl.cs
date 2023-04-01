using FTS.Data.ApplicationRegistrationAction;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ApplicationRegistrationActionMaster
{
    
    public class ApplicationregistratationActionMasterBl : IApplicationregistratationActionMasterBl
    {
        private readonly IApplicationRegistrationActionMasterRepository _ApplicationRegistrationActionMasterRepository;
        public ApplicationregistratationActionMasterBl(IApplicationRegistrationActionMasterRepository ApplicationregistratationActionMasterRepositor)
        {
            this._ApplicationRegistrationActionMasterRepository = ApplicationregistratationActionMasterRepositor;
        }
        public List<ApplicationRegistrationActionMasterModel> ApplicationRegistratationActionList()
        {
            return _ApplicationRegistrationActionMasterRepository.ApplicationRegistratationActionList();
        }

        public ApplicationRegistrationActionMasterModel ApplicationRegistratationActionRecord(int ActionID)
        {
            var keyValuePairs = _ApplicationRegistrationActionMasterRepository.ApplicationRegistratationActionRecord(ActionID);
            return keyValuePairs;
        }

        public ApplicationRegistrationActionMasterModel SaveAppRegActionRecord(ApplicationRegistrationActionMasterModel ObjAction)
        {
            var keyValuePairs = _ApplicationRegistrationActionMasterRepository.SaveAppRegActionRecord(ObjAction);
            return keyValuePairs;
        }

        public ApplicationRegistrationActionMasterModel DeleteAppREgActionRecord(int UserID, int ActionID)
        {
            var keyValuePairs = _ApplicationRegistrationActionMasterRepository.DeleteAppREgActionRecord(UserID, ActionID);
            return keyValuePairs;
        }
    }
   
}
