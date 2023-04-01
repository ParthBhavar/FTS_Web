using FTS.Data.Common;
using FTS.Data.ConciliationApplication;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ConciliationApplication
{
    public class ConciliationApplicationRepository : IConciliationApplicationRepository
    {

        #region Private Variables
        private readonly IRepository<ConciliationApplicationModel> _conciliationRepository;
        #endregion

        #region Constructor
        public ConciliationApplicationRepository(IRepository<ConciliationApplicationModel> ConciliationRepository)
        {
            _conciliationRepository = ConciliationRepository;

        }
        #endregion

        public List<ConciliationApplicationModel> ConciliationList(ConciliationApplicationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<ConciliationApplicationModel> lstConciliation = new List<ConciliationApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            //param.Add("@p_DistrictID", model.DistrictID);
            //param.Add("@p_TalukaID", model.TalukaCode);
             param.Add("@p_PositionID", model.PositionID);
            param.Add("@p_PositionDistrictID", model.PositionDistrictID);

            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetConciliationAppliactionList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstConciliation = result1.Select(x => new ConciliationApplicationModel
                {
                    //ApplicationID = (int)x.ApplicationID,
                    //ApplicantName = (string)x.ApplicantName,
                    //IsActive = Convert.ToBoolean(x.IsActive),
                    //IsStatus = Convert.ToBoolean(x.IsStatus),
                    ID = (int)x.ID,
                    AppID = (string)x.AppID,
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    ApplicationMode = (string)x.ApplicationMode,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    Status = Convert.ToBoolean(x.Status),
                    StatusName = (string)x.StatusName,
                    AppDate = (string)x.CreatedOn,
                    UserMode = (int)x.UserMode,
                    ActionCode = (int)x.ActionCode,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    Query = (string)x.Query,

                }).ToList();
            };
            return lstConciliation;
        }

        public ConciliationApplicationModel AppliactionRecord(int ApplicationID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", ApplicationID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetRecordConciliationAppliaction, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    ApplicantEmailId = (string)x.ApplicantEmailId,
                    Address1 = (string)x.Address1,
                    Address2 = (string)x.Address2,
                    DistrictID = (int)x.DistrictId,
                    TalukaID = (int)x.TalukaCode,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Pincode = (int)x.PinCode,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    Status = Convert.ToBoolean(x.Status),
                    IsActive = Convert.ToBoolean(x.IsActive),
                    IsSendHO = Convert.ToBoolean(x.IsSendHO)
                    //Pincode = (int)x.Pincode,
                    //EstablishmentCode =(string)x.EstablishmentCode,
                    //TradeUnionRegistrationNo = (string)x.TradeUnionRegistrationNo,
                    //TradeUnionRegistrationName =(string)x.p_TradeUnionRegistrationName,

                    //TradeUnionAddress = (string)x.TradeUnionAddress,
                    //TradeUnionDistrict = (int)x.TradeUnionDistrict,
                    //TradeUnionTaluka = (int)x.TradeUnionTaluka,
                    //TradeUnionPincode = (int)x.TradeUnionPincode,
                    //TotalNoOfWorkingEmpInUnion = (int)x.TotalNoOfWorkingEmpInUnion,
                    //NumberOfEmpUnderDemand = (int)x.NumberOfEmpUnderDemand,
                    //DepartmentName = (string)x.DepartmentName,

                    //BusinessOfEstablishment = (string)x.BusinessOfEstablishment,
                    //Demand = (string)x.Demand,
                    //Remarks = (string)x.Remarks,
                    //CopyofCharteredDemandFile = (string)x.CopyofCharteredDemandFile,
                    //LetterOfIntervention = (string)x.LetterOfIntervention,
                    //OwnerCopyOfChartered = (string)x.OwnerCopyOfChartered,
                    //Conciliationcol = (string)x.Conciliationcol,

                    //MembershipVerificaionDetail = (string)x.MembershipVerificaionDetail,
                    //AnnualReturnStatement = (string)x.AnnualReturnStatement,
                    //IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    //IsStatus = Convert.ToBoolean(x.IsStatus),
                    //IsActive = Convert.ToBoolean(x.IsActive),
                    //IsFinal = (int)x.IsFinal,
                    //Status = (int)x.Status,

                }).FirstOrDefault();
            };
            return response;
        }
        public List<ConciliationApplicationModel> ConcilEstablishmentRecord(int AppliactionID, int ISNew)
        {
            List<ConciliationApplicationModel> lstReiniEstablishment = new List<ConciliationApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", AppliactionID);
            param.Add("@p_ISNew", ISNew);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetRecordConcilEstablishment, param);
            ;
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstReiniEstablishment = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    EstablisDetailID = (int)x.EstablisDetailID,
                    EstablishmentCode = (string)x.EstablishmentCode,
                    EstablishmentName = (string)x.EstablishmentName,
                    EstablishEmailId = (string)x.EstablishEmailId,
                    MobileNo = (string)x.MobileNo,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Pincode = (int)x.Pincode,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    isReqiredTradDetail = Convert.ToBoolean(x.isReqiredTradDetail),
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstReiniEstablishment;
        }
        public ConciliationApplicationModel SaveConcilEstablishmentRecord(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_XMLString", Obj.XMLString);
            param.Add("@p_isReqiredTradDetail", true);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConcilEstablishmentDetail, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    ISNew = (int)x.ISNew,
                    isReqiredTradDetail = Convert.ToBoolean(x.isReqiredTradDetail),

                }).FirstOrDefault();
            };
            return response;
        }

        public List<ConciliationApplicationModel> ConciliationTradunionRecord(int ApplicationID, int ISNew)
        {
            List<ConciliationApplicationModel> lstConciclTradunion = new List<ConciliationApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", ApplicationID);
            param.Add("@p_ISNew", ISNew);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetRecordConcilTradunionregistration, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstConciclTradunion = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    TradeDetailID = (int)x.TradeDetailID,
                    TradeUnionRegistrationNo = (string)x.TradeUnionRegistrationNo,
                    TradeUnionRegistrationName = (string)x.TradeUnionRegistrationName,
                    TradeUnionPAddress = (string)x.TradeUnionPAddress,
                    TradeUnionSAddress = (string)x.TradeUnionSAddress,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    TradeUnionPincode = (int)x.Pincode,
                    TotalNoOfWorkingEmpInUnion = (int)x.TotalNoOfWorkingEmpInUnion,
                    TotalWorkingEmpInEstablishment = (int)x.TotalWorkingEmpInEstablishment,
                    NumberOfEmpUnderDemand = (int)x.NumberOfEmpUnderDemand,
                    DepartmentName = (string)x.DepartmentName,
                    BusinessOfEstablishment = (string)x.BusinessOfEstablishment,
                    Demand = (string)x.Demand,
                    Remarks = (string)x.Remarks,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstConciclTradunion;
        }

        public ConciliationApplicationModel SaveAppliactionRecord(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_ApplicantName", Obj.ApplicantName);
            param.Add("@p_ApplicantEmailId", Obj.ApplicantEmailId);
            param.Add("@p_Address1", Obj.Address1);
            param.Add("@p_Address2", Obj.Address2);
            param.Add("@p_DistrictId", Obj.DistrictID);
            param.Add("@p_TalukaCode", Obj.TalukaCode);
            param.Add("@p_Pincode", Obj.Pincode);
            param.Add("@p_IPAddress", Obj.IP_Address);
            param.Add("@p_Url", Obj.URL);
            param.Add("@p_ZoneID", Obj.ZoneID);
            param.Add("@p_AreaID", Obj.AreaID);
            param.Add("@p_UserMode", Obj.UserMode);
            param.Add("@p_IsSendHO", Obj.IsSendHO);
            param.Add("@p_PositionID", Obj.PositionID);
            param.Add("@p_PositionDistrictID", Obj.PositionDistrictID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.ConciliationAddEditAppliaction, param);

            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public ConciliationApplicationModel SaveConciliationTradunionRecord(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_XMLString", Obj.XMLString);
            param.Add("@p_isReqiredTradDetail", true);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConciliationTradeUnionDetail, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    ISNew = (int)x.ISNew,
                    //isReqiredTradDetail = Convert.ToBoolean(x.isReqiredTradDetail),

                }).FirstOrDefault();
            };
            return response;

            //DynamicParameters param = new DynamicParameters();
            //param.Add("@p_TradeUnionRegistrationNo", Obj.TradeUnionRegistrationNo);
            //param.Add("@p_TradeUnionRegistrationName", Obj.TradeUnionRegistrationName);
            //param.Add("@p_TradeUnionAddress", Obj.TradeUnionAddress);
            //param.Add("@p_TradeUnionAddress1", Obj.TradeUnionAddress1);
            //param.Add("@p_TradeUnionDistrict", Obj.TradeUnionDistrict);
            //param.Add("@p_TradeUnionTaluka", Obj.TradeUnionTaluka);
            //param.Add("@p_TradeUnionPincode", Obj.TradeUnionPincode);
            //param.Add("@p_TotalNoOfWorkingEmpInUnion", Obj.TotalNoOfWorkingEmpInUnion);
            //param.Add("@p_TotalWorkingEmpInEstablishment", Obj.TotalWorkingEmpInEstablishment);
            //param.Add("@p_NumberOfEmpUnderDemand", Obj.NumberOfEmpUnderDemand);
            //param.Add("@p_DepartmentName", Obj.DepartmentName);
            //param.Add("@p_BusinessOfEstablishment", Obj.BusinessOfEstablishment);
            //param.Add("@p_Demand", Obj.Demand);
            //param.Add("@p_Remarks", Obj.Remarks);
            //param.Add("@p_ApplicationID", Obj.ApplicationID);
            //var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConciliationTradeUnionDetail, param);
            //var response = new ConciliationApplicationModel();
            //if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            //{
            //    response = result1.Select(x => new ConciliationApplicationModel
            //    {
            //        ErrorCode = (int)x.ErrorCode,
            //        ErrorMassage = (string)x.ErrorMassage,
            //        ApplicationID = (int)x.ApplicationID,
            //        //ISNew = (int)x.ISNew,
            //        // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

            //    }).FirstOrDefault();
            //};
            //return response;

        }

        public ConciliationApplicationModel SaveDocumnetandapplication(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_fileName", Obj.fileName);
            param.Add("@p_DocumentID", Obj.DocumentID);
            param.Add("@P_isSubmit", Obj.IsSubmit);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_AppID", Obj.AppID);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConcilAppliactionFinalsubmit, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    EMailIDList = (string)x.MailID,
                    EmailSubject = (string)x.EmailSubject,
                    EmailBody = (string)x.EmailBody,

                }).FirstOrDefault();
            };
            return response;
        }


        public ConciliationApplicationModel ConcilAppliactionDocumentURLRecord(int AppliactionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", AppliactionID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetRecordConcilAppliactionDocumentURL, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    CopyofCharteredDemandFile = (string)x.CopyofCharteredDemandFile,
                    LetterOfIntervention = (string)x.LetterOfIntervention,
                    OwnerCopyOfChartered = (string)x.OwnerCopyOfChartered,
                    MembershipVerificaionDetail = (string)x.MembershipVerificaionDetail,
                    AnnualReturnStatement = (string)x.AnnualReturnStatement,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    ActionCode = (int)x.ActionCode,
                }).FirstOrDefault();
            };
            return response;
        }
        public ConciliationApplicationModel ConciliationAppliactionUpdateSubmit(ConciliationApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@P_isSubmit", Obj.IsSubmit);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_AppID", Obj.AppID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConciliationAppliactionUpdateSubmit, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //EMailIDList = (string)x.MailID,
                    //EmailSubject = (string)x.EmailSubject,
                    //EmailBody = (string)x.EmailBody,

                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                ApplicantMailDetail = result2.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body
                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                EshtablishmentMailDetail = result3.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body

                }).ToList();
            };
            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            return response;
        }

        public List<ConciliationApplicationModel> ConciliationACLList(ConciliationApplicationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<ConciliationApplicationModel> lstConciliaiton = new List<ConciliationApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetConciliationACLList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstConciliaiton = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    StatusName = (string)x.Status,
                    ActionCode = (int)x.ActionCode,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    HearingDateString = (string)x.HearingDate,
                    ResolutionReason = (string)x.ResolutionStatus,
                }).ToList();
            };
            return lstConciliaiton;
        }

        public ConciliationApplicationModel ConciliationACLRecord(int AppliactionID, int ISNew)
        {
            DynamicParameters param = new DynamicParameters();
            List<ConciliationApplicationModel> HearingdetailACL = new List<ConciliationApplicationModel>();
            List<ConciliationApplicationModel> EstalishmentdetailACL = new List<ConciliationApplicationModel>();
            param.Add("@p_AppliactionID", AppliactionID);
            param.Add("@p_ISNew", ISNew);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetRecordConciliationACL, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    HearingDate = (DateTime)x.HearingDate,
                    HearingRemarks = (string)x.HearingRemarks,
                    HearingReason = (string)x.HearingReason,

                    SettlementDate = Convert.ToDateTime(x.SettlementDate),
                    SettlementRemark = (string)x.SettlementRemark,
                    SettlementDocURL = (string)x.SettlementDocURL,
                    ResolutionReasonID = (int)x.ResolutionReasonID,

                    SendLCDate = Convert.ToDateTime(x.SendLCDate),
                    SendLCRemark = (string)x.SendLCRemark,
                    ActionCode = (int)x.ActionCode,
                    OrderOutwardDate = Convert.ToDateTime(x.OrderOutwardDate),
                    OrderOutwardNo = (int)x.OrderOutwardNo,
                   


                    ISNew = (int)ISNew,
                }).FirstOrDefault();

                if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
                {
                    EstalishmentdetailACL = result2.Select(x => new ConciliationApplicationModel
                    {
                        ApplicationID = (int)x.ApplicationID,
                        EstablisDetailID = (int)x.EstablisDetailID,
                        EstablishmentName = (string)x.EstablishmentName,
                        DistrictID = (int)x.DistrictID,
                        TalukaID = (int)x.TalukaID,
                        ZoneID = (int)x.ZoneID,
                        AreaID = (int)x.AreaID,
                        Pincode = (int)x.Pincode,
                        EstablishEmailId = (string)x.EstablishEmailId,
                        EstablisMobileNo = (string)x.MobileNo,
                    }).ToList();
                }

                if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
                {
                    HearingdetailACL = result3.Select(x => new ConciliationApplicationModel
                    {
                        ApplicationID = (int)x.ApplicationID,
                        Id = (int)x.Id,
                        HearingDate = Convert.ToDateTime(x.HearingDate),
                        HearingRemarks = (string)x.HearingRemarks,
                        HearingReasonID = (int)x.HearingReasonID,
                        IsActive = Convert.ToBoolean(x.IsActive),
                        IsCancle = Convert.ToBoolean(x.IsCancle),
                    }).ToList();
                }
            };
            response.HearingdetailACL = HearingdetailACL;
            response.EstalishmentdetailACL = EstalishmentdetailACL;
            return response;
        }
        public ConciliationApplicationModel SaveConciliationHearingACLRecord(ConciliationApplicationModel Obj)
        {

            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            List<EmailReportModel> EmailReportDetail = new List<EmailReportModel>();
            DynamicParameters param = new DynamicParameters();
           
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_XMLString", Obj.XMLString);
            param.Add("@p_IsNotice", Obj.IsNotice);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConciliationHearingACL, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //EMailIDList = (string)x.MailID,
                    //EmailSubject = (string)x.EmailSubject,
                    //EmailBody = (string)x.EmailBody,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                ApplicantMailDetail = result2.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body
                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                EshtablishmentMailDetail = result3.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body

                }).ToList();
            };
            if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
            {
                EmailReportDetail = result4.Select(x => new EmailReportModel
                {
                    DictrictACLOffice = (string)x.DictrictACLOffice,
                    Date = (string)x.Date,
                    Establishmentdetail = (string)x.Establishmentdetail,
                    ApplicantDetail = (string)x.ApplicantDetail,
                    AppID = (string)x.AppID,
                    HearingDate = (string)x.HearingDate,
                    HearingTime = (string)x.HearingTime,
                    ACLDistrict = (string)x.ACLDistrict
                   
                }).ToList();
            };

            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            response.EmailReportDetail = EmailReportDetail;
            return response;
        }
        public ConciliationApplicationModel SaveConciliationSendtolabourACLRecord(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@P_SendLCDate", Obj.SendLCDate);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_SendLCRemark", Obj.SendLCRemark);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConciliationSendLCACL, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public ConciliationApplicationModel SaveConciliationACLSettlementrecord(ConciliationApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@P_SettlementDate", Obj.SettlementDateIn);
            param.Add("@p_SettlementRemark", Obj.SettlementRemark);
            param.Add("@p_SettlementDoc", Obj.fileName);
            param.Add("@p_ReasonID", Obj.ResolutionReasonID);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_OrderOutwardDate", Obj.OrderOutwardDatee);
            param.Add("@p_OrderOutwardNo", Obj.OrderOutwardNo);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConciliationSettlementACL, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //EMailIDList = (string)x.MailID,
                    //EmailSubject = (string)x.EmailSubject,
                    //EmailBody = (string)x.EmailBody,

                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                ApplicantMailDetail = result2.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body
                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                EshtablishmentMailDetail = result3.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body

                }).ToList();
            };
            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            return response;
        }


        #region conciliaion History
        public ConciliationApplicationModel ConciliationHistory(ConciliationApplicationModel Obj)
        {
             List<ConciliationApplicationModel> lstConciliationApplicationdetail = new List<ConciliationApplicationModel>();
            List<ConciliationApplicationModel> lstConciliationEshtablishDetail = new List<ConciliationApplicationModel>();
            List<ConciliationApplicationModel> lstConciliattionTradeUnionDetail = new List<ConciliationApplicationModel>();
            List<ConciliationApplicationModel> lstConciliationSettlementDetail = new List<ConciliationApplicationModel>();
            //List<ConciliationApplicationModel> lstConciliationSendLCDetail = new List<ConciliationApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConciliationApplicationHistory, param);
            //DataSet keyValuePairsDs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConciliationApplicationHistory, param);

            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,

                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                lstConciliationApplicationdetail = result2.Select(x => new ConciliationApplicationModel
                {
                    ApplicantName = (string)x.ApplicantName,
                    Address1 = (string)x.Address1,
                    Address2 = (string)x.Address2,
                    ApplicantEmailId =(string)x.ApplicantEmailId,
                    DistrictName = (string)x.DistrictName,
                    TalukaName = (string)x.TalukaName,
                    Pincode = (int)x.Pincode,
                    ZoneName = (string)x.ZoneName,
                    AreaName = (string)x.AreaName,
                    CopyofCharteredDemandFile = (string)x.CopyofCharteredDemandFile,
                    LetterOfIntervention = (string)x.LetterOfIntervention,
                    OwnerCopyOfChartered = (string)x.OwnerCopyOfChartered,
                    MembershipVerificaionDetail = (string)x.MembershipVerificaionDetail,
                    AnnualReturnStatement = (string)x.AnnualReturnStatement


                }).ToList();
            };
            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                lstConciliationEshtablishDetail = result3.Select(x => new ConciliationApplicationModel
                {
                    EstablishmentCode = (string)x.EstablishmentCode,
                    EstablishmentName = (string)x.EstablishmentName,
                    EstablishEmailId =(string)x.EstablishEmailId,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictName = (string)x.DistrictName,
                    TalukaName = (string)x.TalukaName,
                    ZoneName = (string)x.ZoneName,
                    AreaName = (string)x.AreaName,
                    Pincode = (int)x.Pincode

                }).ToList();
            };
            if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
            {
                lstConciliattionTradeUnionDetail = result4.Select(x => new ConciliationApplicationModel
                {
                    TradeUnionRegistrationNo = (string)x.TradeUnionRegistrationNo,
                    TradeUnionRegistrationName = (string)x.TradeUnionRegistrationName,
                    TradeUnionAddress = (string)x.TradeUnionPAddress,
                    TradeUnionAddress1 = (string)x.TradeUnionSAddress,
                    TotalNoOfWorkingEmpInUnion = (int)x.TotalNoOfWorkingEmpInUnion,
                    TotalWorkingEmpInEstablishment = (int)x.TotalWorkingEmpInEstablishment,
                    NumberOfEmpUnderDemand = (int)x.NumberOfEmpUnderDemand,
                    DepartmentName = (string)x.DepartmentName,
                    BusinessOfEstablishment = (string)x.BusinessOfEstablishment,
                    Demand = (string)x.Demand,
                    Remarks = (string)x.Remarks,
                    Pincode = (int)x.Pincode,
                    DistrictName =(string)x.DistrictName,
                    TalukaName = (string)x.TalukaName,

                }).ToList();
            };
            //if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
            //{
            //    lstConciliationhearingDetail = result4.Select(x => new ConciliationApplicationModel
            //    {
            //        HearingDate = Convert.ToDateTime(x.HearingDate),
            //        HearingRemarks = (string)x.HearingRemarks,
            //        ReasonName = (string)x.ReasonName,


            //    }).ToList();
            //};
            if (keyValuePairs["result5"] is IEnumerable<dynamic> result5 && result5.Any())
            {
                lstConciliationSettlementDetail = result5.Select(x => new ConciliationApplicationModel
                {
                    SettlementDate = Convert.ToDateTime(x.SettlementDate),
                    SettlementRemark = (string)x.SettlmentRemark,
                    SettlementDocURL = (string)x.SettlementDocURL,


                }).ToList();
            };

            //if (keyValuePairs["result6"] is IEnumerable<dynamic> result6 && result6.Any())
            //{
            //    lstConciliationSendLCDetail = result6.Select(x => new ConciliationApplicationModel
            //    {
            //        SendLCDate = Convert.ToDateTime(x.SendLCDate),
            //        SendLCRemark = (string)x.SendLCRemark,
            //        SettlementDocURL = (string)x.SettlementDocURL,


            //    }).ToList();
            //};

            response.basicdetailtlst = lstConciliationApplicationdetail;
            response.establishdetaillst = lstConciliationEshtablishDetail;
            response.ConcilTradUnionDetail = lstConciliattionTradeUnionDetail;
            response.Settlementdetaillst = lstConciliationSettlementDetail;
            //response.SendLCdetaillst = lstConciliationSendLCDetail;
            return response;

        }
        #endregion
        public ConciliationApplicationModel ConcilupdateClerkStatustoACL(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            List<EmailReportModel> EmailReportDetail = new List<EmailReportModel>();

            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_IsPUS", Obj.IsPUS);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConciliationClerkStatus, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                ApplicantMailDetail = result2.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body
                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                EshtablishmentMailDetail = result3.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body

                }).ToList();
            };
            if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
            {
                EmailReportDetail = result4.Select(x => new EmailReportModel
                {
                    ACLDistrict = (string)x.ACLDistrict,
                    DictrictACLOffice = (string)x.DictrictACLOffice,
                    Date = (string)x.Date,
                    ApplicantName = (string)x.ApplicantName,
                    Establishmentdetail = (string)x.Establishmentdetail,
                    AppID = (string)x.AppID,
                    EstablishmentName = (string)x.EstablishmentName

                }).ToList();
            };
            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            response.EmailReportDetail = EmailReportDetail;
            return response;
        }
        public ConciliationApplicationModel UpdateQueryByACLClerk(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_Query", Obj.Query);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConcilQueryByACLCLerk, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    EMailIDList = (string)x.MailID,
                    EmailSubject = (string)x.EmailSubject,
                    EmailBody = (string)x.EmailBody,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
        public ConciliationApplicationModel ConcilupdateACLClerk(string id)
        {
            DynamicParameters param = new DynamicParameters();
            List<ConciliationApplicationModel> lstConciliationApplicationdetail = new List<ConciliationApplicationModel>();
            List<ConciliationApplicationModel> lstConciliationEshtablishDetail = new List<ConciliationApplicationModel>();

            param.Add("@p_ApplicationID", id);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetRecordConcilACLClerk, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,

                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                lstConciliationApplicationdetail = result2.Select(x => new ConciliationApplicationModel
                {
                    ApplicantName = (string)x.ApplicantName,
                    Address1 = (string)x.Address1,
                    Address2 = (string)x.Address2,
                    DistrictName = (string)x.DistrictName,
                    TalukaName = (string)x.TalukaName,
                    Pincode = (int)x.Pincode,
                    ZoneName = (string)x.ZoneName,
                    AreaName = (string)x.AreaName,
                    TradeUnionRegistrationNo = (string)x.TradeUnionRegistrationNo,
                    TradeUnionRegistrationName = (string)x.TradeUnionRegistrationName,
                    TradeUnionAddress = (string)x.TradeUnionAddress,
                    TradeUnionAddress1 = (string)x.TradeUnionAddress1,
                    TotalNoOfWorkingEmpInUnion = (int)x.TotalNoOfWorkingEmpInUnion,
                    TotalWorkingEmpInEstablishment = (int)x.TotalWorkingEmpInEstablishment,
                    NumberOfEmpUnderDemand = (int)x.NumberOfEmpUnderDemand,
                    DepartmentName = (string)x.DepartmentName,
                    BusinessOfEstablishment = (string)x.BusinessOfEstablishment,
                    Demand = (string)x.Demand,
                    Remarks = (string)x.Remarks,
                    CopyofCharteredDemandFile = (string)x.CopyofCharteredDemandFile,
                    LetterOfIntervention = (string)x.LetterOfIntervention,
                    OwnerCopyOfChartered = (string)x.OwnerCopyOfChartered,
                    MembershipVerificaionDetail = (string)x.MembershipVerificaionDetail,
                    AnnualReturnStatement = (string)x.AnnualReturnStatement


                }).ToList();
            };
            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                lstConciliationEshtablishDetail = result3.Select(x => new ConciliationApplicationModel
                {
                    EstablishmentCode = (string)x.EstablishmentCode,
                    EstablishmentName = (string)x.EstablishmentName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictName = (string)x.DistrictName,
                    TalukaName = (string)x.TalukaName,
                    ZoneName = (string)x.ZoneName,
                    AreaName = (string)x.AreaName,
                    Pincode = (int)x.Pincode

                }).ToList();
            };

            if (keyValuePairs["result7"] is IEnumerable<dynamic> result7 && result7.Any())
            {
                response = result7.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    IsPUS = (bool)x.IsPUS,
                    Query = (string)x.Query
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            response.basicdetailtlst = lstConciliationApplicationdetail;
            response.establishdetaillst = lstConciliationEshtablishDetail;
            return response;
        }

        public List<ConciliationApplicationModel> ConciliationDCLClerkList(ConciliationApplicationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<ConciliationApplicationModel> lstConciliation = new List<ConciliationApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetConciliationDCClerkList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstConciliation = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    StatusName = (string)x.Status,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstConciliation;
        }

        public ConciliationApplicationModel UpdateStatusByDCLClerk(ConciliationApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_DCLReferId", Obj.DCLReferStatusID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConcilStatusByDCLClerk, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                ApplicantMailDetail = result2.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body
                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                EshtablishmentMailDetail = result3.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body

                }).ToList();
            };
            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            return response;
        }
        public ConciliationApplicationModel ConciliationDCLClerkRecord(int ApplicationID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", ApplicationID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetRecordConciliationDCLClerk, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    DCLReferStatusID = (int)x.DCLReferStatusID,
                }).FirstOrDefault();
            };
            return response;

        }
        public List<ConciliationApplicationModel> ConciliationDCLList(ConciliationApplicationModel model)
        {
            List<ConciliationApplicationModel> lstConciliation = new List<ConciliationApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetConciliationDCLList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstConciliation = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    StatusName = (string)x.Status,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstConciliation;
        }
        public ConciliationApplicationModel ConciliationDCLRecord(int ApplicationID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", ApplicationID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetRecordConciliationDCLClerk, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    DCLReferStatusID = (int)x.DCLReferStatusID,
                }).FirstOrDefault();
            };
            return response;

        }
        public ConciliationApplicationModel UpdateStatusByDCL(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            List<EmailReportModel> EmailReportDetail = new List<EmailReportModel>();
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_DCLReferId", Obj.DCLReferStatusID);

            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConcilStatusByDCL, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                ApplicantMailDetail = result2.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body
                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                EshtablishmentMailDetail = result3.Select(x => new MailDetail
                {
                    Email = (string)x.Email,
                    Subject = (string)x.Subject,
                    Body = (string)x.Body

                }).ToList();
            };
            if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
            {
                EmailReportDetail = result4.Select(x => new EmailReportModel
                {
                    ACLDistrict = (string)x.DCLDistrict,
                    DictrictACLOffice = (string)x.DictrictDCLOffice,
                    Date = (string)x.Date,
                    ShakhaNo = (string)x.Shakhano,
                    ApplicantName = (string)x.ApplicantName,
                    Establishmentdetail = (string)x.Establishmentdetail,
                    AppID = (string)x.AppID

                }).ToList();
            };
            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            response.EmailReportDetail = EmailReportDetail;
            return response;
        }
        public List<ConciliationApplicationModel> ConciliationHOClerkList(ConciliationApplicationModel model)
        {
            List<ConciliationApplicationModel> lstConciliation = new List<ConciliationApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetConciliationHOClerkList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstConciliation = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    StatusName = (string)x.Status,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstConciliation;
        }

        public ConciliationApplicationModel ConciliationHOClerkRecord(int ApplicationID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", ApplicationID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetRecordConcilHOClerk, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    IsPUS = (bool)x.IsPUS,
                    Query = (string)x.Query
                }).FirstOrDefault();
            };
            return response;

        }
        public ConciliationApplicationModel ConcilupdateHOClerkStatus(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_HOReferStatusID", Obj.HOReferStatusID);
            param.Add("@p_IsPUS", Obj.IsPUS);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConcilStatusByHOClerk, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
        public List<ConciliationApplicationModel> ConciliationHOList(ConciliationApplicationModel model)
        {
            List<ConciliationApplicationModel> lstConciliation = new List<ConciliationApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetConciliationHOList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstConciliation = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    StatusName = (string)x.Status,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstConciliation;
        }

        public ConciliationApplicationModel ConciliationHORecord(int ApplicationID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", ApplicationID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.GetRecordConcilHO, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    IsPUS = (bool)x.IsPUS,
                    HOReferStatusID = (int)x.HOReferStatusID
                }).FirstOrDefault();
            };
            return response;

        }
        public ConciliationApplicationModel UpdateStatusByHO(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_HOReferStatusID", Obj.HOReferStatusID);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConcilStatusByHO, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
        public ConciliationApplicationModel UpdateQueryByHOClerk(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_Query", Obj.Query);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConcilQueryByHOCLerk, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    EMailIDList = (string)x.MailID,
                    EmailSubject = (string)x.EmailSubject,
                    EmailBody = (string)x.EmailBody,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
        public ConciliationApplicationModel UpdateEshtablishmentdetailByACL(ConciliationApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_EstablisDetailID", Obj.EstablisDetailID);
            param.Add("@p_DistrictID", Obj.DistrictID);
            param.Add("@p_TalukaID", Obj.TalukaID);
            param.Add("@p_AreaID", Obj.AreaID);
            param.Add("@p_ZoneID", Obj.ZoneID);
            param.Add("@p_Pincode", Obj.Pincode);
            param.Add("@p_EstablishEmailId", Obj.EstablishEmailId);
            param.Add("@p_EstablishMobile", Obj.EstablisMobileNo);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _conciliationRepository.QueryMultipleByProcedure(SPConstants.UpdateConciliationUpdatedetailFrom, param);
            var response = new ConciliationApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ConciliationApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    EMailIDList = (string)x.MailID,
                    EmailSubject = (string)x.EmailSubject,
                    EmailBody = (string)x.EmailBody,
                }).FirstOrDefault();
            };
            return response;
        }
    }
}
