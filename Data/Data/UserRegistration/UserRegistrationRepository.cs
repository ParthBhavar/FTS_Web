using FTS.Data.Common;
using FTS.Model.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.UserRegistration
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        #region Private Variables
        private readonly IRepository<ApplicantMasterModel> _userRegistrationRepository;
        //private readonly IRepository<> _applicantRepository;
        #endregion

        #region Constructor
        public UserRegistrationRepository(IRepository<ApplicantMasterModel> userRegistrationRepository)
        {
            _userRegistrationRepository = userRegistrationRepository;
        }
        #endregion
        public ApplicantMasterModel SaveUserRegisterRecord(ApplicantMasterModel ObjReg)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_FirstName", ObjReg.FirstName);
            param.Add("@p_MiddleName", ObjReg.MiddleName);
            param.Add("@p_LastName", ObjReg.LastName);
            param.Add("@p_EmailID", ObjReg.EmailID);
            param.Add("@p_MobileNo", ObjReg.MobileNo);
            param.Add("@p_PAddress", ObjReg.PAddress);
            param.Add("@p_SAddress", ObjReg.SAddress);
            param.Add("@p_Gender", 1);
            param.Add("@p_DOB", ObjReg.DOB);
            param.Add("@p_Password", ObjReg.Password);
            param.Add("@p_OTP", ObjReg.ApplicantOTP);
            //param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjReg.Password));
            var keyValuePairs = _userRegistrationRepository.QueryMultipleByProcedure(SPConstants.RegistrationAdd, param);
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

        public ApplicantMasterModel UserVerifyOTP(ApplicantMasterModel ObjReg)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_EmailID", ObjReg.EmailID);
            param.Add("@p_OTP", ObjReg.ApplicantOTP);
            //param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjReg.Password));
            var keyValuePairs = _userRegistrationRepository.QueryMultipleByProcedure(SPConstants.ApplicantVerifyOTP, param);
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
    }
}



