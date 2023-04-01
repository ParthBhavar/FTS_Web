
using System;

namespace DMS_10.Model.DTO
{
	public class RoleMasterDTO 
	{

		public string RoleID { get; set; }

		public string RoleName { get; set; }

		public string ParentRoleID { get; set; }

		public string Level { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public string ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public string IsActive { get; set; }

		public string IsDeleted { get; set; }


	}
}
