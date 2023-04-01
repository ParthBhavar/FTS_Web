using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class AppealDayMasterModel : BaseEntity
    {
        public int AppealMasterIDEdit { get; set; }
        public int ID { get; set; }
        public int Days { get; set; }
        public string Action { get; set; }
        public string serchstring { get; set; }
        public int TotalRecord { get; set; }
        public List<MCommonList> AppealDayList { get; set; }
    }
}
