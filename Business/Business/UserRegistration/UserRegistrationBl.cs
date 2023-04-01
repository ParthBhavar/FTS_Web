using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTS.Data.UserRegistration;
using FTS.Model.Entities;
using Master.ViewModel;

namespace FTS.Business.UserRegistration
{
    public class UserRegistrationBl : IUserRegistrationBl
    {
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        public UserRegistrationBl(IUserRegistrationRepository UserRegistrationRepository)
        {
            this._userRegistrationRepository = UserRegistrationRepository;
        }
        public ApplicantMasterModel SaveUserRegisterRecord(ApplicantMasterModel ObjReg)
        {
            var keyValuePairs = _userRegistrationRepository.SaveUserRegisterRecord(ObjReg);
            return keyValuePairs;
        }
        public ApplicantMasterModel UserVerifyOTP(ApplicantMasterModel ObjReg)
        {
            var keyValuePairs = _userRegistrationRepository.UserVerifyOTP(ObjReg);
            return keyValuePairs;
        }
    } 
}
