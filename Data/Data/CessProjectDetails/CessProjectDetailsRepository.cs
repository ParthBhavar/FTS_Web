using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.CessProjectDetails
{
    public class CessProjectDetailsRepository : ICessProjectDetailsRepository
    {
        #region Private Variables
        private readonly IRepository<CessProjectDetailsModel> _projectdetailsRepository;
        #endregion

        #region Constructor
        public CessProjectDetailsRepository(IRepository<CessProjectDetailsModel> projectdetailsRepository)
        {
            _projectdetailsRepository = projectdetailsRepository;

        }
        #endregion

        public List<CessProjectDetailsModel> ProjectDetailsList()
        {
            List<CessProjectDetailsModel> lstProjectDetails = new List<CessProjectDetailsModel>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _projectdetailsRepository.QueryMultipleByProcedure(SPConstants.GetListProjectDetails, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstProjectDetails = result1.Select(x => new CessProjectDetailsModel
                {
                    ProjectID = (int)x.ProjectID,
                    ProjectName = (string)x.ProjectName,
                    NoOfWorkersEmployed = (int)x.NoOfWorkersEmployed,
                    TotalEstimatedProjectCost = (decimal)x.TotalEstimatedProjectCost,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstProjectDetails;
        }

        public CessProjectDetailsModel ProjectDetailsRecord(int ProjectID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ProjectID", ProjectID);
            var keyValuePairs = _projectdetailsRepository.QueryMultipleByProcedure(SPConstants.GetRecordProjectDetails, param);
            var response = new CessProjectDetailsModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new CessProjectDetailsModel
                {
                    ProjectID = (int)x.ProjectID,
                    ProjectName = (string)x.ProjectName,
                    ProjectDescription = (string)x.ProjectDescription,
                    ProjectAddress = (string)x.ProjectAddress,
                    AreaRoad = (string)x.AreaRoad,
                    Pincode = (int)x.Pincode,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    LandMark = (string)x.LandMark,
                    DepartmentID = (int)x.DepartmentID,
                    NoOfWorkersEmployed = (int)x.NoOfWorkersEmployed,
                    DateOfCommencementOfWork = Convert.ToDateTime(x.DateOfCommencementOfWork),
                    EstimatedPeriodOfWork = Convert.ToDateTime(x.EstimatedPeriodOfWork),
                    TotalEstimatedProjectCost =  (decimal)x.TotalEstimatedProjectCost,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }

        public CessProjectDetailsModel SaveProjectDetails(CessProjectDetailsModel ObjProjectdtl)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", ObjProjectdtl.UserID);
            param.Add("@p_ProjectID", ObjProjectdtl.ProjectID);
            param.Add("@p_ProjectName", ObjProjectdtl.ProjectName);
            param.Add("@p_ProjectDescription", ObjProjectdtl.ProjectDescription);
            param.Add("@p_ProjectAddress", ObjProjectdtl.ProjectAddress);
            param.Add("@p_AreaRoad", ObjProjectdtl.AreaRoad);
            param.Add("@p_Pincode", ObjProjectdtl.Pincode);
            param.Add("@p_DistrictID", ObjProjectdtl.DistrictID);
            param.Add("@p_TalukaID", ObjProjectdtl.TalukaID);
            param.Add("@p_LandMark", ObjProjectdtl.LandMark);
            param.Add("@p_DepartmentID", ObjProjectdtl.DepartmentID);
            param.Add("@p_NoOfWorkersEmployed", ObjProjectdtl.NoOfWorkersEmployed);
            param.Add("@p_DateOfCommencementOfWork", ObjProjectdtl.DateOfCommencementOfWork);
            param.Add("@p_EstimatedPeriodOfWork", ObjProjectdtl.EstimatedPeriodOfWork);
            param.Add("@p_TotalEstimatedProjectCost", ObjProjectdtl.TotalEstimatedProjectCost);
            param.Add("@p_IsActive", ObjProjectdtl.IsActive);
            param.Add("@p_IsDeleted", ObjProjectdtl.IsDeleted);
            var keyValuePairs = _projectdetailsRepository.QueryMultipleByProcedure(SPConstants.UpdateProjectDetails, param);
            var response = new CessProjectDetailsModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new CessProjectDetailsModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    //IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }


        public CessProjectDetailsModel DeleteProjectDetails(int UserID, int ProjectID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ProjectID", ProjectID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _projectdetailsRepository.QueryMultipleByProcedure(SPConstants.DeleteProjectDetails, param);
            var response = new CessProjectDetailsModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new CessProjectDetailsModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public CessProjectDetailsModel CessCollectionDetailsRecord(int EstablishmentID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_EstablishmentID", EstablishmentID);
            var keyValuePairs = _projectdetailsRepository.QueryMultipleByProcedure(SPConstants.GetRecordCessCollectionDetails, param);
            var response = new CessProjectDetailsModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new CessProjectDetailsModel
                {
                    EstablishmentID = (int)x.EstablishmentID,                  
                    EstablishmentName = (string)x.EstablishmentName,
                    ProjectID = (int)x.ProjectID,
                    TypeOfBodyID = (int)x.TypeOfBodyID,
                    CessPaymentID = (int)x.CessPaymentID,
                    Amount = (decimal)x.Amount,
                    ChallanNumber = (string)x.ChallanNumber,                    
                    Date = Convert.ToDateTime(x.Date),                   
                    IsActive = Convert.ToBoolean(x.IsActive),
                    Doc1 = (string)x.Doc1,
                   

                }).FirstOrDefault();
            };
            return response;
        }

        public CessProjectDetailsModel SaveCessCollectionDetails(CessProjectDetailsModel ObjCessCollectdtl)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_EstablishmentID", ObjCessCollectdtl.EstablishmentID);
            param.Add("@p_EstablishmentName", ObjCessCollectdtl.EstablishmentName);
            param.Add("@p_ProjectID", ObjCessCollectdtl.ProjectID);
            param.Add("@p_TypeOfBodyID", ObjCessCollectdtl.TypeOfBodyID);
            param.Add("@p_CessPaymentID", ObjCessCollectdtl.CessPaymentID);
            param.Add("@p_Amount", ObjCessCollectdtl.Amount);
            param.Add("@p_ChallanNumber", ObjCessCollectdtl.ChallanNumber);
            param.Add("@p_Date", ObjCessCollectdtl.Date);           
            param.Add("@p_IsActive", ObjCessCollectdtl.IsActive);
            param.Add("@p_IsDeleted", ObjCessCollectdtl.IsDeleted);
            param.Add("@p_Doc1", ObjCessCollectdtl.Doc1);
            param.Add("@p_PositionID", ObjCessCollectdtl.PositionID);
            param.Add("@p_PositionDistrictID", ObjCessCollectdtl.PositionDistrictID);

            var keyValuePairs = _projectdetailsRepository.QueryMultipleByProcedure(SPConstants.UpdateCessCollectionDetails, param);
            var response = new CessProjectDetailsModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new CessProjectDetailsModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    //IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public List<CessProjectDetailsModel> CessCollectionList(CessProjectDetailsModel ObjCessCollectdtl)
        {
            List<CessProjectDetailsModel> lstCessCollectionDetails = new List<CessProjectDetailsModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_PositionID", ObjCessCollectdtl.PositionID);
            param.Add("@p_PositionDistrictID", ObjCessCollectdtl.PositionDistrictID);
            var keyValuePairs = _projectdetailsRepository.QueryMultipleByProcedure(SPConstants.GetListCessCollectionDetails, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstCessCollectionDetails = result1.Select(x => new CessProjectDetailsModel
                {
                    EstablishmentID = (int)x.EstablishmentID,
                    EstablishmentName = (string)x.EstablishmentName,
                    ProjectName = (string)x.ProjectName,
                    Amount = (decimal)x.Amount,
                    Datee = (string)x.Date,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstCessCollectionDetails;
        }
    }
}
