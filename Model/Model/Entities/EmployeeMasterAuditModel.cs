using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Master.ViewModel
{
	public class EmployeeMasterAuditModel
	{

		[Required(ErrorMessage = "E M A I D is required")]
		public int EMAID { get; set; }

		public int? EmployeeID { get; set; }

		[StringLength(150,ErrorMessage = "Emp Code must not be more than 150 char")]
		public string EmpCode { get; set; }

		public int? DesignationID { get; set; }

		public int? RegionID { get; set; }

		[StringLength(150,ErrorMessage = "First Name must not be more than 150 char")]
		public string FirstName { get; set; }

		[StringLength(150,ErrorMessage = "Middle Name must not be more than 150 char")]
		public string MiddleName { get; set; }

		[StringLength(150,ErrorMessage = "Last Name must not be more than 150 char")]
		public string LastName { get; set; }

		[RegularExpression(@"^[a-zA-Z0-9_\.-]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,6}$",ErrorMessage ="Email is not valid")]
		[StringLength(150,ErrorMessage = "Email I D must not be more than 150 char")]
		public string EmailID { get; set; }

		[StringLength(150,ErrorMessage = "Mobile No must not be more than 150 char")]
		public string MobileNo { get; set; }

		public string PAddress { get; set; }

		public string SAddress { get; set; }

		public int? Gender { get; set; }

		public DateTime? DOB { get; set; }

		public string Password { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public int? ModifiedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public int? IsActive { get; set; }

		public int? IsDeleted { get; set; }


	}
}
