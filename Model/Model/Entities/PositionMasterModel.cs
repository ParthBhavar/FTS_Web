using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using FTS.Model.Common;

namespace FTS.Model.Entities
{
	public class PositionMasterModel : BaseEntity
	{

		[Required(ErrorMessage = "Position I D is required")]
		public int PositionID { get; set; }

		public int? RegionID { get; set; }

		public int? RoleID { get; set; }

		public int? BranchID { get; set; }

		[StringLength(150,ErrorMessage = "Position Name must not be more than 150 char")]
		public string PositionName { get; set; }

		public int? ParentPositionID { get; set; }

        public int PositionIDEdit { get; set; }

        public List<PositionMasterModel> MenuList { get; set; }
        //public class TreeViewNode
        //{
        //    public string id { get; set; }
        //    public string parent { get; set; }
        //    public string text { get; set; }
        //}

    }
}
