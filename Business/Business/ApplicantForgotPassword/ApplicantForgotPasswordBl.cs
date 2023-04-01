using FTS.Data.ApplicantForgotPassword;
using FTS.Data.ApplicantLoginMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ApplicantForgotPassword
{
    public class ApplicantForgotPasswordBl : IApplicantForgotPasswordBl
    {
        private readonly IApplicantForgotPasswordRepository _ApplicationForgotPasswordRepository;
        public ApplicantForgotPasswordBl(IApplicantForgotPasswordRepository ApplicationForgotPasswordRepositor)
        {
            this._ApplicationForgotPasswordRepository = ApplicationForgotPasswordRepositor;
        }
        public ApplicantMasterModel SaveApplicantForgotPasswordRecord(ApplicantMasterModel ObjFrgtPwd)
        {
            var keyValuePairs = _ApplicationForgotPasswordRepository.SaveApplicantForgotPasswordRecord(ObjFrgtPwd);
            return keyValuePairs;
        }

        public ApplicantMasterModel ChangePasswordApplicantForgotPassword(ApplicantMasterModel ObjFrgtPwd)
        {
            var keyValuePairs = _ApplicationForgotPasswordRepository.ChangePasswordApplicantForgotPassword(ObjFrgtPwd);
            return keyValuePairs;
        }
        public ApplicantMasterModel GenerateOTP(ApplicantMasterModel ObjGenOtp)
        {
            var keyValuePairs = _ApplicationForgotPasswordRepository.ChangePasswordApplicantForgotPassword(ObjGenOtp);
            return keyValuePairs;
        }
    }
}
