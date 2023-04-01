using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.DistrictMaster
{
    public interface IDistrictMasterBl
    {
        List<DistrictMasterModel> DistrictList();
        DistrictMasterModel DistrictRecord(int DistrictId);
        DistrictMasterModel SaveDistrictRecord(DistrictMasterModel ObjDistrict);
        DistrictMasterModel DeleteDistrictRecord(int DistrictId, int UserID);
    }
}
