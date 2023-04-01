using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.LicenceApplication
{
    public interface ILicenceApplicationBl
    {
        List<LicenceApplicationModel> LicenceIMWAppList(LicenceApplicationModel model);
        public LicenceApplicationModel AppliactionRecord(LicenceApplicationModel obj);
        LicenceApplicationModel SaveContractorAppliactionRecord(LicenceApplicationModel Obj);
        LicenceApplicationModel SaveEstablishmentLicenceRecord(LicenceApplicationModel Obj);
        LicenceApplicationModel SavePrincipalEmployerLicenceRecord(LicenceApplicationModel Obj);
        LicenceApplicationModel SaveLicenceAndSecurityRecord(LicenceApplicationModel Obj);
        LicenceApplicationModel SaveDocumnetandapplication(LicenceApplicationModel Obj);
        List<LicenceApplicationModel> LicenceACLList(LicenceApplicationModel model);
        LicenceApplicationModel ApproveOrRejectLicenceByACL(LicenceApplicationModel Obj);
        LicenceApplicationModel LicenceQueryByACL(LicenceApplicationModel Obj);
        LicenceApplicationModel LicenceQueryByDCLHO(LicenceApplicationModel Obj);
        
        List<LicenceApplicationModel> LicenceRegistrationDCLList(LicenceApplicationModel model);
        List<LicenceApplicationModel> LicenceApplicationHOList(LicenceApplicationModel model);
        LicenceApplicationModel LicenceApproveRejectByACL(LicenceApplicationModel Obj);
        LicenceApplicationModel LicenceAppForwardToDCLHO(LicenceApplicationModel Obj);
        LicenceApplicationModel LicenceApplicationUpdateSubmit(LicenceApplicationModel Obj);
        List<LicenceApplicationModel> LicenceAmendmentList(LicenceApplicationModel ClsAppliactionlst);
    }
}
