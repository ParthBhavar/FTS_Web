using FTS.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FTS.Configuration
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureConfigurationOptions(this IServiceCollection services,IConfiguration configuration)
        {           
            services.Configure<EmailSettings>(option => configuration.GetSection("EmailSettings").Bind(option));
            services.Configure<AuthorizationSettings>(option => configuration.GetSection("Authorization").Bind(option));
            services.Configure<DbSettings>(option => configuration.GetSection("ConnectionStrings").Bind(option));
            services.Configure<JwtAuthorizationSettings>(option => configuration.GetSection("JwtAuthorizationSettings").Bind(option));
            services.Configure<JwtForgotPasswordSettings>(option => configuration.GetSection("JwtForgotPasswordSettings").Bind(option));
            services.Configure<PasswordSettings>(option => configuration.GetSection("PasswordSettings").Bind(option));
            services.Configure<WebSettings>(option => configuration.GetSection("WebSettings").Bind(option));           
            services.Configure<FileUploadSettings>(option => configuration.GetSection("FileUploadSettings").Bind(option));            
            return services;
        }
    }
}
