using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.TFormActionMaster
{
    public interface ITFormActionMasterBl
    {
        List<TFormActionMasterModel> TFormActionList();
        TFormActionMasterModel TFormActionRecord(int ActionID);
        TFormActionMasterModel SaveTFormActionRecord(TFormActionMasterModel ObjTFormAction);
        TFormActionMasterModel DeleteTFormActionRecord(int UserID, int ActionID);

    }
}
