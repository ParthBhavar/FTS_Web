using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class TFormActionMasterModel : BaseEntity
    {

        public int ActionID { get; set; }
        public int ActionCode { get; set; }
        public string ActionName { get; set; }
        public string Status { get; set; }
        public string ActionText { get; set; }
        public int PerformActionID { get; set; }
        public int ForwardTo { get; set; }
        public int ParentActionID { get; set; }
        public bool IsQuery { get; set; }
        public bool IsActive { get; set; }
        public int TFormActionIDEdit { get; set; }
        public int TotalRecord { get; set; }
    }
}
