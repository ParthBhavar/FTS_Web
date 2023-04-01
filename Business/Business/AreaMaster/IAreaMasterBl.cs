using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.AreaMaster
{
    public interface IAreaMasterBl
    {
        List<AreaMasterModel> AreaList();
        AreaMasterModel AreaRecord(int AreaID);
        AreaMasterModel SaveAreaRecord(AreaMasterModel ObjArea);
        AreaMasterModel DeleteAreaRecord(int UserID, int AreaID);
    }
}
