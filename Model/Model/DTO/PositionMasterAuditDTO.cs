
using System;

namespace DMS_10.Model.DTO
{
	public class PositionMasterAuditDTO
	{

		public string PAID { get; set; }

		public string PositionID { get; set; }

		public string RegionID { get; set; }

		public string RoleID { get; set; }

		public string BranchId { get; set; }

		public string ZoneId { get; set; }

		public string PositionName { get; set; }

		public string ParentPositionID { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public string ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public string IsActive { get; set; }

		public string IsDeleted { get; set; }


	}
}
