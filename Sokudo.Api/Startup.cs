namespace Sokudo.Api
{
    using System;
    using System.Linq;
    using Sokudo.Api.Constants;
    using Boilerplate.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public class Startup : IStartup
    {
        /// <summary>
        /// Gets or sets the application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html
        /// </summary>
        private readonly IConfigurationRoot configuration;

        /// <summary>
        /// The environment the application is running under. This can be Development, Staging or Production by default.
        /// To set the hosting environment on Windows:
        /// 1. On your server, right click 'Computer' or 'My Computer' and click on 'Properties'.
        /// 2. Go to 'Advanced System Settings'.
        /// 3. Click on 'Environment Variables' in the Advanced tab.
        /// 4. Add a new System Variable with the name 'ASPNETCORE_ENVIRONMENT' and value of Production, Staging or
        /// whatever you want. See http://docs.asp.net/en/latest/fundamentals/environments.html
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Gets or sets the port to use for HTTPS. Only used in the development environment.
        /// </summary>
        private readonly int? sslPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development,
        /// Staging or Production by default.</param>
        /// <param name="loggerFactory">The type used to configure the applications logging system.
        /// See http://docs.asp.net/en/latest/fundamentals/logging.html</param>
        public Startup(IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            this.hostingEnvironment = hostingEnvironment;

            this.configuration = new ConfigurationBuilder()
                .SetBasePath(this.hostingEnvironment.ContentRootPath)
                // Add configuration from the appsettings.json file.
                .AddJsonFile("appsettings.json")
                // Add configuration from an optional appsettings.development.json, appsettings.staging.json or
                // appsettings.production.json file, depending on the environment. These settings override the ones in
                // the appsettings.json file.
                .AddJsonFile($"appsettings.{this.hostingEnvironment.EnvironmentName}.json", optional: true)
                // This reads the configuration keys from the secret store. This allows you to store connection strings
                // and other sensitive settings, so you don't have to check them into your source control provider.
                // Only use this in Development, it is not intended for Production use. See
                // http://docs.asp.net/en/latest/security/app-secrets.html
                .AddIf(
                    this.hostingEnvironment.IsDevelopment(),
                    x => x.AddUserSecrets<Startup>())
                // Add configuration specific to the Development, Staging or Production environments. This config can
                // be stored on the machine being deployed to or if you are using Azure, in the cloud. These settings
                // override the ones in all of the above config files.
                // Note: To set environment variables for debugging navigate to:
                // Project Properties -> Debug Tab -> Environment Variables
                // Note: To get environment variables for the machine use the following command in PowerShell:
                // [System.Environment]::GetEnvironmentVariable("[VARIABLE_NAME]", [System.EnvironmentVariableTarget]::Machine)
                // Note: To set environment variables for the machine use the following command in PowerShell:
                // [System.Environment]::SetEnvironmentVariable("[VARIABLE_NAME]", "[VARIABLE_VALUE]", [System.EnvironmentVariableTarget]::Machine)
                // Note: Environment variables use a colon separator e.g. You can override the site title by creating a
                // variable named AppSettings:SiteTitle. See http://docs.asp.net/en/latest/security/app-secrets.html
                .AddEnvironmentVariables()
                .Build();

            loggerFactory
                // Log to Serilog (A great logging framework). See https://github.com/serilog/serilog-framework-logging.
                // .AddSerilog()
                // Log to the console and Visual Studio debug window if in development mode.
                .AddIf(
                    this.hostingEnvironment.IsDevelopment(),
                    x => x
                        .AddConsole(this.configuration.GetSection("Logging"))
                        .AddDebug());

            if (this.hostingEnvironment.IsDevelopment())
            {
                var launchConfiguration = new ConfigurationBuilder()
                    .SetBasePath(this.hostingEnvironment.ContentRootPath)
                    .AddJsonFile(@"Properties\launchSettings.json")
                    .Build();
                this.sslPort = launchConfiguration.GetValue<int>("iisSettings:iisExpress:sslPort");
            }
        }

        /// <summary>
        /// Configures the services to add to the ASP.NET Core Injection of Control (IoC) container. This method gets
        /// called by the ASP.NET runtime. See
        /// http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services) =>
            services
                .AddCaching()
                .AddCustomOptions(this.configuration)
                .AddCustomRouting()
                .AddResponseCaching()
                .AddCustomResponseCompression(this.configuration)
                .AddSwagger()
                // Add useful interface for accessing the ActionContext outside a controller.
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                // Add useful interface for accessing the HttpContext outside a controller.
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                // Add useful interface for accessing the IUrlHelper outside a controller.
                .AddScoped<IUrlHelper>(x => x
                    .GetRequiredService<IUrlHelperFactory>()
                    .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext))
                .AddCustomVersioning()
                .AddMvcCore()
                .AddApiExplorer()
                .AddAuthorization()
                .AddFormatterMappings()
                .AddDataAnnotations()
                .AddJsonFormatters()
                .AddCustomJsonOptions()
                .AddCustomCors()
                .AddVersionedApiExplorer()
                .AddCustomMvcOptions(this.configuration, this.hostingEnvironment)
                .Services
                .AddCommands()
                .AddRepositories()
                .AddServices()
                .AddTranslators()
                .BuildServiceProvider();

        /// <summary>
        /// Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is
        /// called by the ASP.NET runtime.
        /// </summary>
        public void Configure(IApplicationBuilder application) =>
            application
                // Require HTTPS to be used across the whole site. Also set a custom port to use for SSL in
                // Development. The port number to use is taken from the launchSettings.json file which Visual
                // Studio uses to start the application.
                .UseRewriter(
                    new RewriteOptions().AddRedirectToHttps(StatusCodes.Status301MovedPermanently, this.sslPort))
                .UseResponseCaching()
                .UseResponseCompression()
                .UseStaticFilesWithCacheControl(this.configuration)
                .UseCors(CorsPolicyName.AllowAny)
                .UseIf(
                    this.hostingEnvironment.IsDevelopment(),
                    x => x
                        .UseDebugging()
                        .UseDeveloperErrorPages())
                .UseStrictTransportSecurityHttpHeader()
                .UseMvc()
                .UseApiVersioning()
                .UseSwagger()
                .UseSwaggerUI(
                    options =>
                    {
                        var provider = application.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                        foreach (var apiVersionDescription in provider
                            .ApiVersionDescriptions
                            .OrderByDescending(x => x.ApiVersion))
                        {
                            options.SwaggerEndpoint(
                                $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                                $"Version {apiVersionDescription.ApiVersion}");
                        }
                    });
    }
}