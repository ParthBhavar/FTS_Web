using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public  class LicenceApplicationModel : BaseEntity
    {
        public object fileName { get; set; }
        public int ApplicationID { get; set; }
        public int ApplicationnIDEdit { get; set; }
        public int ID { get; set; }
        public int ISNew { get; set; }
        public string ApplicantName { get; set; }
        public string Filename { get; set; }
        public string ContractorName { get; set; }
        public string QueryByDCL { get; set; }
        public string QueryByACL { get; set; }
        public string ContractorEmailId { get; set; }
        public string ContractorMobileNo { get; set; }
        public string ContractorPAddress { get; set; }
        public string ContractorSAddress { get; set; }
        public DateTime ContractorDOB { get; set; }
        public string ContractorAge { get; set; }
        public string EstablismentCode { get; set; }
        public int DocumentID { get; set; }
      
        public string EstablihmentName { get; set; }
        public string QueryComments { get; set; }
        public int RegistrationIDEdit { get; set; }
        public string EstablishmentPAddress { get; set; }
        public string EMailIDList { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string EstablishmentName { get; set; }
        public string EstablishMobile { get; set; }
        public string EmailID { get; set; }
        public string EstablishmentSAddress { get; set; }
        public int EDistrictID { get; set; }
        public int EstablisDetailID { get; set; }
        public int ETalukaID { get; set; }
        public int EZoneID { get; set; }
        public int EAreaID { get; set; }
        public int TypeOfBusiness { get; set; }
        public string ApplicationMode { get; set; }
        public string StatusName { get; set; }
        public string ISAmendmentType { get; set; }
        public string AppDate { get; set; }
        public string RegistrationNo { get; set; }
        public int PrincipalID { get; set; }

        public DateTime RegistrationDate { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalFatherName { get; set; }
        public string PEmployerPAddress { get; set; }
        public string PEmployerSAddress { get; set; }

        public decimal LicenceFee { get; set; }
     
        public string TreasuryName { get; set; }
        public string TreasurName { get; set; }
        public string LicecneDoc { get; set; }
        public string SecurityDoc { get; set; }
        public int ChallanNo { get; set; }
        public string TEMPID { get; set; }
        public DateTime ChallanDate { get; set; }
        public string DbInChallanDate { get; set; }
        public decimal SecurityDeposit { get; set; }
        public int SecurityChallanNo { get; set; }
        public DateTime SecurityChallanDate { get; set; }
        public string DbInSecurityChallanDate { get; set; }
        public string Doc1 { get; set; }
        public string Doc2 { get; set; }
        public string Doc3 { get; set; }
        public string Doc4 { get; set; }
        public string Doc5 { get; set; }
        public string Doc6 { get; set; }
        public bool IsQryReplied { get; set; }
        public int TypeOfEstablishment { get; set; }
        public string AppID { get; set; }
        public int ActionCode { get; set; }
        public string RejectRemarks { get; set; }
        public string Applicationtype { get; set; }
        public int ISHO { get; set; }
        public int ISACL { get; set; }
        public int ISDCL { get; set; }
        public string Status { get; set; }
        public int ISAmendment { get; set; }
        public int IsEdit { get; set; }
        public int RefRegistrationID { get; set; }
        public string ACL_Review_Comments { get; set; }
        public string DCL_Review_Comments { get; set; }
        public string IPAddress { get; set; }
        public DateTime RemandBackDate { get; set; }
        public DateTime ConfirmDate { get; set; }
        public string ConfirmRemark { get; set; }
        public List<LicenceApplicationModel> PrincipalEmployerInformationdetaillst { get; set; }
        public List<LicenceApplicationModel> Establishmentregistrationdetaillst { get; set; }
        public List<MCommonList> TypeOfEstablishmentList { get; set; }
        public List<MCommonList> CommonList { get; set; }
        public List<MCommonList> EstablisTalukalist { get; set; }
        public List<MCommonList> EstablisDistrictlist { get; set; }
        
        public bool IsCLRA { get; set; }
        public bool IsIMW { get; set; }
        public bool ISMTW { get; set; }
        public string DOC1 { get; set; }
        public string DOC2 { get; set; }
        public string DOC3 { get; set; }
        public string DOC4 { get; set; }
        public string DOC5 { get; set; }
        public string DOC6 { get; set; }
        public string CLRANote { get; set; }
        public string IMWNote { get; set; }
        public string MTWNote { get; set; }
        public bool IsCLRA_verified { get; set; }
        public string Approve_DOC { get; set; }
        public bool IsIMW_verified { get; set; }
        public bool ISMTW_verified { get; set; }
        public int IsMultipul { get; set; }
    }
}
