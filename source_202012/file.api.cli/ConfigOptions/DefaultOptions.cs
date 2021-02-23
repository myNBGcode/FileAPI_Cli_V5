using System;

namespace FileapiCli.ConfigOptions
{
    public class DefaultOptions : IOptions
    {
        public UploadOptions uploadOptions { get; set; }
        public DownloadOptions downloadOptions { get; set; }
        public AddUserTagsOptions addUserTagsOptions { get; set; }
        public RemoveUserTagOptions RemoveUserTagsOptions { get; set; }
        public SendToEthnoFilesOptions sendToEthnoFilesOptions { get; set; }
        public ProcessEnthofilesFileOptions ProcessEnthofilesFileOptions { get; set; }
        public SampleMassPaymentOption sampleMassPaymentOption { get; set; }
        public MassPaymentOption massPaymentOption { get; set; }
        public MassPaymentOutcomeOption massPaymentOutcomeOption { get; set; }
        public RequestPaymentStatusOption RequestPaymentStatusOption { get; set; }
        public RetrievePaymentStatusOption RetrievePaymentStatusOption { get; set; }

        public SampleMassTransfersOption SampleMassTransfersOption { get; set; }
        public MassTransfersCreditOutcomeOption MassTransfersCreditOutcomeOption { get; set; }
        public MassTransfersCreditOption MassTransfersCreditOption { get; set; }

        public RetrieveCustomerApplicationsIncomingOptions RetrieveCustomerApplicationsIncomingOptions { get; set; }
        public RetrieveCustomerApplicationsOutgoingOptions RetrieveCustomerApplicationsOutgoingOptions { get; set; }
        public DownloadFilesOutgoingOptions DownloadFilesOutgoingOptions { get; set; }
        public RetrieveFilesOutgoingOptions RetrieveFilesOutgoingOptions { get; set; }
        public RetrieveFilesOutgoingHistoricOptions RetrieveFilesOutgoingHistoricOptions { get; set; }
        public RetrieveFileOutgoingOptions RetrieveFileOutgoingOptions { get; set; }
        public DownloadFilesIncomingOptions DownloadFilesIncomingOptions { get; set; }
        public RetrieveFilesIncomingOptions RetrieveFilesIncomingOptions { get; set; }
        public RetrieveFilesIncomingHistoricOptions RetrieveFilesIncomingHistoricOptions { get; set; }
        public RetrieveFileIncomingOptions RetrieveFileIncomingOptions { get; set; }
        public SendEthnofilesOptions SendEthnofilesOptions { get; set; }

    }
}
