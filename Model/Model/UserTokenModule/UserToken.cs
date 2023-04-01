using System.ComponentModel.DataAnnotations;
using static FTS.Model.Enum;

namespace FTS.Model.UserTokenModule
{
    public class UserToken : BaseEntity
    {
        [Required(ErrorMessage = "EmailRequired")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "TokenRequired")]
        [MaxLength(500)]
        public string Token { get; set; }

        [Required(ErrorMessage = "TokenTypeRequired")]
        public TokenType TokenType { get; set; }
    }
}
