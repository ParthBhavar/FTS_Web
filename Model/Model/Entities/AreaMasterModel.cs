using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class AreaMasterModel : BaseEntity
    {
        public int AreaIDEdit { get; set; }
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public int ZoneID { get; set; }
        public int DistrictId { get; set; }
        public int TotalRecord { get; set; }
    }
}
