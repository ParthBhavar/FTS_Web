using FTS.Configuration.Options;
using Microsoft.Extensions.Options;

namespace FTS.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration(
            IOptions<EmailSettings> emailSettings,
            IOptions<AuthorizationSettings> authorizationSettings,
            IOptions<DbSettings> dbSettings,
            IOptions<JwtAuthorizationSettings> jwtAuthorizationSettings,
            IOptions<JwtForgotPasswordSettings> jwtForgotPasswordSettings,
            IOptions<PasswordSettings> passwordSettings,
            IOptions<WebSettings> webSettings,           
             IOptions<FileUploadSettings> fileUploadSettings             
            ) 
        {
            EmailSettings = emailSettings.Value;
            AuthorizationSettings = authorizationSettings.Value;
            DbSettings = dbSettings.Value;
            JwtAuthorizationSettings = jwtAuthorizationSettings.Value;
            JwtForgotPasswordSettings = jwtForgotPasswordSettings.Value;
            PasswordSettings = passwordSettings.Value;
            WebSettings = webSettings.Value;           
            FileUploadSettings = fileUploadSettings.Value;            
        }

        public AuthorizationSettings AuthorizationSettings { get; set; }

        public EmailSettings EmailSettings { get; set; }

        public DbSettings DbSettings { get; set; }

        public JwtAuthorizationSettings JwtAuthorizationSettings { get; set; }

        public JwtForgotPasswordSettings JwtForgotPasswordSettings { get; set; }

        public PasswordSettings PasswordSettings { get; set; }

        public WebSettings WebSettings { get; set; }
    

        public FileUploadSettings FileUploadSettings { get; set; }

    }
}
