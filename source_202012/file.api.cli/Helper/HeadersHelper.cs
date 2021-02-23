using System.Collections.Generic;

namespace FileapiCli
{
    public static class HeadersHelper
    {
        public static Dictionary<string, string> GetHeaders(string client_id, string request_id, string token, string sandboxId) => new Dictionary<string, string>
        {
            { "client-id", client_id },
            { "Request-id", request_id },
            { "Authorization", "Bearer " + token },
            { "sandbox_Id", sandboxId }
        };
    }
}
