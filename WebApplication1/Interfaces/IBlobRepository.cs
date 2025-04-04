using BlobStorageAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlobStorageAPI.Interfaces
{
    public interface IBlobRepository
    {
        Task<string> UploadBlobFileAsync(Stream fileStream, string containerName, string fileName);
        Task<BlobObject?> DownloadBlobFileAsync(string containerName, string fileName);
        Task DeleteBlobFileAsync(string containerName, string fileName);
        Task<List<string>> GetBlobFilesAsync(string containerName);
        Task<IEnumerable<string>> GetAllContainersAsync();
        string GetBlobUrl(string containerName, string fileName);
    }
}
