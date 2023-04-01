using FastReport.Data;

namespace FTS.Data.Common
{
    public static class SPConstants
    {
        #region User
        public const string AuthenticateUser = "usp_authenticate_user";
        public const string SaveRefreshToken = "usp_save_refresh_token";
        public const string GetRefreshToken = "usp_check_refresh_token";
        #endregion

        #region RoleMaster
        public const string GetListRoleMaster = "SP_GetrolemasterList";
        public const string GetRecordRolemaster = "SP_GetRecordRolemaster";
        public const string UpdateRolemaster = "SP_RoleMasterAddOrEdit";
        public const string DeleteRoleMaster = "SP_deleteRoleMaster";
        #endregion

        #region BranchMaster
        public const string GetBranchMasterList = "SP_GetBranchMasterList";
        public const string GetRecordBranchMaster = "SP_GetRecordBranchMaster";
        public const string BranchMasterAddEdit = "SP_BranchMasterAddOrEdit";
        public const string DeleteBranchMaster = "SP_deletebranchmaster";
        #endregion

        #region AppealDayMaster
        public const string GetAppealDayMasterList = "SP_GetAppealDayMasterList";
        public const string GetRecordAppealDayMaster = "SP_GetRecordAppealDayMaster";
        public const string AppealDayMasterAddEdit = "SP_AppealDayMasterAddOrEdit";
        public const string DeleteAppealDayMaster = "SP_deleteappealdaymaster";
        #endregion

        #region RegionMaster
        public const string GetListRegionMaster = "SP_GetRegionMasterList";
        public const string GetRecordRegionmaster = "SP_GetRecordRegionMaster";
        public const string UpdateRegionmaster = "SP_RegionMasterAddOrEdit";
        public const string DeleteRegionMaster = "SP_deleteregionmaster";
        #endregion

        #region CessProjectDetails
        public const string GetListProjectDetails = "SP_GetProjectDetailsList";
        public const string GetRecordProjectDetails = "SP_GetRecordProjectDetails";
        public const string UpdateProjectDetails = "SP_ProjectDetailsAddOrEdit";
        public const string DeleteProjectDetails = "SP_deleteprojectdetails";

        public const string UpdateCessCollectionDetails = "SP_CessCollectionDetailsAddOrEdit";
        public const string GetRecordCessCollectionDetails = "SP_GetRecordCessCollectionDetails";
        public const string GetListCessCollectionDetails = "SP_GetCessCollectionDetailsList";
        #endregion

        #region RegistrationCLRA
        public const string GetListRegistrationCLRA = "SP_GetRegistrationCLRAList";
        public const string GetRecordRegistrationCLRA = "SP_GetRecordRegistrationCLRA";
        public const string UpdateRegistrationCLRA = "SP_RegistrationCLRAAddOrEdit";
        #endregion

        #region ZoneMaster
        public const string GetListZoneMaster = "SP_GetZoneMasterList";
        public const string GetRecordZonemaster = "SP_GetRecordZoneMaster";
        public const string UpdateZonemaster = "SP_ZoneMasterAddOrEdit";
        public const string DeleteZoneMaster = "SP_deletezonemaster";
        #endregion

        #region DistrictMaster
        public const string GetListDistrictMaster = "SP_GetDistrictMasterList";
        public const string GetRecordDistrictmaster = "SP_GetRecordDistrictMaster";
        public const string UpdateDistrictmaster = "SP_DistrictMasterAddOrEdit";
        public const string DeleteDistrictMaster = "SP_deletedistrictmaster";
        #endregion

        #region AreaMaster
        public const string GetListAreaMaster = "SP_GetAreaMasterList";
        public const string GetRecordAreaMaster = "SP_GetRecordAreaMaster";
        public const string UpdateAreaMaster = "SP_AreaMasterAddOrEdit";
        public const string DeleteAreaMaster = "SP_deleteareamaster";
        #endregion

        #region ReinstatementActionMaster
        public const string GetListReinstatementActionMaster = "SP_GetReinstatementActionMasterList";
        public const string GetRecordReinstatementActionMaster = "SP_GetRecordReinstatementActionMaster";
        public const string UpdateReinstatementActionMaster = "SP_ReinstatementActionMasterAddOrEdit";
        public const string DeleteReinstatementActionMaster = "SP_deletereinstatementactionmaster";

        #endregion

        #region ApplicationRegistratationActionMaster
        public const string GetListAppRegActionMaster = "SP_GetAppRegActionMasterList";
        public const string GetRecordAppRegActionMaster = "SP_GetRecordAppRegActionMaster";
        public const string UpdateAppRegActionMaster = "SP_AppRegActionMasterAddOrEdit";
        public const string DeleteAppRegActionMaster = "SP_deleteAppRegactionmaster";

        #endregion

        #region LicenceActionMaster
        public const string GetListLicenceActionMaster = "SP_GetLicenceActionMasterList";
        public const string GetRecordLicenceActionMaster = "SP_GetRecordLicenceActionMaster";
        public const string UpdateLicenceActionMaster = "SP_LicenceActionMasterAddOrEdit";
        public const string DeleteLicenceActionMaster = "SP_deleteLicenceactionmaster";

        #endregion

        #region NformActionMaster

        public const string GetListNFormActionMaster = "SP_GetNFormActionMasterList";
        public const string GetRecordNFormActionMaster = "SP_GetRecordNFormActionMaster";
        public const string UpdateNFormActionMaster = "SP_NformActionMasterAddOrEdit";
        public const string DeleteNFormActionMaster = "SP_deletenformactionmaster";


        #endregion

        #region ConcilliationActionMaster

        public const string GetListConcilliationActionMaster = "SP_GetConcilliationActionMasterList";
        public const string GetRecordConcilliationActionMaster = "SP_GetRecordConcilliationActionMaster";
        public const string UpdateConcilliationActionMaster = "SP_ConcilliationActionMasterAddOrEdit";
        public const string DeleteConcilliationActionMaster = "SP_deleteconcilliationactionmaster";
        #endregion

        #region TFormActionMaster

        public const string GetListTFormActionMaster = "SP_GetTFormActionMasterList";
        public const string GetRecordTFormActionMaster = "SP_GetRecordTFormActionMaster";
        public const string UpdateTFormActionMaster = "SP_TFormActionMasterAddOrEdit";
        public const string DeleteTFormActionMaster = "SP_deletetformactionmaster";


        #endregion

        #region TypeOfBody
        public const string GetListTypeOfBody = "SP_GetTypeOfBodyList";
        public const string GetRecordTypeOfBody = "SP_GetRecordTypeOfBody";
        public const string UpdateTypeOfBody = "SP_TypeOfBodyAddOrEdit";
        public const string DeleteTypeOfBody = "SP_deletetypeofbody";
        #endregion

        #region TalukaMaster
        public const string GetListTalukaMaster = "SP_GetTalukaMasterList";
        public const string GetRecordTalukamaster = "SP_GetRecordTalukaMaster";
        public const string UpdateTalukamaster = "SP_TalukaMasterAddOrEdit";
        public const string DeleteTalukaMaster = "SP_deletetalukamaster";
        #endregion

        #region ApplicantMaster
        public const string RegistrationAdd = "SP_RegisterApplicant";
        public const string ApplicantVerifyOTP = "SP_GetRecordVerifyOTPForApplicant";
        public const string ApplicantReVerifyOTP = "SP_GetRecordREVerifyOTPForApplicant";
        public const string ApplicantLogin = "SP_LoginApplicantMaster";
        public const string ForgotPassword = "SP_ForgotPasswordSendMailApplicant";
        public const string ResendEmailVerification = "SP_ResendMailApplicant";
        public const string ChangePassword = "SP_UpdateForgotPasswordByEmailApplicant";
        public const string UpdateIslockedflageApp = "SP_UpdateIslockedflageApp";
        #endregion

        #region ApplicantProfile
        public const string GetApplicantProfile = "SP_GetRecordApplicantProfile";
        public const string GetUserProfileList = "SP_GetUserProfileListByID";
        #endregion

        #region CommonList
        public const string GetActiveZoneList = "SP_GetActive_ZoneList";
        public const string GetAllZoneList = "SP_GetAll_ZoneList";

        public const string GetActivePositionList = "SP_GetActive_PositionList";
        public const string GetAllPositionList = "SP_GetAll_PositionList";


        public const string GetActiveRegionList = "SP_GetActive_RegionList";
        public const string GetAllRegionList = "SP_GetAll_RegionList";

        public const string GetActiveRoleList = "SP_GetActive_RoleList";
        public const string GetAllRoleList = "SP_GetAll_RoleList";

        public const string GetActiveBranchList = "SP_GetActive_BranchList";
        public const string GetAllBranchList = "SP_GetAll_BranchList";

        public const string GetActiveDesignationList = "SP_GetActive_DesignationList";
        public const string GetAllDesignationList = "SP_GetAll_DesignationList";
        public const string GetGanderList = "SP_GanderList";

        public const string GetActiveTalukaList = "SP_GetActive_TalukaList";
        public const string GetAllTalukaList = "SP_GetAll_TalukaList";

        public const string GetActiveEmployeeData = "SP_GetActive_EmployeeData";
        public const string GetAllEmployeeDataList = "SP_GetAll_EmployeeDataList";

        public const string GetActiveDistrictList = "SP_GetActive_DistrictList";
        public const string GetAllDistrictList = "SP_GetAll_DistrictList";


        public const string GetActiveTradunionList = "SP_GetActive_TradunionList";
        public const string GetAllTradunionList = "SP_GetAll_TradunionList";

        public const string GetActiveDepartmentList = "SP_GetActive_DepartmentList";
        public const string GetAllDepartmentList = "SP_GetAll_DepartmentList";

        public const string GetActiveWorkTypeList = "SP_GetActive_WorkTypeList";
        public const string GetAllDWorkTypeList = "SP_GetAll_WorkTypeList";

        public const string GetActiveEstablishmentList = "SP_GetActive_EstablishmentList";
        public const string GetAllEstablishmentList = "SP_GetAll_EstablishmentList";

        public const string GetActiveAreamasterList = "SP_GetActive_AreamasterList";
        public const string GetAllDAreamasterList = "SP_GetAll_AreamasterList";

        public const string GetActiveEmployeeList = "SP_GetActive_EmployeeList";
        public const string GetAllEmployeeList = "SP_GetAll_EmployeeList";



        public const string GetActiveHearingReasonList = "SP_GetActive_HearingReasonList";
        public const string GetAllHearingReasonList = "SP_GetAll_HearingReasonList";

        //public const string GetActiveHearingReasonList = "SP_GetActive_HearingReasonList_Abhishek";

        //public const string GetAllHearingReasonList = "SP_GetAll_HearingReasonList_Abhishek";

        public const string GetActiveMenuList = "SP_GetActive_MenuList";
        public const string GetAllMenuList = "SP_GetAll_MenuList";

        public const string GetActiveTypeOfBodyList = "SP_GetActive_TypeOfBodyList";
        public const string GetAllTypeOfBodyList = "SP_GetAll_TypeOfBodyList";

        public const string GetActiveCessPaymentTypeList = "SP_GetActive_CessPaymnetTypeList";
        public const string GetAllCessPaymentTypeList = "SP_GetAll_CessPaymentTypeList";


        public const string InsertTryCatchErrorLogHistory = "SP_InsertTryCatchErrorLogHistory";
        public const string InsertEmailLogHistory = "SP_InsertEmailLogHistory";
        public const string Insertofficeloginlogout = "SP_Insertofficeloginlogout";
        public const string InsertApplicantloginlogout = "SP_InsertApplicantloginlogout";


        public const string GetActiveProjectList = "SP_GetActive_ProjectList";
        public const string GetAllProjectList = "SP_GetAll_ProjectList";

        public const string GetTypeOfBusinessTradeList = "SP_GetTypeOfBusinessTradeList";

        public const string GetActiveAppealDayList = "SP_GetActive_AppealDayList";
        public const string GetAllAppealDayList = "SP_GetAll_AppealdayList";
        public const string GetActionReferList = "SP_Get_ActionReferList";
        public const string GetHOActionReferList = "SP_Get_HOActionReferList";


        #endregion

        #region TradeUnionRegistrationMaster
        public const string GetListTradeUnionRegistrationMaster = "SP_GetTradeUnionRegistrationMasterList";
        public const string GetRecordTradenionRegistrationmaster = "SP_GetRecordTradeUnionRegistrationMaster";
        public const string UpdateTradeUnionRegistrationmaster = "SP_TradeUnionRegistrationMasterAddOrEdit";
        public const string DeleteTradeUnionRegistrationMaster = "SP_deletetradunionregistrationmaster";
        #endregion

        #region DesignationMaster

        public const string GetListDesignationMaster = "SP_GetDesignationMasterList";
        public const string GetRecoredDesignationMaster = "SP_GetRecordDesignationMaster";
        public const string updateDesignationMaster = "SP_DesignationMasterAddOrEdit";
        public const string DeleteDesignationMaster = "SP_deletedesignationmaster";

        #endregion

        #region EmpPositionMaster
        public const string GetListEmpPositionMasterMaster = "SP_GetEmpPositionMasterList";
        public const string GetRecordEmpPositionMaster = "SP_GetRecordEmpPositionMaster";
        public const string UpdateEmpPositionMaster = "SP_EmpPositionMasterAddOrEdit";
        public const string DeleteEmpPositionMaster = "SP_deleteemppositionmaster";
        #endregion

        #region PositionMaster
        public const string GetListPositionMaster = "SP_GetPositionMasterList";
        public const string GetRecoredPositionMaster = "SP_GetRecordPositionMaster";
        public const string UpdatePositionMaster = "SP_PositionMasterAddOrEdit";
        public const string DeletePositionMaster = "SP_DeletePositionMaster";
        #endregion

        #region EmployeeMaster
        public const string GetListEmployeeMaster = "SP_GetEmployeeMasterList";
        public const string GetRecordEmployeeMaster = "SP_GetRecordEmployeeMaster";
        public const string UpdateEmployeeMaster = "SP_EmployeeMasterAddOrEdit";
        public const string DeleteEmployeeMaster = "SP_deleteemployeemaster";
        public const string changeEmployeepassword = "SP_ChnageemployeePassword";
        public const string GetProfileList = "SP_GetProfileList";
        public const string UpdateIslockedflage = "SP_UpdateIslockedflage";
        public const string ChangeApplicantPassword = "SP_ChnageApplicantPassword";


        #endregion

        #region MenuMaster
        public const string GetListMenuMaster = "SP_GetMenuMasterList";
        public const string GetRecordMenuMaster = "SP_GetRecordMenuMaster";
        public const string UpdateMenuMaster = "SP_MenuMasterAddOrEdit";
        public const string DeleteMenuMaster = "SP_deletemenumaster";
        public const string UpdateAssignMenuDetail = "SP_AssignuserMenu";
        #endregion

        #region EmployeeLoginMaster
        public const string EmployeeLogin = "SP_LoginEmployee";
        public const string GetMenuListing = "SP_GetMenuListing";
        public const string GetUserPositionList = "SP_UserPositionList";
        #endregion

        #region EstablishmentMaster
        public const string GetListEstablishmentMaster = "SP_GetEstablishmentMasterList";
        public const string GetRecordEstablishmentMaster = "SP_GetRecordEstablishmentMaster";
        public const string UpdateEstablishmentMaster = "SP_EstablishmentMasterAddOrEdit";
        public const string DeleteEstablishmentMaster = "SP_EstablishmentMasterAddOrEdit";

        #endregion  

        #region ReinstatementAppliaction
        public const string GetReinstatementAppliactionList = "SP_GetReinstatementAppliactionList";
        public const string GetRecordReinstatementAppliaction = "SP_GetRecordReinstatementAppliaction";
        public const string UpdateReinstatementAppliaction = "SP_ReinstatementAppliactionAddOrEdit";
        public const string GetRecordTradunionregistrationDetail = "SP_GetRecordTradunionregistrationDetail";
        public const string UpdateReiniTradunionregistrationDetail = "SP_ReiniTradunionregistrationDetailAddOrEdit";
        public const string UpdateReiniTradunionregistrationDetail1 = "SP_ReiniTradunionregistrationDetailAddOrEdit";
        public const string GetRecordReiniEstablishment = "SP_GetRecordReiniEstablishmentDetail";
        public const string UpdateReiniEstablishmentDetail = "SP_ReiniEstablishmentDetailAddOrEdit";
        public const string UpdateReiniAppliactionFinalsubmit = "SP_ReinstatementAppliactionFinalsubmit";
        public const string GetRecordReinAppliactionDocumentURL = "SP_ReinAppliactionDocumentURLRecord";
        public const string UpdateReinstatementAppliactionUpdateSubmit = "SP_ReinstatementAppliactionUpdateSubmit";
        public const string GetRecordSubmitVerify = "SP_GetRecordSubmitVerify";

        //ACOL
        public const string GetReinstatementACOLList = "SP_GetReinstatementACOLList";
        public const string GetRecordReinstatementACOL = "SP_GetRecordReinstatementACOL";
        //public const string UpdateReinstatementHearingACOL = "SP_UpdateReinstatementHearingACOL";
        public const string UpdateReinstatementHearingACL = "SP_UpdateReinstatementHearingACL";
        public const string UpdateReinstatementSettlementACOL = "SP_UpdateReinstatementSettlementACOL";
        public const string UpdateReinstatementSendLCACOL = "SP_UpdateReinstatementSendLCACOL";
        public const string GetRecordReinstatementApplicationHistory = "SP_GetRecordReinstatementApplicationHistory";
        #endregion

        #region INFormApplication

        public const string GetNFormApplicationList = "SP_GetNFormAppliactionList";
        public const string GetRecordNFormApplication = "SP_GetRecordNFormApplication";
        public const string UpdateNFormApplication = "SP_NFormApplicationAddOrEdit";
        public const string GetRecordNFormEstablishmentDetail = "SP_GetRecordNFormEstablishmentDetail";
        public const string UpdateNFormEstablishment = "SP_NFormEstablishmentDetailAddOrEdit";
        public const string GetRecordNFormAppliactionDocumentURLRecord = "SP_NFormAppliactionDocumentURLRecord";
        public const string UpdateNFormAppliactionFinalsubmit = "SP_NFormAppliactionFinalsubmit";
        public const string UpdateNFormAppliactionUpdateSubmit = "SP_NFormAppliactionUpdateSubmit";
        public const string UpdateNFormSendtoACLFromClerk = "SP_NFormSendtoACLFromClerk";
        public const string UpdateNFormReviewSendtoACL = "SP_NFormReviewSendtoACL";
        public const string UpdateNFormSendtoCommentFromClerk = "SP_NFormSendtoCommentFromClerk";
        public const string UpdateEshtablishmentAddressdetailFromACL = "SP_NFormUpdateEshtablishmentFromACL";

        public const string UpdateApproveOrRejectReviewFromACL = "SP_ApproveOrRejectReviewFromACL";
        public const string SendTRecoveryForm = "SP_SendTRecoveryForm";



        //ACL
        public const string GetNFormACLList = "SP_GetNFormACLList";
        public const string GetRecordNFormACL = "SP_GetRecordNFormACL";
        //public const string UpdateNFormHearingACOL = "SP_UpdateNFormHearingACOL";
        public const string UpdateNFormHearingACL = "SP_UpdateNFormHearingACL";
        public const string UpdateNFormSettlementACL = "SP_UpdateNFormSettlementACL";
        public const string GetRecordNFormApplicationHistory = "SP_GetRecordNFormApplicationHistory";
        #endregion

        #region ConciliationAppliaction
        public const string GetConciliationAppliactionList = "SP_ConciliationApplicationList";
        public const string GetRecordConciliationAppliaction = "SP_GetRecordConciliationApplication";
        public const string ConciliationAddEditAppliaction = "SP_ConciliationApplicationAddOrEdit";
        public const string GetRecordConcilEstablishment = "SP_GetRecordConcilEstablishmentDetail";
        //public const string GetRecordTradeunionDetails = "SP_GetRecordConcilTradeUnionDetail";
        //public const string UpdateConcilEstablishmentDetail = "SP_ConcilEstablishmentDetailAddOrEdit_newtest";
        // public const string GetRecordConcilEstablishment = "SP_GetRecordConcilEstablishmentDetail_newtest";
        

        public const string UpdateConcilEstablishmentDetail = "SP_ConcilEstablishmentDetailAddOrEdit";
        public const string UpdateConciliationTradeUnionDetail = "SP_UpdateConcilationTradeunion";
        public const string GetRecordConcilAppliactionDocumentURL = "SP_ConcilAppliactionDocumentURLRecord";
        public const string UpdateConcilAppliactionFinalsubmit = "SP_ConciliationAppliactionFinalsubmit";
        public const string GetRecordConcilTradunionregistration = "SP_GetRecordConcilTradeUnionDetail";
        public const string UpdateConciliationAppliactionUpdateSubmit = "SP_ConciliationAppliactionUpdateSubmit";
        //public const string GetRecordSubmitVerify = "SP_GetRecordSubmitVerify";

        //ACOL
        public const string GetConciliationACLList = "SP_GetRecordConciliationACLList";
        public const string GetRecordConciliationACL = "SP_GetRecordConciliationtACL";
        public const string UpdateConciliationHearingACL = "SP_UpdateConciliationHearingACL";
        public const string UpdateConciliationSendLCACL = "SP_UpdateConciliationSendLCByACL";
        public const string UpdateConciliationSettlementACL = "SP_UpdateConciliationSettlementACL";
        public const string UpdateConciliationApplicationHistory = "SP_GetRecordConciliationApplicationHistory";
        public const string UpdateConciliationClerkStatus = "SP_UpdateConciliationClerkStatus";
        public const string UpdateConcilQueryByACLCLerk = "SP_UpdateConcilQueryByACLClerk"; 
        public const string GetRecordConcilACLClerk = "SP_GetRecordConcilACLClerk"; 
        public const string GetConciliationDCClerkList = "SP_GetConciliationDCLClerkList";
        public const string GetRecordConciliationDCLClerk = "SP_GetRecordConcilDCLClerk"; 
        public const string UpdateConcilStatusByDCLClerk = "SP_UpdateConcilByDCLClerk"; 
        public const string GetConciliationDCLList = "SP_GetConciliationDCLList";
        public const string UpdateConcilStatusByDCL = "SP_UpdateConcilByDCL";
        public const string GetConciliationHOClerkList = "SP_GetConciliationHOClerkList"; 
        public const string GetConciliationHOList = "SP_GetConciliationHOList"; 
        public const string GetRecordConcilHOClerk = "SP_GetRecordConcilHOClerk";
        public const string GetRecordConcilHO = "SP_GetRecordConcilHO"; 
        public const string UpdateConcilStatusByHOClerk = "SP_UpdateConcilByHOClerk";
        public const string UpdateConcilStatusByHO = "SP_UpdateConcilStatusByHO";
        public const string UpdateConcilQueryByHOCLerk = "SP_UpdateConcilQueryByHOClerk";
        public const string UpdateConciliationUpdatedetailFrom = "SP_ConciliationUpdateEshtablishmentFromACL";
        //public int MyProperty { get; set; }
        #endregion

        #region TFormAppealApplication
        public const string GetTFormApplicationList = "SP_TformAppealApplicationList";
        public const string GetRecordTFormEmployee = "SP_GetRecordTFormEmployee";
        public const string GetRecordTFormEmployer = "SP_GetRecordTFormEmployer";
        public const string AddTFormApplicationforEmployee = "SP_TFormApplicationForEmployeeAdd";
        public const string GetTFormApplicationDCLLIst = "SP_GetRecordTFormDCLList";
        public const string AddTFormApplicationforEmployer = "SP_TFormApplicationForEmployerSave";

        //DCL
        /*ublic const string GetNFormACLList = "SP_GetNFormACLList";*/
        public const string GetRecordTFormDCL = "SP_GetRecordTFormDCL";
        public const string UpdateTFormHearingDCL = "SP_UpdateTFormHearingByDCL";
        //public const string UpdateTFormHearingDCL = "SP_UpdateTFormHearingByDCL_dummy";
        public const string UpdateTFormRemandBackDCL = "SP_UpdateTFormRemandBackByDCL";
        public const string UpdateTFormConfirmDCL = "SP_UpdateTFormConfirmByDCL";
        public const string UpdateTFormDissmisDCL = "SP_UpdateTFormDissmisByDCL";
        public const string UpdateTFormHistory = "SP_GetRecordTFormApplicationHistory";
        #endregion

        #region Licence Application
        public const string GetLicenceApplicationList = "SP_LicenceIMWApplicationList";
        public const string GetRecordLicenceApplication = "SP_GetRecordLicenceIMWApplication";
        public const string GetRecordLicenceAmendment = "SP_GetRecordLicenceAmendment";
        public const string LicenceContractDetailAddEdit = "SP_LicenceContractDetailAppAddOrEdit";
        public const string LicenceEstablishmentDetailUpdate = "SP_UpdateEstablishmentLicence";
        public const string PrincipleDetailUpdate = "SP_PrincipalEmployerLicenceAddOrEdit";
        public const string PrincipleEmployerLicenceUpdate = "SP_PrincipalEmployerLicenceAddOrEdit";
        public const string LicnecnSecurityDetailUpdate = "SP_LicenceSecurityDetailAddorEdit";
        public const string updateLicenceuploadFile = "SP_LicenceFileUploadAndFinalsubmit";
        public const string LicenceApplicationSubmitFinal = "SP_LicenceApplicationRegistrationUpdateSubmit";

        public const string GetLicenceApplicationACLList = "SP_GetLicenceRegistrationACLList";
        public const string UpdateApproveOrRejectlicenceByACL = "SP_ApproveRejectLicenceApplicationByACL"; 
        public const string UpdateLicenceApplicationByACL = "SP_LicenceApplicationQueryByACL"; 
        public const string QueryLicenceApplicationByDCL = "SP_LicenceApplicationQueryByDCLHO";
        public const string GetLicenceApplicationDCLList = "SP_GetLicenceRegistrationDCLList";
        public const string GetLicenceApproveRejectByACL = "SP_LicenceApproveRejectByACL";
        public const string GetLicenceApplicationHOACLList = "SP_GetLicenceRegistrationHOACLList";
        public const string UpdateLicenceAppSendtoHODCLFromACL = "SP_LicenceAppSendtoHODCLFromACL";
        public const string GetAmendmentList = "SP_LicenceApplicantAmendmentList";
        //DCL
        /*ublic const string GetNFormACLList = "SP_GetNFormACLList";*/


        #endregion

        #region Application Registration
        public const string GetEstablishmentregistrationList = "SP_EstablishmentregistrationList";
        public const string GetApplicationRegistrationAmendmentList = "SP_ApplicationRegistrationAmendmentList";
        public const string GetRecordApplicationRegistration = "SP_GetRecordApplicationRegistration";
        public const string GetRecordAppegistrationAmendment = "SP_GetRecordAppegistrationAmendment";
        public const string UpdateApplicationRegistration = "SP_ApplicationRegistrationAddOrEdit";
        public const string UpdatePrincipalEmployerRegistration = "SP_PrincipalEmployerRegistrationAddOrEdit";
        public const string UpdateContractorRegistration = "SP_ContractorRegistrationAddOrEdit";
        public const string UpdateApplicationRegistrationFinalsubmit = "SP_ApplicationRegistrationFinalsubmit";
        public const string UpdateApplicationRegistrationUpdateSubmit = "SP_ApplicationRegistrationUpdateSubmit";

        public const string GetApplicationRegistrationACLList = "SP_GetApplicationRegistrationACLList";
        public const string GetAppRegistrationSendtoCommentFromACL = "SP_AppRegistrationSendtoCommentFromACL";
        public const string GetApplicationApproveRejectFromACL = "SP_ApplicationApproveRejectFromACLL";
        public const string GetApplicationRegistrationHOACLList = "SP_GetApplicationRegistrationHOACLList";
        public const string UpdateAppRegistrationSendtoHODCLFromACL = "SP_AppRegistrationSendtoHODCLFromACL";
        public const string GetApplicationRegistrationHODCLList = "SP_ApplicationRegistrationHODCLList";
        #endregion

        #region Dashboard
        public const string GetDashBoardDetail = "SP_GetDashboardDetail";
        public const string GetDashboardCaseClimeDetail = "SP_GetDashboardCaseClimeDetaill";

        #endregion

        #region Inspection
        public const string GetInspectionList = "SP_InspectionList";
        public const string GetRecordInspectionApplication = "SP_GetRecordInspectionApplication";
        public const string UpdateInspectionReportInformation = "SP_InspectionReportInformationAddOrEdit";
        public const string UpdateInspectionEstablishments = "SP_InspectionEstablishmentsAddOrEdit";
        public const string UpdateEstablishmentsDetail = "SP_InspectionEstablishmentsDetailAddOrEdit";
        public const string UpdateCompanyDetails = "SP_InspectionCompanyDetailsAddOrEdit";
        public const string UpdateEmployeeDetail = "SP_InspectionEmployeeDetailAddOrEdit";
        public const string UpdateContractorDetails = "SP_InspectionContractorDetailAddOrEdit";
        public const string UpdateInspectionOnsitePicture = "SP_InspectionOnsitePictureAddOrEdit";
        public const string UpdateOtherDetails = "SP_InspectionOtherDetailsAddOrEdit";
        #endregion
    }
}





