using ethnofiles.types;
using ethnofiles.validations.types;
using file.types;
using proxy.types;
using System;

namespace FileapiCli
{
    public interface ICliService
    {
        public void SetPassword(string password);
        public FileDetails GetFile(Guid fileId, string requester, string subject);
        public UploadedFile InitiateUpload(UploadInitiationRequest request);
        public void UploadFile(string inputFile, UploadRequest uploadRequest, Guid fileId, int chunkSize, int? totalChunks);
        void DownloadFile(Guid fileId, string outputFilePath, string requester, string subject);
        void DownloadFile(Guid fileId, string outputFilePath, string requester, string subject, string prefix);
        byte[] GetFileContent(Guid fileId, string requester, string subject);
        void AddUserTags(Guid fileId, UpdateFileRequest request);
        void RemoveUserTags(Guid fileId, UpdateFileRequest request, string[] userTagsToRemove);

        void SendToEthnofiles(SendFileToEthnofilesRequest sendFileToEthnofilesRequest);
        SepaPainFile DeserializeXmlSEPA001ISO20022(string inputFile);
        ConvertToXmlResponse PrepareFile(PrepareFileRequest prepareFileRequest);
        EthnofilesFileTypesResponse PopulateFileTypes(PopulateFiletypesRequest populateFileTyesRequest, string idField);

        EthnofilesFileTypesResponse[] PopulateFileTypes(PopulateFiletypesRequest populateFileTyesRequest);

        void ValidateEthnofilesFilename(ValidateFilenameRequest request);
        void ValidateEthnofilesFile(ValidateFileRequest request);

        CustomerApplication RetrieveCustomerApplication(RetrieveCustomerApplicationsRequest request, int customerApplicationId);
        SendFileResponse SendFile(SendFileRequest request);
        SepaConvertResponse SepaConvert(SepaConvertRequest request);
        bool SepaSetFileStatusAsSent(SepaSetFileStatusAsSentRequest request);
    }
}