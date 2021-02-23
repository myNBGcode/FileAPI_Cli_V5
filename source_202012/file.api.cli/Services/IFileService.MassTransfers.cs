using proxy.types;

namespace FileapiCli
{
    public partial interface IFileService
    {
        MassTransfersSampleResponse GenerateMassTransfersCreditSample(MassTransfersSampleRequest request);
        ResultPayCreditWithFileResponse RetrieveMassTransfersCreditOutcome(ResultPayCreditWithFileRequest request);
        FileCreditVerifyResponse MassiveTransfersVerifyFileCredit(VerifyFileCreditRequest request);
        PayFileResponse MassiveTransfersPayFileCredit(PayFileCreditRequest request);
    }
}
