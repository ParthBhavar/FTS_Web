using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class TalukaMasterModel : BaseEntity
    {
        public int TalukaIDEdit { get; set; }
        public string TalukaCode { get; set; }
       
        [Required(ErrorMessage = "TalukaName")]
        public string TalukaName { get; set; }
        public string serchstring { get; set; }
        public int TotalRecord { get; set; }
      

    }
}
