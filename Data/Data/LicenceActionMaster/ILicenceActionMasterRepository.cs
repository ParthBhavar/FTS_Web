using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.LicenceActionMaster
{
    public interface ILicenceActionMasterRepository
    {
        List<LicenceActionMasterModel> licenceActionList();
        LicenceActionMasterModel licenceActionRecord(int ActionID);
        LicenceActionMasterModel SavelicenceActionRecord(LicenceActionMasterModel ObjAction);
        LicenceActionMasterModel DeletelicenceActionRecord(int UserID, int ActionID);
    }
}
