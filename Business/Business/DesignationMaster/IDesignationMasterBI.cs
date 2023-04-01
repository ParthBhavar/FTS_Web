using FTS.Model.Common;
using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.DesignationMaster
{
    public interface IDesignationMasterBI
    {
        //Task<IEnumerable<DesignationMasterModel>> DesignationMasterList(int DesignationID);

        List<DesignationMasterModel> DesignationList();


        DesignationMasterModel DesignationRecord(int DesignationID);

        DesignationMasterModel SaveDesignationRecord(DesignationMasterModel ObjDes);

        DesignationMasterModel DeleteDesignationRecord(int UserID,int DesignationID);
    }
}
