using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class RegistrationCLRAModel : BaseEntity
    {
        public int RegistrationID { get; set; }
        public int RegistrationIDEdit { get; set; }
        public string EstablishmentName { get; set; }
        public string EstablishmentAddress { get; set; }
        public int DistrictID { get; set; }        
        public int TalukaID { get; set; }
        public int Pincode { get; set; }
        public string TypeOfBusinessTrade { get; set; }
        public string PrincipalEmployerName { get; set; }
        public string FatherName { get; set; }
        public string PrincipalEmployerAddress { get; set; }
        public int EmpDistrictID { get; set; }
        public int EmpTalukaID { get; set; }
        public int EmpPincode { get; set; }
        public string ContractorName { get; set; }
        public string ContractorAddress { get; set; }
        public string NatureOfWork { get; set; }
        public int MaxNoContLab { get; set; }
        public DateTime EstimateddateofCommencement { get; set; }
        public DateTime EstimatedDateOfCompletion { get; set; }
        public int RegistrationFees { get; set; }
        public string Treasury { get; set; }
        public string ChallanNumber { get; set; }
        public DateTime ChallanDate { get; set; }
        public bool Declaration { get; set; }
        public int TotalRecord { get; set; }
    }
}
