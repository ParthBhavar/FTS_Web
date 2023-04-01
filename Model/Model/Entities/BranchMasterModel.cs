using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using FTS.Model.Common;

namespace FTS.Model.Entities
{
	public class BranchMasterModel : BaseEntity
	{

		public int BranchID { get; set; }
		public int BranchIDEdit { get; set; }

		[StringLength(200,ErrorMessage = "Branch Name must not be more than 200 char")]
		public string BranchName { get; set; }

		public int? ParentBranch { get; set; }

		public string serchstring { get; set; }
		public int TotalRecord { get; set; }
		
	
	}
}
