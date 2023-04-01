using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.NFormActionMaster
{
    public interface INFormActionMasterRepository
    {
        List<NFormActionMasterModel> NFormActionList();
        NFormActionMasterModel NFormActionRecord(int ActionID);
        NFormActionMasterModel SaveNFormActionRecord(NFormActionMasterModel ObjAction);
        NFormActionMasterModel DeleteNFormActionRecord(int UserID, int ActionID);
    }
}
