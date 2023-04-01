using System;

namespace DMS_10.Model.DTO
{
	public class BranchMasterDTO
	{

		public string BranchID { get; set; }

		public string BranchName { get; set; }

		public string ParentBranch { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public string ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public string IsActive { get; set; }

		public string IsDeleted { get; set; }


	}
}
