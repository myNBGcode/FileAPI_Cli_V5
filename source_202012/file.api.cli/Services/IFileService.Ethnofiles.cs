using proxy.types;

namespace FileapiCli
{
    public partial interface IFileService
    {
        RetrieveCustomerApplicationsResponse RetrieveCustomerApplications(RetrieveCustomerApplicationsRequest request);
        RetrieveFileResponse RetrieveFile(RetrieveFileRequest request);
        RetrieveFileListResponse RetrieveFileList(RetrieveFileListRequest request);
        SendFileResponse SendFile(SendFileRequest request);
        SepaConvertResponse SepaConvert(SepaConvertRequest request);
        bool SepaSetFileStatusAsSent(SepaSetFileStatusAsSentRequest request);

    }
}
