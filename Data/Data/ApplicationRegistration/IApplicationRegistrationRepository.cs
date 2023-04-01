using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ApplicationRegistration
{
    public interface IApplicationRegistrationRepository
    {
        List<ApplicationRegistrationModel> RegistrationList(ApplicationRegistrationModel ClsAppliactionlst);
        ApplicationRegistrationModel ApplicationRegistrationRecord(ApplicationRegistrationModel obj);
        ApplicationRegistrationModel SaveEstablishmentRegistrationRecord(ApplicationRegistrationModel Obj);
        ApplicationRegistrationModel SavePrincipalEmployerRegistrationRecord(ApplicationRegistrationModel Obj);
        ApplicationRegistrationModel SaveContractorRegistrationRecord(ApplicationRegistrationModel Obj);
        ApplicationRegistrationModel SaveDocumnetandapplication(ApplicationRegistrationModel Obj);
        ApplicationRegistrationModel ApplicationRegistrationUpdateSubmit(ApplicationRegistrationModel Obj);
        List<ApplicationRegistrationModel> ApplicationRegistrationAClList(ApplicationRegistrationModel model);
        List<ApplicationRegistrationModel> ApplicationRegistrationHOAClList(ApplicationRegistrationModel model);
        List<ApplicationRegistrationModel> ApplicationRegistrationHODCLList(ApplicationRegistrationModel model);
        ApplicationRegistrationModel SendtoCommentFromACL(ApplicationRegistrationModel model);
        ApplicationRegistrationModel ApplicationApproveRejectFromACL(ApplicationRegistrationModel Obj);
        ApplicationRegistrationModel AppRegistrationSendtoHODCLFromACL(ApplicationRegistrationModel Obj);
        List<ApplicationRegistrationModel> ApplicationRegistrationAmendmentList(ApplicationRegistrationModel ClsAppliactionlst);
    }
}
