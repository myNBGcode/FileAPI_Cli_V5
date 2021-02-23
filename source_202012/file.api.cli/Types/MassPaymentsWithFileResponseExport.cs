using System.Runtime.Serialization;
using tpp.types;

namespace FileapiCli
{
    [DataContract]
    public class MassPaymentsWithFileResponseExport : MassPaymentsWithFileResponse
    {
        [DataMember(Name = "fileId")]
        public string FileId { get; set; }

        public MassPaymentsWithFileResponseExport()
        {

        }

        public MassPaymentsWithFileResponseExport(MassPaymentsWithFileResponse masspaymentFileResponse)
        {
            this.ReceivedSuccessfully = masspaymentFileResponse.ReceivedSuccessfully;
            this.ErrorMessages = masspaymentFileResponse.ErrorMessages;
        }

        public MassPaymentsWithFileResponseExport(MassPaymentsWithFileResponse masspaymentFileResponse, string fileId)
        {
            this.ReceivedSuccessfully = masspaymentFileResponse.ReceivedSuccessfully;
            this.ErrorMessages = masspaymentFileResponse.ErrorMessages;
            this.FileId = fileId;
        }
    }
}
