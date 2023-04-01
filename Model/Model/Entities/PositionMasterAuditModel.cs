using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Master.ViewModel
{
	public class PositionMasterAuditModel
	{

		[Required(ErrorMessage = "P A I D is required")]
		public int PAID { get; set; }

		public int? PositionID { get; set; }

		public int? RegionID { get; set; }

		public int? RoleID { get; set; }

		public int? BranchId { get; set; }

		public int? ZoneId { get; set; }

		[StringLength(150,ErrorMessage = "Position Name must not be more than 150 char")]
		public string PositionName { get; set; }

		public int? ParentPositionID { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public int? ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public int? IsActive { get; set; }

		public int? IsDeleted { get; set; }


	}
}
