using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTS.Model.Entities;
using Master.ViewModel;

namespace FTS.Business.UserRegistration
{
    public interface IUserRegistrationBl
    {
        ApplicantMasterModel SaveUserRegisterRecord(ApplicantMasterModel ObjReg);
        ApplicantMasterModel UserVerifyOTP(ApplicantMasterModel ObjReg);
    }
}
