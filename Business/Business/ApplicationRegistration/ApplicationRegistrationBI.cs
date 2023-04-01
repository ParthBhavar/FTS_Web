using FTS.Data.ApplicationRegistration;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ApplicationRegistration
{
    public class ApplicationRegistrationBI : IApplicationRegistrationBI
    {

        private readonly IApplicationRegistrationRepository _ApplicationRegistration;

        public ApplicationRegistrationBI(IApplicationRegistrationRepository ApplicationRegistration)
        {
            this._ApplicationRegistration = ApplicationRegistration;
        }

        public List<ApplicationRegistrationModel> RegistrationList(ApplicationRegistrationModel ClsAppliactionlst)
        {
            return _ApplicationRegistration.RegistrationList(ClsAppliactionlst);
        }

        public ApplicationRegistrationModel ApplicationRegistrationRecord(ApplicationRegistrationModel obj)
        {
            var keyValuePairs = _ApplicationRegistration.ApplicationRegistrationRecord(obj);
            return keyValuePairs;
        }

        public ApplicationRegistrationModel SaveEstablishmentRegistrationRecord(ApplicationRegistrationModel Obj)
        {
            var keyValuePairs = _ApplicationRegistration.SaveEstablishmentRegistrationRecord(Obj);
            return keyValuePairs;
        }

        public ApplicationRegistrationModel SavePrincipalEmployerRegistrationRecord(ApplicationRegistrationModel Obj)
        {
            var keyValuePairs = _ApplicationRegistration.SavePrincipalEmployerRegistrationRecord(Obj);
            return keyValuePairs;
        }

        public ApplicationRegistrationModel SaveContractorRegistrationRecord(ApplicationRegistrationModel Obj)
        {
            var keyValuePairs = _ApplicationRegistration.SaveContractorRegistrationRecord(Obj);
            return keyValuePairs;
        }

        public ApplicationRegistrationModel SaveDocumnetandapplication(ApplicationRegistrationModel Obj)
        {
            var keyValuePairs = _ApplicationRegistration.SaveDocumnetandapplication(Obj);
            return keyValuePairs;
        }

        public ApplicationRegistrationModel ApplicationRegistrationUpdateSubmit(ApplicationRegistrationModel Obj)
        {
            var keyValuePairs = _ApplicationRegistration.ApplicationRegistrationUpdateSubmit(Obj);
            return keyValuePairs;
        }

        public ApplicationRegistrationModel SendtoCommentFromACL(ApplicationRegistrationModel Obj)
        {
            var keyValuePairs = _ApplicationRegistration.SendtoCommentFromACL(Obj);
            return keyValuePairs;
        }
        
        public List<ApplicationRegistrationModel> ApplicationRegistrationAClList(ApplicationRegistrationModel ClsAppliactionlst)
        {
            return _ApplicationRegistration.ApplicationRegistrationAClList(ClsAppliactionlst);
        }

        public List<ApplicationRegistrationModel> ApplicationRegistrationHOAClList(ApplicationRegistrationModel ClsAppliactionlst)
        {
            return _ApplicationRegistration.ApplicationRegistrationHOAClList(ClsAppliactionlst);
        }

        public List<ApplicationRegistrationModel> ApplicationRegistrationHODCLList(ApplicationRegistrationModel ClsAppliactionlst)
        {
            return _ApplicationRegistration.ApplicationRegistrationHODCLList(ClsAppliactionlst);
        }

        public ApplicationRegistrationModel ApplicationApproveRejectFromACL(ApplicationRegistrationModel Obj)
        {
            var keyValuePairs = _ApplicationRegistration.ApplicationApproveRejectFromACL(Obj);
            return keyValuePairs;
        }

        public ApplicationRegistrationModel AppRegistrationSendtoHODCLFromACL(ApplicationRegistrationModel Obj)
        {
            var keyValuePairs = _ApplicationRegistration.AppRegistrationSendtoHODCLFromACL(Obj);
            return keyValuePairs;
        }

        public List<ApplicationRegistrationModel> ApplicationRegistrationAmendmentList(ApplicationRegistrationModel ClsAppliactionlst)
        {
            return _ApplicationRegistration.ApplicationRegistrationAmendmentList(ClsAppliactionlst);
        }
    }
}
