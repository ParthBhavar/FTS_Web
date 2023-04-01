using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class EstablishmentMasterModel: BaseEntity
    {
        public int EstablishmentID { get; set; }
        public int EstablishmentIDEdit { get; set; }

        [Required(ErrorMessage = "The EstablishmentCode field is required.")]
        public string EstablishmentCode { get; set; }
        public string EstablishmentName { get; set; }


        [Required (ErrorMessage = "The pincode field is required.")]

         
        public int DeletedOn { get; set; }
        public int? DeletedBy { get; set; }

        public int? IsDelete { get; set; }

        public int TotalRecord { get; set; }

    }
}
