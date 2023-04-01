using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using FTS.Model;

namespace FTS.Model.Entities
{ 
	public class DesignationMasterModel : BaseEntity
	{

		[Required(ErrorMessage = "Designation I D is required")]
		public int DesignationID { get; set; }

		
		public int DesignationIDEdit { get; set; }

		[StringLength(150,ErrorMessage = "Designation Name must not be more than 150 char")]
		public string DesignationName { get; set; }

	
		public int RowNum { get; set; }
		public int TotalRecord { get; set; }


	}
}
