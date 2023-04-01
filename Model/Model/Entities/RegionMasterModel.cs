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
	public class RegionMasterModel : BaseEntity
	{

		[Required(ErrorMessage = "Region ID is required")]
		public int RegionID { get; set; }

		[StringLength(150,ErrorMessage = "Region Name must not be more than 150 char")]
		public string RegionName { get; set; }

	

		public string serchstring { get; set; }

		public int TotalRecord { get; set; }
        public int RegionIDEdit { get; set; }
    }
}
