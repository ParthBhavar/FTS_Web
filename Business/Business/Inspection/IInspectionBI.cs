using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.Inspection
{
    public interface IInspectionBI
    {
        List<inspectionModel> InspectionList(inspectionModel model);
        inspectionModel InspectionApplicationRecord(inspectionModel obj);
        inspectionModel SaveReportInformationRecord(inspectionModel Obj);
        inspectionModel SaveEstablishmentsRecord(inspectionModel Obj);
        inspectionModel SaveEstablishmentsDetailRecord(inspectionModel Obj);
        inspectionModel SaveCompanyDetailsRecord(inspectionModel Obj);
        inspectionModel SaveEmployeeDetailsRecord(inspectionModel Obj);
        inspectionModel SaveContractorDetailsRecord(inspectionModel Obj);
        inspectionModel SaveInspectionOnsitePicture(inspectionModel Obj);
        inspectionModel SaveOtherDetailsRecord(inspectionModel Obj);
    }
}
