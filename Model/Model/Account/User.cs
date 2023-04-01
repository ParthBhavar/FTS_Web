using FTS.Model;
using FTS.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static FTS.Model.Constants.Constants;
using static FTS.Model.Enum;

namespace FTS.Model.Account
{
    public class User : BaseEntity
    {

        public User()
        {
            AllergicSubstance = new List<int>();
        }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }
                
        [Required(ErrorMessage = "NameRequired")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public Role RoleId { get; set; }
        
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        
        [MaxLength(50)]
        [RegularExpression(RegexConstants.EmailFormat, ErrorMessage = "EmailFormat")]
        public string Email { get; set; }

        public int LanguageId { get; set; }
        [MaxLength(200)]
        public string ProfileImage { get; set; }
        
        [MaxLength(15)]
        [RegularExpression(RegexConstants.PhoneNumberFormat, ErrorMessage = "PhoneNumberFormat")]
        public string PhoneNumber { get; set; }
        public string QRCode { get; set; }
        [Required(ErrorMessage = "AlertOnWalletAmountRequired")]
        public decimal AlertOnWalletAmount { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public FileModel FileProfileImage { get; set; }
        public int? OwnerId { get; set; }

        public List<int> RoleMappingId { get; set; }

        public bool? IsVegetarian { get; set; }

        public List<int> AllergicSubstance { get; set; }

        public bool? IsNew { get; set; }

        public bool IsPrimary { get; set; }

        public string Code { get; set; }

        public string ClassRoom { get; set; }

    }

    //public class RefreshTokenModel
    //{
    //    public string RefreshToken { get; set; }
    //}

    //public class UserHash
    //{
    //    public string Hash { get; set; }
    //}
}
