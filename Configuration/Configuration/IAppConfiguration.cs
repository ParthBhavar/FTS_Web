namespace FTS.Configuration
{
    using FTS.Configuration.Options;
    public interface IAppConfiguration
    {
        AuthorizationSettings AuthorizationSettings { get; }
        EmailSettings EmailSettings { get; }

        DbSettings DbSettings { get; }

        JwtAuthorizationSettings JwtAuthorizationSettings { get; }
        JwtForgotPasswordSettings JwtForgotPasswordSettings { get; }

        PasswordSettings PasswordSettings { get; }

        WebSettings WebSettings { get; }
     
        FileUploadSettings FileUploadSettings { get; }

    }
}
