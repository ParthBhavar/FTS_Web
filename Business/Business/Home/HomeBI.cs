using FTS.Data.Home;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.Home
{
    public class HomeBI : IHomeBI
    {
        private readonly IHomeRepository _IHomeRepository;
        public HomeBI(IHomeRepository HomeRepositor)
        {
            this._IHomeRepository = HomeRepositor;
        }
        public ApplicantMasterModel SaveUserLoginRecord(ApplicantMasterModel ObjReglogin)
        {
            var keyValuePairs = _IHomeRepository.SaveUserLoginRecord(ObjReglogin);
            return keyValuePairs;
        }
        public ApplicantMasterModel SaveApplicantForgotPasswordRecord(ApplicantMasterModel ObjFrgtPwd)
        {
            var keyValuePairs = _IHomeRepository.SaveApplicantForgotPasswordRecord(ObjFrgtPwd);
            return keyValuePairs;
        }
        public ApplicantMasterModel SaveApplicantResendEmailRecord(ApplicantMasterModel ObjFrgtPwd)
        {
            var keyValuePairs = _IHomeRepository.SaveApplicantResendEmailRecord(ObjFrgtPwd);
            return keyValuePairs;
        }

        public ApplicantMasterModel ChangePasswordApplicantForgotPassword(ApplicantMasterModel ObjFrgtPwd)
        {
            var keyValuePairs = _IHomeRepository.ChangePasswordApplicantForgotPassword(ObjFrgtPwd);
            return keyValuePairs;
        }
        public ApplicantMasterModel GenerateOTP(ApplicantMasterModel ObjGenOtp)
        {
            var keyValuePairs = _IHomeRepository.ChangePasswordApplicantForgotPassword(ObjGenOtp);
            return keyValuePairs;
        }
        public ApplicantMasterModel ChangeApplicantPassword(ApplicantMasterModel Objreg)
        {
            var keyValuePairs = _IHomeRepository.ChangeApplicantPassword(Objreg);
            return keyValuePairs;
        }
        public ApplicantMasterModel UserReVerifyOTP(ApplicantMasterModel ObjReg)
        {
            var keyValuePairs = _IHomeRepository.UserReVerifyOTP(ObjReg);
            return keyValuePairs;
        }
        public ApplicantMasterModel UpdateIslockedflageApp(ApplicantMasterModel Obj)
        {
            var keyValuePairs = _IHomeRepository.UpdateIslockedflageApp(Obj);
            return keyValuePairs;
        }
    }
}
