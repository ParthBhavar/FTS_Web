using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class ApplicationRegistrationActionMasterModel : BaseEntity
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
        public int CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
