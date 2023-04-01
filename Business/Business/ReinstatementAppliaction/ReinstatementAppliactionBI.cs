using FTS.Data.ReinstatementAppliaction;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ReinstatementAppliaction
{
    public class ReinstatementAppliactionBI : IReinstatementAppliactionBI
    {  

        private readonly IReinstatementAppliactionRepository _ReinstatemenRepository;

        public ReinstatementAppliactionBI(IReinstatementAppliactionRepository ReinstatRepository)
        {
            this._ReinstatemenRepository = ReinstatRepository;
        }

        //public List<MRoleMaster> RoleList(PaginationRequest model)
        //{
        //    return  _RoleMasterRepository.RoleList(model);
        //}

        //public List<ReinstatementAppliactionModel> ReinstatementList()
        //{
        //    return _ReinstatemenRepository.ReinstatementList();
        //}

        public List<ReinstatementAppliactionModel> ReinstatementList(ReinstatementAppliactionModel model)
        {
            return _ReinstatemenRepository.ReinstatementList(model);
        }

        public ReinstatementAppliactionModel AppliactionRecord(int AppliactionID)
        {
            var keyValuePairs = _ReinstatemenRepository.AppliactionRecord(AppliactionID);
            return keyValuePairs;
        }

        public ReinstatementAppliactionModel SaveAppliactionRecord(ReinstatementAppliactionModel Obj)
        {
            var keyValuePairs = _ReinstatemenRepository.SaveAppliactionRecord(Obj);
            return keyValuePairs;
        }

        public ReinstatementAppliactionModel ReinstatementTradunionRecord(int AppliactionID, int ISNew)
        {
            var keyValuePairs = _ReinstatemenRepository.ReinstatementTradunionRecord(AppliactionID, ISNew);
            return keyValuePairs;
        }

        public ReinstatementAppliactionModel SaveReinstatementTradunionRecord(ReinstatementAppliactionModel Obj)
        {
            var keyValuePairs = _ReinstatemenRepository.SaveReinstatementTradunionRecord(Obj);
            return keyValuePairs;
        }

        //public ReinstatementAppliactionModel ReiniEstablishmentRecord(int AppliactionID, int ISNew)
        //{
        //    var keyValuePairs = _ReinstatemenRepository.ReiniEstablishmentRecord(AppliactionID, ISNew);
        //    return keyValuePairs;
        //}


        public List<ReinstatementAppliactionModel> ReiniEstablishmentRecord(int AppliactionID, int ISNew)
        {
            return  _ReinstatemenRepository.ReiniEstablishmentRecord(AppliactionID, ISNew);
        }

        public ReinstatementAppliactionModel SaveReiniEstablishmentRecord(ReinstatementAppliactionModel Obj)
        {
            var keyValuePairs = _ReinstatemenRepository.SaveReiniEstablishmentRecord(Obj);
            return keyValuePairs;
        }

        public ReinstatementAppliactionModel ReinAppliactionDocumentURLRecord(int AppliactionID)
        {
            var keyValuePairs = _ReinstatemenRepository.ReinAppliactionDocumentURLRecord(AppliactionID);
            return keyValuePairs;
        }

        public ReinstatementAppliactionModel SaveDocumnetandapplication(ReinstatementAppliactionModel Obj)
        {
            var keyValuePairs = _ReinstatemenRepository.SaveDocumnetandapplication(Obj);
            return keyValuePairs;
        }

        public ReinstatementAppliactionModel SaveReinstatementHearingACOLRecord(ReinstatementAppliactionModel Obj)
        {
            var keyValuePairs = _ReinstatemenRepository.SaveReinstatementHearingACOLRecord(Obj);
            return keyValuePairs;
        }

        public ReinstatementAppliactionModel SaveReinstatementACOLSettlementrecord(ReinstatementAppliactionModel Obj)
        {
            var keyValuePairs = _ReinstatemenRepository.SaveReinstatementACOLSettlementrecord(Obj);
            return keyValuePairs;
        }

          public ReinstatementAppliactionModel SaveReinstatementSendtolabourACOLRecord(ReinstatementAppliactionModel Obj)
        {
            var keyValuePairs = _ReinstatemenRepository.SaveReinstatementSendtolabourACOLRecord(Obj);
            return keyValuePairs;
        }

        public ReinstatementAppliactionModel ReinstatementAppliactionUpdateSubmit(ReinstatementAppliactionModel Obj)
        {
            var keyValuePairs = _ReinstatemenRepository.ReinstatementAppliactionUpdateSubmit(Obj);
            return keyValuePairs;
        }

        public ReinstatementAppliactionModel ReinstatementHistory(ReinstatementAppliactionModel Obj)
        {
            var keyValuePairs =  _ReinstatemenRepository.ReinstatementHistory(Obj);
            return keyValuePairs;
        }

        public ReinstatementAppliactionModel ReinstatementACOLRecord(int AppliactionID, int ISNew)
        {
            var keyValuePairs = _ReinstatemenRepository.ReinstatementACOLRecord(AppliactionID, ISNew);
            return keyValuePairs;
        }

        public List<ReinstatementAppliactionModel> ReinstatementACOLList(ReinstatementAppliactionModel model)
        {
            return _ReinstatemenRepository.ReinstatementACOLList(model);
        }
    }
}
