using ethnofiles.types;
using ethnofiles.validations.types;
using file.types;
using RestSharp;
using System;
using proxy.types;
using tpp.types;

namespace FileapiCli
{
    public partial interface IFileService
    {
        /// <summary>
        /// POST : files/upload-initiation
        /// </summary>
        /// <param name="request">body</param>
        UploadedFile InitiateUpload(UploadInitiationRequest request);
        /// <summary>
        /// PUT : files/{fileId}/upload
        /// </summary>
        /// <param name="request">body</param>
        /// <param name="fileId">route</param>
        IRestResponse UploadChunk(UploadRequest request, Guid? fileId);
        /// <summary>
        /// GET : files/{fileId}
        /// </summary>
        /// <param name="requester">header</param>
        /// <param name="subject">header</param>
        /// <param name="fileId">route</param>
        FileDetails GetFile(string requester, string subject, Guid? fileId);
        /// <summary>
        /// GET : files/{fileId}/{chunkPart}
        /// </summary>
        /// <param name="chunkPart">route</param>
        /// <param name="requester">header</param>
        /// <param name="subject">header</param>
        /// <param name="fileId">route</param>
        FileContent DownloadFile(int chunkPart, string requester, string subject, Guid? fileId);
        ValidateMassPaymentsFileResponse ValidateMassPaymentFile(ValidateMassPaymentsFileBpRequest request);
        MassPaymentsWithFileResponse MassPaymentWithFile(MassPaymentsWithFileBpRequest request, string requestId);

        /// <summary>
        /// PUT : files/{fileId}
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        IRestResponse UpdateFile(UpdateFileRequest request, Guid? fileId);
        /// <summary>
        /// POST : /populatefiletypes
        /// Call to ethnofiles api.
        /// </summary>
        /// <param name="populateFileTyesRequest"></param>
        /// <returns></returns>
        PopulateFiletypesResponse PopulateFileTypes(PopulateFiletypesRequest populateFileTyesRequest);
        /// <summary>
        /// POST : /preparefile
        /// Call to ethnofiles api.
        /// </summary>
        /// <param name="populateFileTyesRequest"></param>
        /// <returns></returns>
        ConvertToXmlResponse PrepareFile(PrepareFileRequest prepareFileRequest);
        /// <summary>
        /// POST : /sendfiletoethnofiles
        /// Call to ethnofiles api.
        /// </summary>
        /// <param name="sendFileToEthnofilesRequest"></param>
        /// <returns></returns>
        UpdateXactionIdResponse SendFileToEthnofiles(SendFileToEthnofilesRequest sendFileToEthnofilesRequest);
        void ValidateEthnofilesFilename(ValidateFilenameRequest request);
        void ValidateEthnofilesFile(ValidateFileRequest request);

        public string GetAccessToken(string scope);

        GenerateMassPaymentsSampleFileResponse GenerateMassPaymentSampleFile(GenerateMassPaymentsSampleFileBpRequest request);
        RetrieveMassPaymentsWithFileOutcomeResponse RetrieveMassPaymentsOutcome(RetrieveMassPaymentsOutcomeWithFileBpRequest request);
        RequestIndividualPaymentsStatusWithFileResponse RequestPaymentStatus(RequestIndividualPaymentsStatusWithFileBpRequest request);
        RetrieveIndividualPaymentsStatusWithFileResponse RetrievePaymentStatus(RetrieveIndividualPaymentsStatusWithFileBpRequest request);


    }
}
