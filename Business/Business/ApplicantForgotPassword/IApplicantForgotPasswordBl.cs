using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ApplicantForgotPassword
{
    public interface IApplicantForgotPasswordBl
    {
        ApplicantMasterModel SaveApplicantForgotPasswordRecord(ApplicantMasterModel ObjFrgtPwd);

        ApplicantMasterModel ChangePasswordApplicantForgotPassword(ApplicantMasterModel ObjFrgtPwd);
    }
}
