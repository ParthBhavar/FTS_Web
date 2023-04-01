using FTS.Data.Inspection;
using FTS.Data.ReinstatementAppliaction;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.Inspection
{
    public class InspectionBI : IInspectionBI
    {
        private readonly IInspectionRepository _InspectionRepository;

        public InspectionBI(IInspectionRepository InspectionRepository)
        {
            this._InspectionRepository = InspectionRepository;
        }

        public List<inspectionModel> InspectionList(inspectionModel model)
        {
            return _InspectionRepository.InspectionList(model);
        }

        public inspectionModel InspectionApplicationRecord(inspectionModel model)
        {
            var keyValuePairs = _InspectionRepository.InspectionApplicationRecord(model);
            return keyValuePairs;
        }

        public inspectionModel SaveReportInformationRecord(inspectionModel model)
        {
            var keyValuePairs = _InspectionRepository.SaveReportInformationRecord(model);
            return keyValuePairs;
        }

        public inspectionModel SaveEstablishmentsRecord(inspectionModel model)
        {
            var keyValuePairs = _InspectionRepository.SaveEstablishmentsRecord(model);
            return keyValuePairs;
        }

        public inspectionModel SaveEstablishmentsDetailRecord(inspectionModel model)
        {
            var keyValuePairs = _InspectionRepository.SaveEstablishmentsDetailRecord(model);
            return keyValuePairs;
        }

        public inspectionModel SaveCompanyDetailsRecord(inspectionModel model)
        {
            var keyValuePairs = _InspectionRepository.SaveCompanyDetailsRecord(model);
            return keyValuePairs;
        }

        public inspectionModel SaveEmployeeDetailsRecord(inspectionModel model)
        {
            var keyValuePairs = _InspectionRepository.SaveEmployeeDetailsRecord(model);
            return keyValuePairs;
        }

        public inspectionModel SaveContractorDetailsRecord(inspectionModel model)
        {
            var keyValuePairs = _InspectionRepository.SaveContractorDetailsRecord(model);
            return keyValuePairs;
        }
        public inspectionModel SaveInspectionOnsitePicture(inspectionModel model)
        {
            var keyValuePairs = _InspectionRepository.SaveInspectionOnsitePicture(model);
            return keyValuePairs;
        }
        public inspectionModel SaveOtherDetailsRecord(inspectionModel model)
        {
            var keyValuePairs = _InspectionRepository.SaveOtherDetailsRecord(model);
            return keyValuePairs;
        }
    }
}
