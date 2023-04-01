using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Master.ViewModel
{
	public class EmpPositionMasterAuditModel
	{

		[Required(ErrorMessage = "E P A I D is required")]
		public int EPAID { get; set; }

		public int? EmpPosID { get; set; }

		public int? PositionID { get; set; }

		public int? EmployeeID { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public int? ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public int? IsActive { get; set; }

		public int? IsDeleted { get; set; }


	}
}
