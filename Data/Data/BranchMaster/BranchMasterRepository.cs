using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FTS.Data.BranchMaster
{
    public class BranchMasterRepository : IBranchMasterRepository
    {
        #region Private Variables
        private readonly IRepository<BranchMasterModel> _branchRepository;
        #endregion

        #region Constructor
        public BranchMasterRepository(IRepository<BranchMasterModel> brnachRepository)
        {
            _branchRepository = brnachRepository;

        }
        #endregion

        public List<BranchMasterModel> BranchList()
        {
            try
            {
                List<BranchMasterModel> lstBranchMaster = new List<BranchMasterModel>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _branchRepository.QueryMultipleByProcedure(SPConstants.GetBranchMasterList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstBranchMaster = result1.Select(x => new BranchMasterModel
                    {
                        BranchID = (int)x.BranchID,
                        BranchName = (string)x.BranchName,
                        //ParentBranch = (int)x.ParentBranch,
                        IsActive = Convert.ToBoolean(x.IsActive),
                    }).ToList();
                };
                return lstBranchMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public BranchMasterModel BranchRecord(int BranchID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_BranchID", BranchID);
                var keyValuePairs = _branchRepository.QueryMultipleByProcedure(SPConstants.GetRecordBranchMaster, param);
                var response = new BranchMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new BranchMasterModel
                    {
                        BranchID = (int)x.BranchID,
                        BranchName = (string)x.BranchName,
                        ParentBranch = (int)x.ParentBranch,
                        DistrictID = (int)x.DistrictID,
                        IsActive = Convert.ToBoolean(x.IsActive),
                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BranchMasterModel SaveBranchRecord(BranchMasterModel ObjBranch)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", 1);
                param.Add("@p_BranchID", ObjBranch.BranchID);
                param.Add("@p_BranchName", ObjBranch.BranchName);
                param.Add("@p_ParentBranch", ObjBranch.ParentBranch);
                param.Add("@p_DistrictID", ObjBranch.DistrictID);
                param.Add("@p_IsActive", ObjBranch.IsActive);
                //param.Add("@p_IsDeleted", ObjBranch.IsDeleted);
                var keyValuePairs = _branchRepository.QueryMultipleByProcedure(SPConstants.BranchMasterAddEdit, param);
                var response = new BranchMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new BranchMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
                        // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BranchMasterModel DeleteBranchRecord(int UserID, int BranchID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_BranchID", BranchID);
                param.Add("@p_UserID", UserID);
                var keyValuePairs = _branchRepository.QueryMultipleByProcedure(SPConstants.DeleteBranchMaster, param);
                var response = new BranchMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new BranchMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
                        // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
   

