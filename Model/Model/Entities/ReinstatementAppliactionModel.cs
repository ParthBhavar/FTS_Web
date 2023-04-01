using FTS.Model.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class ReinstatementAppliactionModel : BaseEntity
    {
     

        public bool isReqiredTradDetail { get; set; }
        public int ApplicationID { get; set; }
        public int ISACL { get; set; }
        public string ApplicantName { get; set; }
        public int AppliactionIDEdit { get; set; }
        public int ISNew { get; set; }
        public int TradunionID { get; set; }
        public string RegistrationNo { get; set; }
        public string RegistrationName { get; set; }
        public int TotalWorkers { get; set; }
        public int WorkerAge { get; set; }
        public int Department { get; set; }
        public int WorkType { get; set; }
        public int LastSalaryDrawn { get; set; }
        public DateTime JoinningDate { get; set; }
        public string UPJoinningDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public string UPTerminationDate { get; set; }
        public DateTime HearingDate { get; set; }
        public string HearingDateString { get; set; }
        public string TotalYr { get; set; }
        public int EstablisDetailID { get; set; }
        public string EstablishmentCode { get; set; }
        public string EstablishmentName { get; set; }
        public string HearingNote { get; set; }
        public string fileName { get; set; }
        public int DocumentID { get; set; }
        public string DOC1 { get; set; }
        public string DOC2 { get; set; }
        public string DOC3 { get; set; }
        public string DOC4 { get; set; }
        public string AppID { get; set; }

        public List<ReinstatementAppliactionModel> ClsEstablish { get; set; }
        public List<ReinstatementAppliactionModel> basicdetailtlst { get; set; }
        public List<ReinstatementAppliactionModel> establishdetaillst { get; set; }
        public List<ReinstatementAppliactionModel> tradedetaillst { get; set; }
        public List<ReinstatementAppliactionModel> Settlementdetaillst { get; set; }
        public List<ReinstatementAppliactionModel> SendLCdetaillst { get; set; }
        public List<ReinstatementAppliactionModel> hearingdetaillst { get; set; }
        public string Name { get; set; }
        public string AppDate { get; set; }
        public int ID { get; set; }
        public string ApplicationMode { get; set; }
        public string Status { get; set; }
        public string UPSettlementDate { get; set; }
        public DateTime SendLCDate { get; set; }
        public string UPSendLCDate { get; set; }
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
        public string SendLCDatee { get; set; }
        public string JoinningDatee { get; set; }
        public string TerminationDatee { get; set; }
        public List<MCommonList> HearingReasonList { get; set; }
        public string HearingReasonName { get; set; }
        public string WorkName { get; set; }
        public int ActionCode { get; set; }
   
        public int HearingID { get; set; }
        public List<ReinstatementAppliactionModel> HearingdetailACL { get; set; }
        public string EMailIDList { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public IFormFile Attachment { get; set; }

        public List<string> EMailID { get; set; }

        public int CID { get; set; }
        public List<ReinstatementAppliactionModel> TestList { get; set; }
        public string[] TestList1 { get; set; }
        public List<ReinstatementAppliactionModel> List { get; set; }
        public List<EmailReportModel> EmailReportDetail { get; set; }
    }
}
