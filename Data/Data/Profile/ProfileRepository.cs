using FTS.Data.Common;
using FTS.Model.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        #region Private Variables
        private readonly IRepository<EmployeeMasterModel> _ProfileRepo;
        #endregion

        #region Constructor
        public ProfileRepository(IRepository<EmployeeMasterModel> ProfileRepo)
        {
            _ProfileRepo = ProfileRepo;

        }
        #endregion

        public List<EmployeeMasterModel> ProfileList(int UserID)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<EmployeeMasterModel> lstProfile = new List<EmployeeMasterModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _ProfileRepo.QueryMultipleByProcedure(SPConstants.GetProfileList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstProfile = result1.Select(x => new EmployeeMasterModel
                {
                    //EmployeeID = (int)x.EmployeeID,
                    FirstName = (string)x.FirstName,
                    MiddleName = (string)x.MiddleName,
                    LastName = (string)x.LastName,
                    EmailID = (string)x.EmailID,
                    MobileNo = (string)x.MobileNo,
                }).ToList();
            };
            return lstProfile;
        }


        public EmployeeMasterModel ChangeEmployeePassword(EmployeeMasterModel ObjReglogin)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_EmployeeID", ObjReglogin.UserID);
            param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjReglogin.Password));
            param.Add("@p_CPassword", Encrypt_Decrypt.Encrypt(ObjReglogin.CPassword));
            var keyValuePairs = _ProfileRepo.QueryMultipleByProcedure(SPConstants.changeEmployeepassword, param);
            var response = new EmployeeMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new EmployeeMasterModel
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
