
using System;

namespace DMS_10.Model.DTO
{
	public class ApplicantMasterDTO 
	{

		public string ApplicantID { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public string EmailID { get; set; }

		public string MobileNo { get; set; }

		public string PAddress { get; set; }

		public string SAddress { get; set; }

		public string Gender { get; set; }

		public DateTime? DOB { get; set; }

		public string Password { get; set; }

		public string CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public string ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public string IsActive { get; set; }

		public string IsDeleted { get; set; }


	}
}
