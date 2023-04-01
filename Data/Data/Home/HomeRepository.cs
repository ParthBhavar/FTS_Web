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

namespace FTS.Data.Home
{
     public class HomeRepository : IHomeRepository
    {

        #region Private Variables
        public readonly IRepository<ApplicantMasterModel> _homeRepository;
        #endregion

        #region Constructor
        public HomeRepository(IRepository<ApplicantMasterModel> homeRepository)
        {
            _homeRepository = homeRepository;
        }
        #endregion
        public ApplicantMasterModel SaveUserLoginRecord(ApplicantMasterModel ObjReglogin)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_MobileNo", ObjReglogin.MobileNo);
            param.Add("p_Password", ObjReglogin.Password);   
            var keyValuePairs = _homeRepository.QueryMultipleByProcedure(SPConstants.ApplicantLogin, param);
            var response = new ApplicantMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicantMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicantID = (int)x.ApplicantID,
                    EmailID = (string)x.EmailID,
                    MobileNo = (string)x.MobileNo,
                    Gender = (int)x.Gender,
                    Name = (string)x.Name,
                    Islocked = (int)x.Islocked,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public ApplicantMasterModel SaveApplicantForgotPasswordRecord(ApplicantMasterModel ObjFrgtPwd)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_EmailID", ObjFrgtPwd.EmailID);
            var keyValuePairs = _homeRepository.QueryMultipleByProcedure(SPConstants.ForgotPassword, param);
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

        public ApplicantMasterModel SaveApplicantResendEmailRecord(ApplicantMasterModel ObjFrgtPwd)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_EmailID", ObjFrgtPwd.EmailID);
            param.Add("p_OTP", ObjFrgtPwd.ApplicantOTP);
            var keyValuePairs = _homeRepository.QueryMultipleByProcedure(SPConstants.ResendEmailVerification, param);
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
            param.Add("@p_Password1", ObjFrgtPwd.Password);
            //param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjFrgtPwd.Password));
            var keyValuePairs = _homeRepository.QueryMultipleByProcedure(SPConstants.ChangePassword, param);
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

        public ApplicantMasterModel ChangeApplicantPassword(ApplicantMasterModel Objreg)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicantID", Objreg.UserID);
            param.Add("@p_Password", Objreg.Password);
            param.Add("@p_CPassword", Objreg.CPassword);
            //param.Add("@p_Password", Encrypt_Decrypt.Encrypt(Objreg.Password));
            //param.Add("@p_CPassword", Encrypt_Decrypt.Encrypt(Objreg.CPassword));
            var keyValuePairs = _homeRepository.QueryMultipleByProcedure(SPConstants.ChangeApplicantPassword, param);
            var response = new ApplicantMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicantMasterModel
                {
                    ErrorMassage = (string)x.ErrorMassage,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
        public ApplicantMasterModel UserReVerifyOTP(ApplicantMasterModel ObjReg)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_EmailID", ObjReg.EmailID);
            param.Add("@p_OTP", ObjReg.ApplicantOTP);
            //param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjReg.Password));
            var keyValuePairs = _homeRepository.QueryMultipleByProcedure(SPConstants.ApplicantReVerifyOTP, param);
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

        public ApplicantMasterModel UpdateIslockedflageApp(ApplicantMasterModel Obj)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_ApplicantID", Obj.ApplicantID);
                param.Add("@p_Islocked", Obj.Islocked);
                param.Add("@p_LastLoginTime", Obj.LastLoginTime);
                param.Add("@p_IPAddress", Obj.IPAddress);
                var keyValuePairs = _homeRepository.QueryMultipleByProcedure(SPConstants.UpdateIslockedflageApp, param);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
