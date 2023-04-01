using FTS.Model.Common;
using FluentValidation;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    /// <summary>
    /// If Inspection Type is Complain Based  
    /// Complain Based Property : dateOfComplain, subjectOfComplain, nameOfComplainant
    /// </summary>
    public class inspectionModel : BaseEntity
    {
        public int InspectionID { get; set; }
        protected string EncryptInspectionID { get; set; }
        public string ReportNo { get; set; }
        //public int DistrictId { get; set; }
        //public string DistrictName { get; set; }
        //public int DistrictCodeId { get; set; }
        //public string DistrictCode { get; set; }
        //public int TalukaId { get; set; }
        //public string TalukaName { get; set; }
        //public int TalukaCodeId { get; set; }
        //public string TalukaCode { get; set; }
        public DateTime InspectionDate { get; set; }
        public int InspectionType { get; set; }
        public string InspectionTime { get; set; }
        public DateTime dateOfComplain { get; set; }
        public string subjectOfComplain { get; set; }
        public string nameOfComplainant { get; set; }
        public string IFPApplicationNo { get; set; }
        public int EstablishmentsType { get; set; }
        public bool haveContractors { get; set; }
        public string AuthorizedPersonName { get; set; }
        public string AuthorizedPersonEmail { get; set; }
        public string AuthorizedPersonContactNo { get; set; }
        public string AuthorizedPersonDesignation { get; set; }
        public int IsAuthorizedPersonSignature { get; set; }
        public string AuthorizedSignPath { get; set; }
        public string AuthorizedPersonSignatureRemark { get; set; }
        public int IsNonComplianceObserved { get; set; }
        public int ApplicationStatus { get; set; }
        public string InspectorName { get; set; }
        public string InspectorEmail { get; set; }
        public string InspectorSignPath { get; set; }
        public int IsComplianceDocument { get; set; }
        public string ComplianceDate { get; set; }
        public string ComplianceTime { get; set; }
        public int ActionID { get; set; }
        public int EID { get; set; }
        public string EstablishmentName { get; set; }
        //public string EAddress { get; set; }
        //public int EDistrictId { get; set; }
        //public string EDistrict { get; set; }
        //public int ETalukaId { get; set; }
        //public string ETaluka { get; set; }
        //public string Epincode { get; set; }
        public string RegistrationNo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RegisteredUnder { get; set; }
        public int IndustryTypeId { get; set; }
        public string OtherIndustryType { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
        public int TotalEmployees { get; set; }
        public int EstablishmentsTypeId { get; set; }
        public int RegisteredId { get; set; }
        public string OtherRegisteredActName { get; set; }
        public string EmployeeName { get; set; }
        public int Age { get; set; }
        public int HDNEmployeeDetailsCount { get; set; }
        public int Designation { get; set; }
        public string Address { get; set; }
        public string OtherDesignation { get; set; }
        public int CID { get; set; }
        public string ContractorName { get; set; }
        public string CompanyName { get; set; }
        public string JobType { get; set; }
        public int hasLicense { get; set; }
        public DateTime CommencementDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public int MaleEmployeesContractCount { get; set; }
        public int FemaleEmployeesContractCount { get; set; }
        public int TotalEmployeesContractCount { get; set; }
        public int AID { get; set; }
        public int ActID { get; set; }
        public int DID { get; set; }
        public string DocumentPath { get; set; }
        public inspectionModel _inspectionEstablishmentsDetailsModel { get; set; }
        public inspectionModel _inspectionEstablishmentEmployeeDetailsModel { get; set; }
        public inspectionModel _inspectionEstablishmentContractorDetailsModel { get; set; }
        public List<inspectionModel> _inspectionList { get; set; }
        public List<inspectionModel> _inspectionEstablishmentsDetailsList { get; set; }
        public List<inspectionModel> _inspectionEstablishmentEmployeeDetailsList { get; set; }
        public List<inspectionModel> _inspectionEstablishmentContractorDetailsList { get; set; }
        public List<inspectionModel> _inspectionApplicationActsDetailsList { get; set; }
        public List<inspectionModel> _inspectionApplicationDocumentsList { get; set; }
        public string ComplainName { get; set; }
        public string ComplainSubject { get; set; }
        public DateTime ComplainDate { get; set; }
        public int ActionCode { get; set; }
        public string AuthorizedPersonSignatureNote { get; set; }
        public string ComplianceDocumentReceivedby { get; set; }
        public string ComplianceSignPath { get; set; }
        public DateTime ComplianceDocumentReceivedDate { get; set; }
        public int InspectionIDEdit { get; set; }
        public string InspectionDatee { get; set; }
        public int IsRegisteredUnder { get; set; }
        public string RegistrationNumber { get; set; }
        public int EstablishmenttypeID { get; set; }
        public int IndustrytypeID { get; set; }
        public bool IsFactoriesAct { get; set; }
        public bool IsTrustAct { get; set; }
        public bool IsSocietyAct { get; set; }
        public bool IsLabourAct { get; set; }
        public bool IsEstablishmentAct { get; set; }
        public bool IsLicense { get; set; }
        public bool IsContractors { get; set; }
        public List<MCommonList> DesignationList { get; set; }
        public int DesignationID { get; set; }
        public int EmployeeID { get; set; }
        public string OtherDesignationNote { get; set; }
        public string String64AuthorizedPersonSignature { get; set; }
        public string String64InspectorSignature { get; set; }
        public string String64ComplianceSignature { get; set; }
        public List<MCommonList> YesNoList { get; set; }
        public int TotalWorkerCount { get; set; }
        public int ContractorID { get; set; }
        public List<MCommonList> EstablishmenttypeList { get; set; }
        public List<MCommonList> EstablishmentDetailstypeList { get; set; }
        public List<inspectionModel> DocumentList { get; set; }

        //public inspectionModel()
        //{
        //    ReportNo = String.Format("{0:ddMMyyyy'/'HHmmss}", DateTime.Now) + '/' + rendomNum.GenerateRandomNo();
        //    InspectionDatee = DateTime.Now.ToString("dd/MM/yyyy");
        //    InspectionTime = DateTime.Now.ToString("HH:mm");

        //    //_inspectionEstablishmentsDetailsModel = new inspectionEstablishmentsDetails();
        //    //_inspectionEstablishmentEmployeeDetailsModel = new inspectionEstablishmentEmployeeDetails();
        //    //_inspectionEstablishmentContractorDetailsModel = new inspectionEstablishmentContractorDetails();
        //    //_inspectionModelsList = new List<inspectionModel>();
        //    //_inspectionEstablishmentsDetailsList = new List<inspectionEstablishmentsDetails>();
        //    //_inspectionEstablishmentEmployeeDetailsList = new List<inspectionEstablishmentEmployeeDetails>();
        //    //_inspectionEstablishmentContractorDetailsList = new List<inspectionEstablishmentContractorDetails>();
        //    //_inspectionApplicationActsDetailsList = new List<inspectionApplicationActsDetails>();
        //    //_inspectionApplicationDocumentsList = new List<inspectionApplicationDocuments>();
        //}
    }

    //public class inspectionEstablishmentsDetails :BaseEntity
    //{
    //    public int EID { get; set; }
    //    public int InspectionID { get; set; }
    //    public string EstablishmentName { get; set; }
    //    //public string EAddress { get; set; }
    //    //public int EDistrictId { get; set; }
    //    //public string EDistrict { get; set; }
    //    //public int ETalukaId { get; set; }
    //    //public string ETaluka { get; set; }
    //    //public string Epincode { get; set; }
    //    public string RegistrationNo { get; set; }
    //    public string RegistrationDate { get; set; }
    //    public string RegisteredUnder { get; set; }
    //    public int IndustryTypeId { get; set; }
    //    public string OtherIndustryType { get; set; }
    //    public int MaleCount { get; set; }
    //    public int Female { get; set; }
    //    public int TotalEmployees { get; set; }
    //}
    //public class establishmentRegisteredUnder : BaseEntity
    //{
    //    public int InspectionID { get; set; }
    //    public int EID { get; set; }
    //    public int EstablishmentsTypeId { get; set; }
    //    public int RegisteredId { get; set; }
    //    public string OtherRegisteredActName { get; set; }
       
    //}
    //public class inspectionEstablishmentEmployeeDetails : BaseEntity
    //{
    //    public int EID { get; set; }
    //    public int InspectionID { get; set; }
    //    public string EmployeeName { get; set; }
    //    public int Age { get; set; }
    //    public int Designation { get; set; }
    //    public string Address { get; set; }
    //    public string OtherDesignation { get; set; }

    //}
    //public class inspectionEstablishmentContractorDetails : BaseEntity
    //{
    //    public int CID { get; set; }
    //    public int InspectionID { get; set; }
    //    public string ContractorName { get; set; }
    //    public string CompanyName { get; set; }
    //    public string JobType { get; set; }
    //    public int hasLicense { get; set; }
    //    public DateTime CommencementDate { get; set; }
    //    public DateTime CompletionDate { get; set; }
    //    public int MaleEmployeesContractCount { get; set; }
    //    public int FemaleEmployeesContractCount { get; set; }
    //    public int TotalEmployeesContractCount { get; set; }

    //}
    //public class inspectionApplicationActsDetails : BaseEntity
    //{
    //    public int AID { get; set; }
    //    public int InspectionID { get; set; }
    //    public int ActID { get; set; }
    //}
    //public class inspectionApplicationDocuments : BaseEntity
    //{
    //    public int DID { get; set; }
    //    public int InspectionID { get; set; }
    //    public string DocumentPath { get; set; }
    //}

    /// <summary>
    /// inspectionModelValidator is validation class for custom validation 
    /// </summary>
    public class inspectionModelValidator : AbstractValidator<inspectionModel>
    {
        public inspectionModelValidator()
        {
            RuleFor(x => x.IFPApplicationNo)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(15)
                .NotEqual("hello");

            RuleFor(x => x.DistrictID)
            .NotEmpty()
            .NotEqual(0);

            RuleFor(x => x.TalukaID)
         .NotEmpty()
         .NotEqual(0);

            RuleFor(x => x.Pincode)
                 .NotEmpty()
                 .NotEqual(00);

            // RuleFor(x => x.TalukaID)
            //.NotEmpty()
            //.NotEqual(0);


            //RuleFor(x => x.Category)
            //    .NotEmpty();

            //RuleFor(x => x.InStock)
            //    .NotEqual(0).When(x => x.Active).WithMessage("'In Stock' must not be equal to '0' when the product is active.");
        }
    }

    public class rendomNum
    {
        public static int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }

}
