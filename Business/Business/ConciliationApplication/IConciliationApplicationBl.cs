using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ConciliationApplication
{
    public interface IConciliationApplicationBl
    {
        List<ConciliationApplicationModel> ConciliationList(ConciliationApplicationModel model);
        public ConciliationApplicationModel AppliactionRecord(int ApplicationID);
        public ConciliationApplicationModel SaveAppliactionRecord(ConciliationApplicationModel Obj);
        List<ConciliationApplicationModel> ConciliationTradunionRecord(int AppliactionID, int ISNew);
        List<ConciliationApplicationModel> ConcilEstablishmentRecord(int AppliactionID, int ISNew);
        ConciliationApplicationModel SaveConcilEstablishmentRecord(ConciliationApplicationModel Obj);
        ConciliationApplicationModel SaveConciliationTradunionRecord(ConciliationApplicationModel Obj);
        ConciliationApplicationModel SaveDocumnetandapplication(ConciliationApplicationModel Obj);
        ConciliationApplicationModel ConcilAppliactionDocumentURLRecord(int AppliactionID);
        ConciliationApplicationModel ConciliationAppliactionUpdateSubmit(ConciliationApplicationModel Obj);
        List<ConciliationApplicationModel> ConciliationACLList(ConciliationApplicationModel model);
        ConciliationApplicationModel ConciliationACLRecord(int ApplicationID, int ISNew);
        ConciliationApplicationModel SaveConciliationHearingACLRecord(ConciliationApplicationModel Obj);
        ConciliationApplicationModel SaveConciliationSendtolabourACLRecord(ConciliationApplicationModel Obj);
        ConciliationApplicationModel SaveConciliationACLSettlementrecord(ConciliationApplicationModel Obj);
        ConciliationApplicationModel ConciliationHistory(ConciliationApplicationModel Obj);
        ConciliationApplicationModel ConcilupdateClerkStatustoACL(ConciliationApplicationModel Obj);
        ConciliationApplicationModel UpdateQueryByACLClerk(ConciliationApplicationModel Obj);
        List<ConciliationApplicationModel> ConciliationDCLClerkList(ConciliationApplicationModel model);
        ConciliationApplicationModel ConcilupdateACLClerk(string id);
        ConciliationApplicationModel UpdateStatusByDCLClerk(ConciliationApplicationModel Obj);
        ConciliationApplicationModel ConciliationDCLClerkRecord(int ApplicationID );
        List<ConciliationApplicationModel> ConciliationDCLList(ConciliationApplicationModel model);
        ConciliationApplicationModel ConciliationDCLRecord(int ApplicationID);
        ConciliationApplicationModel UpdateStatusByDCL(ConciliationApplicationModel Obj);
        List<ConciliationApplicationModel> ConciliationHOClerkList(ConciliationApplicationModel model);
        ConciliationApplicationModel ConciliationHOClerkRecord(int ApplicationID);
        ConciliationApplicationModel ConcilupdateHOClerkStatus(ConciliationApplicationModel Obj);
        List<ConciliationApplicationModel> ConciliationHOList(ConciliationApplicationModel model);
        ConciliationApplicationModel ConciliationHORecord(int ApplicationID);
        ConciliationApplicationModel UpdateStatusByHO(ConciliationApplicationModel Obj);
        ConciliationApplicationModel UpdateQueryByHOClerk(ConciliationApplicationModel Obj);
        ConciliationApplicationModel UpdateEshtablishmentdetailByACL(ConciliationApplicationModel Obj);

    }
}
