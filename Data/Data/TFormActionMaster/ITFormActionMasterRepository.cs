using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.TFormActionMaster
{
    public interface ITFormActionMasterRepository
    {
        List<TFormActionMasterModel> TFormActionList();
        TFormActionMasterModel TFormActionRecord(int ActionID);
        TFormActionMasterModel SaveTFormActionRecord(TFormActionMasterModel ObjTFormAction);
        TFormActionMasterModel DeleteTFormActionRecord(int UserID, int ActionID);

    }
}
