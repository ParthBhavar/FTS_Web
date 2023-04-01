using FTS.Data.Common;
using FTS.Model.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.Inspection
{
    public class InspectionRepository : IInspectionRepository
    {
        #region Private Variables
        private readonly IRepository<inspectionModel> _InspectionRepository;
        #endregion

        #region Constructor
        public InspectionRepository(IRepository<inspectionModel> InspectionRepository)
        {
            _InspectionRepository = InspectionRepository;
        }
        #endregion

        public List<inspectionModel> InspectionList(inspectionModel model)
        {
            try
            {
                List<inspectionModel> lstInspection = new List<inspectionModel>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _InspectionRepository.QueryMultipleByProcedure(SPConstants.GetInspectionList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstInspection = result1.Select(x => new inspectionModel
                    {
                        ID = (int)x.ID,
                        InspectionID = (int)x.InspectionID,
                        ReportNo = (string)x.ReportNo,
                        InspectionDatee = (string)x.InspectionDate,
                        ComplainName = (string)x.ComplainName,
                        ComplainSubject = (string)x.ComplainSubject,
                        IsSubmit = Convert.ToBoolean(x.IsSubmit),

                    }).ToList();
                };
                return lstInspection;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public inspectionModel InspectionApplicationRecord(inspectionModel obj)
        {
            List<inspectionModel> inspectionList = new List<inspectionModel>();
            List<inspectionModel> EstablishmentsDetailsList = new List<inspectionModel>();
            List<inspectionModel> EmployeeDetailsList = new List<inspectionModel>();
            List<inspectionModel> ContractorDetailsList = new List<inspectionModel>();
            List<MCommonList> lstDesignation = new List<MCommonList>();
            List<MCommonList> lstYesNo = new List<MCommonList>();
            List<MCommonList> EstablishmenttypeList = new List<MCommonList>();
            List<MCommonList> EstablishmentDetailstypeList = new List<MCommonList>();
            List<inspectionModel> DocumentList = new List<inspectionModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", obj.UserID);
            param.Add("@p_UserMode", obj.UserMode);
            param.Add("@p_InspectionID", obj.InspectionID);

            var keyValuePairs = _InspectionRepository.QueryMultipleByProcedure(SPConstants.GetRecordInspectionApplication, param);
            var response = new inspectionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new inspectionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    InspectionID = (int)x.InspectionID,
                    //RefRegistrationID = (int)x.RefRegistrationID,

                }).FirstOrDefault();
            };

            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                inspectionList = result2.Select(x => new inspectionModel
                {

                    InspectionID = (int)x.InspectionID,
                    ReportNo = (string)x.ReportNo,
                    InspectionDate = Convert.ToDateTime(x.InspectionDate),
                    IFPApplicationNo = (string)x.IFPApplicationNo,
                    InspectionType = (int)x.InspectionType,
                    ComplainDate = Convert.ToDateTime(x.ComplainDate),
                    ComplainSubject = (string)x.ComplainSubject,
                    ComplainName = (string)x.ComplainName,
                    haveContractors = Convert.ToBoolean(x.haveContractors),
                    AuthorizedPersonName = (string)x.AuthorizedPersonName,
                    AuthorizedPersonEmail = (string)x.AuthorizedPersonEmail,
                    AuthorizedPersonContactNo = (string)x.AuthorizedPersonContactNo,
                    AuthorizedPersonDesignation = (string)x.AuthorizedPersonDesignation,
                    IsAuthorizedPersonSignature = (int)x.IsAuthorizedPersonSignature,
                    AuthorizedSignPath = (string)x.AuthorizedSignPath,
                    AuthorizedPersonSignatureNote = (string)x.AuthorizedPersonSignatureNote,
                    IsNonComplianceObserved = (int)x.IsNonComplianceObserved,
                    ApplicationStatus = (int)x.ApplicationStatus,
                    InspectorName = (string)x.InspectorName,
                    //Isverified = (int)x.Isverified,
                    InspectorEmail = (string)x.InspectorEmail,
                    InspectorSignPath = (string)x.InspectorSignPath,
                    IsComplianceDocument = (int)x.IsComplianceDocument,
                    ComplianceDocumentReceivedby = (string)x.ComplianceDocumentReceivedby,
                    ComplianceSignPath = (string)x.ComplianceSignPath,
                    ComplianceDocumentReceivedDate = Convert.ToDateTime(x.ComplianceDocumentReceivedDate),
                    ActionID = (int)x.ActionID,
                    RoleID = (int)x.RoleID,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    Pincode = (int)x.Pincode,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    ActionCode = (int)x.ActionCode,
                    Email = (string)x.Email,
                    mobile = (string)x.mobile,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),

                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                EstablishmentsDetailsList = result3.Select(x => new inspectionModel
                {

                    EID = (int)x.EID,
                    InspectionID = (int)x.InspectionID,
                    EstablishmenttypeID = (int)x.EstablishmenttypeID,
                    EstablishmentName = (string)x.EstablishmentName,
                    MaleCount = (int)x.MaleCount,
                    FemaleCount = (int)x.FemaleCount,
                    TotalEmployees = (int)x.TotalEmployees,
                    IsRegisteredUnder = (int)x.IsRegisteredUnder,
                    RegisteredUnder = (string)x.RegisteredUnder,
                    RegistrationNumber = (string)x.RegistrationNumber,
                    RegistrationDate = Convert.ToDateTime(x.RegistrationDate),
                    IndustrytypeID = (int)x.IndustrytypeID,
                    PAddress = (string)x.PAddress,
                    IsFactoriesAct = Convert.ToBoolean(x.IsFactoriesAct),
                    IsEstablishmentAct = Convert.ToBoolean(x.IsEstablishmentAct),
                    IsLabourAct = Convert.ToBoolean(x.IsLabourAct),
                    IsSocietyAct = Convert.ToBoolean(x.IsSocietyAct),
                    IsTrustAct = Convert.ToBoolean(x.IsTrustAct),
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    Pincode = (int)x.Pincode,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Email = (string)x.Email,
                    mobile = (string)x.mobile,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };

            if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
            {
                EmployeeDetailsList = result4.Select(x => new inspectionModel
                {

                    EmployeeID = (int)x.EmployeeID,
                    InspectionID = (int)x.InspectionID,
                    EmployeeName = (string)x.EmployeeName,
                    DesignationID = (int)x.DesignationID,
                    PAddress = (string)x.PAddress,
                    DistrictID = (int)x.DistrictID,
                    OtherDesignationNote = (string)x.OtherDesignationNote,
                    Age = (int)x.Age,
                    TalukaID = (int)x.TalukaID,
                    Pincode = (int)x.Pincode,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Email = (string)x.Email,
                    mobile = (string)x.mobile,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };

            if (keyValuePairs["result5"] is IEnumerable<dynamic> result5 && result5.Any())
            {
                lstDesignation = result5.Select(x => new MCommonList
                {
                    DataValue = (int)x.DataValue,
                    DisplayValue = (string)x.DisplayValue,
                }).ToList();
            };

            if (keyValuePairs["result6"] is IEnumerable<dynamic> result6 && result6.Any())
            {
                lstYesNo = result6.Select(x => new MCommonList
                {
                    DataValue = (int)x.DataValue,
                    DisplayValue = (string)x.DisplayValue,
                }).ToList();
            };

            if (keyValuePairs["result7"] is IEnumerable<dynamic> result7 && result7.Any())
            {
                EstablishmenttypeList = result7.Select(x => new MCommonList
                {
                    DataValue = (int)x.DataValue,
                    DisplayValue = (string)x.DisplayValue,
                }).ToList();
            };

            if (keyValuePairs["result8"] is IEnumerable<dynamic> result8 && result8.Any())
            {
                EstablishmentDetailstypeList = result8.Select(x => new MCommonList
                {
                    DataValue = (int)x.DataValue,
                    DisplayValue = (string)x.DisplayValue,
                }).ToList();
            };

            if (keyValuePairs["result9"] is IEnumerable<dynamic> result9 && result9.Any())
            {
                ContractorDetailsList = result9.Select(x => new inspectionModel
                {
                    ContractorID = (int)x.ContractorID,
                    InspectionID = (int)x.InspectionID,
                    ContractorName = (string)x.ContractorName,
                    CompanyName = (string)x.CompanyName,
                    JobType = (string)x.JobType,
                    CommencementDate = Convert.ToDateTime(x.CommencementDate),
                    CompletionDate = Convert.ToDateTime(x.CompletionDate),
                    MaleCount = (int)x.MaleCount,
                    FemaleCount = (int)x.FemaleCount,
                    TotalWorkerCount = (int)x.TotalWorkerCount,
                    Email = (string)x.Email,
                    mobile = (string)x.mobile,
                    IsContractors = Convert.ToBoolean(x.IsContractors),
                    IsLicense = Convert.ToBoolean(x.IsLicense),
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };

            if (keyValuePairs["result10"] is IEnumerable<dynamic> result10 && result10.Any())
            {
                DocumentList = result10.Select(x => new inspectionModel
                {
                    DID = (int)x.DID,
                    InspectionID = (int)x.InspectionID,
                    DocumentPath = (string)x.DocumentPath,
                 }).ToList();
            };
            response._inspectionList = inspectionList;
            response.DesignationList = lstDesignation;
            response.YesNoList = lstYesNo;
            response.EstablishmenttypeList = EstablishmenttypeList;
            response._inspectionEstablishmentsDetailsList = EstablishmentsDetailsList;
            response._inspectionEstablishmentEmployeeDetailsList = EmployeeDetailsList;
            response._inspectionEstablishmentContractorDetailsList = ContractorDetailsList;
            response.EstablishmentDetailstypeList = EstablishmentDetailstypeList;
            response.DocumentList = DocumentList;
            return response;
        }

        public inspectionModel SaveReportInformationRecord(inspectionModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_InspectionID", Obj.InspectionID);
            param.Add("@p_InspectionDate", Obj.InspectionDatee);
            param.Add("@p_ReportNo", Obj.ReportNo);
            param.Add("@p_DistrictID", Obj.DistrictID);
            param.Add("@p_TalukaID", Obj.TalukaID);
            param.Add("@p_Pincode", Obj.Pincode);
            param.Add("@p_ZoneID", Obj.ZoneID);
            param.Add("@p_AreaID", Obj.AreaID);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_UserMode", Obj.UserMode);
            var keyValuePairs = _InspectionRepository.QueryMultipleByProcedure(SPConstants.UpdateInspectionReportInformation, param);
            var response = new inspectionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new inspectionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    InspectionID = (int)x.InspectionID,
                    //IsMultipul = (int)x.IsMultipul,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public inspectionModel SaveEstablishmentsRecord(inspectionModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_InspectionID", Obj.InspectionID);
            param.Add("@p_IFPApplicationNo", Obj.IFPApplicationNo);
            param.Add("@p_ComplainName", Obj.ComplainName);
            param.Add("@p_ComplainSubject", Obj.ComplainSubject);
            param.Add("@p_ComplainDate", Obj.Date);
            param.Add("@p_InspectionType", Obj.InspectionType);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_UserMode", Obj.UserMode);
            var keyValuePairs = _InspectionRepository.QueryMultipleByProcedure(SPConstants.UpdateInspectionEstablishments, param);
            var response = new inspectionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new inspectionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    InspectionID = (int)x.InspectionID,
                    //IsMultipul = (int)x.IsMultipul,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;

        }

        public inspectionModel SaveEstablishmentsDetailRecord(inspectionModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_InspectionID", Obj.InspectionID);
            param.Add("@p_EstablishmentName", Obj.EstablishmentName);
            param.Add("@p_PAddress", Obj.PAddress);
            param.Add("@p_DistrictID", Obj.DistrictID);
            param.Add("@p_TalukaID", Obj.TalukaID);
            param.Add("@p_Pincode", Obj.Pincode);
            param.Add("@p_ZoneID", Obj.ZoneID);
            param.Add("@p_AreaID", Obj.AreaID);
            param.Add("@p_RegisteredUnder", Obj.RegisteredUnder);
            param.Add("@p_RegistrationNo", Obj.RegistrationNumber);
            param.Add("@p_RegistrationDate", Obj.Date);
            param.Add("@p_IndustrytypeID", Obj.IndustrytypeID);
            param.Add("@p_MaleCount", Obj.MaleCount);
            param.Add("@p_FemaleCount", Obj.FemaleCount);
            param.Add("@p_TotalWorker", Obj.TotalEmployees);
            param.Add("@p_IsFactoriesAct", Obj.IsFactoriesAct);
            param.Add("@p_IsEstablishmentAct", Obj.IsEstablishmentAct);
            param.Add("@p_IsLabourAct", Obj.IsLabourAct);
            param.Add("@p_IsSocietyAct", Obj.IsSocietyAct);
            param.Add("@p_IsTrustAct", Obj.IsTrustAct);
            param.Add("@p_IsRegisteredUnder", Obj.IsRegisteredUnder);
            param.Add("@p_EID", Obj.EID);
            param.Add("@p_EstablishmenttypeID", Obj.EstablishmenttypeID);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_UserMode", Obj.UserMode);
            var keyValuePairs = _InspectionRepository.QueryMultipleByProcedure(SPConstants.UpdateEstablishmentsDetail, param);
            var response = new inspectionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new inspectionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    InspectionID = (int)x.InspectionID,
                    //IsMultipul = (int)x.IsMultipul,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;

        }

        public inspectionModel SaveCompanyDetailsRecord(inspectionModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_InspectionID", Obj.InspectionID);
            param.Add("@p_AuthorizedPersonName", Obj.AuthorizedPersonName);
            param.Add("@p_AuthorizedPersonEmail", Obj.AuthorizedPersonEmail);
            param.Add("@p_AuthorizedPersonContactNo", Obj.AuthorizedPersonContactNo);
            param.Add("@p_AuthorizedPersonDesignation", Obj.AuthorizedPersonDesignation);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_UserMode", Obj.UserMode);
            var keyValuePairs = _InspectionRepository.QueryMultipleByProcedure(SPConstants.UpdateCompanyDetails, param);
            var response = new inspectionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new inspectionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    InspectionID = (int)x.InspectionID,
                    //IsMultipul = (int)x.IsMultipul,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;

        }

        public inspectionModel SaveEmployeeDetailsRecord(inspectionModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_XMLString", Obj.XMLString);
            var keyValuePairs = _InspectionRepository.QueryMultipleByProcedure(SPConstants.UpdateEmployeeDetail, param);
            var response = new inspectionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new inspectionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    InspectionID = (int)x.InspectionID,
                    //IsMultipul = (int)x.IsMultipul,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;

        }
        public inspectionModel SaveContractorDetailsRecord(inspectionModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_XMLString", Obj.XMLString);
            var keyValuePairs = _InspectionRepository.QueryMultipleByProcedure(SPConstants.UpdateContractorDetails, param);
            var response = new inspectionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new inspectionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    InspectionID = (int)x.InspectionID,
                    //IsMultipul = (int)x.IsMultipul,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;

        }

        public inspectionModel SaveInspectionOnsitePicture(inspectionModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_InspectionID", Obj.InspectionID);
            param.Add("@p_DocumentPath", Obj.fileName);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_URL", Obj.URL);
            var keyValuePairs = _InspectionRepository.QueryMultipleByProcedure(SPConstants.UpdateInspectionOnsitePicture, param);
            var response = new inspectionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new inspectionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    InspectionID = (int)x.InspectionID,
                    //IsMultipul = (int)x.IsMultipul,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;

        }

        public inspectionModel SaveOtherDetailsRecord(inspectionModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_InspectionID", Obj.InspectionID);
            param.Add("@p_IsNonComplianceObserved", Obj.IsNonComplianceObserved);
            param.Add("@p_ApplicationStatus", Obj.ApplicationStatus);
            param.Add("@p_InspectorName", Obj.InspectorName);
            param.Add("@p_InspectorEmail", Obj.InspectorEmail);
            param.Add("@p_AuthorizedPersonSignatureNote", Obj.AuthorizedPersonSignatureNote);
            param.Add("@p_IsAuthorizedPersonSignature", Obj.IsAuthorizedPersonSignature);
            param.Add("@p_IsComplianceDocument", Obj.IsComplianceDocument);
            param.Add("@p_ComplianceDocumentReceivedby", Obj.ComplianceDocumentReceivedby);
            param.Add("@p_AuthorizedSignPath", Obj.AuthorizedSignPath);
            param.Add("@p_InspectorSignPath", Obj.InspectorSignPath);
            param.Add("@p_ComplianceSignPath", Obj.ComplianceSignPath);
            param.Add("@p_ComplianceDocumentReceivedDate", Obj.Date);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_IsSubmit", Obj.IsSubmit);
            var keyValuePairs = _InspectionRepository.QueryMultipleByProcedure(SPConstants.UpdateOtherDetails, param);
            var response = new inspectionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new inspectionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    InspectionID = (int)x.InspectionID,
                    //IsMultipul = (int)x.IsMultipul,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;

        }
    }
}
