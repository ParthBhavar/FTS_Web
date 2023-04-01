using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.NFormActionMaster
{
    public interface INFormActionMasterBl
    {
        List<NFormActionMasterModel> NFormActionList();
        NFormActionMasterModel NFormActionRecord(int ActionID);
        NFormActionMasterModel SaveNFormActionRecord(NFormActionMasterModel ObjAction);
        NFormActionMasterModel DeleteNFormActionRecord(int UserID, int ActionID);
    }
}
