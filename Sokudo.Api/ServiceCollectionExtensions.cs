namespace Sokudo.Api
{
    using System;
    using System.IO.Compression;
    using System.Linq;
    using System.Reflection;
    using Sokudo.Api.Constants;
    using Sokudo.Api.OperationFilters;
    using Sokudo.Api.Repositories;
    using Sokudo.Api.Services;
    using Sokudo.Api.Settings;
    using Sokudo.Api.ViewModels;
    using Boilerplate;
    using Boilerplate.AspNetCore;
    using Boilerplate.AspNetCore.Filters;
    using Boilerplate.AspNetCore.Swagger;
    using Boilerplate.AspNetCore.Swagger.OperationFilters;
    using Boilerplate.AspNetCore.Swagger.SchemaFilters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Swashbuckle.AspNetCore.Swagger;
    using Sokudo.Email.Options;
    using Sokudo.Email.Service;
    using Newtonsoft.Json.Serialization;
    using Sokudo.Service.Transport;
    using Sokudo.Domain.Transport;
    using AspNetCoreIdentityBoilerplate.Service;
    using Sokudo.DataAccess.Repository.Transport;
    using Sokudo.DataAccess.UnitOfWork;
    using AspNetCoreIdentityBoilerplate.UnitOfWork;

    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures caching for the application. Registers the <see cref="IDistributedCache"/> and
        /// <see cref="IMemoryCache"/> types with the services collection or IoC container. The
        /// <see cref="IDistributedCache"/> is intended to be used in cloud hosted scenarios where there is a shared
        /// cache, which is shared between multiple instances of the application. Use the <see cref="IMemoryCache"/>
        /// otherwise.
        /// </summary>
        public static IServiceCollection AddCaching(this IServiceCollection services) =>
            services
                // Adds IMemoryCache which is a simple in-memory cache.
                .AddMemoryCache()
                // Adds IDistributedCache which is a distributed cache shared between multiple servers. This adds a
                // default implementation of IDistributedCache which is not distributed. See below:
                .AddDistributedMemoryCache();
        // Uncomment the following line to use the Redis implementation of IDistributedCache. This will
        // override any previously registered IDistributedCache service.
        // Redis is a very fast cache provider and the recommended distributed cache provider.
        // .AddDistributedRedisCache(
        //     options =>
        //     {
        //     });
        // Uncomment the following line to use the Microsoft SQL Server implementation of IDistributedCache.
        // Note that this would require setting up the session state database.
        // Redis is the preferred cache implementation but you can use SQL Server if you don't have an alternative.
        // .AddSqlServerCache(
        //     x =>
        //     {
        //         x.ConnectionString = "Server=.;Database=ASPNET5SessionState;Trusted_Connection=True;";
        //         x.SchemaName = "dbo";
        //         x.TableName = "Sessions";
        //     });

        /// <summary>
        /// Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
        /// Objects (POCO) and adding <see cref="IOptions{T}"/> objects to the services collection.
        /// </summary>
        public static IServiceCollection AddCustomOptions(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                // Adds IOptions<CacheProfileSettings> to the services container.
                .Configure<CacheProfileSettings>(configuration.GetSection(nameof(CacheProfileSettings)))
                .Configure<EmailAuthOptions>(configuration.GetSection(nameof(EmailAuthOptions)))
                .Configure<HostSettings>(configuration.GetSection(nameof(HostSettings)))
                .Configure<EmailConfirmationSettings>(configuration.GetSection(nameof(EmailConfirmationSettings)));

        /// <summary>
        /// Adds response compression to enable GZIP compression of responses.
        /// </summary>
        public static IServiceCollection AddCustomResponseCompression(
            this IServiceCollection services,
            IConfigurationRoot configuration) =>
            services
                .AddResponseCompression(
                    options =>
                    {
                        // Enable response compression over HTTPS connections.
                        options.EnableForHttps = true;
                        // Add additional MIME types (other than the built in defaults) to enable GZIP compression for.
                        var responseCompressionSettings = configuration.GetSection<ResponseCompressionSettings>(
                            nameof(ResponseCompressionSettings));
                        options.MimeTypes = ResponseCompressionDefaults
                            .MimeTypes
                            .Concat(responseCompressionSettings.MimeTypes);
                    })
                .Configure<GzipCompressionProviderOptions>(
                    options => options.Level = CompressionLevel.Optimal);

        /// <summary>
        /// Add custom routing settings which determines how URL's are generated.
        /// </summary>
        public static IServiceCollection AddCustomRouting(this IServiceCollection services) =>
            services.AddRouting(
                options =>
                {
                    // Improve SEO by stopping duplicate URL's due to case differences or trailing slashes.
                    // See http://googlewebmastercentral.blogspot.co.uk/2010/04/to-slash-or-not-to-slash.html
                    // All generated URL's should append a trailing slash.
                    options.AppendTrailingSlash = true;
                    // All generated URL's should be lower-case.
                    options.LowercaseUrls = true;
                });

        public static IServiceCollection AddCustomVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });

        public static IMvcCoreBuilder AddCustomMvcOptions(
            this IMvcCoreBuilder builder,
            IConfigurationRoot configuration,
            IHostingEnvironment hostingEnvironment) =>
            builder.AddMvcOptions(
                options =>
                {
                    // Controls how controller actions cache content from the appsettings.json file.
                    var cacheProfileSettings = configuration.GetSection<CacheProfileSettings>();
                    foreach (var keyValuePair in cacheProfileSettings.CacheProfiles)
                    {
                        options.CacheProfiles.Add(keyValuePair);
                    }

                    if (hostingEnvironment.IsDevelopment())
                    {
                        // Lets you pass a format parameter into the query string to set the response type:
                        // e.g. ?format=application/json. Good for debugging.
                        options.Filters.Add(new FormatFilterAttribute());
                    }

                    // Check model state for null or invalid models and automatically return a 400 Bad Request.
                    options.Filters.Add(new ValidateModelStateAttribute());

                    // Remove string and stream output formatters. These are not useful for an API serving JSON or XML.
                    options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    // Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
                    options.ReturnHttpNotAcceptable = true;
                });

        /// <summary>
        /// Adds customized JSON serializer settings.
        /// </summary>
        public static IMvcCoreBuilder AddCustomJsonOptions(this IMvcCoreBuilder builder) =>
            builder.AddJsonOptions(
                options =>
                {
                    // Parse dates as DateTimeOffset values by default. You should prefer using DateTimeOffset over
                    // DateTime everywhere. Not doing so can cause problems with time-zones.
                    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    // Output enumeration values as strings in JSON.
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

        /// <summary>
        /// Add cross-origin resource sharing (CORS) services and configures named CORS policies. See
        /// https://docs.asp.net/en/latest/security/cors.html
        /// </summary>
        public static IMvcCoreBuilder AddCustomCors(this IMvcCoreBuilder builder) =>
            builder.AddCors(
                options =>
                {
                    // Create named CORS policies here which you can consume using application.UseCors("PolicyName")
                    // or a [EnableCors("PolicyName")] attribute on your controller or action.
                    options.AddPolicy(
                        CorsPolicyName.AllowAny,
                        x => x
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
                });

        /// <summary>
        /// Adds Swagger services and configures the Swagger services.
        /// </summary>
        public static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(
                options =>
                {
                    var assembly = typeof(Startup).GetTypeInfo().Assembly;
                    var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                    var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

                    options.DescribeAllEnumsAsStrings();
                    options.DescribeAllParametersInCamelCase();
                    options.DescribeStringEnumsInCamelCase();

                    // Add the XML comment file for this assembly, so it's contents can be displayed.
                    options.IncludeXmlCommentsIfExists(assembly);

                    options.OperationFilter<ApiVersionOperationFilter>();
                    // Show an example model for JsonPatchDocument<T>.
                    options.SchemaFilter<JsonPatchDocumentSchemaFilter>();
                    // Show an example model for ModelStateDictionary.
                    options.SchemaFilter<ModelStateDictionarySchemaFilter>();

                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                    {
                        var info = new Info()
                        {
                            Title = $"{assemblyProduct} (Version {apiVersionDescription.ApiVersion})",
                            Description = apiVersionDescription.IsDeprecated ?
                                $"{assemblyDescription} This API version has been deprecated." :
                                assemblyDescription,
                            Version = apiVersionDescription.ApiVersion.ToString()
                        };
                        options.SwaggerDoc(apiVersionDescription.GroupName, info);
                    }
                });

        /// <summary>
        /// Adds project repositories.
        /// </summary>
        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddScoped<IUnitOfWork, UnitOfWork>();
                //.AddScoped<ICarRepository, CarRepository>()
                //.AddScoped<ITransportDefinitionRepository, TransportDefinitionRepository>();

        /// <summary>
        /// Adds project services.
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .AddSingleton<IClockService, ClockService>()
                .AddSingleton<IEmailSender, EmailSender>()
                .AddSingleton<IEmailService, EmailService>()
                .AddScoped<ITransportDefinitionService, TransportDefinitionService>()
                .AddScoped<ITransportManufacturerService, TransportManufacturerService>()
                .AddScoped<ITransportModelService, TransportModelService>();
    }
}
