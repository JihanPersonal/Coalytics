using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Coalytics.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using BackendServiceDispatcher.Services;
using BackendServiceDispatcher.Extensions;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

// TODO finish autofac impl
using Autofac;
using Coalytics.Models.Auth;
using Coalytics.Contracts.Auth;

namespace BackendServiceDispatcher
{
    /// <summary>
    /// Define startup behavior for BackendServiceDispatcher
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Config, as set by Autofac build
        /// </summary>
        public IConfigurationRoot Configuration { get; private set; }

        /// <summary>
        /// Get the host environment 
        /// </summary>
        public IHostingEnvironment HostingEnvironment { get; }

        /// <summary>
        /// Create new Startup instance
        /// </summary>
        /// <param name="env">Web hosting env config</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        public void ConfigureServices(IServiceCollection services)
        {

            string connectionString;
            string azureStorageConnectionstring;

            if (HostingEnvironment.IsDevelopment())
            {
                //connectionString = "DevelopmentSqliteConnection";
                connectionString = "DevelopmentSqlConnection";
                azureStorageConnectionstring = "DevelopmentAzureStorageConnection";

                services.AddCors(options =>
                options.AddPolicy("AllowAll",
                    builder =>
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader()
                    ));
            }
            else
            {
                connectionString = "ProductionSqlServerConnection";
                azureStorageConnectionstring = "ProductionAzureStorageConnection";

            }


            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString(connectionString))
                        //options.UseSqlite(Configuration.GetConnectionString(connectionString))
                        );
            services.AddScoped<ICoalyticsRepository, CoalyticsRepository>();
            services.AddSingleton(CloudStorageAccount.Parse(Configuration.GetConnectionString(azureStorageConnectionstring)));
            services.AddTransient<IBlobUploader, BlobUploader>(s => new BlobUploader(services.BuildServiceProvider().GetService<CloudStorageAccount>()));
            services.AddTransient<IBlobDownloader, BlobDownloader>(s => new BlobDownloader(services.BuildServiceProvider().GetService<CloudStorageAccount>()));
            services.AddTransient<IBlobDeleter, BlobDeleter>(s => new BlobDeleter(services.BuildServiceProvider().GetService<CloudStorageAccount>()));
            services
                .AddIdentity<CoalyticsUser, IdentityRole>(config =>
                {
                    config.Tokens.EmailConfirmationTokenProvider = "EmailConfirmTokenProvider";
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequiredLength = 8;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<CoalyticsUser>>("EmailConfirmTokenProvider");

            #region Set Token Life Span 
            double resetPassowrdTokenLife = Double.Parse(Configuration["TokenLifeSpan:ResetPassword"]);
            services.Configure<DataProtectionTokenProviderOptions>(options =>options.TokenLifespan = TimeSpan.FromHours(resetPassowrdTokenLife));
            double emailTokenLife= Double.Parse(Configuration["TokenLifeSpan:EmailConfirmation"]);
            services.Configure<EmailConfirmationTokenProviderOptions>(options =>options.TokenLifespan = TimeSpan.FromDays(emailTokenLife));
            #endregion Set Token Expire Time

            //services.AddAuthentication()
            //    .AddExternalAuth(new ExternalAuthCredential(CredentialType.FACEBOOK, Configuration["facebookAuthApiAppId"], Configuration["facebookAuthApiAppSecret"]))
            //    .AddExternalAuth(new ExternalAuthCredential(CredentialType.GOOGLE, Configuration["googleAuthApiClientId"], Configuration["googleAuthApiClientSecret"]))
            //    .AddExternalAuth(new ExternalAuthCredential(CredentialType.MICROSOFT, Configuration["microsoftAuthApiClientId"], Configuration["microsoftAuthApiClientSecret"]))
            //    .AddExternalAuth(new ExternalAuthCredential(CredentialType.TWITTER, Configuration["twitterAuthApiConsumerId"], Configuration["twitterAuthApiConsumerSecret"]));

            #region Add OAUTH Services
            //Google. For the Google ID and Google Key, please reference READ.md
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Environment.GetEnvironmentVariable("GOOGLEID");
                googleOptions.ClientSecret = Environment.GetEnvironmentVariable("GOOGLEKEY");
            });

            //Microsoft. For the Microsoft ID and Microsoft Key, please reference READ.md
            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Environment.GetEnvironmentVariable("MICROSOFTID");
                microsoftOptions.ClientSecret = Environment.GetEnvironmentVariable("MICROSOFTKEY");
            });

            //Twitter. For the Twitter ID and Twitter Key, please reference READ.md
            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = Environment.GetEnvironmentVariable("TWITTERID");
                twitterOptions.ConsumerSecret = Environment.GetEnvironmentVariable("TWITTERKEY");
            });

            ////Facebook. For the Facebook ID and Facebook Key, please reference READ.md
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Environment.GetEnvironmentVariable("FACEBOOKID");
                facebookOptions.AppSecret = Environment.GetEnvironmentVariable("FACEBOOKKEY");
            });
            #endregion Add OAUTH Servicecs

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISocialMediaScanner, SocialMediaScanner>();
            services
                .AddMvc()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            services.AddSingleton(Configuration);
            services.AddTransient<IServiceProvider>(instance => services.BuildServiceProvider());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Coalytics API",
                        Version = "v1",
                        Description = "APIs for Coalytics",
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Bomo Shen", Email = "sxxxxxxx@gmail.com", Url = "https://a.com/" },
                        License = new License { Name = "None", Url = "https://license.com/" }
                    });
                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "BackendServiceDispatcher.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">ApplicationBuilder instance</param>
        /// <param name="env">Hosting env config</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Order matters when registering middlewares
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            app.UseAuthentication();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/docs/{documentName}/apis.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/docs/v1/apis.json", "Coalytics API");
                c.RoutePrefix = "api/docs";
            });
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
