using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;

namespace FileapiCli
{
    public static class HttpRequestClient
    {
        public static IRestResponse ExecuteRestGet(string path, Dictionary<string, string> headers = null)
        {
            return ExecuteRestRequest(path, Method.GET, headers: headers);
        }

        public static IRestResponse ExecuteRestPost(string path, string jsonBody, Dictionary<string, string> headers = null, Parameter parameter = null)
        {
            return ExecuteRestRequest(path, Method.POST, jsonBody, headers, parameter);
        }

        public static T GetValue<T>(this IRestResponse restResponse, string responseField)
        {
            return JObject.Parse(JsonConvert.DeserializeObject(restResponse.Content).ToString()).Value<T>(responseField);
        }

        public static IRestResponse ExecuteRestPut(string path, string jsonBody, Dictionary<string, string> headers = null)
        {
            return ExecuteRestRequest(path, Method.PUT, jsonBody, headers);
        }

        private static IRestResponse ExecuteRestRequest(string path, Method method, string jsonBody = null, Dictionary<string, string> headers = null, Parameter parameter = null)
        {
            ExecuteRequestSettings(path);

            var client = new RestClient(path);


            var request = new RestRequest(method);

            if (headers != null) request = FillHeaders(request, headers);

            if (jsonBody.Clear() != null)
            {
                request.AddParameter("application/json", System.Text.Encoding.UTF8.GetBytes(jsonBody),
                    ParameterType.RequestBody);
            }

            if (parameter != null)
                request.AddParameter(parameter);

            //Log.Verbose($"RestApi request:{JsonConvert.SerializeObject(request)}");

            var response = client.Execute(request);

            //Log.Verbose($"RestApi response:{JsonConvert.SerializeObject(response)}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Log.Information("UnAuthorized. Token may have Expired.");
                throw new Exception("UnAuthorized");
            }
            return response;
        }

        private static void ExecuteRequestSettings(string path)
        {
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 0;
        }

        private static RestRequest FillHeaders(RestRequest request, Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            return request;
        }
    }
}
