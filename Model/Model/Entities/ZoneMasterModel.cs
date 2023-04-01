using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using FTS.Model;

namespace FTS.Model.Entities
{
	public class ZoneMasterModel : BaseEntity
	{

		public int ZoneIDEdit { get; set; }

		[StringLength(150,ErrorMessage = "Zone Name must not be more than 150 char")]
		public string ZoneName { get; set; }

        public string serchstring { get; set; }
		public int TotalRecord { get; set; }

	}
}
