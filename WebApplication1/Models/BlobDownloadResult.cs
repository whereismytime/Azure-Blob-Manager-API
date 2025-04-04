using Azure;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Blobs.Models;

namespace BlobStorageAPI.Models
{
    public class BlobDownloadResult
    {
        public BinaryData Content { get; set; }
        public string ContentType { get; set; }
    }
}
