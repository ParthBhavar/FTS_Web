using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.Home
{
    public interface IHomeRepository
    {
        ApplicantMasterModel SaveUserLoginRecord(ApplicantMasterModel ObjReglogin);

        ApplicantMasterModel SaveApplicantForgotPasswordRecord(ApplicantMasterModel ObjFrgtPwd);
        ApplicantMasterModel SaveApplicantResendEmailRecord(ApplicantMasterModel ObjFrgtPwd);

        ApplicantMasterModel ChangePasswordApplicantForgotPassword(ApplicantMasterModel ObjFrgtPwd);
        ApplicantMasterModel ChangeApplicantPassword(ApplicantMasterModel Objreg);
        ApplicantMasterModel UserReVerifyOTP(ApplicantMasterModel ObjReg);
        ApplicantMasterModel UpdateIslockedflageApp(ApplicantMasterModel Obj);

    }
}
