using FTS.Data.ApplicationRegistration;
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
using System.Web;

namespace FTS.Data.Registration
{
    public class ApplicationRegistrationRepository : IApplicationRegistrationRepository
    {
        #region Private Variables
        private readonly IRepository<ApplicationRegistrationModel> _ApplicationRegistrationRepository;
        #endregion

        #region Constructor
        public ApplicationRegistrationRepository(IRepository<ApplicationRegistrationModel> ApplicationRegistrationRepository)
        {
            _ApplicationRegistrationRepository = ApplicationRegistrationRepository;

        }
        #endregion


        public List<ApplicationRegistrationModel> RegistrationList(ApplicationRegistrationModel ClsAppliactionlst)
        {
            try
            {
                List<ApplicationRegistrationModel> lstRegistration = new List<ApplicationRegistrationModel>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", ClsAppliactionlst.UserID);
                param.Add("@p_UserMode", ClsAppliactionlst.UserMode);
                param.Add("@p_PositionID", ClsAppliactionlst.PositionID);
                param.Add("@p_PositionDistrictID", ClsAppliactionlst.PositionDistrictID);
                var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.GetEstablishmentregistrationList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstRegistration = result1.Select(x => new ApplicationRegistrationModel
                    {
                        RegistrationID = (int)x.RegistrationID,
                        ID = (int)x.ID,
                        EstablishmentName = (string)x.Name,
                        UserMode = (int)x.UserMode,
                        IsActive = Convert.ToBoolean(x.IsActive),
                        ApplicationMode = (string)x.ApplicationMode,
                        ActionCode = (int)x.ActionCode,
                        ISAmendment = (int)x.ISAmendment,
                        ISAmendmentType = (string)x.ISAmendmentType,
                        Status = (string)x.status,
                        AppDate = (string)x.CreatedOn,
                        Comments = (string)x.Comments,
                        AppID = x.AppID,
                        Applicationtype = x.Applicationtype,
                        IsCLRA = Convert.ToBoolean(x.AppliedIsCLRA),
                        IsIMW = Convert.ToBoolean(x.AppliedIsIMW),
                        ISMTW = Convert.ToBoolean(x.AppliedISMTW),
                        IsSubmit = Convert.ToBoolean(x.IsSubmit),

                    }).ToList();
                };
                return lstRegistration;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ApplicationRegistrationModel ApplicationRegistrationRecord(ApplicationRegistrationModel obj)
        {
            List<ApplicationRegistrationModel> lstEstablishmentregistrationdetail = new List<ApplicationRegistrationModel>();
            List<ApplicationRegistrationModel> lstPrincipalEmployerInformationdetail = new List<ApplicationRegistrationModel>();
            List<ApplicationRegistrationModel> lstContractordetail = new List<ApplicationRegistrationModel>();
            List<MCommonList> lstCommonList = new List<MCommonList>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", obj.UserID);
            param.Add("@p_UserMode", obj.UserMode);
            param.Add("@p_RegistrationID", obj.RegistrationID);
            param.Add("@p_ISAmendment", obj.ISAmendment);
            string SP = "";
            if (obj.ISAmendment == 1 && obj.IsEdit == 0)
            {
                SP = SPConstants.GetRecordAppegistrationAmendment;
            }
            else
            {
                SP = SPConstants.GetRecordApplicationRegistration;
            }
                var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SP, param);
           
            var response = new ApplicationRegistrationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    RegistrationID = (int)x.RegistrationID,
                    //RefRegistrationID = (int)x.RefRegistrationID,

                }).FirstOrDefault();
            };

            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                lstEstablishmentregistrationdetail = result2.Select(x => new ApplicationRegistrationModel
                {

                    RegistrationID = (int)x.RegistrationID,
                    EstablishmentName = (string)x.Name,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Pincode = (int)x.Pincode,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    IsActive = Convert.ToBoolean(x.IsActive),
                    DOC1 = (string)x.DOC1,
                    DOC2 = (string)x.DOC2,
                    DOC3 = (string)x.DOC3,
                    DOC4 = (string)x.DOC4,
                    DOC5 = (string)x.DOC5,
                    DOC6 = (string)x.DOC6,
                    EstablishmentNote = (string)x.Note,
                    TypeOfBusinessTrade = (int)x.TypeOfBusinessTrade,
                    Registrationfees = (int)x.Registrationfees,
                    Isverified = Convert.ToBoolean(x.Isverified),
                    //Isverified = (int)x.Isverified,
                    Treasury = (string)x.Treasury,
                    ChallanNo = (string)x.ChallanNo,
                    ChallanDate = (DateTime)x.ChallanDate,
                    IsCLRA = Convert.ToBoolean(x.IsCLRA),
                    IsIMW = Convert.ToBoolean(x.IsIMW),
                    ISMTW = Convert.ToBoolean(x.ISMTW),
                    IsCLRA_verified = Convert.ToBoolean(x.IsCLRA_verified),
                    IsIMW_verified = Convert.ToBoolean(x.IsIMW_verified),
                    ISMTW_verified = Convert.ToBoolean(x.ISMTW_verified),
                    CLRANote = (string)x.CLRANote,
                    IMWNote = (string)x.IMWNote,
                    MTWNote = (string)x.MTWNote,
                    Email = (string)x.email,
                    IsMultipul =(int)x.IsMultipul,
                    ActionCode = (int)x.ActionCode,
                    ISAmendment = (int)x.ISAmendment,
                    RefRegistrationID = (int)x.RefRegistrationID,
                    ACL_Review_Comments = (string)x.ACL_Review_Comments,
                    DCL_Review_Comments = (string)x.DCL_Review_Comments,
                    mobile =(string)x.mobile,
                    mobile2 =(string)x.mobile2,
                    Approve_DOC = (string)x.Approve_DOC,

                }).ToList();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                lstPrincipalEmployerInformationdetail = result3.Select(x => new ApplicationRegistrationModel
                {

                    RegistrationID = (int)x.RegistrationID,
                    PrincipalID = (int)x.PrincipalID,
                    PrincipalName = (string)x.Name,
                    PrincipalFatherName = (string)x.FatherName,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Pincode = (int)x.Pincode,
                    Email = (string)x.email,
                    mobile = (string)x.mobile,
                    MaleWorker = (int)x.MaleWorker,
                    FemaleWorker = (int)x.FemaleWorker,
                    TotalWorker = (int)x.TotalWorker,
                    ContractMaleWorker = (int)x.ContractMaleWorker,
                    ContractFemaleWorker = (int)x.ContractFemaleWorker,
                    ContractTotalWorker = (int)x.ContractTotalWorker,
                    FixTermMaleWorker = (int)x.FixTermMaleWorker,
                    FixTermFemaleWorker = (int)x.FixTermFemaleWorker,
                    FixTermTotalWorker = (int)x.FixTermTotalWorker,
                    OthersMaleWorker = (int)x.OthersMaleWorker,
                    OthersFemaleWorker = (int)x.OthersFemaleWorker,
                    OthersTotalWorker = (int)x.OthersTotalWorker,
                    IsActive = Convert.ToBoolean(x.IsActive),

                }).ToList();
            };

            if (keyValuePairs["result4"] is IEnumerable<dynamic> result4 && result4.Any())
            {
                lstContractordetail = result4.Select(x => new ApplicationRegistrationModel
                {   

                    RegistrationID = (int)x.RegistrationID,
                    ContractorID = (int)x.ContractorID,
                    ContractorName = (string)x.Name,
                    Natureofwork = (string)x.Natureofwork,
                    commencement = (DateTime)x.commencement,
                    completion = (DateTime)x.completion,
                    MaxnoContLab = (int)x.MaxnoContLab,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Pincode = (int)x.Pincode,
                    Email = (string)x.email,
                    mobile = (string)x.mobile,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };


            if (keyValuePairs["result5"] is IEnumerable<dynamic> result5 && result5.Any())
            {
                lstCommonList = result5.Select(x => new MCommonList
                {
                    DataValue = (int)x.DataValue,
                    DisplayValue = (string)x.DisplayValue,
                }).ToList();
            };

            response.Establishmentregistrationdetaillst = lstEstablishmentregistrationdetail;
            response.PrincipalEmployerInformationdetaillst = lstPrincipalEmployerInformationdetail;
            response.Contractordetaillst = lstContractordetail;
            response.CommonList = lstCommonList;
            return response;
        }


        public ApplicationRegistrationModel SaveEstablishmentRegistrationRecord(ApplicationRegistrationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_RegistrationID", Obj.RegistrationID);
            param.Add("@p_Name", Obj.EstablishmentName);
            param.Add("@p_PAddress", Obj.PAddress);
            param.Add("@p_SAddress", Obj.SAddress);
            param.Add("@p_DistrictID", Obj.DistrictID);
            param.Add("@p_TalukaID", Obj.TalukaID);
            param.Add("@p_Pincode", Obj.Pincode);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_ZoneID", Obj.ZoneID);
            param.Add("@p_AreaID", Obj.AreaID);
            param.Add("@p_UserMode", Obj.UserMode);
            param.Add("@p_TypeOfBusinessTrade", Obj.TypeOfBusinessTrade);
            param.Add("@p_email", Obj.Email);
            param.Add("@P_IsCLRA", Obj.IsCLRA);
            param.Add("@P_IsIMW", Obj.IsIMW);
            param.Add("@P_ISMTW", Obj.ISMTW);
            param.Add("@p_IsMultipul", Obj.IsMultipul);
            param.Add("@p_PositionID", Obj.PositionID);
            param.Add("@p_PositionDistrictID", Obj.PositionDistrictID);
            param.Add("@p_mobile", Obj.mobile);
            param.Add("@p_ISAmendment", Obj.ISAmendment);
            param.Add("@p_RefRegistrationID", Obj.RefRegistrationID);
            param.Add("@p_mobile2", Obj.mobile2);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.UpdateApplicationRegistration, param);
            var response = new ApplicationRegistrationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    RegistrationID = (int)x.RegistrationID,
                    IsMultipul = (int)x.IsMultipul,
                    ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public ApplicationRegistrationModel SavePrincipalEmployerRegistrationRecord(ApplicationRegistrationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_XMLString", Obj.XMLString);
            param.Add("@p_UserID", Obj.UserID);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.UpdatePrincipalEmployerRegistration, param);
            var response = new ApplicationRegistrationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    RegistrationID = (int)x.RegistrationID,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public ApplicationRegistrationModel SaveContractorRegistrationRecord(ApplicationRegistrationModel Obj) 
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_XMLString", Obj.XMLString);
            param.Add("@p_UserID", Obj.UserID);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.UpdateContractorRegistration, param);
            var response = new ApplicationRegistrationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    RegistrationID = (int)x.RegistrationID,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public ApplicationRegistrationModel SaveDocumnetandapplication(ApplicationRegistrationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_RegistrationID", Obj.RegistrationID);
            param.Add("@p_fileName", Obj.fileName);
            param.Add("@p_DocumentID", Obj.DocumentID);
            param.Add("@P_isSubmit", Obj.IsSubmit);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_AppID", Obj.AppID);
            param.Add("@p_UserMode", Obj.UserMode);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.UpdateApplicationRegistrationFinalsubmit, param);
            var response = new ApplicationRegistrationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    RegistrationID = (int)x.RegistrationID,

                }).FirstOrDefault();
            };
            return response;
        }


        public ApplicationRegistrationModel ApplicationRegistrationUpdateSubmit(ApplicationRegistrationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_RegistrationID", Obj.RegistrationID);
            param.Add("@P_isSubmit", Obj.IsSubmit);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_AppID", Obj.AppID);
            param.Add("@p_UserMode", Obj.UserMode);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.UpdateApplicationRegistrationUpdateSubmit, param);
            var response = new ApplicationRegistrationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    RegistrationID = (int)x.RegistrationID,

                }).FirstOrDefault();
            };
            return response;
        }

        public List<ApplicationRegistrationModel> ApplicationRegistrationAClList(ApplicationRegistrationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<ApplicationRegistrationModel> lstReinstatement = new List<ApplicationRegistrationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.GetApplicationRegistrationACLList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstReinstatement = result1.Select(x => new ApplicationRegistrationModel
                {
                    RegistrationID = (int)x.RegistrationID,
                    EstablishmentName = (string)x.Name,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    ActionCode = (int)x.ActionCode,
                    Status = (string)x.Status,
                    Comments = (string)x.Comments,
                    Applicationtype = x.Applicationtype,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstReinstatement;
        }

        public ApplicationRegistrationModel SendtoCommentFromACL(ApplicationRegistrationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_RegistrationID", Obj.RegistrationID);
            param.Add("@p_Comments", Obj.Comments);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_IsDCL", Obj.ISDL);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.GetAppRegistrationSendtoCommentFromACL, param);
            var response = new ApplicationRegistrationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    RegistrationID = (int)x.RegistrationID,
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

        public ApplicationRegistrationModel ApplicationApproveRejectFromACL(ApplicationRegistrationModel Obj)
        {
            if(Obj.IMWNote == null)
            {
                Obj.IMWNote = "";
            }
            if (Obj.MTWNote == null)
            {
                Obj.MTWNote = "";
            }
            if (Obj.CLRANote == null)
            {
                Obj.CLRANote = "";
            }
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_RegistrationID", Obj.RegistrationID);
            param.Add("@p_IsCLRA_verified", Obj.IsCLRA_verified);
            param.Add("@p_IsIMW_verified", Obj.IsIMW_verified);
            param.Add("@p_ISMTW_verified", Obj.ISMTW_verified);
            param.Add("@p_IMWNote", Obj.IMWNote);
            param.Add("@p_MTWNote", Obj.MTWNote);
            param.Add("@p_CLRANote", Obj.CLRANote);
            param.Add("@p_IsCLRA", Obj.IsCLRA);
            param.Add("@p_IsIMW", Obj.IsIMW);
            param.Add("@p_ISMTW", Obj.ISMTW);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_DCL_Review_Comments", Obj.DCL_Review_Comments);
            param.Add("@p_ACL_Review_Comments", Obj.ACL_Review_Comments);
            param.Add("@p_Approve_DOC", Obj.fileName);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.GetApplicationApproveRejectFromACL, param);
            var response = new ApplicationRegistrationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    RegistrationID = (int)x.RegistrationID,
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

        public List<ApplicationRegistrationModel> ApplicationRegistrationHOAClList(ApplicationRegistrationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<ApplicationRegistrationModel> lstReinstatement = new List<ApplicationRegistrationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.GetApplicationRegistrationHOACLList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstReinstatement = result1.Select(x => new ApplicationRegistrationModel
                {
                    RegistrationID = (int)x.RegistrationID,
                    EstablishmentName = (string)x.Name,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    ActionCode = (int)x.ActionCode,
                    Status = (string)x.Status,
                    Comments = (string)x.Comments,
                    DCLComments = (string)x.DCLComments,
                    Applicationtype = x.Applicationtype,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstReinstatement;
        }

        public ApplicationRegistrationModel AppRegistrationSendtoHODCLFromACL(ApplicationRegistrationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_RegistrationID", Obj.RegistrationID);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_ACL_Review_Comments", Obj.Comments);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.UpdateAppRegistrationSendtoHODCLFromACL, param);
            var response = new ApplicationRegistrationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new ApplicationRegistrationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    RegistrationID = (int)x.RegistrationID,
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

        public List<ApplicationRegistrationModel> ApplicationRegistrationHODCLList(ApplicationRegistrationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<ApplicationRegistrationModel> lstReinstatement = new List<ApplicationRegistrationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.GetApplicationRegistrationHODCLList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstReinstatement = result1.Select(x => new ApplicationRegistrationModel
                {
                    RegistrationID = (int)x.RegistrationID,
                    EstablishmentName = (string)x.Name,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    ActionCode = (int)x.ActionCode,
                    Status = (string)x.Status,
                    Comments = (string)x.Comments,
                    Applicationtype = x.Applicationtype,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstReinstatement;
        }

        public List<ApplicationRegistrationModel> ApplicationRegistrationAmendmentList(ApplicationRegistrationModel ClsAppliactionlst)
        {
            try
            {
                List<ApplicationRegistrationModel> lstRegistration = new List<ApplicationRegistrationModel>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", ClsAppliactionlst.UserID);
                param.Add("@p_UserMode", ClsAppliactionlst.UserMode);
                param.Add("@p_Mobile", ClsAppliactionlst.mobile);
                var keyValuePairs = _ApplicationRegistrationRepository.QueryMultipleByProcedure(SPConstants.GetApplicationRegistrationAmendmentList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstRegistration = result1.Select(x => new ApplicationRegistrationModel
                    {
                        RegistrationID = (int)x.RegistrationID,
                        ID = (int)x.ID,
                        EstablishmentName = (string)x.Name,
                        IsActive = Convert.ToBoolean(x.IsActive),
                        Applicationtype = x.Applicationtype,

                    }).ToList();
                };
                return lstRegistration;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}

