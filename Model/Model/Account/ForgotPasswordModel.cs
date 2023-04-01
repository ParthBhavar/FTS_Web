using System.ComponentModel.DataAnnotations;

namespace FTS.Model.Account
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "TokenRequired")]
        public string Token { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        public string Password { get; set; }
    }
}
