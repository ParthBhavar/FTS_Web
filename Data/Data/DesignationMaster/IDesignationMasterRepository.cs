using FTS.Model.Common;
using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.DesignationMaster
{
    public interface IDesignationMasterRepository
    {
        //Task<IEnumerable<DesignationMasterModel>> DesignationMasterList(int p_DesignationID);

        List<DesignationMasterModel> DesignationList();


        DesignationMasterModel DesignationRecord(int DesignationID);

        DesignationMasterModel SaveDesignationRecord(DesignationMasterModel ObjDes);

        DesignationMasterModel DeleteDesignationRecord(int UserID,int DesignationID);
    }
}
