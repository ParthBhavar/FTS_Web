using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COL.Data.ApplicantMaster;
using COL.Model.Entities;
using Master.ViewModel;

namespace COL.Business.ApplicantMaster
{
    public class ApplicantMasterBl : IApplicantMasterBl
    {
        private readonly IApplicantMasterRepository _ApplicationMasterRepository;
        public ApplicantMasterBl(IApplicantMasterRepository ApplicationMasterRepositor)
        {
            this._ApplicationMasterRepository = ApplicationMasterRepositor;
        }
        public ApplicantMasterModel SaveUserRegisterRecord(ApplicantMasterModel ObjReg)
        {
            var keyValuePairs = _ApplicationMasterRepository.SaveUserRegisterRecord(ObjReg);
            return keyValuePairs;
        }
    } 
}
