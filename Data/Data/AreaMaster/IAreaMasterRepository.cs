using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.AreaMaster
{
    public interface IAreaMasterRepository
    {
        List<AreaMasterModel> AreaList();
        AreaMasterModel AreaRecord(int AreaID);
        AreaMasterModel SaveAreaRecord(AreaMasterModel ObjArea);
        AreaMasterModel DeleteAreaRecord(int UserID, int AreaID);
    }
}
