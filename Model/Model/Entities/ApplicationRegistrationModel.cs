using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class ApplicationRegistrationModel : BaseEntity
    {
        public object fileName { get; set; }

        public int RegistrationID { get; set; }
        public int RegistrationIDEdit { get; set; }
        public string EstablishmentName { get; set; }
        public string DOC1 { get; set; }
        public string DOC2 { get; set; }
        public string DOC3 { get; set; }
        public string DOC4 { get; set; }
        public string DOC5 { get; set; }
        public string DOC6 { get; set; }
        public string EstablishmentNote { get; set; }
        public bool Isverified { get; set; }
        public int ActionCode { get; set; }
        public int TypeOfBusinessTrade { get; set; }
        public float Registrationfees { get; set; }
        public string ChallanNo { get; set; }
        public string Treasury { get; set; }
        public DateTime ChallanDate { get; set; }
        public bool IsCLRA { get; set; }
        public bool IsIMW { get; set; }
        public bool ISMTW { get; set; }
        public bool IsCLRA_verified { get; set; }
        public bool IsIMW_verified { get; set; }
        public bool ISMTW_verified { get; set; }
        public int IsMultipul { get; set; }
        public string CLRANote { get; set; }
        public string IMWNote { get; set; }
        public string MTWNote { get; set; }
        //public int ID { get; set; }
        public string ApplicationMode { get; set; }
        public string AppDate { get; set; }
        public string AppID { get; set; }

        public int PrincipalID { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalFatherName { get; set; }

        public List<ApplicationRegistrationModel> Establishmentregistrationdetaillst { get; set; }
        public List<MCommonList> TypeOfBusinessTradeList { get; set; }
        public List<ApplicationRegistrationModel> PrincipalEmployerInformationdetaillst { get; set; }
        public int ContractorID { get; set; }
        public string ContractorName { get; set; }
        public string Natureofwork { get; set; }
        public DateTime commencement { get; set; }
        public DateTime completion { get; set; }
        public int MaxnoContLab { get; set; }
        public List<ApplicationRegistrationModel> Contractordetaillst { get; set; }
        public List<MCommonList> CommonList { get; set; }
    
        public int DocumentID { get; set; }
        public string Status { get; set; }
        public int ISAL { get; set; }
        public int ISDL { get; set; }

        //public int MaleWorker { get; set; }
        //public int FemaleWorker { get; set; }
        //public int TotalWorker { get; set; }
        //public int ContractMaleWorker { get; set; }
        //public int ContractFemaleWorker { get; set; }
        //public int ContractTotalWorker { get; set; }
        //public int FixTermMaleWorker { get; set; }
        //public int FixTermFemaleWorker { get; set; }
        //public int FixTermTotalWorker { get; set; }
        //public int OthersMaleWorker { get; set; }

        //public int? OthersFemaleWorker { get; set; }
        //public int? OthersTotalWorker { get; set; }
        public string Applicationtype { get; set; }
        public int ISHO { get; set; }
        public string DCLComments { get; set; }
        public string DCL_Review_Comments { get; set; }
        public string ACL_Review_Comments { get; set; }
        public int ISAmendment { get; set; }
        public int RefRegistrationID { get; set; }
        public int ISAmendmentEdit { get; set; }
        public int IsEdit { get; set; }
        public string ISAmendmentType { get; set; }
        public int ISNew { get; set; }
        public string Approve_DOC { get; set; }
    }
}
