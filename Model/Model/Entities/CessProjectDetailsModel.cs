using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class CessProjectDetailsModel : BaseEntity
    {
        public string fileName;

        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectAddress { get; set; }
        public string AreaRoad { get; set; }

        public int DistrictID { get; set; }
        public string Doc1 { get; set; }
       
        public int TalukaID { get; set; }
        public string LandMark { get; set; }
        //public  int DepartmentID { get; set; }
        public int NoOfWorkersEmployed { get; set; }
        public DateTime DateOfCommencementOfWork { get; set; }
        public DateTime EstimatedPeriodOfWork { get; set; }
        public decimal TotalEstimatedProjectCost { get; set; }
        public int ProjectIDEdit { get; set; }
        public int TotalRecord { get; set; }
        public int DepartmentID { get; set; }
        public int EstablishmentID { get; set; }
        public string EstablishmentName { get; set; }
        public int TypeOfBodyID { get; set; }
        public int CessPaymentID { get; set; }
        public decimal Amount { get; set; }
        public string ChallanNumber { get; set; }
        
        public DateTime Date { get; set; }
        public int EstablishmentIDEdit { get; set; }
        public List<MCommonList> CessPaymentTypelist { get; set; }

        public List<MCommonList> Projectlist { get; set; }

        public List<MCommonList> TypeOfBodylist { get; set; }
        public string Datee { get; set; }

        public class ProjectList
        {
            public int DataValue { get; set; }
            public string DisplayValue { get; set; }

        }
    }
}
