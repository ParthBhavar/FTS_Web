using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ConcilliationActionMaster
{
    public interface IConcilliationActionMasterRepository
    {
        List<ConcilliationActionMasterModel> ConcilliationActionList();
        ConcilliationActionMasterModel ConcilliationActionRecord(int ActionID);
        ConcilliationActionMasterModel SaveConcilliationActionRecord(ConcilliationActionMasterModel ObjConcAction);
        ConcilliationActionMasterModel DeleteConcilliationActionRecord(int UserID, int ActionID);


    }
}
