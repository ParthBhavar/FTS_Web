using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using FTS.Data.Common;
using Data.Generic;
using FTS.Model.Entities;
using System.Threading.Tasks;
using FTS.Model;
using FTS.Model.Common;

namespace FTS.Data.RoleMaster
{
    public class RoleMasterRepository : IRoleMasterRepository
    {
        #region Private Variables
        private readonly IRepository<MRoleMaster> _roleRepository;
        #endregion

        #region Constructor
        public RoleMasterRepository(IRepository<MRoleMaster> roleRepository)
        {
            _roleRepository = roleRepository;

        }
        #endregion

        public List<MRoleMaster> RoleList()
        {
            try {
                //if (model.SearchText == null)
                //{
                //    model.SearchText = "";
                //}
                List<MRoleMaster> lstRoleMaster = new List<MRoleMaster>();
                DynamicParameters param = new DynamicParameters();

                //param.Add("@P_PageNumber", model.PageNumber);
                //param.Add("@P_PageSize", 100);
                //param.Add("@P_SearchText", model.SearchText);
                var keyValuePairs = _roleRepository.QueryMultipleByProcedure(SPConstants.GetListRoleMaster, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstRoleMaster = result1.Select(x => new MRoleMaster
                    {
                        RoleID = (int)x.RoleID,
                        RoleName = (string)x.RoleName,
                        ParentRoleID = (int)x.ParentRoleID,
                        //RowNum = (int)x.RowNum,
                        //TotalRecord = (int)x.TotalRecord,
                        Level = (int)x.Level,
                        IsActive = Convert.ToBoolean(x.IsActive),
                    }).ToList();
                };
                return lstRoleMaster;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MRoleMaster RoleRecord(int RoleID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_RoleID", RoleID);
                var keyValuePairs = _roleRepository.QueryMultipleByProcedure(SPConstants.GetRecordRolemaster, param);
                var response = new MRoleMaster();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new MRoleMaster
                    {
                        RoleID = (int)x.RoleID,
                        RoleName = (string)x.RoleName,
                        ParentRoleID = (int)x.ParentRoleID,
                        Level = (int)x.Level,
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

        public MRoleMaster SaveRoleRecord(MRoleMaster ObjROle)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", 1);
                param.Add("@p_RoleID", ObjROle.RoleID);
                param.Add("@p_RoleName", ObjROle.RoleName);
                param.Add("@p_ParentRoleID", ObjROle.ParentRoleID);
                param.Add("@p_Level", ObjROle.Level);
                param.Add("@p_IsActive", ObjROle.IsActive);
                var keyValuePairs = _roleRepository.QueryMultipleByProcedure(SPConstants.UpdateRolemaster, param);
                var response = new MRoleMaster();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new MRoleMaster
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


        public MRoleMaster DeleteRoleRecord(int UserID, int RoleID)
        {
          
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_RoleID", RoleID);
                param.Add("@p_UserID", UserID);
                var keyValuePairs = _roleRepository.QueryMultipleByProcedure(SPConstants.DeleteRoleMaster, param);
                var response = new MRoleMaster();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new MRoleMaster
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
                        // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                    }).FirstOrDefault();
                };
                return response;
            
        } 
     }
}
