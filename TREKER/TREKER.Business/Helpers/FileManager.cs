using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.Business.Helpers
{
    public static class FileManager
    {
        private static string containerName = "trekker";

        public static bool CheckFile(this IFormFile file)
        {
            return file is not null ? file.ContentType.Contains("image") && file.Length < 10 * 1024 * 1024 : false;
        }

        public static async Task<string> UploadFileAsync(this IFormFile file, string connectionString, string folderPath)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var fileName = Guid.NewGuid().ToString() + file.FileName;
            var blobClient = containerClient.GetBlobClient(folderPath + fileName);

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.AbsoluteUri;

        }

        public static async Task DeleteFileAsync(string blobName, string connectionString, string folderPath)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient($"{folderPath}/{blobName}");

            await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
        }
    }
}
