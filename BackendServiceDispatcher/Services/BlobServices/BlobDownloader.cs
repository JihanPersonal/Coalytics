using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BackendServiceDispatcher.Services
{
    public class BlobDownloader : IBlobDownloader
    {
        private CloudStorageAccount Account { get; set; }
        private CloudBlobClient Client { get; set; }

        public BlobDownloader(CloudStorageAccount account)
        {
            Account = account;
            Client = Account.CreateCloudBlobClient();
        }

        public async Task<string> DownloadBlob(string userId, string fileName)
        {
            CloudBlobContainer container = Client.GetContainerReference(userId);
            if (await container.ExistsAsync())
            {
                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
                return await blob.DownloadTextAsync();
            }
            else
            {
                return "File not found.";
            }
        }
    }
}
