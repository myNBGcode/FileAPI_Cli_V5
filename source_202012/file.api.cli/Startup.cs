using AutoMapper;
using FileapiCli.CommandHandlers;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace FileapiCli
{
    public class Startup
    {
        public IConfigurationRoot ConfigureServices(IServiceCollection services, IConfigurationRoot Configuration, string envName)
        {

            services.AddSingleton(LoggerFactory.Create(builder => { builder.AddSerilog(dispose: true); }));

            services.AddSingleton(Configuration);

            services.AddSingleton(GetUserInfo(Configuration));

            services.AddSingleton(GetDefaultOptions(Configuration));

            services.AddSingleton(GetAppSettings(Configuration));

            services.AddSingleton(envName);

            services.AddAutoMapper(typeof(InitiateUploadHandler).Assembly);

            services.AddScoped<ICliService, CliService>();

            services.AddScoped<IFileService, FileService>();

            // Register Command Services
            AddCommandServices(services);
            // Register validators
            AddCommandValidationServices(services);

            services.AddTransient<FileCliInvoker>();
            services.AddTransient<MassPaymentInvoker>();
            services.AddTransient<EthnoFilesInvoker>();

            services.AddTransient<MassTransfersInvoker>();

            return Configuration;
        }

        private AppSettingsOptions GetAppSettings(IConfigurationRoot Configuration)
        {
            var appSettings = new AppSettingsOptions();
            Configuration.Bind(appSettings);

            // decrypt the encrypted password from the appsettings.json
            if (appSettings.Safe_password)
            {
                var decryptedPassword = CliEncryption.DecryptCipherTextToPlainText(appSettings.Password);
                appSettings.Password = decryptedPassword;
            }

            return appSettings;
        }

        private void AddCommandServices(IServiceCollection serviceCollection)
        {
            Assembly assembly = typeof(InitiateUploadHandler).Assembly;

            var mappings =
                from type in assembly.GetTypes()
                where !type.IsAbstract
                where !type.IsGenericType
                from i in type.GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)
                select new {service = i, implementation = type};

            foreach (var mapping in mappings)
            {
                Type commandType = mapping.service.GetGenericArguments()[0];
                serviceCollection.AddTransient(mapping.service, mapping.implementation);
            }
        }

        private void AddCommandValidationServices(IServiceCollection serviceCollection)
        {
            Assembly assembly = typeof(InitiateUploadHandler).Assembly;

            var mappings =
                from type in assembly.GetTypes()
                where !type.IsAbstract
                where !type.IsGenericType
                from i in type.GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(ICommandValidator<>)
                select new {service = i, implementation = type};

            foreach (var mapping in mappings)
            {
                Type commandType = mapping.service.GetGenericArguments()[0];
                serviceCollection.AddTransient(mapping.service, mapping.implementation);
            }
        }

        private DefaultOptions GetDefaultOptions(IConfigurationRoot Configuration)
        {
            var defaultOptions = new DefaultOptions();
            Configuration.GetSection("DefaultOptions").Bind(defaultOptions);
            return defaultOptions;
        }

        private IUserInfo GetUserInfo(IConfigurationRoot Configuration)
        {
            var userInfo = new UserInfo()
            {
                UserName = Configuration["appusername"],
                Registry = Configuration["Registry"],
                SubjectUser = Configuration["SubjectUser"],
                SubjectRegistry = Configuration["SubjectRegistry"]
            };
            if (string.IsNullOrEmpty(userInfo.UserName))
            {
                throw new ArgumentException("AppUserName cannot be null. Please configure in appsettings.json");
            }

            if (!string.IsNullOrEmpty(userInfo.SubjectUser))
                return userInfo;

            userInfo.SubjectUser = userInfo.UserName;
            userInfo.SubjectRegistry = userInfo.Registry;

            return userInfo;
        }

       
    }
}

