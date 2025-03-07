namespace reeltok.api.users.utils
{
    public static class ImageUtils
    {
        private static readonly string[] AllowedExtensions = new[]
        {
            ".jpg", ".jpeg", ".png", ".heic", ".heif"
        };

        private static readonly string[] AllowedMimeTypes = new[]
        {
            "image/jpeg", "image/png", "image/heic", "image/heif"
        };

        // Compares file signature to validate the file type, and also enhances security.
        private static readonly Dictionary<string, byte[]> FileSignatures = new Dictionary<string, byte[]>
        {
            { ".jpg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { ".jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { ".png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } },
            { ".heic", new byte[] { 0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70, 0x68, 0x65, 0x69, 0x63 } },
            { ".heif", new byte[] { 0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70, 0x68, 0x65, 0x69, 0x66 } }
        };

        public static async Task<bool> IsValidImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return false;
            }

            // Check the MIME type
            if (!AllowedMimeTypes.Contains(imageFile.ContentType))
            {
                return false;
            }

            // Check the file extension
            var extension = Path.GetExtension(imageFile.FileName);
            if (string.IsNullOrEmpty(extension) || !AllowedExtensions.Contains(extension))
            {
                return false;
            }

            // Check the file signature
            using (var stream = imageFile.OpenReadStream())
            {
                var signature = new byte[FileSignatures[extension].Length];
                await stream.ReadAsync(signature).ConfigureAwait(false);

                if (!FileSignatures[extension].SequenceEqual(signature))
                {
                    return false;
                }
            }

            return true;
        }

        public static string GenerateUniqueFileName(IFormFile imageFile)
        {
            string fileExtension = Path.GetExtension(imageFile.FileName).ToUpperInvariant();
            Guid randomFileName = Guid.NewGuid();

            return $"{randomFileName}{fileExtension}";
        }
    }
}