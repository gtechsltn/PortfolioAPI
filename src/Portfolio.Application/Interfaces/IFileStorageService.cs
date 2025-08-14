using Microsoft.AspNetCore.Http;
namespace Portfolio.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveOrReplaceAsync(IFormFile file, string folderName);
        Task<string> SaveFileAsync(IFormFile file, string folderName, string fileName);
        Task<string> GenerateFilePath(string folderName, string originalFileName);
        Task DeleteFileAsync(string folderName, string fileName);
    }
}
