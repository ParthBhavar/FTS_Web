using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ReinstatementActionMaster
{
    public interface IReinstatementActionMasterRepository
    {
        List<ReinstatementActionMasterModel> ReinstatementActionList();
        ReinstatementActionMasterModel ReinstatementActionRecord(int ActionID);
        ReinstatementActionMasterModel SaveReinstatementActionRecord(ReinstatementActionMasterModel ObjAction);
        ReinstatementActionMasterModel DeleteReinstatementActionRecord(int UserID, int ActionID);

    }
}
