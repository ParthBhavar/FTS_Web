using FTS.Data.TFormApplication;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.TFormApplication
{
    public class TFormApplicationBl :ITFormApplicationBl
    {
        private readonly ITFormApplicationRepository _tFormApplicationRepository;
        public TFormApplicationBl(ITFormApplicationRepository TFormApplicationRepository)
        {
            this._tFormApplicationRepository = TFormApplicationRepository;
        }
        public List<TFormAppealApplicationModel> TFormApplicationList(TFormAppealApplicationModel model)
        {
            return _tFormApplicationRepository.TFormApplicationList(model);
        }
        public TFormAppealApplicationModel TFormForEmployeeRecord(int AppealID)
        {
            var keyValuePairs = _tFormApplicationRepository.TFormForEmployeeRecord(AppealID);
            return keyValuePairs;
        }
        public TFormAppealApplicationModel TFormForEmployerRecord(int AppealID)
        {
            var keyValuePairs = _tFormApplicationRepository.TFormForEmployerRecord(AppealID);
            return keyValuePairs;
        }
        public TFormAppealApplicationModel SaveTFormForEmployee(TFormAppealApplicationModel Obj)
        {
            var keyValuePairs = _tFormApplicationRepository.SaveTFormForEmployee(Obj);
            return keyValuePairs;
        }
        public List<TFormAppealApplicationModel> TFormApplicationDCLList(TFormAppealApplicationModel model)
        {
            return _tFormApplicationRepository.TFormApplicationDCLList(model);
        }
        public TFormAppealApplicationModel TFormDCLStatusUpdate(int AppealID, int ISNew)
        {
            var keyValuePairs = _tFormApplicationRepository.TFormDCLStatusUpdate(AppealID, ISNew);
            return keyValuePairs;
        }
        public TFormAppealApplicationModel SaveTFormHearingDCLRecord(TFormAppealApplicationModel Obj)
        {
            var keyValuePairs = _tFormApplicationRepository.SaveTFormHearingDCLRecord(Obj);
            return keyValuePairs;
        }
        public TFormAppealApplicationModel SaveTFormResolutionBackDCLRecord(TFormAppealApplicationModel Obj)
        {
            var keyValuePairs = _tFormApplicationRepository.SaveTFormResolutionBackDCLRecord(Obj);
            return keyValuePairs;
        }
        public TFormAppealApplicationModel SaveTFormConfirmDCLRecord(TFormAppealApplicationModel Obj)
        {
            var keyValuePairs = _tFormApplicationRepository.SaveTFormConfirmDCLRecord(Obj);
            return keyValuePairs;
        }
        public TFormAppealApplicationModel SaveTFormDismissDCLRecord(TFormAppealApplicationModel Obj)
        {
            var keyValuePairs = _tFormApplicationRepository.SaveTFormDismissDCLRecord(Obj);
            return keyValuePairs;
        }
        public TFormAppealApplicationModel TFormDCLHistory(TFormAppealApplicationModel Obj)
        {
            var keyValuePairs = _tFormApplicationRepository.TFormDCLHistory(Obj);
            return keyValuePairs;
        }
        public TFormAppealApplicationModel SaveTFormForEmployer(TFormAppealApplicationModel Obj)
        {
            var keyValuePairs = _tFormApplicationRepository.SaveTFormForEmployer(Obj);
            return keyValuePairs;
        }
        public TFormAppealApplicationModel TFormACLRecord(int AppliactionID, int ISNew)
        {
            var keyValuePairs = _tFormApplicationRepository.TFormACLRecord(AppliactionID, ISNew);
            return keyValuePairs;
        }
    }
}
