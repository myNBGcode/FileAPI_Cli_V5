using Nbg.NetCore.Common.Types;
using Newtonsoft.Json;
using proxy.types;

namespace FileapiCli
{
    public partial class FileService
    {

        public MassTransfersSampleResponse GenerateMassTransfersCreditSample(MassTransfersSampleRequest request)
        {
            var path = $"{_appSettingsOptions.ProxyUrl}/massiveTransfers/generateMassTransfersSample";
            var headers = GetCommonHeaders();
            var serviceRequest = new Request<MassTransfersSampleRequest>()
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
            var response = InspectPayloadForErrors<MassTransfersSampleResponse>(restResponse);

            return response.Payload;
        }

        public ResultPayCreditWithFileResponse RetrieveMassTransfersCreditOutcome(ResultPayCreditWithFileRequest request)
        {
            var path = $"{_appSettingsOptions.TppProxyUrl}/massiveTransfers/retrieveMassTransfersOutcomeCredit";
            var headers = GetCommonHeaders();
            var serviceRequest = new Request<ResultPayCreditWithFileRequest>()
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
            var response = InspectPayloadForErrors<ResultPayCreditWithFileResponse>(restResponse);

            return response.Payload;
        }

        public FileCreditVerifyResponse MassiveTransfersVerifyFileCredit(VerifyFileCreditRequest request)
        {
            var path = $"{_appSettingsOptions.TppProxyUrl}/massiveTransfers/verifyFile";
            var headers = GetCommonHeaders();
            var serviceRequest = new Request<VerifyFileCreditRequest>()
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
            var response = InspectPayloadForErrors<FileCreditVerifyResponse>(restResponse);

            return response.Payload;
        }

        public PayFileResponse MassiveTransfersPayFileCredit(PayFileCreditRequest request)
        {
            var path = $"{_appSettingsOptions.TppProxyUrl}/massiveTransfers/payFile";
            var headers = GetCommonHeaders();
            var serviceRequest = new Request<PayFileCreditRequest>()
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
            var response = InspectPayloadForErrors<PayFileResponse>(restResponse);

            return response.Payload;
        }

    }
}
