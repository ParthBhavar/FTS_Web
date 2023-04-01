using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.DashBoard
{
    public class DashBoardRepository : IDashBoardRepository
    {
        #region Private Variables
        private readonly IRepository<DashBoardModel> _DashBoardRepo;
        #endregion

        #region Constructor
        public DashBoardRepository(IRepository<DashBoardModel> DashBoardRepo)
        {
            _DashBoardRepo = DashBoardRepo;

        }
        #endregion

        public DashBoardModel DashBoardDetail(DashBoardModel DashBoardRepo)
        {
            List<DashBoardModel> lstdashboarddetaildetail = new List<DashBoardModel>();
            List<DashBoardModel> lstdashboraddetdetail = new List<DashBoardModel>();
            List<DashBoardModel> lstTotalCounting = new List<DashBoardModel>();
            List<DashBoardModel> lstTotalApplication = new List<DashBoardModel>();
            List<DashBoardModel> lstCashClaimdetail = new List<DashBoardModel>();
          
            //List<ReinstatementAppliactionModel> lstReinstatementtreadUniDetail = new List<ReinstatementAppliactionModel>();
            //List<ReinstatementAppliactionModel> lstReinstatementhearingDetail = new List<ReinstatementAppliactionModel>();
            //List<ReinstatementAppliactionModel> lstReinstatementSettlementDetail = new List<ReinstatementAppliactionModel>();
            //List<ReinstatementAppliactionModel> lstReinstatementSendLCDetail = new List<ReinstatementAppliactionModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", DashBoardRepo.UserID); 
            param.Add("@p_UserMode", DashBoardRepo.UserMode); 
            param.Add("@p_ZoneID", DashBoardRepo.ZoneID); 
            param.Add("@p_DistrictID", DashBoardRepo.DistrictID); 
            param.Add("@p_TalukaID", DashBoardRepo.TalukaID); 
            param.Add("@p_RoleID", DashBoardRepo.RoleID); 
            param.Add("@p_PositionID", DashBoardRepo.PositionID);
            param.Add("@p_IsDashoardList", DashBoardRepo.IsDashoardList);
            //param.Add("@p_IsDashoardList", 2);
            var keyValuePairs = _DashBoardRepo.QueryMultipleByProcedure(SPConstants.GetDashBoardDetail, param);
            var response = new DashBoardModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new DashBoardModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    UserMode = (int)x.UserMode
                }).FirstOrDefault();
            };

            //if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            //{
            //    lstdashboraddetdetail = result2.Select(x => new DashBoardModel
            //    {
            //        ApplicationPageName = (string)x.ApplicationPageName,
            //        SubmitApplication = (int)x.SubmitApplication,
            //        PageLink = (string)x.PageLink
            //    }).ToList();
            //};


            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                lstTotalCounting = result2.Select(x => new DashBoardModel
                {
                    TotalCounting = (int)x.TotalCounting
                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                lstTotalApplication = result3.Select(x => new DashBoardModel
                {
                    Count = (int)x.Count,
                    ApplicationName = (string)x.ApplicationName
                }).ToList();
            };

            if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
            {
                lstCashClaimdetail = result4.Select(x => new DashBoardModel
                {
                    Count = (int)x.count,
                    ID = (int)x.ID,
                    ApplicationName = (string)x.ApplicationName
                }).ToList();
            };


            response.basicdashboarddetailtlst = lstdashboraddetdetail;
            response.TotalCountingList = lstTotalCounting;
            response.TotalApplicationList = lstTotalApplication;
            response.CashClaimdetailList = lstCashClaimdetail;
            
            return response;
        }

        public DashBoardModel CaseClimeDetail(DashBoardModel DashBoardRepo)
        {

           if(DashBoardRepo.StartDate == null)
            {
                DashBoardRepo.StartDate = "";
            }
            if (DashBoardRepo.EndDate == null)
            {
                DashBoardRepo.EndDate = "";
            }

            List<DashboardDetail> lstCashClaimdetail = new List<DashboardDetail>();
            List<TotalDashboardDetail> lstTotalDashboardDetail = new List<TotalDashboardDetail>();
            //List<ReinstatementAppliactionModel> lstReinstatementtreadUniDetail = new List<ReinstatementAppliactionModel>();
            //List<ReinstatementAppliactionModel> lstReinstatementhearingDetail = new List<ReinstatementAppliactionModel>();
            //List<ReinstatementAppliactionModel> lstReinstatementSettlementDetail = new List<ReinstatementAppliactionModel>();
            //List<ReinstatementAppliactionModel> lstReinstatementSendLCDetail = new List<ReinstatementAppliactionModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", DashBoardRepo.UserID);
            param.Add("@p_UserMode", DashBoardRepo.UserMode);
            param.Add("@P_StartDate", DashBoardRepo.StartDate);
            param.Add("@P_EndDate", DashBoardRepo.EndDate);
            param.Add("@p_ID", DashBoardRepo.ID);
            param.Add("@p_DistrictID", DashBoardRepo.DistrictID);
            param.Add("@p_PositionDistrictID", DashBoardRepo.PositionDistrictID);
            param.Add("@p_IsDashoardList", DashBoardRepo.IsDashoardList);
            param.Add("@p_PositionID", DashBoardRepo.PositionID);
            //param.Add("@p_IsDashoardList", 2);
            param.Add("@p_RoleID", DashBoardRepo.RoleID);
            param.Add("@p_ZoneID", DashBoardRepo.ZoneID);
            var keyValuePairs = _DashBoardRepo.QueryMultipleByProcedure(SPConstants.GetDashboardCaseClimeDetail, param);
            var response = new DashBoardModel();

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new DashBoardModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ID = (int)x.ID,
                    IsDashoardList = (int)x.IsDashoardList,
                }).FirstOrDefault();
            };

            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                lstCashClaimdetail = result2.Select(x => new DashboardDetail
                {
                    ID = (int)x.ID,
                    Submited = (int)x.Submited,
                    Total = (int)x.Total,
                    Pending = (int)x.Pending,
                    CaseClose = (int)x.CaseClose,
                    WithCleak = (int)x.WithCleak,
                    WithACL = (int)x.WithACL,
                    Hearing = (int)x.Hearing,
                    Setttalement = (int)x.Setttalement,
                    Labourcourt = (int)x.Labourcourt,
                    Resolution = (int)x.Resolution,
                    WithconfirmDCL = (int)x.WithconfirmDCL,
                    WithDismissDCL = (int)x.WithDismissDCL,
                    WithDCL = (int)x.WithDCL,
                    Query = (int)x.Query,
                    RemandBack = (int)x.RemandBack,
                    Review = (int)x.Review,
                    ReviewApprove = (int)x.ReviewApprove,
                    ReviewReject = (int)x.ReviewReject,
                    DistrictName = (string)x.DistrictName,
                    WithHOQuery = (int)x.WithHOQuery,
                    WithHOLE = (int)x.WithHOLE,
                    WithHOCOl = (int)x.WithHOCOl,
                    WithHOClerkQuery = (int)x.WithHOClerkQuery,
                    WithHoClerk = (int)x.WithHoClerk,
                    AClQuery = (int)x.AClQuery,
                    WithDCLHOCLERK = (int)x.WithDCLHOCLERK,
                    WithDCLLC = (int)x.WithDCLLC,
                    WithDCLTC = (int)x.WithDCLTC,
                    WithDCLclerk = (int)x.WithDCLclerk,
                    ReviewResolution = (int)x.ReviewResolution,
                    ReviewHearing = (int)x.ReviewHearing,
                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                lstTotalDashboardDetail = result3.Select(x => new TotalDashboardDetail
                {
                   TotalTotal = (int)x.Total,
                   TotalSubmited = (int)x.Totalsubmmited,
                   TotalPending = (int)x.TotalPending,
                   TotalCaseClose = (int)x.TotalCaseClose,
                   TotalWithCleak = (int)x.TotalWithCleak,
                   TotalWithACL = (int)x.TotalWithACL,
                   TotalHearing = (int)x.TotalHearing,
                   TotalQuery = (int)x.TotalQuery,
                   TotalRemandBack = (int)x.TotalRemandBack,
                   TotalReview = (int)x.TotalReview,
                   TotalReviewApprove = (int)x.TotalReviewApprove,
                   TotalReviewReject = (int)x.TotalReviewReject,
                   TotalSetttalement = (int)x.TotalSetttalement,
                   TotalLabourcourt = (int)x.TotalLabourcourt,
                   TotalResolution = (int)x.TotalResolution,
                   TotalWithconfirmDCL = (int)x.TotalWithconfirmDCL,
                   TotalWithDismissDCL = (int)x.TotalWithDismissDCL,
                   TotalWithDCL = (int)x.TotallWithDCL,
                    TotalWithDCLclerk = (int)x.TotalWithDCLclerk,
                    TotalWithDCLTC = (int)x.TotalWithDCLTC,
                    TotalWithDCLLC = (int)x.TotalWithDCLLC,
                    TotalWithDCLHOCLERK = (int)x.TotalWithDCLHOCLERK,
                    TotalAClQuery = (int)x.TotalAClQuery,
                    TotalWithHoClerk = (int)x.TotalWithHoClerk,
                    TotalWithHOClerkQuery = (int)x.TotalWithHOClerkQuery,
                    TotalWithHOCOl = (int)x.TotalWithHOCOl,
                    TotalWithHOLE = (int)x.TotalWithHOLE,
                    TotalWithHOQuery = (int)x.TotalWithHOQuery,
                    TotalReviewResolution = (int)x.TotalReviewResolution,
                    TotalReviewHearing = (int)x.TotalReviewHearing,


                }).ToList();
            };

        
            response.CashClaimList = lstCashClaimdetail;
            response.TotalDashboardDetail = lstTotalDashboardDetail;
            return response;
        }
    }
}
