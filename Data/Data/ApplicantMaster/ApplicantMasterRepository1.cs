using COL.Data.Common;
using COL.Model.Common;
using COL.Model.Entities;
using Dapper;
using Data.Generic;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COL.Data.ApplicantMaster
{
    public class ApplicantMasterRepository1 : IApplicantMasterRepository
    {
        #region Private Variables
        private readonly IRepository<ApplicantMasterModel> _applicantRepository;
        //private readonly IRepository<> _applicantRepository;
        #endregion

        #region Constructor
        public ApplicantMasterRepository1(IRepository<ApplicantMasterModel> applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }
        #endregion
        public ApplicantMasterModel SaveUserRegisterRecord(ApplicantMasterModel ObjReg)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_FirstName", "");
            param.Add("@p_MiddleName", "");
            param.Add("@p_LastName", "");
            param.Add("@p_EmailID", ObjReg.EmailID);
            param.Add("@p_MobileNo", ObjReg.MobileNo);
            param.Add("@p_PAddress", ObjReg.PAddress);
            param.Add("@p_SAddress", ObjReg.SAddress);
            param.Add("@p_Gender", 1);
            param.Add("@p_DOB", ObjReg.DOB);
            param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjReg.Password));
            var keyValuePairs = _applicantRepository.QueryMultipleByProcedure(SPConstants.RegistrationAdd, param);
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
