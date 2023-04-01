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
	public class EmpPositionMasterModel :BaseEntity
	{

		[Required(ErrorMessage = "Emp Pos I D is required")]
		public int EmpPosID { get; set; }

		public int? PositionID { get; set; }

		public int EmployeeID { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }
	
		public string serchstring { get; set; }
		public int TotalRecord { get; set; }

		public int EmpPosIDEdit { get; set; }
		public List<MCommonList> PositionList { get; set; }
		public List<MEmployeeData> EmployeeData { get; set; }
            public string PositionName { get; set; }
          public string EmployeeName { get; set; }
		public List<EmpPositionMasterModel> MenuList { get; set; }
          public int DataValue { get; set; }
        public string DisplayValue { get; set; }
	 	public int ParentId { get; set; }
		public bool SetAsDefault { get; set; }
        public int IsCheck { get; set; }
		public string TEMPID { get; set; }
		public string email { get; set; }
		public string MobileNo { get; set; }
        public string UPStartDate { get; set; }
        public string UPEndDate { get; set; }
    }
}
