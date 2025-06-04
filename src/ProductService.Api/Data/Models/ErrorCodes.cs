namespace ProductService.Api.Data.Models
{
    public static class ErrorCodes
    {
        public const int DomainValidationError = 1001;
        public const int ProductNameEmpty = 1002;
        public const int PriceNegative = 1003;
        public const int ProductNotFound = 1004;
        public const int UnexpectedError = 9999;
    }
}