using FTS.Data.Common;
using FTS.Model.Common;
using FTS.Data.LicenceApplication;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.LicenceApplication
{
    public class LicenceApplicationRepository : ILicenceApplicationRepository
    {
        #region Private Variables
        private readonly IRepository<LicenceApplicationModel> _LicenceIMWRepository;
        #endregion

        #region Constructor
        public LicenceApplicationRepository(IRepository<LicenceApplicationModel> LicenceIMWRepository)
        {
            _LicenceIMWRepository = LicenceIMWRepository;

        }
        #endregion

        public List<LicenceApplicationModel> LicenceAppList(LicenceApplicationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<LicenceApplicationModel> lstLincenceIMW = new List<LicenceApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);

            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.GetLicenceApplicationList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstLincenceIMW = result1.Select(x => new LicenceApplicationModel
                {
                    ID = (int)x.ID,
                    AppID = (string)x.AppID,
                    ApplicationID = (int)x.ApplicationID,
                    ContractorName = (string)x.ContractorName,
                    ApplicationMode = (string)x.ApplicationMode,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ISAmendmentType = (string)x.ISAmendmentType,
                    StatusName = (string)x.StatusName,
                    AppDate = (string)x.AppDate,
                    UserMode = (int)x.UserMode,
                    Applicationtype = x.Applicationtype,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    ActionCode = (int)x.ActionCode,
                    RejectRemarks = (string)x.QueryByACL,
                    IsCLRA = Convert.ToBoolean(x.CLR),
                    IsIMW = Convert.ToBoolean(x.IMW),
                    ISMTW = Convert.ToBoolean(x.MTW),
                    CLRANote = (string)x.CLRANote,
                    IMWNote = (string)x.IMWNote,
                    MTWNote = (string)x.MTWNote,

                }).ToList();
            };
            return lstLincenceIMW;
        }
       
        public LicenceApplicationModel AppliactionRecord(LicenceApplicationModel obj)
        {
            List<LicenceApplicationModel> lstEstablishmentregistrationdetail = new List<LicenceApplicationModel>();
            List<LicenceApplicationModel> lstPrincipalEmployerInformationdetail = new List<LicenceApplicationModel>();
            List<MCommonList> lstCommonList = new List<MCommonList>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", obj.UserID);
            param.Add("@p_UserMode", obj.UserMode);
            param.Add("@p_ApplicationID", obj.ApplicationID);
            param.Add("@p_ISAmendment", obj.ISAmendment);
            string SP = "";
            if (obj.ISAmendment == 1 && obj.IsEdit == 0)
            {
                SP = SPConstants.GetRecordLicenceAmendment;
            }
            else
            {
                SP = SPConstants.GetRecordLicenceApplication;
            }
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SP, param);

            //var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.GetRecordLicenceApplication, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,


                }).FirstOrDefault();
            };
            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                response = result2.Select(x => new LicenceApplicationModel
                {

                    ApplicationID = (int)x.ApplicationID,
                    ContractorName = (string)x.ContractorName,
                    ContractorPAddress = (string)x.ContractorPAddress,
                    ContractorSAddress = (string)x.ContractorSAddress,
                    ContractorDOB = (DateTime)x.ContractorDOB,
                    ContractorAge = (string)x.ContractorAge,
                    ContractorEmailId = (string)x.ContractorEmailId,
                    ContractorMobileNo = (string)x.ContractorMobile,  
                    //EstablismentCode = (string)x.EstablismentCode,
                    //EstablihmentName = (string)x.EstablihmentName,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    Pincode = (int)x.PinCode,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    EstablismentCode = (string)x.EstablismentCode,
                    EstablihmentName = (string)x.EstablihmentName,
                    EstablishMobile =(string)x.EMobile,
                    EmailID =(string)x.EmailID,
                    EstablishmentPAddress = (string)x.EstablishmentPAddress,
                    EstablishmentSAddress = (string)x.EstablishmentSAddress,
                    EDistrictID = (int)x.EDistrictID,
                    ETalukaID = (int)x.ETalukaID,
                    EZoneID = (int)x.EZoneID,
                    EAreaID = (int)x.EAreaID,
                    EPincode = (int)x.EPincode,
                    LicenceFee = (decimal)x.LicenceFee, 
                    TreasuryName = (string)x.TreasuryName,
                    TreasurName = (string)x.TreasurName,
                    ChallanNo = (int)x.ChallanNo,
                    ChallanDate = (DateTime)x.ChallanDate,
                    SecurityDeposit = (decimal)x.SecurityDeposit,
                    SecurityChallanNo = (int)x.SecurityChallanNo,
                    SecurityChallanDate = (DateTime)x.SecurityChallanDate,
                    LicecneDoc = (string)x.LicecneDoc, 
                    SecurityDoc =(string)x.SecurityDoc,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    IsCLRA = Convert.ToBoolean(x.CLR),
                    ISMTW = Convert.ToBoolean(x.MTW),
                    IsIMW = Convert.ToBoolean(x.IMW),
                    IsMultipul = (int)x.IsMultiple,
                    IsCLRA_verified = Convert.ToBoolean(x.CLRApprove),
                    IsIMW_verified = Convert.ToBoolean(x.IMWApprove),
                    ISMTW_verified = Convert.ToBoolean(x.MTWApprove),
                    CLRANote = (string)x.CLRANote,
                    IMWNote = (string)x.IMWNote,
                    MTWNote = (string)x.MTWNote,
                    TypeOfBusiness = (int)x.TypeOfBusiness,
                    Doc1 = (string)x.Doc1,
                    Doc2 = (string)x.Doc2,
                    Doc3 = (string)x.Doc3,
                    Doc4 = (string)x.Doc4,
                    Doc5 = (string)x.Doc5,
                    Doc6 = (string)x.Doc6,  
                    ActionCode = (int)x.ActionCode,
                    ISAmendment = (int)x.ISAmendment,
                    RefRegistrationID = (int)x.RefRegistrationID,
                    ACL_Review_Comments = (string)x.ACL_Review_Comments,
                    DCL_Review_Comments = (string)x.DCL_Review_Comments,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    Approve_DOC = (string)x.Approve_DOC,

                }).FirstOrDefault();
            };

            if (keyValuePairs["result3"] is IEnumerable<dynamic> result3 && result3.Any())
            {
                lstPrincipalEmployerInformationdetail = result3.Select(x => new LicenceApplicationModel
                {

                    ApplicationID = (int)x.ApplicationID,
                    PrincipalID = (int)x.PrincipalID,
                    PrincipalName = (string)x.EmployerName,
                    PrincipalFatherName = (string)x.FatherName,
                    EmailID = (string)x.EmailID,
                    PAddress = (string)x.PAddress,
                    SAddress = (string)x.SAddress,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    ZoneID = (int)x.ZoneID,
                    AreaID = (int)x.AreaID,
                    Pincode = (int)x.Pincode,
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
                lstCommonList = result4.Select(x => new MCommonList
                {
                    DataValue = (int)x.DataValue,
                    DisplayValue = (string)x.DisplayValue,
                }).ToList();
            };
            //response.Establishmentregistrationdetaillst = lstEstablishmentregistrationdetail;
            response.PrincipalEmployerInformationdetaillst = lstPrincipalEmployerInformationdetail;
            response.CommonList = lstCommonList;
            return response;
        }
        public LicenceApplicationModel SaveContractorAppliactionRecord(LicenceApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_ContractorName", Obj.ContractorName);
            param.Add("@p_ContractorPAddress", Obj.ContractorPAddress);
            param.Add("@p_ContractorSAddress", Obj.ContractorSAddress);
            param.Add("@p_ContractorDOB", Obj.ContractorDOB);
            param.Add("@p_ContractorAge", Obj.ContractorAge);
            param.Add("@p_ContractorEmailId", Obj.ContractorEmailId);
            param.Add("@p_ContractorMobile", Obj.ContractorMobileNo);
            param.Add("@p_DistrictId", Obj.DistrictID);
            param.Add("@p_TalukaID", Obj.TalukaID);
            param.Add("@p_ZoneID", Obj.ZoneID);
            param.Add("@p_AreaID", Obj.AreaID);
            param.Add("@p_Pincode", Obj.Pincode);
            param.Add("@p_IPAddress", Obj.IPAddress);
            param.Add("@p_Url", Obj.URL);
            param.Add("@p_UserMode", Obj.UserMode);
            param.Add("@p_IsMultiple", Obj.IsMultipul);
            param.Add("@P_MTW", Obj.ISMTW);
            param.Add("@P_CLR", Obj.IsCLRA);
            param.Add("@P_IMW", Obj.IsIMW);
            param.Add("@p_ISAmendment", Obj.ISAmendment);
            param.Add("@p_RefRegistrationID", Obj.RefRegistrationID);

            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.LicenceContractDetailAddEdit, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,
                    ISNew = (int)x.ISNew,
                    //ISNew = (int)x.ISNew,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public LicenceApplicationModel SaveEstablishmentLicenceRecord(LicenceApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_EstablismentCode", Obj.EstablismentCode);
            param.Add("@p_EstablismentName", Obj.EstablihmentName);
            param.Add("@P_EmailID", Obj.EmailID);
            param.Add("@p_EMobileNo", Obj.EstablishMobile);
            param.Add("@p_EstablismentPAddress", Obj.EstablishmentPAddress);
            param.Add("@p_EstablismentSAddress", Obj.EstablishmentSAddress);
            param.Add("@p_TypeOfBusiness", Obj.TypeOfBusiness);
            param.Add("@p_EDistrictID", Obj.EDistrictID);
            param.Add("@p_ETalukaID", Obj.ETalukaID);
            param.Add("@P_EZoneID", Obj.EZoneID);
            param.Add("@P_EAreaID", Obj.EAreaID);
            param.Add("@P_EPinCode", Obj.EPincode);
  
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.LicenceEstablishmentDetailUpdate, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
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
        public LicenceApplicationModel SavePrincipalEmployerLicenceRecord(LicenceApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_XMLString", Obj.XMLString);
            param.Add("@p_UserID", Obj.UserID);

            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.PrincipleEmployerLicenceUpdate, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
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
        public LicenceApplicationModel SaveLicenceAndSecurityRecord(LicenceApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_LicenceFee", Obj.LicenceFee);
            param.Add("@p_TreasuryName", Obj.TreasuryName);
            param.Add("@p_TreasureName", Obj.TreasurName);
            param.Add("@p_ChallanNo", Obj.ChallanNo);
            param.Add("@p_ChallanDate", Obj.DbInChallanDate);
            param.Add("@p_securityDeposit", Obj.SecurityDeposit);
            param.Add("@p_securityChallanNo", Obj.SecurityChallanNo);
            param.Add("@p_securityChallanDate", Obj.DbInSecurityChallanDate);
            param.Add("@p_LicenceDoc", Obj.LicecneDoc);
            param.Add("@p_SecurityDoc", Obj.SecurityDoc);

            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.LicnecnSecurityDetailUpdate, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
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
        public LicenceApplicationModel SaveDocumnetandapplication(LicenceApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_fileName", Obj.Filename);
            param.Add("@p_DocumentID", Obj.DocumentID);
            param.Add("@P_isSubmit", Obj.IsSubmit);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_AppID", Obj.AppID);
            param.Add("@p_UserMode", Obj.UserMode);
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.updateLicenceuploadFile, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,

                }).FirstOrDefault();
            };
            return response;
        }
        public LicenceApplicationModel LicenceApplicationUpdateSubmit(LicenceApplicationModel Obj)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@P_isSubmit", Obj.IsSubmit);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_AppID", Obj.AppID);
            param.Add("@p_UserMode", Obj.UserMode);
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.LicenceApplicationSubmitFinal, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    ApplicationID = (int)x.ApplicationID,

                }).FirstOrDefault();
            };
            return response;
        }
        public List<LicenceApplicationModel> LicenceACLList(LicenceApplicationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<LicenceApplicationModel> lstConciliaiton = new List<LicenceApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.GetLicenceApplicationACLList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstConciliaiton = result1.Select(x => new LicenceApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ApplicantName = (string)x.ApplicantName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    //ApplicationMode = (string)x.ApplicationMode,
                    //UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    StatusName = (string)x.Status,
                    ActionCode = (int)x.ActionCode,
                    QueryComments = (string)x.QueryComments,
                    Applicationtype = x.Applicationtype,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                }).ToList();
            };
            return lstConciliaiton;
        }

        public LicenceApplicationModel ApproveOrRejectLicenceByACL(LicenceApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_ActionCode", Obj.ActionCode);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.UpdateApproveOrRejectlicenceByACL, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
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
            response.ApplicantMailDetail = ApplicantMailDetail;
            response.EshtablishmentMailDetail = EshtablishmentMailDetail;
            return response;
        }

        public LicenceApplicationModel LicenceQueryByACL(LicenceApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_Comments", Obj.QueryComments);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.UpdateLicenceApplicationByACL, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
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

        public LicenceApplicationModel LicenceQueryByDCLHO(LicenceApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_Comments", Obj.QueryComments);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.QueryLicenceApplicationByDCL, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
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

        public List<LicenceApplicationModel> LicenceRegistrationDCLList(LicenceApplicationModel model)
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<LicenceApplicationModel> lstLicence = new List<LicenceApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.GetLicenceApplicationDCLList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstLicence = result1.Select(x => new LicenceApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ContractorName = (string)x.Name,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    ActionCode = (int)x.ActionCode,
                    Status = (string)x.Status,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    Applicationtype = x.Applicationtype,
                }).ToList();
            };
            return lstLicence;
        }

        public List<LicenceApplicationModel> LicenceApplicationHOList(LicenceApplicationModel model)  //HO ACL
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<LicenceApplicationModel> lstLicence = new List<LicenceApplicationModel>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", model.UserID);
            param.Add("@p_UserMode", model.UserMode);
            param.Add("@p_ZoneID", model.ZoneID);
            param.Add("@p_DistrictID", model.DistrictID);
            param.Add("@p_TalukaID", model.TalukaID);
            param.Add("@p_RoleID", model.RoleID);
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.GetLicenceApplicationHOACLList, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstLicence = result1.Select(x => new LicenceApplicationModel
                {
                    ApplicationID = (int)x.ApplicationID,
                    ContractorName = (string)x.Name,
                    IsActive = Convert.ToBoolean(x.IsActive),
                    QueryByDCL = (string)x.QueryByDCL,
                    QueryByACL = (string)x.QueryByACL,  
                    ApplicationMode = (string)x.ApplicationMode,
                    UserMode = (int)x.UserMode,
                    AppDate = (string)x.CreatedOn,
                    AppID = x.AppID,
                    ID = (int)x.ID,
                    ActionCode = (int)x.ActionCode,
                    Status = (string)x.Status,
                    IsSubmit = Convert.ToBoolean(x.IsSubmit),
                    Applicationtype = x.Applicationtype,
                }).ToList();
            };
            return lstLicence;
        }

        public LicenceApplicationModel LicenceApproveRejectByACL(LicenceApplicationModel Obj)
        {
            if (Obj.IMWNote == null)
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
            param.Add("@p_ApplicationID", Obj.ApplicationID);
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
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.GetLicenceApproveRejectByACL, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
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

        public LicenceApplicationModel LicenceAppForwardToDCLHO(LicenceApplicationModel Obj)
        {
            List<MailDetail> ApplicantMailDetail = new List<MailDetail>();
            List<MailDetail> EshtablishmentMailDetail = new List<MailDetail>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", Obj.UserID);
            param.Add("@p_ApplicationID", Obj.ApplicationID);
            param.Add("@p_URL", Obj.URL);
            param.Add("@p_IP_Address", Obj.IP_Address);
            param.Add("@p_ACL_Review_Comments", Obj.Comments);
            var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.UpdateLicenceAppSendtoHODCLFromACL, param);
            var response = new LicenceApplicationModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new LicenceApplicationModel
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
        public List<LicenceApplicationModel> LicenceAmendmentList(LicenceApplicationModel ClsAppliactionlst)
        {
            try
            {
                List<LicenceApplicationModel> lstLicence = new List<LicenceApplicationModel>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", ClsAppliactionlst.UserID);
                param.Add("@p_UserMode", ClsAppliactionlst.UserMode);
                param.Add("@p_Mobile", ClsAppliactionlst.mobile);
                var keyValuePairs = _LicenceIMWRepository.QueryMultipleByProcedure(SPConstants.GetAmendmentList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstLicence = result1.Select(x => new LicenceApplicationModel
                    {
                        ApplicationID = (int)x.ApplicationID,
                        ID = (int)x.ID,
                        ApplicantName = (string)x.Name,
                        IsActive = Convert.ToBoolean(x.IsActive),
                        Applicationtype = x.Applicationtype,
                        AppID = (string)x.AppID,

                    }).ToList();
                };
                return lstLicence;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
