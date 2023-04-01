using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTS.Model.Entities;
using Master.ViewModel;

namespace FTS.Data.UserRegistration
{
    public interface IUserRegistrationRepository
    {
        ApplicantMasterModel SaveUserRegisterRecord(ApplicantMasterModel ObjReg);

        ApplicantMasterModel UserVerifyOTP(ApplicantMasterModel ObjReg);
    }
}
