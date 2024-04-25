
namespace Customer.WebAPI.Helpers
{
    public static class FileHelper
    {
        private static readonly string[] allowedContentType = { "application/pdf", "application/zip" };

        public static void ValidateFileType(string fileType)
        {
            if (!allowedContentType.Contains(fileType))
            {
                throw new ArgumentException("Only PDF and ZIP files are allowed.");
            }
        }

        public static void CheckExistFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }
        }
    }
}