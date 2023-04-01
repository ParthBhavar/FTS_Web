using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ReinstatementAppliaction
{
    public class ReinstatementAppliactionRepository : IReinstatementAppliactionRepository
    {

        #region Private Variables
        private readonly IRepository<ReinstatementAppliactionModel> _ReinstatementRepo;
        #endregion

        #region Constructor
        public ReinstatementAppliactionRepository(IRepository<ReinstatementAppliactionModel> ReinstatementRepo)
        {
            _ReinstatementRepo = ReinstatementRepo;

        }
        #endregion
          
        public List<ReinstatementAppliactionModel> ReinstatementList(ReinstatementAppliactionModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<ReinstatementAppliactionModel> lstReinstatement = new List<ReinstatementAppliactionModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_PositionID", model.PositionID);
            param.Add("@p_PositionDistrictID", model.PositionDistrictID);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.GetReinstatementAppliactionList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstReinstatement = result1.Select(x => new ReinstatementAppliactionModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    Name = (string)x.Name,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstReinstatement;
        }

        public ReinstatementAppliactionModel AppliactionRecord(int AppliactionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_AppliactionID", AppliactionID);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.GetRecordReinstatementAppliaction, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
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
                    isReqiredTradDetail = Convert.ToBoolean(x.isReqiredTradDetail),
                }).FirstOrDefault();
            };
            return response;
        }

        public ReinstatementAppliactionModel SaveAppliactionRecord(ReinstatementAppliactionModel Obj)
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
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.UpdateReinstatementAppliaction, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
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

        public ReinstatementAppliactionModel ReinstatementTradunionRecord(int AppliactionID,int ISNew)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", AppliactionID);
            param.Add("@p_ISNew", ISNew);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.GetRecordTradunionregistrationDetail, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    TradunionID = (int)x.TradunionID,
                    RegistrationNo = (string)x.RegistrationNo,
                    RegistrationName = (string)x.RegistrationName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    AreaID = (int)x.AreaID,
                    ZoneID = (int)x.ZoneID,
                    Pincode = (int)x.Pincode,
                    TotalWorkers = (int)x.TotalWorkers,
                    WorkerAge = (int)x.WorkerAge,
                    Department = (int)x.Department,
                    WorkType = (int)x.WorkType,
                    LastSalaryDrawn = (int)x.LastSalaryDrawn,
                    JoinningDate = Convert.ToDateTime(x.JoinningDate),
                    TerminationDate = Convert.ToDateTime(x.TerminationDate),
                    TotalYr = (string)x.TotalYr,
                    Note = (string)x.Note,
                    mobile = (string)x.mobile,
                    Email = (string)x.email,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).FirstOrDefault();
            };
            return response;
        }
     
        public ReinstatementAppliactionModel SaveReinstatementTradunionRecord(ReinstatementAppliactionModel Obj)
        {
            if (Obj.SAddress == null)
            {
                Obj.SAddress = "";
            }
            if (Obj.Note == null)
            {
                Obj.Note = "";
            }
            if (Obj.PAddress == null)
            {
                Obj.PAddress = "";
            }
            if (Obj.RegistrationNo == null)
            {
                Obj.RegistrationNo = "";
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_TradunionID", Obj.TradunionID);
            param.Add("@p_RegistrationNo", Obj.RegistrationNo);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_RegistrationName", Obj.RegistrationName);
            param.Add("@p_PAddress", Obj.PAddress);
            param.Add("@p_SAddress", Obj.SAddress);
            param.Add("@p_TalukaID", Obj.TalukaID);
            param.Add("@p_DistrictID", Obj.DistrictID);
            param.Add("@p_Pincode", Obj.Pincode);
            param.Add("@p_TotalWorkers", Obj.TotalWorkers);
            param.Add("@p_WorkerAge", Obj.WorkerAge);
            param.Add("@p_Department", Obj.Department);
            param.Add("@p_WorkType", Obj.WorkType);
            param.Add("@p_LastSalaryDrawn", Obj.LastSalaryDrawn);
            param.Add("@p_JoinningDate", Obj.UPJoinningDate);
            param.Add("@p_TerminationDate", Obj.UPTerminationDate);
            param.Add("@p_TotalYr", Obj.TotalYr);
            param.Add("@p_Note", Obj.Note);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_ZoneID", Obj.ZoneID);
            param.Add("@p_AreaID", Obj.AreaID);
            param.Add("@p_mobile", Obj.mobile);
            param.Add("@p_email", Obj.Email);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.UpdateReiniTradunionregistrationDetail1, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
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

        public List<ReinstatementAppliactionModel> ReiniEstablishmentRecord(int AppliactionID, int ISNew)
        {
            List<ReinstatementAppliactionModel> lstReiniEstablishment = new List<ReinstatementAppliactionModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", AppliactionID);
            param.Add("@p_ISNew", ISNew);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.GetRecordReiniEstablishment, param);
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstReiniEstablishment = result1.Select(x => new ReinstatementAppliactionModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    EstablisDetailID = (int)x.EstablisDetailID,
                    EstablishmentCode = (string)x.EstablishmentCode,
                    EstablishmentName = (string)x.EstablishmentName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    Email = (string)x.Email,
                    mobile = (string)x.mobile,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Pincode = (int)x.Pincode,
                    isReqiredTradDetail = Convert.ToBoolean(x.isReqiredTradDetail),
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstReiniEstablishment;
        }

        public ReinstatementAppliactionModel SaveReiniEstablishmentRecord(ReinstatementAppliactionModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_XMLString", Obj.XMLString);
            param.Add("@p_isReqiredTradDetail", Obj.isReqiredTradDetail);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.UpdateReiniEstablishmentDetail, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
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

        public ReinstatementAppliactionModel SaveDocumnetandapplication(ReinstatementAppliactionModel Obj)
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
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.UpdateReiniAppliactionFinalsubmit, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
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


        public ReinstatementAppliactionModel ReinAppliactionDocumentURLRecord(int AppliactionID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", AppliactionID);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.GetRecordReinAppliactionDocumentURL, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    DOC1 = (string)x.DOC1,
                    DOC2 = (string)x.DOC2,
                    DOC3 = (string)x.DOC3,
                    DOC4 = (string)x.DOC4,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).FirstOrDefault();
            };
            return response;
        }

        public ReinstatementAppliactionModel ReinstatementAppliactionUpdateSubmit(ReinstatementAppliactionModel Obj)
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
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.UpdateReinstatementAppliactionUpdateSubmit, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
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

        public List<ReinstatementAppliactionModel> ReinstatementACOLList(ReinstatementAppliactionModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<ReinstatementAppliactionModel> lstReinstatement = new List<ReinstatementAppliactionModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.GetReinstatementACOLList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstReinstatement = result1.Select(x => new ReinstatementAppliactionModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    Name = (string)x.Name,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    ActionCode = (int)x.ActionCode,
                    Status = (string)x.Status,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    HearingDateString = (string)x.HearingDate,
                }).ToList();
            };
            return lstReinstatement;
        }

        public ReinstatementAppliactionModel ReinstatementACOLRecord(int AppliactionID, int ISNew)
        {
            DynamicParameters param = new DynamicParameters();
            List<ReinstatementAppliactionModel> HearingdetailACL = new List<ReinstatementAppliactionModel>();
            param.Add("@p_AppliactionID", AppliactionID);
            param.Add("@p_IsNew", ISNew);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.GetRecordReinstatementACOL, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    HearingDate = Convert.ToDateTime(x.HearingDate),
                    HearingNote = (string)x.HearingNote,
                    HearingReasonID = (int)x.HearingReasonID,
                    SettlementDate = Convert.ToDateTime(x.SettlementDate),
                    SettlementNote = (string)x.SettlementNote,
                    SettlementDocURL = (string)x.SettlementDocURL,
                    OrderOutwardDate = Convert.ToDateTime(x.OrderOutwardDate),
                    OrderOutwardNo = (int)x.OrderOutwardNo,
                    SendLCDate = Convert.ToDateTime(x.SendLCDate),
                    SendLCNote = (string)x.SendLCNote,
                    ActionCode = (int)x.ActionCode,
                    ISNew = (int)ISNew,
             
                }).FirstOrDefault();

                if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
                {
                    HearingdetailACL = result2.Select(x => new ReinstatementAppliactionModel
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
            response.HearingdetailACL = HearingdetailACL;
            return response;
        }


        //public ReinstatementAppliactionModel SaveReinstatementHearingACOLRecord(ReinstatementAppliactionModel Obj)
        //{
        //    DynamicParameters param = new DynamicParameters();
        //    param.Add("@p_UserID", Obj.UserID);
        //    param.Add("@p_ApplicationID", Obj.ApplicationID);
        //    param.Add("@P_HearingDate", Obj.HearingDate);
        //    param.Add("@p_HearingNote", Obj.HearingNote);
        //    param.Add("@p_HearingReasonID", Obj.HearingReasonID);
        //    param.Add("@p_URL", Obj.URL);
        //    param.Add("@p_IP_Address", Obj.IP_Address);
        //    var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.UpdateReinstatementHearingACOL, param);
        //    var response = new ReinstatementAppliactionModel();
        //    if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
        //    {
        //        response = result1.Select(x => new ReinstatementAppliactionModel
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

        public ReinstatementAppliactionModel SaveReinstatementHearingACOLRecord(ReinstatementAppliactionModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            List<EmailReportModel> EmailReportDetail = new List<EmailReportModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_XMLString", Obj.XMLString);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.UpdateReinstatementHearingACL, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
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
                    DictrictACL = (string)x.DictrictACL,
                    AppID = (string)x.AppID,
                    ACLName = (string)x.ACLName,
                    ApplicantDetail = (string)x.ApplicantDetail,
                    Establishmentdetail = (string)x.Establishmentdetail,
                    HearingDate = (string)x.HearingDate,
                    HearingTime = (string)x.HearingTime,
                    DictrictACLOffice = (string)x.DictrictACLOffice,
                    Date = (string)x.Date,

                }).ToList();
            };

            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            response.EmailReportDetail = EmailReportDetail;
            return response;
        }


        public ReinstatementAppliactionModel SaveReinstatementACOLSettlementrecord(ReinstatementAppliactionModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@P_SettlementDate", Obj.UPSettlementDate);
            param.Add("@p_SettlementNote", Obj.SettlementNote);
            param.Add("@p_SettlementDoc", Obj.fileName);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_ReasonId", Obj.ResolutionReasonID);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_OrderOutwardDate", Obj.OrderOutwardDatee);
            param.Add("@p_OrderOutwardNo", Obj.OrderOutwardNo);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.UpdateReinstatementSettlementACOL, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
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

        public ReinstatementAppliactionModel SaveReinstatementSendtolabourACOLRecord(ReinstatementAppliactionModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            List<EmailReportModel> EmailReportDetail = new List<EmailReportModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@P_SendLCDate", Obj.UPSendLCDate);
            param.Add("@p_SendLCNote", Obj.SendLCNote);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.UpdateReinstatementSendLCACOL, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    //EMailIDList = (string)x.MailID,
                    //EmailSubject = (string)x.EmailSubject,
                    //EmailBody = (string)x.EmailBody
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
                    ACLName = (string)x.ACLName,
                    LCAddress = (string)x.LCAddress,
                    Date = (string)x.Date,
                    SendLCDate = (string)x.SendLCDate

                }).ToList();
            };

            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            response.EmailReportDetail = EmailReportDetail;
            return response;
        }



        public ReinstatementAppliactionModel ReinstatementHistory(ReinstatementAppliactionModel Obj)
        {
            List<ReinstatementAppliactionModel> lstReinstatementApplicationdetail = new List<ReinstatementAppliactionModel>();
            List<ReinstatementAppliactionModel> lstReinstatementEshtablishDetail = new List<ReinstatementAppliactionModel>();
            List<ReinstatementAppliactionModel> lstReinstatementtreadUniDetail = new List<ReinstatementAppliactionModel>();
            List<ReinstatementAppliactionModel> lstReinstatementhearingDetail = new List<ReinstatementAppliactionModel>();
            List<ReinstatementAppliactionModel> lstReinstatementSettlementDetail = new List<ReinstatementAppliactionModel>();
            List<ReinstatementAppliactionModel> lstReinstatementSendLCDetail = new List<ReinstatementAppliactionModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            var keyValuePairs = _ReinstatementRepo.QueryMultipleByProcedure(SPConstants.GetRecordReinstatementApplicationHistory, param);
            var response = new ReinstatementAppliactionModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ReinstatementAppliactionModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,

                }).FirstOrDefault();
            };

            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                lstReinstatementApplicationdetail = result2.Select(x => new ReinstatementAppliactionModel
                {
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
                    Email = (string)x.Email

                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                lstReinstatementEshtablishDetail = result3.Select(x => new ReinstatementAppliactionModel
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

            if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
            {
                lstReinstatementtreadUniDetail = result4.Select(x => new ReinstatementAppliactionModel
                {
                    RegistrationNo = (string)x.RegistrationNo,
                    RegistrationName = (string)x.RegistrationName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictName = (string)x.DistrictName,
                    TalukaName = (string)x.TalukaName,
                    ZoneName = (string)x.ZoneName,
                    AreaName = (string)x.AreaName,
                    Pincode = (int)x.Pincode,
                    DepartmentName = (string)x.DepartmentName,
                    Note = (string)x.Note,
                    TotalYr = (string)x.TotalYr,
                    JoinningDatee = x.JoinningDate,
                    TerminationDatee = x.TerminationDate,
                    WorkerAge = (int)x.WorkerAge,
                    WorkName = (string)x.WorkName,
                    TotalWorkers = (int)x.TotalWorkers,
                    LastSalaryDrawn = (int)x.LastSalaryDrawn,

                }).ToList();
            };

            //if (keyValuePairs["result5"] is IEnumerable<dynamic> result5 && result5.Any())
            //{
            //    lstReinstatementhearingDetail = result5.Select(x => new ReinstatementAppliactionModel
            //    {
            //        HearingDatee = x.HearingDate,
            //        HearingNote = (string)x.HearingNote,
            //        HearingReasonName = (string)x.ReasonName,
                  

            //    }).ToList();
            //};

            if (keyValuePairs["result5"] is IEnumerable<dynamic> result5 && result5.Any())
            {
                lstReinstatementSettlementDetail = result5.Select(x => new ReinstatementAppliactionModel
                {
                    SettlementDatee = x.SettlementDate,
                    SettlementNote = (string)x.SettlementNote,
                    SettlementDocURL = (string)x.SettlementDocURL,


                }).ToList();
            };

            if (keyValuePairs["result6"] is IEnumerable<dynamic> result6 && result6.Any())
            {
                lstReinstatementSendLCDetail = result6.Select(x => new ReinstatementAppliactionModel
                {
                    SendLCDatee = x.SendLCDate,
                    SendLCNote = (string)x.SendLCNote,


                }).ToList();
            };

            response.basicdetailtlst = lstReinstatementApplicationdetail;
            response.establishdetaillst = lstReinstatementEshtablishDetail;
            response.tradedetaillst = lstReinstatementtreadUniDetail;
            response.hearingdetaillst = lstReinstatementhearingDetail;
            response.Settlementdetaillst = lstReinstatementSettlementDetail;
            response.SendLCdetaillst = lstReinstatementSendLCDetail;
            return response;
        }
    }
}
