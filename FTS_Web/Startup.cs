using AutoMapper;
using Cryptography;
using FTS.Business.Extensions;
using FTS.Configuration;
using FTS.Email;
using GlobalException;
using JwtManager;
using Logger;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Web;
using NLog.Extensions.Logging;
using Mapper;
using DNTCaptcha.Core;
using Email;
using FTS.Configuration.Options;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;
//using FastReport.Data;
using Microsoft.AspNetCore.Http.Features;
using NLog;
//using Rotativa.AspNetCore;
using Newtonsoft.Json.Converters;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using FTS.Model.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;

using System.Reflection;
using FormHelper;

namespace FTS_Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            //LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
            BuildRootPath();
        }

        public IConfiguration Configuration { get; }

        #region Configure services
        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAutoMapper(services);
            ConfigureCookiePolicy(services);
            ConfigureDependencyInjection(services);
            ConfigureIdentity(services);
            ConfigureJwt(services);
            ConfigureCors(services);

            services.ConfigureConfigurationOptions(Configuration);
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddControllersWithViews(options => options.SuppressOutputFormatterBuffering = true);
            services.AddRazorPages();
            services.AddDNTCaptcha(option => option.UseCookieStorageProvider().ShowThousandsSeparators(false).WithEncryptionKey("123456"));

            //FastReport.Utils.RegisteredObjects.AddConnection(typeof(MySqlDataConnection));
            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation()
                    .AddFormHelper()
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    });
            services.AddTransient<IValidator<inspectionModel>, inspectionModelValidator>();

            services.AddMvc();
            services.AddMvc(options =>
            {
                options.MaxModelBindingCollectionSize = int.MaxValue;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers();
            services.Configure<FormOptions>(options =>
            {
                // Set the limit to 128 MB
                options.MultipartBodyLengthLimit = 134217728;
                options.ValueLengthLimit = 134217728;
                options.MemoryBufferThreshold = Int32.MaxValue;
            });

            services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
            options.JsonSerializerOptions
            .PropertyNamingPolicy = null;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers()
             .AddNewtonsoftJson(jsonOptions =>
             {
                 jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
             });

            services.AddControllers()
               .AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });


            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");




            //services.AddAntiforgery(option => option.Cookie.Name = "MyAntiforgeryCookie");
            //services.AddSwaggerGen(c =>
            //{ 
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DMS API", Version = "v1" });
            //    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            //    c.OperationFilter<SecurityRequirementsOperationFilter>();
            //    c.AddSecurityDefinition("Jwt token", new OpenApiSecurityScheme
            //    {
            //        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
            //        In = ParameterLocation.Header,
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey
            //    });
            //});
            // services.AddApplicationInsightsTelemetry();

            // In production, the Angular files will be served from this directory         
        }

        /// <summary>Configures the JWT settings.</summary>
        /// <param name="services">The services.</param>
        private void ConfigureJwt(IServiceCollection services)
        {




            var jwtSettings = Configuration.GetSection("JwtAuthorizationSettings");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config =>
            {
                config.CookieManager = new ChunkingCookieManager();
                config.SlidingExpiration = true;
                config.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(jwtSettings["ExpirationTimeInMinute"]));
                config.Cookie.HttpOnly = true;
                config.Cookie.SameSite = SameSiteMode.None;
                config.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                    };
                });
            services.AddTransient<ITokenGenerator, TokenGenerator>();



            //services.ConfigureJwt(jwtSettings["Issuer"], jwtSettings["Audience"], jwtSettings["SecretKey"]);
        }
        /// <summary>Configures the cookie policy.</summary>
        /// <param name="services">The services.</param>
        private void ConfigureCookiePolicy(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;

            });
        }


        /// <summary>Configures the dependency injection.</summary>
        /// <param name="services">The services.</param>
        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            #region Cryptography
            services.AddTransient<ICipher, Cipher>();
            #endregion
            #region Logger
            services.AddTransient<Logger.ILoggerFactory, Logger.LoggerFactory>();
            //services.AddTransient<ILogging, Logging>();
            services.AddScoped<ILogging>(_ => new Logging("Test"));

            #endregion

            services.ConfigureDependencyInjection(Configuration);
        }

        /// <summary>Configures the cors.</summary>
        /// <param name="services">The services.</param>
        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",

                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        /// <summary>Configures the identity.</summary>
        /// <param name="services">The services.</param>
        private void ConfigureIdentity(IServiceCollection services)
        {
            #region Identity

            //services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            #endregion           
            #region Session
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(Convert.ToInt32(Configuration["Authorization:SessionIdleTimeout"]));
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            #endregion
            //LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            #region Identity options
            services.Configure<IdentityOptions>(configureOptions =>
            {
                // Password settings.
                configureOptions.Password.RequireDigit = true;
                configureOptions.Password.RequiredLength = 8;
                configureOptions.Password.RequireNonAlphanumeric = true;
                configureOptions.Password.RequireUppercase = false;
                configureOptions.Password.RequireLowercase = true;

                // Lockout settings.
                configureOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(Configuration["Authorization:DefaultLockoutTime"]));
                configureOptions.Lockout.MaxFailedAccessAttempts = Convert.ToInt32(Configuration["Authorization:AccessFailedCount"]);
                configureOptions.Lockout.AllowedForNewUsers = true;

                // User settings.            
                configureOptions.User.RequireUniqueEmail = true;
            });
            #endregion
        }


        #endregion
        #region Configure env
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>Configures the specified application.</summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, IHostingEnvironment env1)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            var path = Directory.GetCurrentDirectory();
            //loggerFactory.AddFile($"{path}\\Logs\\Log.txt");
            // To serve unknown files as json type to resolve payconiq universal linking
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/json"
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.ConfigureCustomExceptionMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseStaticFiles();
            app.UseCookiePolicy();
           
            //app.UseFastReport();


            if (env.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI(c =>
                //{
                //    c.RoutePrefix = "";
                //    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                //    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "DMS API");
                //});
            }

            // add NLog
            loggerFactory.AddNLog();
            // app.AddNLogWeb();
            // NLogBuilder.ConfigureNLog("NLog.config");           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //RotativaConfiguration.Setup(env1);
            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();         

            //app.UseEndpoints(endpoints =>
            //{                
            //    endpoints.MapRazorPages();
            //});
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' cdn.jsdelivr.net;");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                context.Response.Headers.Add("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                await next();
            });

        }
        #endregion        

        #region Private Method

        private void BuildRootPath()
        {
            EmailSenderExtensions.RootPath = @_env.WebRootPath.ToString();
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            // AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperModelConfiguration());
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
        #endregion
    }
}
