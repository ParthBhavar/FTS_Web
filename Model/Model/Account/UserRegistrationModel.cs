using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static FTS.Model.Constants.Constants;

namespace FTS.Model.Account
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "NameRequired")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "EmailRequired")]
        [RegularExpression(RegexConstants.EmailFormat, ErrorMessage = "EmailFormat")]
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(20)]
        [RegularExpression(RegexConstants.PhoneNumberFormat, ErrorMessage = "PhoneNumberFormat")]
        public string PhoneNumber { get; set; }
    }
}
