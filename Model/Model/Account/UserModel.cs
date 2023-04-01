using FTS.Model.Common;
using System;
using System.Collections.Generic;

namespace FTS.Model.Account
{
    public class UserModel 
    {

        public string Id { get; set; }
        public string UserId { get; set; }

        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string CompanyCode { get; set; }

        public string CompanyName { get; set; }

        public string RoleCode { get; set; }

        public string RoleName { get; set; }

        public string Prefix { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Address1 { get; set; }

        public DateTime? BirthDate { get; set; }

        public string ProfilePic { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Ip { get; set; }


    }

    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
    }

    public class UserHash
    {
        public string Hash { get; set; }
    }
}
