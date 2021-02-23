using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("requeststatus", HelpText = "Request Payment Status for individual payments.")]
    public class RequestPaymentStatusOption : IOptions
    {
        [Option("FileId", Required = false, HelpText = "The id of the file")]
        public string FileId { get; set; }

    }
}
