using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTS.Model.Entities;

namespace FTS.Business.BranchMaster
{
    public  interface IBranchMasterBl
    {
        List<BranchMasterModel> BranchList();
        BranchMasterModel GetBranchRecord(int BranchID);
        BranchMasterModel SaveBranchRecord(BranchMasterModel ObjBranch);
        BranchMasterModel DeleteBranchRecord(int UserID, int BranchID);
    }
}
