using CommandLine;
using CommandLine.Text;
using FileapiCli.ConfigOptions;
using FileapiCli.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileapiCli
{
    class Program
    {
        public static IConfigurationRoot Configuration;

        static int Main(string[] args)
        {
            try
            {
                MainAsync(args);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("An error occured. {0}", ex.Message);
                return 1;
            }
        }
        static int MainAsync(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;

            var parserResult = ConfigureAndRunParser(args);

            if (parserResult.Tag == ParserResultType.NotParsed)
            {
                return -1;
            }
            if (NotParsedArgsExist(args))
            {
                Console.Error.WriteLine(HelpOutput(parserResult));
                return -1;
            }
   
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            DisplayEnvironment(envName);

            try
            {
                Startup startUp = new Startup();
                ServiceCollection services = new ServiceCollection();

                startUp.ConfigureServices(services, Configuration, envName);

                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                };
     
                var options = (parserResult as Parsed<Object>)?.Value;

                Log.Debug("Building service provider");
                IServiceProvider serviceProvider = services.BuildServiceProvider();
                Log.Debug("Starting service");

                //start the program
                Invoker(options, serviceProvider);

                Log.Debug("Ending service");
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "An error has occured.");
                Log.Error("An error has occured:"+ ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
            return 0;
        }

        private static ParserResult<object> ConfigureAndRunParser(string[] args)
        {
            Type[] types =
            {
                typeof(SetPasswordOptions), typeof(UploadOptions), typeof(DownloadOptions),
                typeof(AddUserTagsOptions), typeof(RemoveUserTagOptions),
                typeof(PopulateFileTypesOptions), typeof(SendToEthnoFilesOptions),
                typeof(ProcessEnthofilesFileOptions), typeof(SampleMassPaymentOption),
                typeof(MassPaymentOption), typeof(MassPaymentOutcomeOption),
                typeof(RequestPaymentStatusOption), typeof(RetrievePaymentStatusOption),
                typeof(SampleMassTransfersOption), typeof(MassTransfersCreditOutcomeOption),
                typeof(MassTransfersCreditOption),
                typeof(RetrieveCustomerApplicationsOutgoingOptions),typeof(RetrieveCustomerApplicationsIncomingOptions),
                typeof(RetrieveFileOutgoingOptions),typeof(RetrieveFileIncomingOptions),
                typeof(RetrieveFilesIncomingHistoricOptions),typeof(RetrieveFilesIncomingOptions),
                typeof(RetrieveFilesOutgoingOptions),typeof(RetrieveFilesOutgoingHistoricOptions),
                typeof(DownloadFilesOutgoingOptions),typeof(DownloadFilesIncomingOptions),
                typeof(SendEthnofilesOptions)
            };

            using var parser = new Parser(settings =>
            {
                settings.CaseSensitive = false;
                settings.HelpWriter = Console.Error;
                settings.EnableDashDash = false;
                settings.AutoVersion = true;
                settings.IgnoreUnknownArguments = false;
                
            });

            ParserResult<object> parserResult = parser.ParseArguments(args, types);
            return parserResult;
        }

        /// <summary>
        ///  Display Error for additional validation
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static string HelpOutput(ParserResult<object> result)
        {
            var helpText = new StringBuilder();
            helpText.AppendLine(HelpText.AutoBuild(result, null, null));
          
            helpText.AppendLine("Syntax Error Detected.");
            helpText.AppendLine("An argument with no parameter qualification was found.");
            return helpText.ToString();
        }

        /// <summary>
        ///  Additional Validation to the one provided by the commandlineparser library.
        ///  Flag error is not parsed arguments exist.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static bool NotParsedArgsExist(string[]args)
        {
            if (args.Select(cur => !cur.StartsWith("-") && !cur.StartsWith("--") && !cur.EndsWith(","))
                .Where((notParameter, i) => notParameter && args.Length > i + 1
                                                         && !args[i + 1].StartsWith("-")
                                                         && !args[i + 1].StartsWith("--")).Any()
            ) return true;

            return false;
        }

        /// <summary>
        ///  Run the appropriate invoker based on arguments on input
        /// </summary>
        /// <param name="options"></param>
        /// <param name="serviceProvider"></param>
        private static void Invoker(object options, IServiceProvider serviceProvider)
        {
            switch (options)
            {
                case Version _:
                    Console.WriteLine("FileApiCli Version 1.0.0.0");
                    break;
                case SetPasswordOptions _:
                case UploadOptions _:
                case DownloadOptions _:
                case AddUserTagsOptions _:
                case RemoveUserTagOptions _:
                    serviceProvider.GetService<FileCliInvoker>().Run(options);
                    break;
                case PopulateFileTypesOptions _:
                case ProcessEnthofilesFileOptions _:
                case SendToEthnoFilesOptions _:
                case SendEthnofilesOptions _:
                case RetrieveCustomerApplicationsOutgoingOptions _:
                case RetrieveCustomerApplicationsIncomingOptions _:
                case RetrieveFileOutgoingOptions _:
                case RetrieveFileIncomingOptions _:
                case RetrieveFilesIncomingHistoricOptions _:
                case RetrieveFilesIncomingOptions _:
                case RetrieveFilesOutgoingHistoricOptions _:
                case RetrieveFilesOutgoingOptions _:
                case DownloadFilesOutgoingOptions _:
                case DownloadFilesIncomingOptions _:
                    serviceProvider.GetService<EthnoFilesInvoker>().Run(options);
                    break;

                case SampleMassTransfersOption _:
                case MassTransfersCreditOutcomeOption _:
                case MassTransfersCreditOption _:
                    serviceProvider.GetService<MassTransfersInvoker>().Run(options);
                    break;

                default:
                    serviceProvider.GetService<MassPaymentInvoker>().Run(options);
                    break;
            }
        }

        private static void DisplayEnvironment(string env)
        {
            Console.WriteLine("env" , env);
            if (string.IsNullOrEmpty(env) || env == "null")
            {
                Log.Warning("Environment not set. Taking settings from appsettings.json");
            }
            else
            {
                Log.Information($"Environment is set to:\"{env}\". Using appsettings.{env}.json");
            }
        }

        private static void MapDefaultArguments(object obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var val = prop.GetValue(obj);

                if (!val.IsNullOrEmpty()) continue;

                var defaultVal = Configuration[prop.Name];

                if (defaultVal.IsNullOrEmpty()) continue;

                var safeValue = Helper.GetSafeValue(prop, defaultVal);
                prop.SetValue(obj, safeValue, null);
            }
        }
    }
}