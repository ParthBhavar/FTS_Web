using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ApplicationRegistrationAction
{
    public interface IApplicationRegistrationActionMasterRepository
    {
        List<ApplicationRegistrationActionMasterModel> ApplicationRegistratationActionList();
        ApplicationRegistrationActionMasterModel ApplicationRegistratationActionRecord(int ActionID);
        ApplicationRegistrationActionMasterModel SaveAppRegActionRecord(ApplicationRegistrationActionMasterModel ObjAction);
        ApplicationRegistrationActionMasterModel DeleteAppREgActionRecord(int UserID, int ActionID);
    }
}
