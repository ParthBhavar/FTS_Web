using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class LoginAudit
    {		
		public string LA_ID { get; set; }
		public string CompanyCode { get; set; }		
		public string UserId { get; set; }
		public string UserCode { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public DateTime LoginDate { get; set; }		
		public string IP { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
	}
}
