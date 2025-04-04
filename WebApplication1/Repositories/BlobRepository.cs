using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorageAPI.Interfaces;
using BlobStorageAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlobStorageAPI.Repositories
{
    public class BlobRepository : IBlobRepository
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobRepository(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadBlobFileAsync(Stream fileStream, string containerName, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<BlobObject?> DownloadBlobFileAsync(string containerName, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                var downloadResult = await blobClient.DownloadContentAsync();
                var content = downloadResult.Value.Content.ToStream();
                var contentType = downloadResult.Value.Details.ContentType;

                return new BlobObject
                {
                    Content = content,
                    ContentType = contentType
                };
            }

            return null;
        }


        public async Task DeleteBlobFileAsync(string containerName, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetBlobFilesAsync(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var result = new List<string>();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                result.Add(blobItem.Name);
            }

            return result;
        }

        public async Task<IEnumerable<string>> GetAllContainersAsync()
        {
            var containers = new List<string>();
            await foreach (BlobContainerItem container in _blobServiceClient.GetBlobContainersAsync())
            {
                containers.Add(container.Name);
            }
            return containers;
        }

        public string GetBlobUrl(string containerName, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return containerClient.GetBlobClient(fileName).Uri.AbsoluteUri;
        }
    }
}
