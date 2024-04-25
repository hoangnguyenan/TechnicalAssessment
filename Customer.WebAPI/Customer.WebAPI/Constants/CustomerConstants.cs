
namespace Customer.WebAPI.Constants
{
    public static class CustomerConstants
    {
        public const string OutletNameRequiredErrorMessage = "Outlet Name is required.";

        public const string PhoneNumberRequiredErrorMessage = "Phone number is required.";

        public const string PhoneNumberInValidErrorMessage = "Phone number must contain only digits.";

        public const string PhoneNumberInValidLengthErrorMessage = "Phone number must be 10 characters.";

        public const string FileRequiredErrorMessage = "File is required.";
        
        public const string FileLengthErrorMessage = "File size exceeds the maximum limit (20MB).";
    }
}