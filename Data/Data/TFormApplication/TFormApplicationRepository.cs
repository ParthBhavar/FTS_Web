using FTS.Data.Common;
using FTS.Model.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.TFormApplication
{
    public class TFormApplicationRepository : ITFormApplicationRepository
    {
        #region Private Variables
        private readonly IRepository<TFormAppealApplicationModel> _TFormApplicationRepository;
        #endregion

        #region Constructor
        public TFormApplicationRepository(IRepository<TFormAppealApplicationModel> TFormApplicationRepo)
        {
            _TFormApplicationRepository = TFormApplicationRepo;

        }
        #endregion
        public List<TFormAppealApplicationModel> TFormApplicationList(TFormAppealApplicationModel model)
        {
           
            List<TFormAppealApplicationModel> lstNForm = new List<TFormAppealApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.GetTFormApplicationList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstNForm = result1.Select(x => new TFormAppealApplicationModel
                {
                    AppealID = (int)x.AppealID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.AppDate,
                    StatusName=(string)x.StatusName,
                    RefNApplicationID =(int)x.RefNApplicationID,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstNForm;
        }
        public TFormAppealApplicationModel TFormForEmployeeRecord(int AppealID)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
           
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", AppealID);
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.GetRecordTFormEmployee, param);
            var response = new TFormAppealApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    ActionCode = (int)x.ActionCode,
                    CaseFavourIn = (int)x.CaseFavourIn,
                    Status = (string)x.Status,
                  
                }).FirstOrDefault();
            };

            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;

            return response;
        }
        public TFormAppealApplicationModel TFormForEmployerRecord(int AppealID)
        {
            List<MCommonList> lstCommonList = new List<MCommonList>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_AppealID", AppealID);
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.GetRecordTFormEmployer, param);
            var response = new TFormAppealApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    AppealID = (int)x.AppealID,
                    ApplicantName = (string)x.ApplicantName,
                    RefNApplicationID = (int)x.RefNApplicationID,
                     ChallanNo = (string)x.ChallanNo,
                    ChallanAmount = (decimal)x.ChallanAmount,
                    DateOfChallan = (DateTime)x.DateOfChallan,
                    ChallanFile = (string)x.ChallanFile,
                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                response = result2.Select(x => new TFormAppealApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    Status = (string)x.Status,
                    ApplicantName = (string)x.ApplicantName,
                    CaseFavourIn = (int)x.CaseFavourIn,
                    DateOfChallan = (DateTime)x.DateOfChallan,
                    DiffDay = (int)x.DiffDay,
                }).FirstOrDefault();
            };
            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                lstCommonList = result3.Select(x => new MCommonList
                {
                    DataValue = (int)x.DataValue,
                    DisplayValue = (string)x.DisplayValue,
                }).ToList();
            };
            response.GraduityAppIDList = lstCommonList;
            return response;
        }
        public TFormAppealApplicationModel SaveTFormForEmployee(TFormAppealApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicantName", Obj.ApplicantName);
            param.Add("@P_RefNApplicationID", Obj.AppealID);
            param.Add("@P_ActionCode", 2);
            param.Add("@P_UserMode", 2);
            param.Add("@P_CaseFavourIn", Obj.CaseFavourIn);
            param.Add("@P_AppID", Obj.AppID);
            param.Add("@P_AppealReason", Obj.AppealReason);
            param.Add("@p_URL", Obj.URL); 
            param.Add("@p_IPAddress", Obj.IPAddress);
           
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.AddTFormApplicationforEmployee, param);
            var response = new TFormAppealApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    AppealID = (int)x.AppealID,
               
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
        public List<TFormAppealApplicationModel> TFormApplicationDCLList(TFormAppealApplicationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<TFormAppealApplicationModel> lstTFormAppeal = new List<TFormAppealApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.GetTFormApplicationDCLLIst, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstTFormAppeal = result1.Select(x => new TFormAppealApplicationModel
                {
                    AppealID = (int)x.AppealID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    RefNApplicationID =(int)x.RefNApplicationID,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.AppDate,
                    ID = (int)x.ID,
                    StatusName = (string)x.Status,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    HearingDateString = (string)x.HearingDate,
                    ActionCode = (int)x.ActionCode,
                }).ToList();
            };
            return lstTFormAppeal;
        }
        public TFormAppealApplicationModel TFormDCLStatusUpdate(int AppealID, int ISNew)
        {
            List<TFormAppealApplicationModel> HearingdetailACL = new List<TFormAppealApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_AppealID", AppealID);
            param.Add("@p_ISNew", ISNew);
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.GetRecordTFormDCL, param);
            var response = new TFormAppealApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    AppealID = (int)x.AppealID,
                    HearingDate = (DateTime)x.HearingDate,
                    HearingRemarks = (string)x.HearingRemarks,
                    HearingReasonID = (int)x.HearingReasonID,

                    ReamandBackDate = Convert.ToDateTime(x.ReamandBackDate),
                    RemandRemark = (string)x.RemandRemark,

                    SettlementDate = Convert.ToDateTime(x.ConfirmDate),
                    ConfirmRemark = (string)x.ConfirmRemark,

                    DissmissDate = Convert.ToDateTime(x.DissmissDate),
                    DissmisRemark = (string)x.DissmisRemark,
                    RefNApplicationID  = (int)x.RefNApplicationID,

                    ISNew = (int)ISNew,
                }).FirstOrDefault();

                if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
                {
                    HearingdetailACL = result2.Select(x => new TFormAppealApplicationModel
                    {
                        AppealID = (int)x.AppealID,
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
            return response;
        }

        public TFormAppealApplicationModel SaveTFormHearingDCLRecord(TFormAppealApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            //param.Add("@p_UserID", Obj.UserID);
            //param.Add("@p_AppealID", Obj.AppealID);
            //param.Add("@P_HearingDate", Obj.HearingDate);
            //param.Add("@p_HearingReasonID", Obj.HearingReasonID);
            //param.Add("@p_HearingRemark", Obj.HearingRemarks);
            //param.Add("@p_URL", Obj.URL);
            //param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_XMLString", Obj.XMLString);
            param.Add("@p_IsNotice", Obj.IsNotice);
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.UpdateTFormHearingDCL, param);
            var response = new TFormAppealApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    AppealID = (int)x.AppealID,
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
        public TFormAppealApplicationModel SaveTFormResolutionBackDCLRecord(TFormAppealApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_AppealID", Obj.AppealID);
            param.Add("@P_RemandBackDate", Obj.ResolutionDateIn);
            param.Add("@p_RemandReamrk", Obj.RemandRemark); 
            param.Add("@p_ResolutionReasonId", Obj.ResolutionReasonID);
            param.Add("@p_RefNApplicationID", Obj.RefNApplicationID);
            param.Add("@p_ResolutionDoc", Obj.fileName);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address); 
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.UpdateTFormRemandBackDCL, param);
            var response = new TFormAppealApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    AppealID = (int)x.AppealID,
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
        public TFormAppealApplicationModel SaveTFormConfirmDCLRecord(TFormAppealApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_AppealID", Obj.AppealID);
            param.Add("@P_RemandBackDate", Obj.ConfirmDate);
            param.Add("@p_RemandRemark", Obj.ConfirmRemark);
           
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.UpdateTFormConfirmDCL, param);
            var response = new TFormAppealApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    AppealID = (int)x.AppealID,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
        public TFormAppealApplicationModel SaveTFormDismissDCLRecord(TFormAppealApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_AppealID", Obj.AppealID);
            param.Add("@P_DissmissDate", Obj.DissmissDate);
            param.Add("@p_DissmisReason", Obj.DissmisRemark);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.UpdateTFormDissmisDCL, param);
            var response = new TFormAppealApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    AppealID = (int)x.AppealID,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            
            return response;
        }
        public TFormAppealApplicationModel SaveTFormForEmployer(TFormAppealApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicantName", Obj.ApplicantName);
            param.Add("@P_RefNApplicationID", Obj.RefNApplicationID);
            param.Add("@P_ActionCode", 1);
            param.Add("@P_UserMode", 1);
            param.Add("@P_CaseFavourIn", Obj.CaseFavourIn);

            param.Add("@P_ChallanNo", Obj.ChallanNo);
            param.Add("@P_ChallanAmount", Obj.ChallanAmount);
            param.Add("@P_ChallanDate", Obj.DateOfChallan);
            param.Add("@P_ChallanFile", Obj.ChallanFile);

            param.Add("@P_AppID", Obj.AppID);
            param.Add("@P_AppealReason", Obj.AppealReason);
            param.Add("@p_URL", Obj.URL); 
            param.Add("@p_IPAddress", Obj.IPAddress);

            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.AddTFormApplicationforEmployer, param);
            var response = new TFormAppealApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    AppealID = (int)x.AppealID,

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

        #region TForm Application History
        public TFormAppealApplicationModel TFormDCLHistory(TFormAppealApplicationModel Obj)
        {
            List<TFormAppealApplicationModel> lstTFormApplicationdetail = new List<TFormAppealApplicationModel>();
            //List<TFormAppealApplicationModel> lstChallanDetails = new List<TFormAppealApplicationModel>();
            //List<TFormAppealApplicationModel> lstConciliationhearingDetail = new List<TFormAppealApplicationModel>();
            //List<TFormAppealApplicationModel> lstConciliationSettlementDetail = new List<TFormAppealApplicationModel>();
            //List<TFormAppealApplicationModel> lstConciliationSendLCDetail = new List<TFormAppealApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_AppealID", Obj.AppealID);
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.UpdateTFormHistory, param);
            var response = new TFormAppealApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    AppealID = (int)x.AppealID,

                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                lstTFormApplicationdetail = result2.Select(x => new TFormAppealApplicationModel
                {
                    ApplicantName = (string)x.ApplicantName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictName = (string)x.DistrictName,
                    TalukaName = (string)x.TalukaName,
                    Pincode = (int)x.Pincode,
                    ZoneName = (string)x.ZoneName,
                    AreaName = (string)x.AreaName,
                    ChallanNo = (string)x.ChallanNo,
                    ChallanAmount = (decimal)x.ChallanAmount,
                    DateOfChallan = (DateTime)x.DateOfChallan,
                    ChallanFile = (string)x.ChallanFile,

                }).ToList();
            };
           

            response.basicdetailtlst = lstTFormApplicationdetail;
            //response.Challandetailtlst = lstChallanDetails;


            return response;

        }
        #endregion

        public TFormAppealApplicationModel TFormACLRecord(int AppliactionID, int ISNew)
        {
            List<MCommonList> CaseFavourList = new List<MCommonList>();
            List<TFormAppealApplicationModel> EstalishmentdetailACL = new List<TFormAppealApplicationModel>();
            List<TFormAppealApplicationModel> HearingdetailACL = new List<TFormAppealApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_AppliactionID", AppliactionID);
            param.Add("@p_IsNew", ISNew);
            var keyValuePairs = _TFormApplicationRepository.QueryMultipleByProcedure(SPConstants.GetRecordNFormACL, param);
            var response = new TFormAppealApplicationModel();

           
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TFormAppealApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    HearingDate = Convert.ToDateTime(x.HearingDate),
                    HearingNote = (string)x.HearingNote,
                    HearingReasonID = (int)x.HearingReasonID,

                    SettlementDate = Convert.ToDateTime(x.SettlementDate),
                    SettlementNote = (string)x.SettlementNote,
                    SettlementDocURL = (string)x.SettlementDocURL,
                    CaseFavourIn = (int)x.CaseFavourIn,

                    ISNew = (int)ISNew,

                }).FirstOrDefault();

                if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
                {
                    CaseFavourList = result2.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                }


                if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
                {
                    EstalishmentdetailACL = result3.Select(x => new TFormAppealApplicationModel
                    {
                        ApplicationID = (int)x.ApplicationID,
                        EstablisDetailID = (int)x.EstablisDetailID,
                        EstablishmentName = (string)x.EstablishmentName,
                        DistrictID = (int)x.DistrictID,
                        TalukaID = (int)x.TalukaID,
                        ZoneID = (int)x.ZoneID,
                        AreaID = (int)x.AreaID,
                        Pincode = (int)x.Pincode,
                        EstablishEmailId = (string)x.Email,
                        EstablisMobileNo = (string)x.mobile,
                    }).ToList();
                }

                if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
                {
                    HearingdetailACL = result4.Select(x => new TFormAppealApplicationModel
                    {
                        ApplicationID = (int)x.ApplicationID,
                        HearingID = (int)x.HearingID,
                        HearingDate = Convert.ToDateTime(x.HearingDate),
                        HearingNote = (string)x.HearingNote,
                        HearingReasonID = (int)x.HearingReasonID,
                        IsActive = Convert.ToBoolean(x.IsActive),
                        IsCancle = Convert.ToBoolean(x.IsCancle),
                    }).ToList();
                }


            };
            response.CaseFavourList = CaseFavourList;
            response.EstalishmentdetailACL = EstalishmentdetailACL;
            response.HearingdetailACL = HearingdetailACL;
            return response;
        }

    }
}
