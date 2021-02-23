using obp.exceptionTypes;

namespace proxy.types
{
    public static class MassiveTransfersExceptions
    {
        public static readonly ValidationException FileIdIsMissing = new ValidationException("20001", "File Id is required");

        public static readonly ResourceNotFoundException RequestedFileError = new ResourceNotFoundException("20002", "The requested file can not be processed");

        public static readonly ResourceNotFoundException TransactionsNotFound = new ResourceNotFoundException("20003", "Transactions not found");

        public static readonly ValidationException FileTypeIsMissing = new ValidationException("20004", "File type is required");
    }
}
