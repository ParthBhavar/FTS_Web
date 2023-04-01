using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace FTS.Model.Entities
{
	public class ApplicantMasterModel: BaseEntity
	{
		public int ApplicantID { get; set; }
		
		public string FirstName { get; set; }
	
		public string MiddleName { get; set; }

		public string LastName { get; set; }
		public string EmailID { get; set; }
		public string MobileNo { get; set; }
		public DateTime? DOB { get; set; }
		public string Password { get; set; }
		public string OTP { get; set; }
		public int ApplicantOTP { get; set; }

		public string VerifyOTP { get; set; }
        public string EncrptmailID { get; set; }
        public string Name { get; set; }
		public string IPAddress { get; set; }
		public int Islocked { get; set; }
		public DateTime LastLoginTime { get; set; }
	}
}
