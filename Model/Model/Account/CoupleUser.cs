using FTS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static FTS.Model.Constants.Constants;
using static FTS.Model.Enum;

namespace FTS.Model.Account
{
   public   class CoupleUser : BaseEntity
    {
        public CoupleUser()
        {
        }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public Role RoleId { get; set; }
        
        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(50)]
        [RegularExpression(RegexConstants.EmailFormat, ErrorMessage = "EmailFormat")]
        public string Email { get; set; }

        public int LanguageId { get; set; }


        [MaxLength(15)]
        [RegularExpression(RegexConstants.PhoneNumberFormat, ErrorMessage = "PhoneNumberFormat")]
        public string PhoneNumber { get; set; }


        public string ClassRoom { get; set; }

        public int ChildId { get; set; }

        public int ParentId { get; set; }

        public bool IsPrimary { get; set; }

    }
}
