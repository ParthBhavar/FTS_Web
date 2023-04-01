using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class UserAssignMenuModel : BaseEntity
    {
        public int MenuId { get; set; }
        public int EmployeeID { get; set; }
        public int? EmpPosID { get; set; }



    }
}
