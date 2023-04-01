using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using FTS.Model;
using FTS.Model.Common;

namespace FTS.Model.Entities
{
	public class EmployeeMasterModel : BaseEntity
	{



       [Required(ErrorMessage = "Employee I D is required")]
		public int EmployeeID { get; set; }


		public int EmployeeIDEdit { get; set; }

		public string PasswordStringEdit { get; set; }

		[StringLength(150,ErrorMessage = "Emp Code must not be more than 150 char")]
		public string EmpCode { get; set; }

		public int DesignationID { get; set; }

		public int RegionID { get; set; }

		[StringLength(150,ErrorMessage = "First Name must not be more than 150 char")]
		public string FirstName { get; set; }

		[StringLength(150,ErrorMessage = "Middle Name must not be more than 150 char")]
		public string MiddleName { get; set; }

		[StringLength(150,ErrorMessage = "Last Name must not be more than 150 char")]
		public string LastName { get; set; }
		public string EmailID { get; set; }
		public string MobileNo { get; set; }

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime? DOB { get; set; }
		
		public string Password { get; set; }

	    public int RowNum { get; set; }
		public int TotalRecord { get; set; }
		
		public List<MCommonList> DesignationList { get; set; }
    
        public int EmpPosID { get; set; }
       
        public int BranchID { get; set; }
		public string PositionName { get; set; }

		public DateTime? EndDate { get; set; }
        public int IsOnchange { get; set; }
        public string UserName { get; set; }
        public int Islocked { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string UPDOB { get; set; }
    }


	public class MenuListModel
    {
	    public int MenuID { get; set; }
		public string MenuName { get; set; }
		public string PageName { get; set; }
		public string PageURL { get; set; }
		public int ParentMenuId { get; set; }
		public string ParentMenuName { get; set; }
        public int EmployeeID { get; set; }
        public int EmpPosID { get; set; }
        public int IsAssign { get; set; }
        public int ID { get; set; }
		public string Icon { get; set; }
	}

	public class UserPositionListModel
    {
		public int EmployeeID { get; set; }
		public int EmpPosID { get; set; }
		public int PositionID { get; set; }
		public string PositionName { get; set; }
		public int v_IsDefaultPo { get; set; }


	}
}
