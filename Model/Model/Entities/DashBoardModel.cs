using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class DashBoardModel : BaseEntity
    {
        public List<DashboardDetail> CashClaimList { get; set; }
        public int EmployeeID { get; set; }
        public int DesignationID { get; set; }
        public int RegionID { get; set; }
        public int BranchID { get; set; }
        public int EmpPosID { get; set; }
        public string ApplicationPageName { get; set; }
        public int SubmitApplication { get; set; }
        public string PageLink { get; set; }
        public List<DashBoardModel> basicdashboarddetailtlst { get; set; }
        public int TotalCounting { get; set; }
        public List<DashBoardModel> TotalCountingList { get; set; }
        public int Count { get; set; }
        public string ApplicationName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<DashBoardModel> TotalApplicationList { get; set; }
        public List<DashBoardModel> CashClaimdetailList { get; set; }
        public List<TotalDashboardDetail> TotalDashboardDetail { get; set; }
    }

    public class DashboardDetail
    {
        public int ID { get; set; }
        public int Submited { get; set; }
        public int Pending { get; set; }
        public int CaseClose { get; set; }
        public int WithCleak { get; set; }
        public int WithACL { get; set; }
        public int Hearing { get; set; }
        public int Query { get; set; }
        public int RemandBack { get; set; }
        public int Review { get; set; }
        public int ReviewApprove { get; set; }
        public int ReviewReject { get; set; }
        public string DistrictName { get; set; }
        public int Total { get; set; }
        public int Setttalement { get; set; }
        public int Labourcourt { get; set; }
        public int Resolution { get; set; }
        public int WithconfirmDCL { get; set; }
        public int WithDismissDCL { get; set; }
        public int WithDCL { get; set; }
        public int WithClerk { get; set; }
        public int WithDCLclerk { get; set; }
        public int WithDCLTC { get; set; }
        public int WithDCLLC { get; set; }
        public int WithDCLHOCLERK { get; set; }
        public int AClQuery { get; set; }
        public int WithHoClerk { get; set; }
        public int WithHOClerkQuery { get; set; }
        public int WithHOCOl { get; set; }
        public int WithHOLE { get; set; }
        public int WithHOQuery { get; set; }
        public int ReviewHearing { get; set; }
        public int ReviewResolution { get; set; }
       
    }

    public class TotalDashboardDetail
    {
        public int TotalSubmited { get; set; }
        public int TotalPending { get; set; }
        public int TotalCaseClose { get; set; }
        public int TotalWithCleak { get; set; }
        public int TotalWithACL { get; set; }
        public int TotalHearing { get; set; }
        public int TotalQuery { get; set; }
        public int TotalRemandBack { get; set; }
        public int TotalReview { get; set; }
        public int TotalReviewApprove { get; set; }
        public int TotalReviewReject { get; set; }
 
        public int TotalTotal { get; set; }
        public int TotalSetttalement { get; set; }
        public int TotalLabourcourt { get; set; }
        public int TotalResolution { get; set; }
        public int TotalWithconfirmDCL { get; set; }
        public int TotalWithDismissDCL { get; set; }
        public int TotalWithDCL { get; set; }
        public int TotalWithClerk { get; set; }
        public int TotalWithDCLclerk { get; set; }
        public int TotalWithDCLTC { get; set; }
        public int TotalWithDCLLC { get; set; }
        public int TotalWithDCLHOCLERK { get; set; }
        public int TotalAClQuery { get; set; }
        public int TotalWithHoClerk { get; set; }
        public int TotalWithHOClerkQuery { get; set; }
        public int TotalWithHOCOl { get; set; }
        public int TotalWithHOLE { get; set; }
        public int TotalWithHOQuery { get; set; }
        public int TotalReviewHearing { get; set; }
        public int TotalReviewResolution { get; set; }

    }

}



