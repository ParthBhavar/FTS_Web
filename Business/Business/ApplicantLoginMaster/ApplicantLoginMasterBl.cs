using FTS.Data.ApplicantLoginMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ApplicantLoginMaster
{
    public class ApplicantLoginMasterBl : IApplicantLoginMasterBl
    {
        private readonly IApplicantLoginMasterRepository _ApplicationLoginMasterRepository;
        public ApplicantLoginMasterBl(IApplicantLoginMasterRepository ApplicationLoginMasterRepositor)
        {
            this._ApplicationLoginMasterRepository = ApplicationLoginMasterRepositor;
        }
        public ApplicantMasterModel SaveApplicantLoginRecord(ApplicantMasterModel ObjReglogin)
        {
            var keyValuePairs = _ApplicationLoginMasterRepository.SaveApplicantLoginRecord(ObjReglogin);
            return keyValuePairs;
        }
    }
}
