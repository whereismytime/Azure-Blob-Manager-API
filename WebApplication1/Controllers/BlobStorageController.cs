using BlobStorageAPI.Interfaces;
using BlobStorageAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobStorageController : ControllerBase
    {
        private readonly IBlobRepository _blobRepository;

        public BlobStorageController(IBlobRepository blobRepository)
        {
            _blobRepository = blobRepository;
        }

        [HttpPost("UploadBlobFile")]
        public async Task<IActionResult> UploadBlobFile(IFormFile file, [FromQuery] string containerName)
        {
            if (file == null || file.Length == 0 || string.IsNullOrWhiteSpace(containerName))
                return BadRequest("File and container name are required.");

            using var stream = file.OpenReadStream();
            var url = await _blobRepository.UploadBlobFileAsync(stream, containerName, file.FileName);
            return Ok(new { fileUrl = url });
        }


        [HttpGet("DownloadBlobFile")]
        public async Task<IActionResult> DownloadBlobFile([FromQuery] string containerName, [FromQuery] string fileName)
        {
            if (string.IsNullOrWhiteSpace(containerName) || string.IsNullOrWhiteSpace(fileName))
                return BadRequest("Both container name and file name are required.");

            var blob = await _blobRepository.DownloadBlobFileAsync(containerName, fileName);
            if (blob == null || blob.Content == null)
                return NotFound("File not found.");

            return File(blob.Content, blob.ContentType ?? "application/octet-stream", fileName);
        }

        [HttpDelete("DeleteBlobFile")]
        public async Task<IActionResult> DeleteBlobFile([FromQuery] string containerName, [FromQuery] string fileName)
        {
            if (string.IsNullOrWhiteSpace(containerName) || string.IsNullOrWhiteSpace(fileName))
                return BadRequest("Container name and file name are required.");

            await _blobRepository.DeleteBlobFileAsync(containerName, fileName);
            return Ok("File deleted successfully.");
        }

        [HttpGet("GetBlobFiles")]
        public async Task<IActionResult> GetBlobFiles([FromQuery] string containerName)
        {
            if (string.IsNullOrWhiteSpace(containerName))
                return BadRequest("Container name is required.");

            var files = await _blobRepository.GetBlobFilesAsync(containerName);
            return Ok(files);
        }

        [HttpGet("GetContainers")]
        public async Task<IActionResult> GetContainers()
        {
            var containers = await _blobRepository.GetAllContainersAsync();
            return Ok(containers);
        }

        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage([FromQuery] string containerName, [FromQuery] string fileName)
        {
            if (string.IsNullOrWhiteSpace(containerName) || string.IsNullOrWhiteSpace(fileName))
                return BadRequest("Both container and file name are required.");

            var blob = await _blobRepository.DownloadBlobFileAsync(containerName, fileName);

            if (blob == null || blob.Content == null)
                return NotFound("File not found.");

            // ✅ Возвращаем именно КАРТИНКУ, а не JSON
            return File(blob.Content, blob.ContentType ?? "image/jpeg");
        }


    }
}
