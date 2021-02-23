using ethnofiles.types;
using ethnofiles.validations.types;
using file.types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using proxy.types;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace FileapiCli
{
    public class CliService : ICliService
    {
        private readonly IFileService _fileService;
        private readonly ILogger<CliService> _logger;

        public CliService(IFileService fileService, ILoggerFactory loggerFactory)
        {
            _fileService = fileService;
            _logger = loggerFactory.CreateLogger<CliService>();
        }

        public void SetPassword(string password)
        {
            var encrypredPassword = CliEncryption.EncryptPlainTextToCipherText(password);
            Helper.UpdateSettingsValue("password", encrypredPassword);
        }


        public UploadedFile InitiateUpload(UploadInitiationRequest request)
        {
            return _fileService.InitiateUpload(request);
        }
        public void UploadFile(string inputFile, UploadRequest uploadRequest, Guid fileId, int chunkSize, int? totalChunks)
        {
            using (Stream input = File.OpenRead(inputFile))
            {
                var index = 1;
                const int BUFFER_SIZE = 20 * 1024;
                var buffer = new byte[BUFFER_SIZE];// use buffer to write to memory stream

                // before hits end of file
                while (input.Position < input.Length)
                {
                    // write a single chunk to memory stream
                    using (var ms = new MemoryStream())
                    {
                        var chunkPart = new List<byte>().ToArray();
                        var remaining = chunkSize;
                        int bytesRead;

                        // use buffer to write to memory stream until the chunk size is reached
                        while (remaining > 0 && (bytesRead = input.Read(buffer, 0, Math.Min(remaining, BUFFER_SIZE))) > 0)
                        {
                            ms.Write(buffer, 0, bytesRead);
                            remaining -= bytesRead;
                        }

                        // create upload chunk request message
                      
                        uploadRequest.ChunkPart = index;
                        uploadRequest.FileContent = ms.ToArray();
                      
                        UploadChunkWithRetry(uploadRequest, fileId, index, totalChunks, 3);
                    }
                    index++;
                }
            }
        }

        private void UploadChunkWithRetry(UploadRequest uploadRequest, Guid fileId, int index, int? totalChunks, int retryAttempts)
        {
            var tries = 0;
            do
            {
                // rest call to upload chunk 
                var response = _fileService.UploadChunk(uploadRequest, fileId);

                if (totalChunks.HasValue)
                {
                    _logger.LogInformation($"Chunk #{index}/{totalChunks}. Response: {response.Content} {(int)response.StatusCode}, {response.StatusCode}");
                    _logger.LogInformation($"Uploading: {(int)((decimal)index / totalChunks * 100)}%");
                }
                if ((int)response.StatusCode == 308 || (int)response.StatusCode == 200) break;

                tries++;
                _logger.LogError("Error uploading Chunk #" + index + ". Retry attempt:" + tries + "/" + retryAttempts + ".");
            } while (tries < retryAttempts);
        }

        public void DownloadFile(Guid fileId, string outputFilePath, string requester, string subject, string prefix)
        {
            // Rest call to Get File
            var fileDetails = _fileService.GetFile($"{requester}", $"{subject}", fileId);

            var fileName = fileDetails.FileName;
            if (!String.IsNullOrEmpty(prefix)) fileName = prefix + fileName;
            if (File.Exists(outputFilePath + fileName)) fileName = $"{DateTime.Now:yyyyMMddHHmmss}_Copy_of_" + fileName;

            _logger.LogInformation($"Downloading file with id: {fileId} as '{fileName}'");
            // open stream to append chunks to output file
            using (var stream = new FileStream($"{outputFilePath}{fileName}", FileMode.Append))
            {
                for (var chunkPart = 1; chunkPart <= fileDetails.TotalChunks; chunkPart++)
                {
                    // rest call to download file chunk part
                    var fileContent = _fileService.DownloadFile(chunkPart, $"{requester}", $"{subject}", fileId);

                    stream.Write(fileContent._FileContent, 0, fileContent._FileContent.Length);

                    _logger.LogInformation($"Downloading: {(int)((decimal)chunkPart / fileDetails.TotalChunks * 100)}%");
                }
            }
            _logger.LogInformation("Download Completed.");
        }

        public void DownloadFile(Guid fileId, string outputFilePath, string requester, string subject)
        {
            DownloadFile(fileId, outputFilePath, requester, subject, null);
        }

        public byte[] GetFileContent(Guid fileId, string requester, string subject)
        {
            // Rest call to Get File
            var fileDetails = _fileService.GetFile($"{requester}", $"{subject}", fileId);

            // open stream to append chunks to output file
            using (var stream = new MemoryStream())
            {
                for (var chunkPart = 1; chunkPart <= fileDetails.TotalChunks; chunkPart++)
                {
                    // rest call to download file chunk part
                    var fileContent = _fileService.DownloadFile(chunkPart, $"{requester}", $"{subject}", fileId);

                    stream.Write(fileContent._FileContent, 0, fileContent._FileContent.Length);
                }
                return stream.ToArray();
            }
        }

        public void AddUserTags(Guid fileId, UpdateFileRequest request)
        {
            Log.Information($"Adding tags to file with id: {fileId} ");

            var fileDetails = _fileService.GetFile($"{request.Requester.Registry}:{request.Requester.Id}", $"{request.Subject.Registry}:{request.Subject.Id}", fileId);
            if (fileDetails == null)
            {
                Log.Warning($"AddUserTags: File Not Found with id {fileId}");
                return;
            }

            request.FileName = fileDetails.FileName;
     
            //remove existing tags
            if (fileDetails.UserTags.Any())
            {
               request.UserTags.RemoveAll(p => fileDetails.UserTags.Contains(p));
            }
            // after removing existing tags, no tags left to add, so return.
            if (!request.UserTags.Any())
            {
                Log.Information($"Tags exist. No additional tags added.");
                return;
            };
            
            request.UserTags.AddRange(fileDetails.UserTags);
            // update file
            _fileService.UpdateFile(request, fileId);
            Log.Information($"Tags added to file. All File Tags: {string.Join(",", request.UserTags)} Completed.");
        }
       
        public void RemoveUserTags(Guid fileId, UpdateFileRequest request, string[] userTagsToRemove)
        {
            Log.Information($"Remove tags from file with id: {fileId} ");

            var fileDetails = _fileService.GetFile($"{request.Requester.Registry}:{request.Requester.Id}", $"{request.Subject.Registry}:{request.Subject.Id}", fileId);
            if (fileDetails == null)
            {
                Log.Warning($"RemoveUserTags: File Not Found with id {fileId}");
                return;
            }

            request.FileName = fileDetails.FileName;
            request.UseFolderId = false;
            if (fileDetails.UserTags != null && fileDetails.UserTags.Any())
            {
                foreach (var userTag in userTagsToRemove)
                    fileDetails.UserTags.Remove(userTag);

                request.UserTags = new List<string>();
                request.UserTags.AddRange(fileDetails.UserTags);

                // update file
                if (!request.UserTags.Any())
                {
                    Log.Warning($"RemoveUserTags: You cannot remove all user tags from the file.");
                    return;
                }

                _fileService.UpdateFile(request, fileId);
                Log.Information($"RemoveUserTags Completed.");
            }
        }

        public void SendToEthnofiles(SendFileToEthnofilesRequest sendFileToEthnofilesRequest)
        {
            Log.Information($"Send to ethnofiles with id: {sendFileToEthnofilesRequest.FileApiFileId} ");

            var updateXactionIdResponse = _fileService.SendFileToEthnofiles(sendFileToEthnofilesRequest);

            if (updateXactionIdResponse.IsDeferred)
                Log.Information($"Succesfully send file with id {sendFileToEthnofilesRequest.FileApiFileId} for Approval. TransactionDate: {updateXactionIdResponse.TransactionDate}");
            else
                Log.Information($"Succesfully send file with id {sendFileToEthnofilesRequest.FileApiFileId} to ethnofiles. TransactionDate: {updateXactionIdResponse.TransactionDate}, XactionId: {updateXactionIdResponse.XactionId}, InboxId: {updateXactionIdResponse.InboxId}");
        }

        public SepaPainFile DeserializeXmlSEPA001ISO20022(string inputFile)
        {
            try
            {
                var xml = File.ReadAllText(inputFile);
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                var jsonText = JsonConvert.SerializeXmlNode(doc);
                var sepaPainFile = JsonConvert.DeserializeObject<SepaPainFile>(jsonText);

                return sepaPainFile;
            }
            catch (Exception)
            {
                Log.Error($"An Error occured while deserializing the pain Xml file:{inputFile}");
                throw;
            }
        }

        public ConvertToXmlResponse PrepareFile(PrepareFileRequest prepareFileRequest)
        {
            return _fileService.PrepareFile(prepareFileRequest);
        }

        public EthnofilesFileTypesResponse PopulateFileTypes(PopulateFiletypesRequest populateFileTyesRequest, string idField)
        {
            var populateFileTypesResponse = _fileService.PopulateFileTypes(populateFileTyesRequest);

            var fileType = populateFileTypesResponse.FileTypeList.FirstOrDefault(p => p.IdField == idField);
            if (fileType == null)
                 Log.Information("FileType not found, with id: " + idField + "." + Environment.NewLine + "Please select from the following file type ids list:\n["
                              + string.Join("\n", populateFileTypesResponse.FileTypeList.Select(p =>
                                  "Id:" + p.IdField +
                                  ", Description:" + p.DescriptionField +
                                  ", FilenamePattern:" + p.FilenamePatternField +
                                  ", ValidationType:" + p.ValidationTypeField +
                                  ", ConvId:" + p.ConvId
                              ))
                              + "]");
            else
                Log.Information($"FileType with id:{idField} , is valid");

            return fileType;
        }
        public EthnofilesFileTypesResponse[] PopulateFileTypes(PopulateFiletypesRequest populateFileTyesRequest)
        {
            var populateFileTypesResponse = _fileService.PopulateFileTypes(populateFileTyesRequest);
            return populateFileTypesResponse.FileTypeList;
        }
        public FileDetails GetFile(Guid fileId, string requester, string subject)
        {
            return _fileService.GetFile($"{requester}", $"{subject}", fileId);
        }

        public void ValidateEthnofilesFilename(ValidateFilenameRequest request)
        {
            _fileService.ValidateEthnofilesFilename(request);
        }

        public void ValidateEthnofilesFile(ValidateFileRequest request)
        {
            _fileService.ValidateEthnofilesFile(request);
        }

        public CustomerApplication RetrieveCustomerApplication(RetrieveCustomerApplicationsRequest request, int customerApplicationId)
        {
            var customerapplications =  _fileService.RetrieveCustomerApplications(request);
            var customerApplication = customerapplications.CustomerApplications.FirstOrDefault(p => p.Id == customerApplicationId.ToString());
            if (customerApplication == null)
                Log.Information("Customer Application not found, with id: " + customerApplicationId + "." + Environment.NewLine + "Please select from the following ids list:\n["
                             + string.Join("\n", customerapplications.CustomerApplications.Select(p =>
                                 "Id:" + p.Id +
                                 ", Description:" + p.Description +
                                 ", FilenamePattern:" + p.FilenamePattern +
                                 ", ValidationType:" + p.ValidationType +
                                 ", ConvId:" + p.ConversionId
                             ))
                             + "]");
            else
                Log.Information($"Customer Application with id:{customerApplicationId} , is valid");

            return customerApplication;
        }

        public SendFileResponse SendFile(SendFileRequest request)
        {
            return _fileService.SendFile(request);
        }

        public SepaConvertResponse SepaConvert(SepaConvertRequest request)
        {
            return _fileService.SepaConvert(request);
        }

        public bool SepaSetFileStatusAsSent(SepaSetFileStatusAsSentRequest request)
        {
            return _fileService.SepaSetFileStatusAsSent(request);
        }
    }
}