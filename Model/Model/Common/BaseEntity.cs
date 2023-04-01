namespace FTS.Model
{
    using FTS.Model.Common;
    using FTS.Model.Entities;
    using System;
    using System.Collections.Generic;

    public class BaseEntity
    {
        public List<MailDetail> ApplicantMailDetail { get; set; }
        public List<MailDetail> EshtablishmentMailDetail { get; set; }
        public int EditId { get; set; }        
        public int UserID { get; set; }        
        public int CreatedBy { get; set; }
    
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }    
        public int ErrorCode { get; set; }    
        public string ErrorMassage { get; set; }
        public string Date { get; set; }
        public string fileName { get; set; }
        public List<MCommonList> Talukalist { get; set; }
        public List<MCommonList> Districtlist { get; set; }
        public List<MCommonList> Ganderlist { get; set; }
        public List<MTradeUnion> TradunionList { get; set; }
        public List<MCommonList> DepartmentList { get; set; }
        public List<MCommonList> WorkTypeList { get; set; }
        public List<MCommonList> EmployeeList { get; set; }
        public List<MArea> AreaList { get; set; }
        public List<MCommonList> BranchList { get; set; }
        public List<MEstablishment> EstablishmentList { get; set; }
        public List<MArea> EstablishAreaList { get; set; }
        public List<MCommonList> ParentPositionlist { get; set; }
        public List<MCommonList> Regionlist { get; set; }
        public List<MCommonList> Rolelist { get; set; }
        public List<MCommonList> Branchlist { get; set; }
        public List<MCommonList> Zonelist { get; set; }
        public List<MCommonList> TypeOfBusinessTradeList { get; set; }
        public string EncryptedId { get; set; }
        public bool IsActive { get; set; }
        public bool IsSubmit { get; set; }
        public int CaseFavourIn  { get; set; }
        public int IsStatus { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string XMLString { get; set; }
        public string PAddress { get; set; }
        public string SAddress { get; set; }
        public int DistrictID { get; set; }
        public int HearingReasonID { get; set; }
        public int ResolutionReasonID { get; set; }
        public bool IsReviewResolution { get; set; }
        public string SettlementNote { get; set; }
        public string SettlementReviewNote { get; set; }
        public DateTime SettlementDate { get; set; }
        public string SettlementDocURL { get; set; }
        public string SettlementReviewDocURL { get; set; }
        public int TalukaID { get; set; }
        public int ZoneID { get; set; }
        public int AreaID { get; set; }
        public int ddlArea { get; set; }
        public int Pincode { get; set; }
        public int EPincode { get; set; }
        public int Gender { get; set; }
        public string Note { get; set; }
        public int RoleID { get; set; }
        public string URL { get; set; }
        public string IP_Address { get; set; }
        public string ResolutionDateIn { get; set; }
        public string Comments { get; set; }
        public int ISView { get; set; }
        public int AppealDays { get; set; }
        public int AppealDayDiff { get; set; }
        public int RecoveryDays { get; set; }
        public string CPassword { get; set; }
        public string mobile { get; set; }
        public string mobile2 { get; set; }
        public int MaleWorker { get; set; }
        public int FemaleWorker { get; set; }
        public int TotalWorker { get; set; }
        public int ContractMaleWorker { get; set; }
        public int ContractFemaleWorker { get; set; }
        public int ContractTotalWorker { get; set; }
        public int FixTermMaleWorker { get; set; }
        public int FixTermFemaleWorker { get; set; }
        public int FixTermTotalWorker { get; set; }
        public int OthersMaleWorker { get; set; }

        public int? OthersFemaleWorker { get; set; }
        public int? OthersTotalWorker { get; set; }
        public string Email { get; set; }
        public int UserMode { get; set; }
        public int stpid { get; set; }
        public string ContractorPAddress { get; set; }
        public string ContractorSAddress { get; set; }
        public int PositionID { get; set; }
        public int PositionDistrictID { get; set; }
        public int IsDashoardList { get; set; }
        public int IsNotice { get; set; }
        public int IsReviewHearing { get; set; }
        public int ID { get; set; }
        public bool IsCancle { get; set; }
        public string TEMPID { get; set; }
        public DateTime OrderOutwardDate { get; set; }
        public DateTime ReviewOrderOutDate { get; set; }
        public int OrderOutwardNo { get; set; }
        public int ReviewOrderOutwardNo { get; set; }
        public string OrderOutwardDatee { get; set; }
    }
}

