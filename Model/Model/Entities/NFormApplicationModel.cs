using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class NFormApplicationModel : BaseEntity
    {
        public bool isReqiredTradDetail { get; set; }
        public bool IsRecovery { get; set; }
        public int ApplicationID { get; set; }

        public int ReviewHearingID { get; set; }
        public int ISACL { get; set; }
        public string CaseFavour { get; set; }
        public int AppliactionIDEdit { get; set; }
        public int ISNew { get; set; }
        public string ReviewReason { get; set; }
        public string ReviewDoc { get; set; }
        public string EstablishEmailId { get; set; }
        public string EstablisMobileNo { get; set; }
        public int LastSalaryDrawn { get; set; }
        public DateTime JoinningDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public DateTime HearingDate { get; set; }
        public DateTime ReviewHearingDate { get; set; }
        public string ReviewHearingNote { get; set; }
        public string HearingDateString { get; set; }
        public int TotalYr { get; set; }
        public int EstablisDetailID { get; set; }
        public string EstablishmentCode { get; set; }
        public string EstablishmentName { get; set; }
        public string HearingNote { get; set; }
        public string fileName { get; set; }
        public int DocumentID { get; set; }
        public string reviewfiles { get; set; }

        public string DOC1 { get; set; }
        public string DOC2 { get; set; }
        public string DOC3 { get; set; }
        public string DOC4 { get; set; }
        public string DOC5 { get; set; }
        public string AppID { get; set; }
        public int DiffDay { get; set; }
        public int IsAppeal { get; set; }
        public int Reviewdays { get; set; }
        public int Appealdays { get; set; }
        //public int Recoverydays { get; set; }
        public List<NFormApplicationModel> ClsEstablish { get; set; }
        public List<NFormApplicationModel> basicdetailtlst { get; set; }
        public List<NFormApplicationModel> establishdetaillst { get; set; }
        public List<NFormApplicationModel> tradedetaillst { get; set; }
        public List<NFormApplicationModel> Settlementdetaillst { get; set; }
        public List<NFormApplicationModel> SettlementReviewdetaillst { get; set; }
        public List<NFormApplicationModel> SendLCdetaillst { get; set; }
        public List<NFormApplicationModel> hearingdetaillst { get; set; }
        public List<NFormApplicationModel> Appealdetaillst { get; set; }
        public string Name { get; set; }
        public string AppDate { get; set; }
        public int ID { get; set; }
        public string ApplicationMode { get; set; }
        public string Status { get; set; }
        public string upSettlementDate { get; set; }
        public DateTime SendLCDate { get; set; }
        public string SendLCNote { get; set; }
        public string AreaName { get; set; }
        public string ZoneName { get; set; }
        public string TalukaName { get; set; }
        public string DistrictName { get; set; }
        public string GanderName { get; set; }
        public string DepartmentName { get; set; }

        //String Formet as date
        public string HearingDatee { get; set; }
        public string SettlementDatee { get; set; }
        public string SettlementReviewDatee { get; set; }
        public string SendLCDatee { get; set; }
        public string JoinningDatee { get; set; }
        public string TerminationDatee { get; set; }
        public List<MCommonList> HearingReasonList { get; set; }
        public string HearingReasonName { get; set; }
        public List<MCommonList> CaseFavourList { get; set; }
        public int ActionCode { get; set; }
        public string status { get; set; }
        public string AppealStatus { get; set; }
        public List<NFormApplicationModel> EstalishmentdetailACL { get; set; }
        public int HearingID { get; set; }
        public List<NFormApplicationModel> HearingdetailACL { get; set; }
        public List<NFormApplicationModel> ReviewHearingdetailACL { get; set; }
        public string EMailIDList { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public int CID { get; set; }
        public int AppealID { get; set; }
        public string ChallanNo { get; set; }
        public string ApplicantName { get; set; }
        public string AppealReason { get; set; }
        public decimal ChallanAmount { get; set; }
        public DateTime DateOfChallan { get; set; }
        public string ChallanFile { get; set; }
        public List<EmailReportModel> EmailReportDetail { get; set; }
        public string ResolutionStatus { get; set; }
       
    }

    public class MailDetail
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}
