using ethnofiles.types;
using System.Runtime.Serialization;

namespace FileapiCli
{
    class SendFileToEthnofilesRequestCLI : SendFileToEthnofilesRequest
    {
        [DataMember(Name = "filename")]
        public string Filename { get; set; }
    }
}
