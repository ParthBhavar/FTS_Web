using FTS.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class ConciliationApplicationModel : BaseEntity
    {
        public bool isReqiredTradDetail;

        public int ApplicationID { get; set; }
        public int ISACL { get; set; }
        public int ID { get; set; }
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public bool IsSendHO { get; set; }
        public string ApplicantEmailId { get; set; }
        public string EstablishEmailId { get; set; }
        public string EstablisMobileNo { get; set; }
        public int AppliactionIDEdit { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int TalukaCode { get; set; }
        public int UserMode { get; set; }
        public int ActionCode { get; set; }
        public int DCLReferStatusID { get; set; }
        public int HOReferStatusID { get; set; }
        public int TradunionID { get; set; }
        public DateTime HearingDate { get; set; }
        public string HearingDateString { get; set; }
        public string HearingRemarks { get; set; }
        public bool IsPUS { get; set; }
        public bool IsQuery { get; set; }
        public string Query { get; set; }
        public int TradeDetailID { get; set; }
        public string QueryRemarks { get; set; }
        public string ReasonName { get; set; }
        public string HearingReason { get; set; }
        //public int PinCode { get; set; }
        public int ISNew { get; set; }
        public string EstablishmentCode { get; set; }
        public int EstablisDetailID { get; set; }
        public string EstablishmentName { get; set; }
        public string TradeUnionRegistrationNo { get; set; }
        public string TradeUnionRegistrationName { get; set; }
        public string TradeUnionAddress { get; set; }
        public int TradeUnionDistrict { get; set; }
        public int TradeUnionTaluka { get; set; }
        public int TradeUnionPincode { get; set; }
        public string TradeUnionPAddress { get; set; }
        public string TradeUnionSAddress { get; set; }
        public int TotalNoOfWorkingEmpInUnion { get; set; }
        public int TotalWorkingEmpInEstablishment { get; set; }
        public int NumberOfEmpUnderDemand { get; set; }
        public string  DepartmentName { get; set; }
        public string BusinessOfEstablishment { get; set; }
        public string Demand { get; set; }
        public string Remarks { get; set; }
        public string CopyofCharteredDemandFile { get; set; }
        public string LetterOfIntervention { get; set; }

        public string OwnerCopyOfChartered { get; set; }
        public string Conciliationcol { get; set; }
        public string MembershipVerificaionDetail { get; set; }
        public string AnnualReturnStatement { get; set; }
        public string fileName { get; set; }
        public int DocumentID { get; set; }
        public string TradeUnionAddress1 { get; set; }
        public int IsFinal { get; set; }
        public bool Status { get; set; }
        public string StatusName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string AppID { get; set; }
        public string ApplicationMode { get; set; }
        public string AppDate { get; set; }
        public DateTime SettlementDate { get; set; }
        public string SettlementDateIn { get; set; }
        public string SettlementRemark { get; set; }
        public DateTime OrderOutwardDate { get; set; }
        public int OrderOutwardNo { get; set; }
        public string OrderOutwardDatee { get; set; }
        public string SettlementDocURL { get; set; }
        public DateTime SendLCDate { get; set; }
        public string SendtoLCDatee { get; set; }
        public string SendLCRemark { get; set; }
        public string DistrictName { get; set; }
        public string TalukaName { get; set; }
        public string ZoneName { get; set; }
        public string AreaName { get; set; }
        public List<ConciliationApplicationModel> basicdetailtlst { get; set; }
        public List<ConciliationApplicationModel> ClEstablish { get; set; }
        public List<ConciliationApplicationModel> ClTradunion { get; set; }
        public List<ConciliationApplicationModel> establishdetaillst { get; set; }
        public List<ConciliationApplicationModel> hearingdetaillst;
        public List<ConciliationApplicationModel> ConcilTradUnionDetail;
        public List<ConciliationApplicationModel> Settlementdetaillst { get; set; }
        public List<ConciliationApplicationModel> SendLCdetaillst { get; set; }
        public List<MCommonList> HearingReasonList { get; set; }
        public List<MCommonList> ReferStatuList { get; set; }
        public List<MCommonList> HOReferStatuList { get; set; }
        public List<ConciliationApplicationModel> HearingdetailACL { get; set; }
        public List<ConciliationApplicationModel> EstalishmentdetailACL { get; set; }
        public string EMailIDList { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string MobileNo { get; set; }
        public int HearingReasonID { get; set; }
        public int ResolutionReasonID { get; set; }
        public string ResolutionReason { get; set; }
        public List<MailDetail> ApplicantMailDetail { get; set; }
        public List<MailDetail> EshtablishmentMailDetail { get; set; }
        public List<EmailReportModel> EmailReportDetail { get; set; }
        public List<IFormFile> Files { get; set; }
        //public int CreatedBy { get; set; }
        //public int IsActive { get; set; }
        //public int IsSubmit { get; set; }
        //public string IPAddress { get; set; }
        //public string Url { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }

    }
}