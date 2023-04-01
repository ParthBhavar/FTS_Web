using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COL.Model.Entities
{
    public class RegistrationApplicationCLAModel : BaseEntity
    {
        public int RegistrationCLAID { get; set; }
        public string EstablishmentName { get; set; }
        public string EstablishmentAddress { get; set; }
        public int DistrictID { get; set; }
        public int TalukaID { get; set; }
        public int AreaID { get; set; }
        public int Pincode { get; set; }
        public string FaxNo { get; set; }
        public int PhoneNo { get; set; }
        public string Website { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string PanCardNo { get; set; }
        public int BusinessNatureID { get; set; }
        public int TypeOfOwnershipID { get; set; }
        public DateOnly CommencementDate { get; set; }
        public int EstablishmentID { get; set; }
        public string TFActRegNo { get; set; }
        public DateOnly TFActRegDate { get; set; }
        public string TGSCEActRegNo { get; set; }
        public DateOnly TGSCEActDate { get; set; }
        public string EPFActRegNo { get; set; }
        public DateOnly EPFActRegDate { get; set; }
        public string ESIActRegNo { get; set; }
        public DateOnly ESIActRegDate { get; set; }
        public int PrincipalEmployerWorkerMale { get; set; }
        public int PrincipalEmployerWorkerFemale { get; set; }
        public int PrincipalEmployerWorkerTotal { get; set; }
        public int ContractWorkerMale { get; set; }
        public int ContractWorkerFemale { get; set; }
        public int ContractWorkerTotal { get; set; }
        public string EstablishmentContactPerson { get; set; }
        public string EstablishmentContactPersonPhoneNo { get; set; }
        public string EstablishmentContactPersonEmailID { get; set; }
        public string PrincipalEmployerName { get; set; }
        public string PrincipalEmployerAddress { get; set; }
        public int PrincipalEmployerDesignationID { get; set; }
        public string PrincipalEmployerPhoneNo { get; set; }
        public string PrincipalEmployerMobileNo { get; set; }
        public string PrincipalEmployerEmailID { get; set; }
       


    }
}
