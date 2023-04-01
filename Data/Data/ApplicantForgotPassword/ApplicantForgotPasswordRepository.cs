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

namespace FTS.Data.ApplicantForgotPassword
{
    public class ApplicantForgotPasswordRepository : IApplicantForgotPasswordRepository
    {
        #region Private Variables
        public readonly IRepository<ApplicantMasterModel> _applicantForgotPasswordRepository;
        #endregion

        #region Constructor
        public ApplicantForgotPasswordRepository(IRepository<ApplicantMasterModel> applicantForgotPasswordRepository)
        {
            this._applicantForgotPasswordRepository = applicantForgotPasswordRepository;
        }
        #endregion
        public ApplicantMasterModel SaveApplicantForgotPasswordRecord(ApplicantMasterModel ObjFrgtPwd)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_EmailID", ObjFrgtPwd.EmailID);
            //param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjFrgtPwd.Password));
            var keyValuePairs = _applicantForgotPasswordRepository.QueryMultipleByProcedure(SPConstants.ForgotPassword, param);
            var response = new ApplicantMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicantMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                   

                }).FirstOrDefault();
            };
            return response;
        }
       

        public ApplicantMasterModel ChangePasswordApplicantForgotPassword(ApplicantMasterModel ObjFrgtPwd)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_EmailID", ObjFrgtPwd.EmailID);
            param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjFrgtPwd.Password));
            var keyValuePairs = _applicantForgotPasswordRepository.QueryMultipleByProcedure(SPConstants.ChangePassword, param);
            var response = new ApplicantMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicantMasterModel
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
