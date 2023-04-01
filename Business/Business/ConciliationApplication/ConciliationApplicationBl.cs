using FTS.Data.ConciliationApplication;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ConciliationApplication
{
    public class ConciliationApplicationBl : IConciliationApplicationBl
    {
        private readonly IConciliationApplicationRepository _conciliationApplication;

        public ConciliationApplicationBl(IConciliationApplicationRepository ConciliationApplication)
        {
            this._conciliationApplication = ConciliationApplication;

        }

        public List<ConciliationApplicationModel> ConciliationList(ConciliationApplicationModel model)
        {
            return _conciliationApplication.ConciliationList(model);
        }
        public ConciliationApplicationModel AppliactionRecord(int ApplicationID)
        {
            var keyValuePairs = _conciliationApplication.AppliactionRecord(ApplicationID);
            return keyValuePairs;
        }

        public ConciliationApplicationModel SaveAppliactionRecord(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.SaveAppliactionRecord(Obj);
            return keyValuePairs;
        }

        public List<ConciliationApplicationModel> ConcilEstablishmentRecord(int AppliactionID, int ISNew)
        {
            return _conciliationApplication.ConcilEstablishmentRecord(AppliactionID, ISNew);
        }
        public ConciliationApplicationModel SaveConcilEstablishmentRecord(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.SaveConcilEstablishmentRecord(Obj);
            return keyValuePairs;
        }

        public ConciliationApplicationModel SaveConciliationTradunionRecord(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.SaveConciliationTradunionRecord(Obj);
            return keyValuePairs;
        }
        public ConciliationApplicationModel SaveDocumnetandapplication(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.SaveDocumnetandapplication(Obj);
            return keyValuePairs;
        }
        public ConciliationApplicationModel ConcilAppliactionDocumentURLRecord(int AppliactionID)
        {
            var keyValuePairs = _conciliationApplication.ConcilAppliactionDocumentURLRecord(AppliactionID);
            return keyValuePairs;
        }
        public List<ConciliationApplicationModel> ConciliationTradunionRecord(int AppliactionID, int ISNew)
        {
            return _conciliationApplication.ConciliationTradunionRecord(AppliactionID, ISNew);
            //var keyValuePairs = _conciliationApplication.ConciliationTradunionRecord(AppliactionID, ISNew);
            //return keyValuePairs;
        }
        public ConciliationApplicationModel ConciliationAppliactionUpdateSubmit(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.ConciliationAppliactionUpdateSubmit(Obj);
            return keyValuePairs;
        }
        public List<ConciliationApplicationModel> ConciliationACLList(ConciliationApplicationModel model)
        {
            return _conciliationApplication.ConciliationACLList(model);
        }
        public ConciliationApplicationModel ConciliationACLRecord(int ApplicationID, int ISNew)
        {
            var keyValuePairs = _conciliationApplication.ConciliationACLRecord(ApplicationID,  ISNew);
            return keyValuePairs;
        }
        public ConciliationApplicationModel SaveConciliationHearingACLRecord(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.SaveConciliationHearingACLRecord(Obj);
            return keyValuePairs;
        }
        public ConciliationApplicationModel SaveConciliationSendtolabourACLRecord(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.SaveConciliationSendtolabourACLRecord(Obj);
            return keyValuePairs;
        }
        public ConciliationApplicationModel SaveConciliationACLSettlementrecord(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.SaveConciliationACLSettlementrecord(Obj);
            return keyValuePairs;
        }
        public ConciliationApplicationModel ConciliationHistory(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.ConciliationHistory(Obj);
            return keyValuePairs;
        }
        public ConciliationApplicationModel ConcilupdateClerkStatustoACL(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.ConcilupdateClerkStatustoACL(Obj);
            return keyValuePairs;
        }
        public ConciliationApplicationModel UpdateQueryByACLClerk(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.UpdateQueryByACLClerk(Obj);
            return keyValuePairs;
        }
        public List<ConciliationApplicationModel> ConciliationDCLClerkList(ConciliationApplicationModel model)
        {
            return _conciliationApplication.ConciliationDCLClerkList(model);
        }
        public ConciliationApplicationModel ConcilupdateACLClerk(string id)
        {
            var keyValuePairs = _conciliationApplication.ConcilupdateACLClerk(id);
            return keyValuePairs;
        }
        public ConciliationApplicationModel UpdateStatusByDCLClerk(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.UpdateStatusByDCLClerk(Obj);
            return keyValuePairs;
        }
        public ConciliationApplicationModel ConciliationDCLClerkRecord(int ApplicationID)
        {
            var keyValuePairs = _conciliationApplication.ConciliationDCLClerkRecord(ApplicationID);
            return keyValuePairs;
        }
        public List<ConciliationApplicationModel> ConciliationDCLList(ConciliationApplicationModel model)
        {
            return _conciliationApplication.ConciliationDCLList(model);
        }
        public ConciliationApplicationModel ConciliationDCLRecord(int ApplicationID)
        {
            var keyValuePairs = _conciliationApplication.ConciliationDCLRecord(ApplicationID);
            return keyValuePairs;
        }
        public ConciliationApplicationModel UpdateStatusByDCL(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.UpdateStatusByDCL(Obj);
            return keyValuePairs;
        }
        public List<ConciliationApplicationModel> ConciliationHOClerkList(ConciliationApplicationModel model)
        {
            return _conciliationApplication.ConciliationHOClerkList(model);
        }
        public ConciliationApplicationModel ConciliationHOClerkRecord(int ApplicationID)
        {
            var keyValuePairs = _conciliationApplication.ConciliationHOClerkRecord(ApplicationID);
            return keyValuePairs;
        }
        public ConciliationApplicationModel ConcilupdateHOClerkStatus(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.ConcilupdateHOClerkStatus(Obj);
            return keyValuePairs;
        }
        public List<ConciliationApplicationModel> ConciliationHOList(ConciliationApplicationModel model)
        {
            return _conciliationApplication.ConciliationHOList(model);
        }

        public ConciliationApplicationModel ConciliationHORecord(int ApplicationID)
        {
            var keyValuePairs = _conciliationApplication.ConciliationHORecord(ApplicationID);
            return keyValuePairs;
        }
        public ConciliationApplicationModel UpdateStatusByHO(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.UpdateStatusByHO(Obj);
            return keyValuePairs;
        }
        public ConciliationApplicationModel UpdateQueryByHOClerk(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.UpdateQueryByHOClerk(Obj);
            return keyValuePairs;
        }
        public ConciliationApplicationModel UpdateEshtablishmentdetailByACL(ConciliationApplicationModel Obj)
        {
            var keyValuePairs = _conciliationApplication.UpdateEshtablishmentdetailByACL(Obj);
            return keyValuePairs;
        }

    }
}
