using FTS.Data.ApplicantProfile;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ApplicantProfile
{
    public class ApplicantProfileBl : IApplicantProfileBl
    {
        public readonly IApplicantProfileRepository _ApplicantProfileRepository;

        public ApplicantProfileBl(IApplicantProfileRepository ApplicantProfileRepositor)
        {
            this._ApplicantProfileRepository = ApplicantProfileRepositor;
        }

        //public ApplicantMasterModel ApplicantProfileRecord(int ApplicantID)
        //{
        //    var keyValuePairs = _ApplicantProfileRepository.ApplicantProfileRecord(ApplicantID);
        //    return keyValuePairs;
        //}
        public ApplicantMasterModel SaveuserRecord(ApplicantMasterModel Objreg)
        {
            var keyValuePairs = _ApplicantProfileRepository.SaveuserRecord(Objreg);
            return keyValuePairs;
        }
        public ApplicantMasterModel GetuserRecord(int ApplicantID)
        {
            var keyValuePairs = _ApplicantProfileRepository.GetuserRecord(ApplicantID);
            return keyValuePairs;
        }

        public ApplicantMasterModel ChangeApplicantPassword(ApplicantMasterModel Objreg)
        {
            var keyValuePairs = _ApplicantProfileRepository.ChangeApplicantPassword(Objreg);
            return keyValuePairs;
        }
    }
}
