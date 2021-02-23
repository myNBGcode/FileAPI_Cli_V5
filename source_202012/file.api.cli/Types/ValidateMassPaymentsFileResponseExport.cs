using System.Runtime.Serialization;
using tpp.types;

namespace FileapiCli
{
    [DataContract]
    public class ValidateMassPaymentsFileResponseExport : ValidateMassPaymentsFileResponse
    {
        [DataMember(Name = "FileId")]
        public string FileId { get; set; }

        public ValidateMassPaymentsFileResponseExport()
        {

        }

        public ValidateMassPaymentsFileResponseExport(ValidateMassPaymentsFileResponse validateMassPaymentsFileResponse)
        {
            this.AreAllValid = validateMassPaymentsFileResponse.AreAllValid;
            this.ValidationErrors = validateMassPaymentsFileResponse.ValidationErrors;
        }

        public ValidateMassPaymentsFileResponseExport(ValidateMassPaymentsFileResponse validateMassPaymentsFileResponse, string fileId)
        {
            this.AreAllValid = validateMassPaymentsFileResponse.AreAllValid;
            this.ValidationErrors = validateMassPaymentsFileResponse.ValidationErrors;
            this.FileId = fileId;
        }
    }
}
