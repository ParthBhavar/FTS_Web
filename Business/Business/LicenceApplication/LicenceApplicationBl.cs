using FTS.Data.LicenceApplication;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.LicenceApplication
{
    public class LicenceApplicationBl :ILicenceApplicationBl
    {
        private readonly ILicenceApplicationRepository _licenceIMWAppRepo;

        public LicenceApplicationBl(ILicenceApplicationRepository LicenceIMWApplication)
        {
            this._licenceIMWAppRepo = LicenceIMWApplication;

        }
        public List<LicenceApplicationModel> LicenceIMWAppList(LicenceApplicationModel model)
        {
            return _licenceIMWAppRepo.LicenceAppList(model);
        }
        public LicenceApplicationModel AppliactionRecord(LicenceApplicationModel obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.AppliactionRecord(obj);
            return keyValuePairs;
        }
        public LicenceApplicationModel SaveContractorAppliactionRecord(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.SaveContractorAppliactionRecord(Obj);
            return keyValuePairs;
        }
        public LicenceApplicationModel SaveEstablishmentLicenceRecord(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.SaveEstablishmentLicenceRecord(Obj);
            return keyValuePairs;
        }
        public LicenceApplicationModel SavePrincipalEmployerLicenceRecord(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.SavePrincipalEmployerLicenceRecord(Obj);
            return keyValuePairs;
        }
        public LicenceApplicationModel SaveLicenceAndSecurityRecord(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.SaveLicenceAndSecurityRecord(Obj);
            return keyValuePairs;
        }
        public LicenceApplicationModel SaveDocumnetandapplication(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.SaveDocumnetandapplication(Obj);
            return keyValuePairs;
        }
        public List<LicenceApplicationModel> LicenceACLList(LicenceApplicationModel model)
        {
            return _licenceIMWAppRepo.LicenceACLList(model);
        }
        public LicenceApplicationModel ApproveOrRejectLicenceByACL(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.ApproveOrRejectLicenceByACL(Obj);
            return keyValuePairs;
        }
        public LicenceApplicationModel LicenceQueryByACL(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.LicenceQueryByACL(Obj);
            return keyValuePairs;
        }
        public LicenceApplicationModel LicenceQueryByDCLHO(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.LicenceQueryByDCLHO(Obj);
            return keyValuePairs;
        }
        public List<LicenceApplicationModel> LicenceRegistrationDCLList(LicenceApplicationModel ClsAppliactionlst)
        {
            return _licenceIMWAppRepo.LicenceRegistrationDCLList(ClsAppliactionlst);
        }
        public List<LicenceApplicationModel> LicenceApplicationHOList(LicenceApplicationModel ClsAppliactionlst)
        {
            return _licenceIMWAppRepo.LicenceApplicationHOList(ClsAppliactionlst);
        }
        public LicenceApplicationModel LicenceApproveRejectByACL(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.LicenceApproveRejectByACL(Obj);
            return keyValuePairs;
        }
        public LicenceApplicationModel LicenceAppForwardToDCLHO(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.LicenceAppForwardToDCLHO(Obj);
            return keyValuePairs;
        }
        public LicenceApplicationModel LicenceApplicationUpdateSubmit(LicenceApplicationModel Obj)
        {
            var keyValuePairs = _licenceIMWAppRepo.LicenceApplicationUpdateSubmit(Obj);
            return keyValuePairs;
        }
        public List<LicenceApplicationModel> LicenceAmendmentList(LicenceApplicationModel ClsAppliactionlst)
        {
            return _licenceIMWAppRepo.LicenceAmendmentList(ClsAppliactionlst);
        }
    }
}
