using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Portfolio.Application.Interfaces;

namespace Portfolio.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;

        public FileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveOrReplaceAsync(IFormFile file, string folderName)
        {
            var folderPath = Path.Combine(_env.WebRootPath, folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var existingFiles = Directory.GetFiles(folderPath);
            foreach (var filePath in existingFiles)
            {
                File.Delete(filePath);
            }

            var originalFileName = Path.GetFileName(file.FileName);
            var physicalPath = Path.Combine(folderPath, originalFileName);

            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var dbFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            return Path.Combine("/", folderName, dbFileName).Replace("\\", "/");
        }


    }
}
