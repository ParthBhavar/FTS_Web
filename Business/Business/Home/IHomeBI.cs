using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.Home
{
    public interface IHomeBI
    {
        ApplicantMasterModel SaveUserLoginRecord(ApplicantMasterModel ObjReglogin);

        ApplicantMasterModel SaveApplicantForgotPasswordRecord(ApplicantMasterModel ObjFrgtPwd);
        ApplicantMasterModel SaveApplicantResendEmailRecord(ApplicantMasterModel ObjFrgtPwd);

        ApplicantMasterModel ChangePasswordApplicantForgotPassword(ApplicantMasterModel ObjFrgtPwd);

        ApplicantMasterModel GenerateOTP(ApplicantMasterModel ObjGenOtp);
        ApplicantMasterModel ChangeApplicantPassword(ApplicantMasterModel Objreg);
        ApplicantMasterModel UserReVerifyOTP(ApplicantMasterModel Objreg);
        ApplicantMasterModel UpdateIslockedflageApp(ApplicantMasterModel Obj);
    }
}
