using FTS.Data.BranchMaster;
using FTS.Model.Entities;
using FTS.Model.Common;
using FTS.Model;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.BranchMaster
{
    public class BranchMasterBl : IBranchMasterBl
    {
        private readonly IBranchMasterRepository _BranchMasterRepository;

        public BranchMasterBl(IBranchMasterRepository BranchMasterRepositor)
        {
            this._BranchMasterRepository = BranchMasterRepositor;
        }
        public List<BranchMasterModel> BranchList()
        {
            return _BranchMasterRepository.BranchList();
        }

        public BranchMasterModel GetBranchRecord(int BranchID)
        {
            var keyValuePairs = _BranchMasterRepository.BranchRecord(BranchID);
            return keyValuePairs;
        }

        public BranchMasterModel SaveBranchRecord(BranchMasterModel ObjBranch)
        {
            var keyValuePairs = _BranchMasterRepository.SaveBranchRecord(ObjBranch);
            return keyValuePairs;
        }

        public BranchMasterModel DeleteBranchRecord(int UserID, int BranchID)
        {
            var keyValuePairs = _BranchMasterRepository.DeleteBranchRecord(UserID, BranchID);
            return keyValuePairs;
        }
    }
}
