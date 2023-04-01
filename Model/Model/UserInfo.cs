namespace Distichain.Model
{
    using System.Collections.Generic;

    public class UserInfo
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }  

        public string CurrentRole { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public int RoleID { get; set; }
     
    }
}
