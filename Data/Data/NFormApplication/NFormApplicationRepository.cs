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

namespace FTS.Data.NFormApplication
{
    public class NFormApplicationRepository : INFormApplicationRepository
    {
        #region Private Variables
        private readonly IRepository<NFormApplicationModel> _NFormRepo;
        #endregion

        #region Constructor
        public NFormApplicationRepository(IRepository<NFormApplicationModel> NFormRepo)
        {
            _NFormRepo = NFormRepo;

        }
        #endregion

        public List<NFormApplicationModel> INFormList(NFormApplicationModel model)
        {
            
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<NFormApplicationModel> lstNForm = new List<NFormApplicationModel>();
            List<NFormApplicationModel> ApealDayslist = new List<NFormApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_PositionID", model.PositionID);
            param.Add("@p_PositionDistrictID", model.PositionDistrictID);
            param.Add("@p_RoleID", model.RoleID);
            param.Add("@p_ZoneID", model.ZoneID);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.GetNFormApplicationList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstNForm = result1.Select(x => new NFormApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppealStatus = (string)x.AppealStatus,
                    AppDate = (string)x.CreatedOn,
                    Name = (string)x.Name,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    ActionCode = (int)x.ActionCode,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    AppealID = (int)x.AppealID,
                    IsRecovery = Convert.ToBoolean(x.IsRecovery),
                    Status = (string)x.status,
                    Comments = (string)x.Comments,
                    AppealDays = (int)x.AppealDays,
                    Reviewdays = (int)x.ReviewDays,
                    RecoveryDays = (int)x.RecoveryDays,
                    DiffDay = (int)x.DiffDay,
                    IsAppeal = (int)x.IsAppeal,

                }).ToList();
            };
            //if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            //{
            //    lstNForm = result2.Select(x => new NFormApplicationModel
            //    {
            //        IsAppeal = (int)x.AppealDays,
            //        Reviewdays = (int)x.ReviewDays,
            //        RecoveryDays = (int)x.RecoveryDays
            //    }).ToList();
            //};
            return lstNForm;

        }

        public NFormApplicationModel NFormAppliactionRecord(int AppliactionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_AppliactionID", AppliactionID);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.GetRecordNFormApplication, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    Email = (string)x.Email,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Pincode = (int)x.Pincode,
                    Gender = (int)x.Gender,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }

        public NFormApplicationModel SaveNFormAppliactionRecord(NFormApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_ApplicantName", Obj.ApplicantName);
            param.Add("@p_PAddress", Obj.PAddress);
            param.Add("@p_SAddress", Obj.SAddress);
            param.Add("@p_DistrictID", Obj.DistrictID);
            param.Add("@p_TalukaID", Obj.TalukaID);
            param.Add("@p_Pincode", Obj.Pincode);
            param.Add("@p_Gender", Obj.Gender);
            param.Add("@p_IsActive", Obj.IsActive);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_ZoneID", Obj.ZoneID);
            param.Add("@p_AreaID", Obj.AreaID);
            param.Add("@p_UserMode", Obj.UserMode);
            param.Add("@p_Email", Obj.Email);
            param.Add("@p_PositionID", Obj.PositionID);
            param.Add("@p_PositionDistrictID", Obj.PositionDistrictID);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateNFormApplication, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
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

        public List<NFormApplicationModel> NFormEstablishmentRecord(int AppliactionID, int ISNew)
        {
            List<NFormApplicationModel> lstNFormEstablishment = new List<NFormApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", AppliactionID);
            param.Add("@p_ISNew", ISNew);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.GetRecordNFormEstablishmentDetail, param);
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstNFormEstablishment = result1.Select(x => new NFormApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    EstablisDetailID = (int)x.EstablisDetailID,
                    EstablishmentCode = (string)x.EstablishmentCode,
                    EstablishmentName = (string)x.EstablishmentName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    mobile = (string)x.mobile,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Pincode = (int)x.Pincode,
                    Email = (string)x.Email,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstNFormEstablishment;
        }

        public NFormApplicationModel SaveNFormEstablishmentRecord(NFormApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_XMLString", Obj.XMLString);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateNFormEstablishment, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,

                }).FirstOrDefault();
            };
            return response;
        }

        public NFormApplicationModel NFormAppliactionDocumentURLRecord(int AppliactionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", AppliactionID);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.GetRecordNFormAppliactionDocumentURLRecord, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    DOC1 = (string)x.DOC1,
                    DOC2 = (string)x.DOC2,
                    DOC3 = (string)x.DOC3,
                    DOC4 = (string)x.DOC4,
                    DOC5 = (string)x.DOC5,
                    ActionCode = (int)x.ActionCode,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).FirstOrDefault();
            };
            return response;
        }

        public NFormApplicationModel SaveDocumnetandapplication(NFormApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_fileName", Obj.fileName);
            param.Add("@p_DocumentID", Obj.DocumentID);
            param.Add("@P_isSubmit", Obj.IsSubmit);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_AppID", Obj.AppID);
            param.Add("@p_UserMode", Obj.UserMode);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateNFormAppliactionFinalsubmit, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //EMailIDList = (string)x.MailID,
                    //EmailSubject = (string)x.EmailSubject,
                    //EmailBody = (string)x.EmailBody,
                    CID = (int)x.CID,

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

        public NFormApplicationModel NFormAppliactionUpdateSubmit(NFormApplicationModel Obj)
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
            param.Add("@p_UserMode", Obj.UserMode);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateNFormAppliactionUpdateSubmit, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
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

        public List<NFormApplicationModel> NFormACLList(NFormApplicationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<NFormApplicationModel> lstReinstatement = new List<NFormApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.GetNFormACLList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstReinstatement = result1.Select(x => new NFormApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ReviewReason =(string)x.ReviewReason,
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    Name = (string)x.Name,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    ActionCode = (int)x.ActionCode,
                    Status = (string)x.Status,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    HearingDateString = (string)(x.HearingDate),
                    ResolutionStatus = (string)(x.ResolutionStatus),
                }).ToList();
            };
            return lstReinstatement;
        }


        public NFormApplicationModel NFormACLRecord(int AppliactionID, int ISNew)
        {
            List<MCommonList> CaseFavourList = new List<MCommonList>();
            List<NFormApplicationModel> EstalishmentdetailACL = new List<NFormApplicationModel>();
            List<NFormApplicationModel> HearingdetailACL = new List<NFormApplicationModel>();
            List<NFormApplicationModel> ReviewHearingdetailACL = new List<NFormApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_AppliactionID", AppliactionID);
            param.Add("@p_IsNew", ISNew);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.GetRecordNFormACL, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    HearingDate = Convert.ToDateTime(x.HearingDate),
                    HearingNote = (string)x.HearingNote,
                    HearingReasonID = (int)x.HearingReasonID,

                    SettlementDate = Convert.ToDateTime(x.SettlementDate),
                    SettlementNote = (string)x.SettlementNote,
                    SettlementDocURL = (string)x.SettlementDocURL,
                    CaseFavourIn = (int)x.CaseFavourIn,
                    OrderOutwardDate = Convert.ToDateTime(x.OrderOutwardDate),
                    ReviewOrderOutDate = Convert.ToDateTime(x.ReviewOrderOutDate),
                    OrderOutwardNo = (int)x.OrderOutwardNo,
                    ReviewOrderOutwardNo = (int)x.ReviewOrderOutwardNo,
                    ResolutionReasonID = (int)x.ResolutionReasonID,
                    ActionCode = (int)x.ActionCode,

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
                    EstalishmentdetailACL = result3.Select(x => new NFormApplicationModel
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
                    HearingdetailACL = result4.Select(x => new NFormApplicationModel
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
                if (keyValuePairs["result5"] is IEnumerable<dynamic> result5 && result5.Any())
                {
                    ReviewHearingdetailACL = result5.Select(x => new NFormApplicationModel
                    {
                        ApplicationID = (int)x.ApplicationID,
                        ReviewHearingID = (int)x.RHearingID,
                        ReviewHearingDate = Convert.ToDateTime(x.RHearingDate),
                        ReviewHearingNote = (string)x.RHearingNote,
                        HearingReasonID = (int)x.HearingReasonID,
                        IsActive = Convert.ToBoolean(x.IsActive),
                        IsCancle = Convert.ToBoolean(x.IsCancle),
                    }).ToList();
                }
            };
            response.CaseFavourList = CaseFavourList;
            response.EstalishmentdetailACL = EstalishmentdetailACL;
            response.HearingdetailACL = HearingdetailACL;
            response.ReviewHearingdetailACL = ReviewHearingdetailACL;
            return response;
        }

        //public NFormApplicationModel SaveNFormHearingACLRecord(NFormApplicationModel Obj)
        //{
        //    DynamicParameters param = new DynamicParameters();
        //    param.Add("@p_UserID", Obj.UserID);
        //    param.Add("@p_ApplicationID", Obj.ApplicationID);
        //    param.Add("@P_HearingDate", Obj.HearingDate);
        //    param.Add("@p_HearingNote", Obj.HearingNote);
        //    param.Add("@p_HearingReasonID", Obj.HearingReasonID);
        //    param.Add("@p_URL", Obj.URL);
        //    param.Add("@p_IP_Address", Obj.IP_Address);
        //    var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateNFormHearingACOL, param);
        //    var response = new NFormApplicationModel();
        //    if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
        //    {
        //        response = result1.Select(x => new NFormApplicationModel
        //        {
        //            ErrorCode = (int)x.ErrorCode,
        //            ErrorMassage = (string)x.ErrorMassage,
        //            ApplicationID = (int)x.ApplicationID,
        //            //ISNew = (int)x.ISNew,
        //            // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

        //        }).FirstOrDefault();
        //    };
        //    return response;
        //}

        public NFormApplicationModel SaveNFormHearingACLRecord(NFormApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            List<EmailReportModel> EmailReportDetail = new List<EmailReportModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_XMLString", Obj.XMLString);
            param.Add("@p_IsNotice", Obj.IsNotice);

            //param.Add("@p_IsReviewHearing", 0);

            param.Add("@p_IsReviewHearing", Obj.IsReviewHearing);

            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateNFormHearingACL, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    IsNotice = (int)x.IsNotice,
                    IsReviewHearing = (int)x.IsReviewHearing,
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
                    AppID = (string)x.AppID,
                    ApplicantDetail = (string)x.ApplicantDetail,
                    Establishmentdetail = (string)x.Establishmentdetail,
                    Date = (string)x.Date,
                    HearingDate = (string)x.HearingDate,
                    HearingTime = (string)x.HearingTime,

                }).ToList();
            };

            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            response.EmailReportDetail = EmailReportDetail;

            return response;
        }


        public NFormApplicationModel SaveNFormSettlementrecord(NFormApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@P_SettlementDate", Obj.upSettlementDate);
            param.Add("@p_SettlementNote", Obj.SettlementNote);
            param.Add("@p_SettlementDoc", Obj.fileName); 
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_ReasonId", Obj.ResolutionReasonID);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_CaseFavourIn", Obj.CaseFavourIn);
            param.Add("@p_IsReviewReolution", Obj.IsReviewResolution);
            param.Add("@p_OrderOutwardDate", Obj.OrderOutwardDatee);
            param.Add("@p_OrderOutwardNo", Obj.OrderOutwardNo);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateNFormSettlementACL, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
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

        public NFormApplicationModel NFormHistory(NFormApplicationModel Obj)
        {
            List<NFormApplicationModel> lstNFormApplicationdetail = new List<NFormApplicationModel>();
            List<NFormApplicationModel> lstNFormEshtablishDetail = new List<NFormApplicationModel>();
            List<NFormApplicationModel> lstNFormhearingDetail = new List<NFormApplicationModel>();
            List<NFormApplicationModel> lstNFormSettlementDetail = new List<NFormApplicationModel>();
            List<NFormApplicationModel> lstNFormReviewSettlementDetail = new List<NFormApplicationModel>();
            List<NFormApplicationModel> lstNFormAppealDetail = new List<NFormApplicationModel>();

            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.GetRecordNFormApplicationHistory, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,

                }).FirstOrDefault();
            };

            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                lstNFormApplicationdetail = result2.Select(x => new NFormApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictName = (string)x.DistrictName,
                    TalukaName = (string)x.TalukaName,
                    GanderName = (string)x.GanderName,
                    ZoneName = (string)x.ZoneName,
                    AreaName = (string)x.AreaName,
                    Pincode = (int)x.Pincode,
                    DOC1 = (string)x.DOC1,
                    DOC2 = (string)x.DOC2,
                    DOC3 = (string)x.DOC3,
                    DOC4 = (string)x.DOC4,
                    DOC5 = (string)x.DOC5,
                    Email = (string)x.Email,
                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                lstNFormEshtablishDetail = result3.Select(x => new NFormApplicationModel
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
         
            //if (keyValuePairs["result5"] is IEnumerable<dynamic> result5 && result5.Any())
            //{
            //    lstNFormhearingDetail = result5.Select(x => new NFormApplicationModel
            //    {
            //        HearingDatee = x.HearingDate,
            //        HearingNote = (string)x.HearingNote,
            //        HearingReasonName = (string)x.ReasonName,


            //    }).ToList();
            //};

            if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
            {
                lstNFormSettlementDetail = result4.Select(x => new NFormApplicationModel
                {
                    SettlementDatee = x.SettlementDate,
                    SettlementNote = (string)x.SettlementNote,
                    SettlementDocURL = (string)x.SettlementDocURL,


                }).ToList();
            };
            if (keyValuePairs["result5"] is IEnumerable<dynamic> result5 && result5.Any())
            {
                lstNFormReviewSettlementDetail = result5.Select(x => new NFormApplicationModel
                {
                    SettlementReviewDatee = x.ReviewSettlementDate,
                    SettlementReviewNote = (string)x.ReviewSettlementNote,
                    SettlementReviewDocURL = (string)x.ReviewResolutionDoc,


                }).ToList();
            };
            if (keyValuePairs["result6"] is IEnumerable<dynamic> result6 && result6.Any())
            {
                lstNFormAppealDetail = result6.Select(x => new NFormApplicationModel
                {
                    AppealID = (int)x.AppealID,
                    ApplicantName = (string)x.ApplicantName,
                    CaseFavour = (string)x.CaseFavour,
                    ChallanNo = (string)x.ChallanNo,
                    AppealReason = (string)x.AppealReason,
                    ChallanAmount = (decimal)x.ChallanAmount,
                    DateOfChallan = Convert.ToDateTime(x.DateOfChallan),
                    ChallanFile = (string)x.ChallanFile,


                }).ToList();
            };

            response.basicdetailtlst = lstNFormApplicationdetail;
            response.establishdetaillst = lstNFormEshtablishDetail;
            response.hearingdetaillst = lstNFormhearingDetail;
            response.Settlementdetaillst = lstNFormSettlementDetail; 
            response.SettlementReviewdetaillst = lstNFormReviewSettlementDetail;
            response.Appealdetaillst = lstNFormAppealDetail;

            return response;
        }

        public NFormApplicationModel SendtoACLFromClerk(NFormApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateNFormSendtoACLFromClerk, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
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
        public NFormApplicationModel SendReviewtoACL(NFormApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address); 
            param.Add("@p_ReviewReason", Obj.ReviewReason);
            param.Add("@p_IP_ReviewDoc", Obj.ReviewDoc);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateNFormReviewSendtoACL, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
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

        public NFormApplicationModel SendtoCommentFromClerk(NFormApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_Comments", Obj.Comments);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateNFormSendtoCommentFromClerk, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
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

        public NFormApplicationModel UpdateEshtablishmentAddressdetailFromACL(NFormApplicationModel Obj)
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
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateEshtablishmentAddressdetailFromACL, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
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

        public NFormApplicationModel ApproveOrRejectReviewFromACL(NFormApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            List<EmailReportModel> EmailReportDetail = new List<EmailReportModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_ActionCode", Obj.ActionCode);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.UpdateApproveOrRejectReviewFromACL, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    EMailIDList = (string)x.MailID,
                    EmailSubject = (string)x.EmailSubject,
                    EmailBody = (string)x.EmailBody,
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

                    Date = (string)x.Date,
                    AppID = (string)x.AppID,
                    ApplicantDetail = (string)x.ApplicantDetail,
                    Establishmentdetail = (string)x.Establishmentdetail,
                    DictrictACLOffice = (string)x.DistrictACLOffice,
                    ACLDistrict = (string)x.ACLDistrict

                }).ToList();
            };
            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            response.EmailReportDetail = EmailReportDetail;
            return response;
        }

        public NFormApplicationModel TFormRecoveryCerti(NFormApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            List<EmailReportModel> EmailReportDetail = new List<EmailReportModel>();
            //param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_AppealID", Obj.AppealID);
            //param.Add("@p_IP_Address", Obj.IP_Address);
            //param.Add("@p_ReviewReason", Obj.ReviewReason);
            //param.Add("@p_IP_ReviewDoc", Obj.ReviewDoc);
            var keyValuePairs = _NFormRepo.QueryMultipleByProcedure(SPConstants.SendTRecoveryForm, param);
            var response = new NFormApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new NFormApplicationModel
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
                  
                    Date = (string)x.Date,
                    AppID = (string)x.AppID,
                    ApplicantDetail = (string)x.ApplicantDetail,
                    Establishmentdetail = (string)x.Establishmentdetail,
                    ChallanAmount = (string)x.ChallanAmount

                }).ToList();
            };
            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            response.EmailReportDetail = EmailReportDetail;
            return response;
        }
    }
}
