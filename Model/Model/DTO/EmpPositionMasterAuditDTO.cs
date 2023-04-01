
using System;

namespace DMS_10.Model.DTO
{
	public class EmpPositionMasterAuditDTO 
	{

		public string EPAID { get; set; }

		public string EmpPosID { get; set; }

		public string PositionID { get; set; }

		public string EmployeeID { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public string ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public string IsActive { get; set; }

		public string IsDeleted { get; set; }


	}
}
