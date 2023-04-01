using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class TFormAppealApplicationModel : BaseEntity
    {
        public int AppealID { get; set; }
        //public int ApplicationID { get; set; }
        public int ISNew { get; set; }
        public string ApplicantName { get; set; }
        public string DistrictName { get; set; }
        public int ISACL { get; set; }
        public string TalukaName { get; set; }
        public int PinCode { get; set; }
        public string fileName { get; set; }
        public string ZoneName { get; set; }
        public string AreaName { get; set; }
        public int ApplicationIDEdit { get; set; }
        public int ApplicationID { get; set; }
        public int GraduityAppId { get; set; }
        public bool IsAccept { get; set; }
        public bool IsDissmis { get; set; }
        public int EstablisDetailID { get; set; }
        public string EstablishmentName { get; set; }
        public string EstablishEmailId { get; set; }
        public string EstablisMobileNo { get; set; }

        public int HearingID { get; set; }
        public string HearingNote { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public int RefNApplicationID { get; set; }
        public int ActionCode { get; set; }
        public int UserMode { get; set; }
        public string StatusName { get; set; }
        public string AppDate { get; set; }
        public int CaseFavourIn { get; set; }
        public int DiffDay { get; set; }
        public string AppID { get; set; }
        public int ID { get; set; }
        public string IPAddress { get; set; }
        public string ApplicationMode { get; set; }
        public string ChallanNo { get; set; }
        public string AppealReason { get; set; }
        public string ResolutionReasonId { get; set; }
        public decimal ChallanAmount { get; set; }
        public DateTime DateOfChallan { get; set; }
        public string ChallanFile { get; set; }
        public DateTime HearingDate { get; set; }
        public string HearingDateString { get; set; }
        public string HearingRemarks { get; set; }
        public DateTime DissmissDate { get; set; }
        public DateTime ConfirmDate { get; set; }
        public DateTime ReamandBackDate { get; set; }
        public string RemandRemark { get; set; }
        public string ConfirmRemark { get; set; }
        public string DissmisRemark { get; set; }
        public List<MCommonList> HearingReasonList { get; set; }
        public List<TFormAppealApplicationModel> basicdetailtlst { get; set; }
        public List<TFormAppealApplicationModel> Challandetailtlst { get; set; }
        public List<TFormAppealApplicationModel> HearingdetailACL { get; set; }
        public List<TFormAppealApplicationModel> EstalishmentdetailACL { get; set; }
        public List<MCommonList> CaseFavourList { get; set; }
        public List<MCommonList> GraduityAppIDList { get; set; }


    }
}
