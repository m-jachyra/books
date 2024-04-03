using Backend.Models;

namespace Backend.Storage
{
    public interface IStorageService
    {
        public Task UploadFileAsync(IFormFile model, string fileName);
        Task DeleteFileAsync(string fileName);
    }
}