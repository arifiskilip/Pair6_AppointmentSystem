using Core.CrossCuttingConcers.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.FileHelper
{
    public static class FileHelper
    {
        private static readonly string BaseDirectory = Path.Combine(Environment.CurrentDirectory, "wwwroot");
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        public static async Task<string> AddAsync(IFormFile file, FileTypeEnum fileType)
        {
            ValidateFile(file, fileType);

            var folderName = GetFolderName(fileType);
            var fileExtension = Path.GetExtension(file.FileName);
            var uniqueFileName = GenerateUniqueFileName(fileExtension);
            var fullPath = Path.Combine(BaseDirectory, folderName, uniqueFileName);

            EnsureDirectoryExists(Path.Combine(BaseDirectory, folderName));
            await SaveFileAsync(file, fullPath);

            return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
        }

        public static async Task<string> UpdateAsync(IFormFile file, string existingFilePath, FileTypeEnum fileType)
        {
            ValidateFile(file, fileType);

            var folderName = GetFolderName(fileType);
            var fileExtension = Path.GetExtension(file.FileName);
            var uniqueFileName = GenerateUniqueFileName(fileExtension);
            var fullPath = Path.Combine(BaseDirectory, folderName, uniqueFileName);

            DeleteFile(existingFilePath);
            EnsureDirectoryExists(Path.Combine(BaseDirectory, folderName));
            await SaveFileAsync(file, fullPath);

            return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
        }

        public static void Delete(string filePath)
        {
            DeleteFile(filePath);
        }

        private static void ValidateFile(IFormFile file, FileTypeEnum fileType)
        {
            if (file == null || file.Length == 0)
            {
                throw new BusinessException("Dosya boş gönderilemez.");
            }

            if (file.Length > MaxFileSize)
            {
                throw new BusinessException("Dosya boyutu maksimum 5 MB sınırını aşıyor.");
            }

            var fileExtension = Path.GetExtension(file.FileName);
            if (!IsValidFileType(fileExtension, fileType))
            {
                throw new BusinessException($"Geçersiz dosya türü. İzin verilen türler: {GetAllowedExtensions(fileType)}.");
            }
        }

        private static string GetFolderName(FileTypeEnum fileType)
        {
            return fileType == FileTypeEnum.Image ? "Folders/Images" : "Folders/Documents";
        }

        private static bool IsValidFileType(string extension, FileTypeEnum fileType)
        {
            return fileType switch
            {
                FileTypeEnum.Image => extension == ".jpeg" || extension == ".png" || extension == ".jpg",
                FileTypeEnum.Text => extension == ".pdf" || extension == ".docx" || extension == ".txt",
                _ => false
            };
        }

        private static string GetAllowedExtensions(FileTypeEnum fileType)
        {
            return fileType switch
            {
                FileTypeEnum.Image => ".jpeg, .png, .jpg",
                FileTypeEnum.Text => ".pdf, .docx, .txt",
                _ => string.Empty
            };
        }

        private static string GenerateUniqueFileName(string extension)
        {
            return $"{Guid.NewGuid()}{extension}";
        }

        private static void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static async Task SaveFileAsync(IFormFile file, string fullPath)
        {
            await using var fileStream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        private static void DeleteFile(string filePath)
        {
            var fullPath = Path.Combine(BaseDirectory, filePath.Replace("/", "\\"));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
