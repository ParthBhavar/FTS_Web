using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class TradeUnionRegistrationMasterModel :BaseEntity
    {
        public string UserID { get; set; }
        public int TradunionID { get; set; }
        public int TradeIDEdit { get; set; }
        [Required(ErrorMessage = "RegistrationNo")]
        public string RegistrationNo { get; set; }
        [Required(ErrorMessage = "RegistrationName")]
        public string RegistrationName { get; set; }

        public string serchstring { get; set; }
        public int RowNum { get; set; }
        public int TotalRecord { get; set; }


    }
}
