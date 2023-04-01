
using System;

namespace DMS_10.Model.DTO
{
	public class RegionMasterDTO
	{

		public string RegionID { get; set; }

		public string RegionName { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public string ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public string IsActive { get; set; }

		public string IsDeleted { get; set; }


	}
}
