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

namespace FTS.Data.ApplicantProfile
{
    public class ApplicantProfileRepository : IApplicantProfileRepository
    {
        #region Private Variables
        private readonly IRepository<ApplicantMasterModel> _applicantmasterRepository;
        #endregion

        #region Constructor
        public ApplicantProfileRepository(IRepository<ApplicantMasterModel> applicantmasterRepository)
        {
            _applicantmasterRepository = applicantmasterRepository;
            #endregion;

        }

        //public ApplicantMasterModel ApplicantProfileRecord(ApplicantMasterModel objap)
        //{
        //    DynamicParameters param = new DynamicParameters();
        //    param.Add("@p_ApplicantID", ApplicantID);
        //    var keyValuePairs = _applicantmasterRepository.QueryMultipleByProcedure(SPConstants.GetApplicantProfile, param);
        //    var response = new EmployeeMasterModel();
        //    if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
        //    {
        //        response = result1.Select(x => new EmployeeMasterModel
        //        {
        //            EmployeeID = (int)x.EmployeeID,
        //            EmpCode = x.EmpCode,
        //            DesignationID = (int)x.DesignationID,
        //            RegionID = (int)x.RegionID,
        //            FirstName = x.FirstName,
        //            MiddleName = x.MiddleName,
        //            LastName = x.LastName,
        //            EmailID = x.EmailID,
        //            MobileNo = x.MobileNo,
        //            PAddress = x.PAddress,
        //            SAddress = x.SAddress,
        //            Gender = (int)x.Gender,
        //            DOB = Convert.ToDateTime(x.DOB),
        //            //DOB = (DateTime)x.DOB,
        //            //DOB = x.DOB,
        //            Password = x.Password,
        //            IsActive = Convert.ToBoolean(x.IsActive)
        //        }).FirstOrDefault();
        //    };
        //    return response;
        //}
        public ApplicantMasterModel SaveuserRecord(ApplicantMasterModel Objreg)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Objreg.ApplicantID);
            param.Add("@p_FirstName", Objreg.FirstName);
            param.Add("@p_MiddleName", Objreg.MiddleName);
            param.Add("@p_LastName", Objreg.LastName);
            param.Add("@p_EmailID", Objreg.EmailID);
            param.Add("@p_MobileNo", Objreg.MobileNo);
            param.Add("@p_PAddress", Objreg.PAddress);
            param.Add("@p_SAddress", Objreg.SAddress);
            param.Add("@p_Gender", Objreg.Gender);
            param.Add("@p_DOB", Objreg.DOB);


            var keyValuePairs = _applicantmasterRepository.QueryMultipleByProcedure(SPConstants.GetApplicantProfile, param);
            var response = new ApplicantMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicantMasterModel
                {

                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    //IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
        public ApplicantMasterModel GetuserRecord(int ApplicantID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicantID", ApplicantID);
            var keyValuePairs = _applicantmasterRepository.QueryMultipleByProcedure(SPConstants.GetApplicantProfile, param);
            var response = new ApplicantMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicantMasterModel
                {
                    ApplicantID = (int)x.ApplicantID,
                    FirstName = (string)x.FirstName,
                    MiddleName = (string)x.MiddleName,
                    LastName = (string)x.LastName,
                    EmailID = (string)x.EmailID,
                    MobileNo = (string)x.MobileNo,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    Gender = (int)x.Gender,
                    DOB = (DateTime)x.DOB,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }




        public ApplicantMasterModel ChangeApplicantPassword(ApplicantMasterModel Objreg)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicantID", Objreg.UserID);
            param.Add("@p_Password", Encrypt_Decrypt.Encrypt(Objreg.Password));
            param.Add("@p_CPassword", Encrypt_Decrypt.Encrypt(Objreg.CPassword));
            var keyValuePairs = _applicantmasterRepository.QueryMultipleByProcedure(SPConstants.ChangeApplicantPassword, param);
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
    }
}
