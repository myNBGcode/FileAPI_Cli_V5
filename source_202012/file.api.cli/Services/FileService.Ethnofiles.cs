using Nbg.NetCore.Common.Types;
using Newtonsoft.Json;
using proxy.types;
using Serilog;

namespace FileapiCli
{
    public partial class FileService
    {

        public RetrieveCustomerApplicationsResponse RetrieveCustomerApplications(RetrieveCustomerApplicationsRequest request)
        {
            Log.Debug($"RetrieveCustomerApplications starting");

            string path = $"{_appSettingsOptions.ProxyUrl}/ethnofiles/retrievecustomerapplications";

            var headers = GetCommonHeaders();

            var serviceRequest = new Request<RetrieveCustomerApplicationsRequest>
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
            var response = InspectPayloadForErrors<RetrieveCustomerApplicationsResponse>(restResponse);
            return response.Payload;
        }
        public RetrieveFileResponse RetrieveFile(RetrieveFileRequest request)
        {
            Log.Debug($"RetrieveFile starting");

            string path = $"{_appSettingsOptions.ProxyUrl}/ethnofiles/retrievefile";

            var headers = GetCommonHeaders();

            var serviceRequest = new Request<RetrieveFileRequest>
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
            var response = InspectPayloadForErrors<RetrieveFileResponse>(restResponse);
            return response.Payload;
        }
        public RetrieveFileListResponse RetrieveFileList(RetrieveFileListRequest request)
        {
            Log.Debug($"RetrieveFileList starting");

            string path = $"{_appSettingsOptions.ProxyUrl}/ethnofiles/retrievefilelist";

            var headers = GetCommonHeaders();

            var serviceRequest = new Request<RetrieveFileListRequest>
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
            var response = InspectPayloadForErrors<RetrieveFileListResponse>(restResponse);
            return response.Payload;
        }
        public SendFileResponse SendFile(SendFileRequest request)
        {
            Log.Debug($"SendFile starting");

            string path = $"{_appSettingsOptions.ProxyUrl}/ethnofiles/sendfile";

            var headers = GetCommonHeaders();

            var serviceRequest = new Request<SendFileRequest>
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
            var response = InspectPayloadForErrors<SendFileResponse>(restResponse);
            return response.Payload;

        }
        public SepaConvertResponse SepaConvert(SepaConvertRequest request)
        {
            Log.Debug($"SepaConvert starting");

            string path = $"{_appSettingsOptions.ProxyUrl}/ethnofiles/sepaconvert";

            var headers = GetCommonHeaders();

            var serviceRequest = new Request<SepaConvertRequest>
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
            var response = InspectPayloadForErrors<SepaConvertResponse>(restResponse);
            return response.Payload;
        }

        public bool SepaSetFileStatusAsSent(SepaSetFileStatusAsSentRequest request)
        {
            Log.Debug($"SepaSetFileStatusAsSent starting");

            string path = $"{_appSettingsOptions.ProxyUrl}/ethnofiles/sepaSetFileStatusAsSent";

            var headers = GetCommonHeaders();

            var serviceRequest = new Request<SepaSetFileStatusAsSentRequest>
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
            var response = InspectPayloadForErrors<bool>(restResponse);
            return response.Payload;
        }

    }
}
