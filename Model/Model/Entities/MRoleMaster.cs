using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTS.Model.Common;

namespace FTS.Model.Entities
{
    public class MRoleMaster : BaseEntity
    {

        public int RoleID { get; set; }
        public int RoleIDEdit { get; set; }
        [Required(ErrorMessage = "RoleName")]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "ParentRoleID")]
        public int ParentRoleID { get; set; }
        public int Level { get; set; }

        public string serchstring { get; set; }
        public int RowNum { get; set; }
        public int TotalRecord { get; set; }
  
        public List<MCommonList> Positionlist { get; set; }

    }
}
