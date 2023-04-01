using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.NFormApplication
{
    public interface INFormApplicationRepository
    {
        List<NFormApplicationModel> INFormList(NFormApplicationModel model);
        NFormApplicationModel NFormAppliactionRecord(int AppliactionID);
        NFormApplicationModel SaveNFormAppliactionRecord(NFormApplicationModel Obj);
        List<NFormApplicationModel> NFormEstablishmentRecord(int AppliactionID, int ISNew);
        NFormApplicationModel SaveNFormEstablishmentRecord(NFormApplicationModel Obj);
        NFormApplicationModel NFormAppliactionDocumentURLRecord(int AppliactionID);
        NFormApplicationModel SaveDocumnetandapplication(NFormApplicationModel Obj);
        NFormApplicationModel NFormAppliactionUpdateSubmit(NFormApplicationModel Obj);
        List<NFormApplicationModel> NFormACLList(NFormApplicationModel model);
        NFormApplicationModel NFormACLRecord(int AppliactionID, int ISNew);
        NFormApplicationModel SaveNFormHearingACLRecord(NFormApplicationModel Obj);
        NFormApplicationModel SaveNFormSettlementrecord(NFormApplicationModel Obj);
        NFormApplicationModel NFormHistory(NFormApplicationModel Obj);
        NFormApplicationModel SendtoACLFromClerk(NFormApplicationModel Obj);
        NFormApplicationModel SendReviewtoACL(NFormApplicationModel Obj);
        NFormApplicationModel SendtoCommentFromClerk(NFormApplicationModel Obj);
        NFormApplicationModel UpdateEshtablishmentAddressdetailFromACL(NFormApplicationModel Obj);
        NFormApplicationModel ApproveOrRejectReviewFromACL(NFormApplicationModel Obj);
        NFormApplicationModel TFormRecoveryCerti(NFormApplicationModel Obj);
    }
}
