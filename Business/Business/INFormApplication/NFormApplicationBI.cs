using FTS.Data.NFormApplication;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.INFormApplication
{
    public class NFormApplicationBI : INFormApplicationBI
    {


        private readonly INFormApplicationRepository _NFormApplicationRepository;

        public NFormApplicationBI(INFormApplicationRepository NFormApplicationRepository)
        {
            this._NFormApplicationRepository = NFormApplicationRepository;
        }

        public List<NFormApplicationModel> INFormList(NFormApplicationModel model)
        {
            return _NFormApplicationRepository.INFormList(model);
        }

        public NFormApplicationModel NFormAppliactionRecord(int AppliactionID)
        {
            var keyValuePairs = _NFormApplicationRepository.NFormAppliactionRecord(AppliactionID);
            return keyValuePairs;
        }


        public NFormApplicationModel SaveNFormAppliactionRecord(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.SaveNFormAppliactionRecord(Obj);
            return keyValuePairs;
        }


        public List<NFormApplicationModel> NFormEstablishmentRecord(int AppliactionID, int ISNew)
        {
            return _NFormApplicationRepository.NFormEstablishmentRecord(AppliactionID, ISNew);
        }

        public NFormApplicationModel SaveNFormEstablishmentRecord(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.SaveNFormEstablishmentRecord(Obj);
            return keyValuePairs;
        }

        public NFormApplicationModel NFormAppliactionDocumentURLRecord(int AppliactionID)
        {
            var keyValuePairs = _NFormApplicationRepository.NFormAppliactionDocumentURLRecord(AppliactionID);
            return keyValuePairs;
        }

        public NFormApplicationModel SaveDocumnetandapplication(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.SaveDocumnetandapplication(Obj);
            return keyValuePairs;
        }

        public NFormApplicationModel NFormAppliactionUpdateSubmit(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.NFormAppliactionUpdateSubmit(Obj);
            return keyValuePairs;
        }
        
        public List<NFormApplicationModel> NFormACLList(NFormApplicationModel model)
        {
            return _NFormApplicationRepository.NFormACLList(model);
        }

        public NFormApplicationModel NFormACLRecord(int AppliactionID, int ISNew)
        {
            var keyValuePairs = _NFormApplicationRepository.NFormACLRecord(AppliactionID, ISNew);
            return keyValuePairs;
        }

        public NFormApplicationModel SaveNFormHearingACLRecord(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.SaveNFormHearingACLRecord(Obj);
            return keyValuePairs;
        }
        public NFormApplicationModel SaveNFormSettlementrecord(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.SaveNFormSettlementrecord(Obj);
            return keyValuePairs;
        }

        public NFormApplicationModel NFormHistory(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.NFormHistory(Obj);
            return keyValuePairs;
        }

        public NFormApplicationModel SendtoACLFromClerk(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.SendtoACLFromClerk(Obj);
            return keyValuePairs;
        }
        public NFormApplicationModel SendReviewtoACL(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.SendReviewtoACL(Obj);
            return keyValuePairs;
        }

        public NFormApplicationModel SendtoCommentFromClerk(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.SendtoCommentFromClerk(Obj);
            return keyValuePairs;
        }

        public NFormApplicationModel UpdateEshtablishmentAddressdetailFromACL(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.UpdateEshtablishmentAddressdetailFromACL(Obj);
            return keyValuePairs;
        }

        public NFormApplicationModel ApproveOrRejectReviewFromACL(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.ApproveOrRejectReviewFromACL(Obj);
            return keyValuePairs; 
        }
        public NFormApplicationModel TFormRecoveryCerti(NFormApplicationModel Obj)
        {
            var keyValuePairs = _NFormApplicationRepository.TFormRecoveryCerti(Obj);
            return keyValuePairs;
        }

    }
}
