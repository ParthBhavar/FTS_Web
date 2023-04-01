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

namespace FTS.Data.ApplicantLoginMaster
{
    public class ApplicantLoginMasterRepository : IApplicantLoginMasterRepository
    {
        #region Private Variables
        public readonly IRepository<ApplicantMasterModel> _applicantLoginRepository;
        #endregion

        #region Constructor
        public ApplicantLoginMasterRepository(IRepository<ApplicantMasterModel> applicantLoginRepository)
        {
            _applicantLoginRepository = applicantLoginRepository;
        }
        #endregion
        public ApplicantMasterModel SaveApplicantLoginRecord(ApplicantMasterModel ObjReglogin)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_MobileNo", ObjReglogin.MobileNo);
            param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjReglogin.Password));
            var keyValuePairs = _applicantLoginRepository.QueryMultipleByProcedure(SPConstants.ApplicantLogin, param);
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
