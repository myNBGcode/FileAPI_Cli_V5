namespace FileapiCli.ConfigOptions
{
    public class AppSettingsOptions
    {
        public string ProxyUrl { get; set; }
        public string AuthorizationServer { get; set; }
        public string Client_id { get; set; }
        public string Client_Secret { get; set; }
        public string Scope { get; set; }
        public string Acr_values { get; set; }
        public string TokenExpirationTimeSeconds { get; set; }
        public string AppUserName { get; set; }
       
        public string Password { get; set; }
        public bool Safe_password { get; set; }
        public string EthnoProxyUrl { get; set; }
        public string TppProxyUrl { get; set; }
        public string Sandbox_id { get; set; }

        public string Token { get; set; }

    }
}

