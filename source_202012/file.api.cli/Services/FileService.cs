using ethnofiles.types;
using ethnofiles.validations.types;
using file.types;
using FileapiCli.ConfigOptions;
using FileapiCli.FileapiCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nbg.NetCore.Common.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using proxy.types;
using tpp.types;
using Serilog.Sinks.SystemConsole.Themes;

namespace FileapiCli
{ 
    public partial class FileService : IFileService
    {
        private readonly AppSettingsOptions _appSettingsOptions;
        private readonly ILogger<FileService> _logger;
        private readonly string _envName;

        IRestResponse restResponse { get; set; }

        public FileService(AppSettingsOptions appSettingsOptions, ILoggerFactory loggerFactory, string envname)
        {
            _logger = loggerFactory.CreateLogger<FileService>();
            _appSettingsOptions = appSettingsOptions;

            _envName = envname;
        }

        #region Files
        public UploadedFile InitiateUpload(UploadInitiationRequest request)
        {
            string path = $"{_appSettingsOptions.ProxyUrl}/files/upload-initiation";
            var jsonBody = JsonConvert.SerializeObject(request);

            var headers = GetCommonHeaders();
            headers.Add("content-length", jsonBody.Length.ToString());

            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);

            if (!restResponse.IsSuccessful && restResponse.StatusCode != System.Net.HttpStatusCode.Created)
                throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

            return JsonConvert.DeserializeObject<UploadedFile>(JsonConvert.DeserializeObject(restResponse.Content)?.ToString() ?? string.Empty);
        }

        public IRestResponse UploadChunk(UploadRequest request, Guid? fileId)
        {
            string path = $"{_appSettingsOptions.ProxyUrl}/files/{fileId}/upload";

            var jsonBody = JsonConvert.SerializeObject(request);

            var headers = GetCommonHeaders();
            headers.Add("content-length", jsonBody.Length.ToString());

            restResponse = HttpRequestClient.ExecuteRestPut(path, jsonBody, headers);

            return restResponse;
        }

        public FileDetails GetFile(string requester, string subject, Guid? fileId)
        {
            string path = $"{_appSettingsOptions.ProxyUrl}/files/" + fileId.ToString();

            var headers = GetCommonHeaders();
            headers.Add("subject", subject);
            headers.Add("requester", requester);

            restResponse = HttpRequestClient.ExecuteRestGet(path, headers);

            if (!restResponse.IsSuccessful)
                throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

            return JsonConvert.DeserializeObject<FileDetails>(JsonConvert.DeserializeObject(restResponse.Content)?.ToString() ?? string.Empty);
        }

        public FileContent DownloadFile(int chunkPart, string requester, string subject, Guid? fileId)
        {
            string path = $"{_appSettingsOptions.ProxyUrl}/files/" + fileId + "/" + chunkPart.ToString();
            var headers = GetCommonHeaders();
            headers.Add("subject", subject);
            headers.Add("requester", requester);

            restResponse = HttpRequestClient.ExecuteRestGet(path, headers);

            if (!restResponse.IsSuccessful)
                throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

            var bodyResponse = JsonConvert.DeserializeObject<FileContent>(JsonConvert.DeserializeObject(restResponse.Content)?.ToString() ?? string.Empty);

            return bodyResponse;
        }

        public IRestResponse UpdateFile(UpdateFileRequest request, Guid? fileId)
        {
            string path = $"{_appSettingsOptions.ProxyUrl}/files/" + fileId;
            var jsonBody = JsonConvert.SerializeObject(request);

            var headers = GetCommonHeaders();

            restResponse = HttpRequestClient.ExecuteRestPut(path, jsonBody, headers);

            if (!restResponse.IsSuccessful)
                throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

            return restResponse;
        }
        #endregion

        #region Ethnofiles
        public PopulateFiletypesResponse PopulateFileTypes(PopulateFiletypesRequest request)
        {
            Log.Debug($"PopulateFileTypes starting");

            string path = $"{_appSettingsOptions.EthnoProxyUrl}/ethnofiles/populatefiletypes";

            var headers = GetCommonHeaders();

            var serviceRequest = new ethnofiles.types.common.Request<ethnofiles.types.common.RequestHeader, PopulateFiletypesRequest>()
            {
                Header = new ethnofiles.types.common.RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"]
                },
                Payload = request
            };

            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<PopulateFiletypesResponse>(restResponse);
            return response.Payload;
        }

        public ConvertToXmlResponse PrepareFile(PrepareFileRequest request)
        {
            Log.Information($"PrepareFile starting");

            string path = $"{_appSettingsOptions.EthnoProxyUrl}/ethnofiles/preparefile";

            var headers = GetCommonHeaders();

            var serviceRequest = new ethnofiles.types.common.Request<ethnofiles.types.common.RequestHeader, PrepareFileRequest>()
            {
                Header = new ethnofiles.types.common.RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"]
                },
                Payload = request
            };

            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<ConvertToXmlResponse>(restResponse);
            return response.Payload;
        }

        public UpdateXactionIdResponse SendFileToEthnofiles(SendFileToEthnofilesRequest request)
        {
            Log.Information($"SendFileToEthnofiles starting");

            string path = $"{_appSettingsOptions.EthnoProxyUrl}/ethnofiles/sendfiletoethnofiles";
            var headers = GetCommonHeaders();

            var serviceRequest = new ethnofiles.types.common.Request<ethnofiles.types.common.RequestHeader, SendFileToEthnofilesRequest>()
            {
                Header = new ethnofiles.types.common.RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"]
                },
                Payload = request
            };

            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<UpdateXactionIdResponse>(restResponse);
            return response.Payload;
        }

        public void ValidateEthnofilesFilename(ValidateFilenameRequest request)
        {
            Log.Information($"ValidateEthnofilesFilename starting");

            string path = $"{_appSettingsOptions.EthnoProxyUrl}/validation/filename";

            var headers = GetCommonHeaders();

            var serviceRequest = new Request<ValidateFilenameRequest>()
            {
                Header = new RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"]
                },
                Payload = request
            };

            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<ethnofiles.types.common.GenericResponse>(restResponse);
        }

        public void ValidateEthnofilesFile(ValidateFileRequest request)
        {
            Log.Information($"ValidateEthnofilesFile starting");

            string path = $"{_appSettingsOptions.EthnoProxyUrl}/validation/file";

            var headers = GetCommonHeaders();

            var serviceRequest = new Request<ValidateFileRequest>()
            {
                Header = new RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"],
                    IsGo4MoreMember = false,
                    Latitude = 0,
                    Longitude = 0
                },
                Payload = request
            };

            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<ethnofiles.types.common.GenericResponse>(restResponse);
        }

        #endregion

        #region MassPayments
        public GenerateMassPaymentsSampleFileResponse GenerateMassPaymentSampleFile(GenerateMassPaymentsSampleFileBpRequest request)
        {
            string path = $"{_appSettingsOptions.TppProxyUrl}/tpp/generatemasspaymentssample";

            var headers = GetCommonHeaders();

            var serviceRequest = new Request<GenerateMassPaymentsSampleFileBpRequest>()
            {
                Header = new RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"]
                },
                Payload = request
            };

            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<GenerateMassPaymentsSampleFileResponse>(restResponse);
            return response.Payload;
        }

        public MassPaymentsWithFileResponse MassPaymentWithFile(MassPaymentsWithFileBpRequest request, string requestId = "")
        {
            string path = $"{_appSettingsOptions.TppProxyUrl}/tpp/massPayments";
            var headers = GetCommonHeaders(requestId);

            var serviceRequest = new Request<MassPaymentsWithFileBpRequest>()
            {
                Header = new RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"]
                },
                Payload = request
            };

            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<MassPaymentsWithFileResponse>(restResponse);
            return response.Payload;
        }

        public RetrieveMassPaymentsWithFileOutcomeResponse RetrieveMassPaymentsOutcome(RetrieveMassPaymentsOutcomeWithFileBpRequest request)
        {
            string path = $"{_appSettingsOptions.TppProxyUrl}/tpp/retrieveMassPaymentsOutcome";
            var headers = GetCommonHeaders();

            var serviceRequest = new Request<RetrieveMassPaymentsOutcomeWithFileBpRequest>()
            {
                Header = new RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"]
                },
                Payload = request
            };

            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<RetrieveMassPaymentsWithFileOutcomeResponse>(restResponse);
            return response.Payload;
        }

        public RequestIndividualPaymentsStatusWithFileResponse RequestPaymentStatus(RequestIndividualPaymentsStatusWithFileBpRequest request)
        {
            string path = $"{_appSettingsOptions.TppProxyUrl}/tpp/requestIndividualPaymentsStatus";
            var headers = GetCommonHeaders();

            var serviceRequest = new Request<RequestIndividualPaymentsStatusWithFileBpRequest>()
            {
                Header = new RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"]
                },
                Payload = request
            };
            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<RequestIndividualPaymentsStatusWithFileResponse>(restResponse);
            return response.Payload;
        }

        public RetrieveIndividualPaymentsStatusWithFileResponse RetrievePaymentStatus(RetrieveIndividualPaymentsStatusWithFileBpRequest request)
        {
            string path = $"{_appSettingsOptions.TppProxyUrl}/tpp/retrieveindividualpaymentsstatus";
            var headers = GetCommonHeaders();

            var serviceRequest = new Request<RetrieveIndividualPaymentsStatusWithFileBpRequest>()
            {
                Header = new RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"]
                },
                Payload = request
            };
            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<RetrieveIndividualPaymentsStatusWithFileResponse>(restResponse);
            return response.Payload;
        }

        public ValidateMassPaymentsFileResponse ValidateMassPaymentFile(ValidateMassPaymentsFileBpRequest request)
        {
            string path = $"{_appSettingsOptions.TppProxyUrl}/tpp/validatemassPaymentsFile";
            var headers = GetCommonHeaders();
            var serviceRequest = new Request<ValidateMassPaymentsFileBpRequest>()
            {
                Header = new RequestHeader()
                {
                    ID = headers["Request-id"],
                    Application = headers["client-id"]
                },
                Payload = request
            };

            var jsonBody = JsonConvert.SerializeObject(serviceRequest);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, headers);
            var response = InspectPayloadForErrors<ValidateMassPaymentsFileResponse>(restResponse);
            return response.Payload;
        }

        private static Response<T> InspectPayloadForErrors<T>(IRestResponse restResponse)
        {
            if (restResponse == null)
                throw new Exception("The restResponse from the api is null!");

            Response<T> response = null;
            // Customer Error Messages for known http codes.
            if (restResponse.StatusCode == HttpStatusCode.NotFound)
            {
                var json = "{" + ($"\"HttpStatus\":\"{restResponse.StatusCode}\", \"StatusDescription\":\"{restResponse.StatusDescription}\",  \"Uri\": \"{restResponse.ResponseUri}\"") + "}";
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                throw new Exception("Not Found " + prettyJson);
            }

            try
            {
                response = JsonConvert.DeserializeObject<Response<T>>(JsonConvert.DeserializeObject(restResponse.Content)?.ToString() ?? string.Empty);
                if (response == null)
                {
                    var json = "{" + ($"\"HttpStatus\":\"{restResponse.StatusCode}\", \"ErrorException\":\"{restResponse.ErrorException}\",\"ErrorMessage\":\"{restResponse.ErrorMessage}\",   \"Uri\": \"{restResponse.ResponseUri}\"") + "}";
                    var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                    throw new Exception("Http Exception " + prettyJson);
                }

            }
            catch (Exception)
            {
                var restResponseMessage = string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content;
                var errorMessage = $"Http rest call responded with HttpStatus code : {restResponse.StatusCode} . Response content is {restResponseMessage} .";
                Log.Error(errorMessage);
                throw new Exception(errorMessage);
            }

            if (response != null && response.Payload != null)
                return response;

            if (response != null && response.Exception != null && response.Exception.Code == "SMSOTP.500")
            {
                throw new SMSOTPException("One Time Password (OTP) Required");
            }

            if (response.Exception == null)
            {
                //both payload and exception are null
                var json = "{" + ($"\"HttpStatus\":\"{restResponse.StatusCode}\", \"ErrorException\":\"{restResponse.ErrorException}\",\"ErrorMessage\":\"{restResponse.ErrorMessage}\",   \"Uri\": \"{restResponse.ResponseUri}\"") + "}";
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                string errorMessage = "Response payload and Exception are empty. Http Response is : " + prettyJson;
                Log.Error(errorMessage);
                throw new Exception(errorMessage);
            }
            else
            {
                string errMessage = response?.Exception.ToString();
                if (response != null && response.Messages != null && response.Messages.Any())
                    foreach (var extraErrMessage in response.Messages)
                        errMessage = errMessage + $", {extraErrMessage?.ToString()} ";

                Log.Error($"Payload is null, Response.Exception is: {errMessage}");
                throw new Exception(errMessage);
            }
        }

        #endregion
        
        #region Private Methods
        public string GetAccessToken(string scope)
        {
            // for dev purposes take token from config, if defined and environment is development
            var token= _appSettingsOptions.Token;
            if (!string.IsNullOrEmpty(token) && _envName.ToLower()=="development")
            {
                TokenHelper.Token = token;
                TokenHelper.TokenCreation = DateTime.Now;
                return TokenHelper.Token;
            }

            if (!CheckAlreadyActiveToken())
            {
                string path = _appSettingsOptions.AuthorizationServer + "connect/token";
                var headers = new Dictionary<string, string> { { "content-type", "application/x-www-form-urlencoded" } };

                var parameter = new Parameter
                {
                    Name = "application/x-www-form-urlencoded",
                    Value = $"grant_type=password" +
                             $"&client_id={_appSettingsOptions.Client_id}" +
                             $"&client_secret={_appSettingsOptions.Client_Secret}" +
                             $"&scope={scope}&username={_appSettingsOptions.AppUserName}" +
                             $"&password={_appSettingsOptions.Password}" +
                             $"&acr_values={_appSettingsOptions.Acr_values}",
                    Type = ParameterType.RequestBody
                };

                restResponse = HttpRequestClient.ExecuteRestPost(path, null, headers, parameter);
                if (!restResponse.IsSuccessful)
                    throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

                TokenHelper.Token = restResponse.GetValue<string>("access_token");
                TokenHelper.TokenCreation = DateTime.Now;
            }

            return TokenHelper.Token;
        }

        private bool CheckAlreadyActiveToken()
        {
            return TokenHelper.TokenCreation != null && TokenHelper.Token.Clear() != null
                && DateTime.Now <= TokenHelper.TokenCreation.Value.AddSeconds(Convert.ToDouble(_appSettingsOptions.TokenExpirationTimeSeconds));
        }

        private Dictionary<string, string> GetCommonHeaders(string requestId)
        {
            return HeadersHelper.GetHeaders(client_id: _appSettingsOptions.Client_id,
                                            request_id: requestId == "" ? Guid.NewGuid().ToString() : requestId,
                                            token: GetAccessToken(_appSettingsOptions.Scope),
                                            sandboxId: _appSettingsOptions.Sandbox_id);
        }
        private Dictionary<string, string> GetCommonHeaders()
        {
            return HeadersHelper.GetHeaders(client_id: _appSettingsOptions.Client_id,
                                            request_id: Guid.NewGuid().ToString(),
                                            token: GetAccessToken(_appSettingsOptions.Scope),
                                            sandboxId: _appSettingsOptions.Sandbox_id);
        }
        #endregion
    }
}
