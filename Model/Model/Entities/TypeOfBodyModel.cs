using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public  class TypeOfBodyModel : BaseEntity
    {
        public int TypeOfBodyIDEdit { get; set; }
        public string Authority { get; set; }
        public int TotalRecord { get; set; }
        public int TypeOfBodyID { get; set; }
       
    }
}
