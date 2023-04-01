using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class EmailReportModel
    {
        public string DictrictACLOffice { get; set; }
        public string ACLDistrict { get; set; }
        public string EstablishmentName { get; set; }

        public string ApplicantName { get; set; }
        public string ShakhaNo { get; set; }
        public string AppID { get; set; }
        public string ApplicantDetail { get; set; }
        public string Establishmentdetail { get; set; }
        public string ChallanAmount { get; set; }
        public string ACLName { get; set; }
        public string LCAddress { get; set; }
        public string Date { get; set; }
        public string SendLCDate { get; set; }

        public string DictrictACL { get; set; }
        public string HearingDate { get; set; }
        public string HearingTime { get; set; }
        public int IsNotice { get; set; }
        public int IsReviewHearing { get; set; }
    }
}