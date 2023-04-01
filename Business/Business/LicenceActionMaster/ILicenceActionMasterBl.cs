using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.LicenceActionMaster
{
    public interface ILicenceActionMasterBl
    {
        List<LicenceActionMasterModel> licenceActionList();
        LicenceActionMasterModel licenceActionRecord(int ActionID);
        LicenceActionMasterModel SavelicenceActionRecord(LicenceActionMasterModel ObjAction);
        LicenceActionMasterModel DeletelicenceActionRecord(int UserID, int ActionID);
    }
}
