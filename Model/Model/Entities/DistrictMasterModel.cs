using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class DistrictMasterModel : BaseEntity
    {

		public int DistrictIDEdit { get; set; }
		public string DistrictName { get; set; }
		public int TotalRecord { get; set; }

	}
}
